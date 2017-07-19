using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using VkNet.Model.RequestParams;
using VkNet.Enums.SafetyEnums;

namespace VkChatBot.BotModules
{
    class WeatherModule
    {
        public WeatherModule(VkApi api, VkNet.Model.Message msg)
        {
            api.Messages.MarkAsRead(msg.Id.Value);

            string html = "http://openweathermap.org/city/524901";
            HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };

            HD = web.Load(html);
            List<string> weather_img = new List<string>();

            foreach (HtmlNode NoAltElements3 in HD.DocumentNode.SelectNodes("//div[@class='weather-widget']/h2/img"))
            {
                weather_img.Add(NoAltElements3.GetAttributeValue("src", null));
            }
            string wimg = weather_img[0];

            HtmlNode NoAltElements = HD.DocumentNode.SelectSingleNode("//div[@class='weather-widget']/h3");
            HtmlNode NoAltElements1 = HD.DocumentNode.SelectSingleNode("//div[@class='weather-widget']/h2");

            string saveLocation = @"weatherImage.jpg";

            byte[] imageBytes;
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(wimg);
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

            MessagesSendParams weatherparams = new MessagesSendParams();

            var uploadServer = api.Photo.GetUploadServer(albumId: 228278348);

            var wc = new WebClient();
            var responseImg = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, fileName: @"weatherImage.jpg"));


            PhotoSaveParams par1 = new PhotoSaveParams();
            par1.SaveFileResponse = responseImg;
            par1.AlbumId = 228278348;
            var photo = api.Photo.Save(par1);


            PhotoGetParams pgp = new PhotoGetParams();
            pgp.AlbumId = PhotoAlbumType.Id(228278348);
            pgp.Count = 1;
            pgp.PhotoIds = new[] { photo[0].Id.ToString() };
            VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();

            pht.AlbumId = 228175732;
            pht.Id = photo[0].Id;
            pht.OwnerId = photo[0].OwnerId;

            weatherparams.Attachments = new[] { pht };
            weatherparams.Message = NoAltElements.InnerText + Environment.NewLine + NoAltElements1.InnerText;
            weatherparams.ChatId = 1;
            api.Messages.Send(weatherparams);
        }
    }
}
