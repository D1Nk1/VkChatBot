using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace VkChatBot.BotModules
{
    class AdviceModule
    {

        private string GET(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url + "?" + Data);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream, Encoding.UTF8);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }

        public AdviceModule(VkApi api, VkNet.Model.Message msg)
        {
            api.Messages.MarkAsRead(msg.Id.Value);


            string html = "http://fucking-great-advice.ru/api/random";

            string text = GET(html, "");
            text = Regex.Replace(text, "&nbsp;", " ");
            text = Regex.Replace(text, "&#151;", "-");

            Newtonsoft.Json.Linq.JObject json = JsonConvert.DeserializeObject<dynamic>(text);
            var txt = json.SelectToken("text").ToString();

            UTF8Encoding utf = new UTF8Encoding();
            byte[] q = utf.GetBytes(txt.ToCharArray());
            string advice = utf.GetString(q);

            MessagesSendParams adviceparam = new MessagesSendParams();
            adviceparam.ChatId = 1;
            adviceparam.Message = advice;
            api.Messages.Send(adviceparam);

        }
    }
}
