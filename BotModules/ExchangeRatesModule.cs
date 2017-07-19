using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;
using VkNet.Enums.SafetyEnums;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace VkChatBot.BotModules
{
    class ExchangeRatesModule 
    {
        public ExchangeRatesModule(VkApi api, VkNet.Model.Message msg)
        {
            api.Messages.MarkAsRead(msg.Id.Value);

            MessagesSendParams exchangeRates = new MessagesSendParams();

            string html = "https://www.yandex.ru/";
            HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };
            HD = web.Load(html);


            HtmlNode NoAltElements = HD.DocumentNode.SelectSingleNode("//div[@class='news']//div[@class='b-inline inline-stocks__item inline-stocks__item_id_1 hint__item']//span[@class='inline-stocks__value']//span[@class='inline-stocks__value_inner']");
            HtmlNode NoAltElements1 = HD.DocumentNode.SelectSingleNode("//div[@class='news']//div[@class='b-inline inline-stocks__item inline-stocks__item_id_23 hint__item']//span[@class='inline-stocks__value']//span[@class='inline-stocks__value_inner']");


            if (NoAltElements != null)
            {
                exchangeRates.Message = "Курс доллара ЦБ РФ: " + NoAltElements.InnerText + "\n" + "Курс евро ЦБ РФ: " + NoAltElements1.InnerText;
            }

            exchangeRates.ChatId = 1;
            api.Messages.Send(exchangeRates);
        }
    }
}
