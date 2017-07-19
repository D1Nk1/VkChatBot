using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot.BotModules
{
    class DotabuffModule
    {
        public DotabuffModule(VkApi api, VkNet.Model.Message msg, string dblink)
        {

            MessagesSendParams dotabuff = new MessagesSendParams();

            string html = dblink;
            HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };
            HD = web.Load(html + "/matches");

            HtmlNode NoAltElements1 = HD.DocumentNode.SelectSingleNode("//td[@class='cell-large']//a[@href]");
            HtmlNode NoAltElements = HD.DocumentNode.SelectSingleNode("//div[@class='content-inner']//table//tr//td[3]//a");

            if (NoAltElements != null)
            {
                //richTextBox3.Text = "dotabuff.com" + NoAltElements1.GetAttributeValue("href", null) + Environment.NewLine + NoAltElements.InnerText;
                dotabuff.Message = "dotabuff.com" + NoAltElements1.GetAttributeValue("href", null) + Environment.NewLine + NoAltElements.InnerText;

            }
            dotabuff.ChatId = 1;
            api.Messages.Send(dotabuff);
        }
    }
}



