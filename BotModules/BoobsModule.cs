using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using VkNet.Model.RequestParams;
using System.Threading;
using VkNet.Enums.SafetyEnums;
using System.Collections;

namespace VkChatBot
{
    class BoobsModule
    {
        public BoobsModule(VkApi api, VkNet.Model.Message msg, Form parent)
        {
            api.Messages.MarkAsRead(msg.Id.Value);

            MessagesSendParams param3 = new MessagesSendParams();
            param3.ChatId = 1;

            Random r = new Random();
            string html = "http://all-hotgirls.tumblr.com/page/" + r.Next(2, 600);
            HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };

            HD = web.Load(html);

            List<string> image_links = new List<string>();
            foreach (HtmlNode NoAltElements in HD.DocumentNode.SelectNodes("//div[@class='photo-wrapper-inner']//a[@href]/img"))
            {
                image_links.Add(NoAltElements.GetAttributeValue("src", null));
            }
            int rPos = r.Next(0, image_links.Count);
            string url = image_links[rPos];
            image_links.RemoveAt(rPos);
            string imageUrl = url;
            string saveLocation = @"someImage.jpg";

            byte[] imageBytes;
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
            WebResponse imageResponse = imageRequest.GetResponse();

            Stream responseStream = imageResponse.GetResponseStream();

            using (BinaryReader br = new BinaryReader(responseStream))
            {
                imageBytes = br.ReadBytes(500000);
                br.Close();
            }
            responseStream.Close();
            imageResponse.Close();

            FileStream fs = new FileStream(saveLocation, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(imageBytes);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }

            //var uploadServer = api.Photo.GetMessagesUploadServer();          NEVER

            var uploadServer = api.Photo.GetUploadServer(albumId: 228175732);

            var wc = new WebClient();
            var responseImg = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, fileName: @"someImage.jpg"));

            //var photo = api.Photo.SaveMessagesPhoto(responseImg);            USE 


            PhotoSaveParams par1 = new PhotoSaveParams();
            par1.SaveFileResponse = responseImg;
            par1.AlbumId = 228175732;

            var photo = api.Photo.Save(par1);

            //var saved = api.Photo.Save(new PhotoSaveParams         THIS 
            //{
            //    SaveFileResponse = responseImg,                    TRASH
            //    AlbumId = 228175732
            //});                                                METHOD!!!

            PhotoGetParams pgp = new PhotoGetParams();
            pgp.AlbumId = PhotoAlbumType.Id(228175732);
            pgp.Count = 1;
            pgp.PhotoIds = new[] { photo[0].Id.ToString() };

            VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();

            pht.AlbumId = 228175732;
            pht.Id = photo[0].Id;
            pht.OwnerId = photo[0].OwnerId;
            
            param3.Attachments = photo;
            param3.Attachments = new[] { pht };
            param3.Message = "Here's your boobs:";
            api.Messages.Send(param3);

            
        }
    }
}
