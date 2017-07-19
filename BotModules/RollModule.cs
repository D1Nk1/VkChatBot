using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot.BotModules
{
    class RollModule
    {
        public RollModule(VkApi api, VkNet.Model.Message msg, string rolllink)
        {
            Random roll = new Random();

            MessagesSendParams rollparams = new MessagesSendParams();
            rollparams.ChatId = 1;
            rollparams.Message = rolllink + " rolls " + roll.Next(1, 100).ToString() + " (1-100)";
            api.Messages.Send(rollparams);
        }
    }
}
