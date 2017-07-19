using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot.BotModules
{
    class MailModule
    {
        public MailModule (VkApi api, VkNet.Model.Message msg, string maillink)
        {
            MessagesSendParams mailparams = new MessagesSendParams();
            //mailparams.Message = Regex.Replace(msg.Body, "!mail " + maillink, "");
            //mailparams.Message = string.Join(" ", msg.Body.Split(' ').Skip(2));
            

            if (msg.Body.Contains("!mail check"))
            {
                string[] lines = File.ReadAllLines(@"answer.txt");
                for(int i=lines.Length-1; i>=0; i--)
                {
                    var temp = lines[i].Split('|');
                    if (temp[1] == maillink)
                    {
                        mailparams.Message = "Отправитель: " + temp[0] + "\nСообщение: " + temp[2];
                        mailparams.ChatId = 1;
                        api.Messages.Send(mailparams);
                        break;
                    }
                }
            }
            else
            {
                string[] opts = msg.Body.Split(' ');
                string name = opts.Skip(1).First();
                string answer = string.Join(" ", opts.Skip(2));
                File.AppendAllLines(@"answer.txt", new string[] {maillink + "|" + name + "|" + answer  });
            }
        }
    }
}
