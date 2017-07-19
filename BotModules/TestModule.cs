using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot
{
    class TestModule
    {
        public TestModule(VkApi api, VkNet.Model.Message msg)
        {
            api.Messages.MarkAsRead(msg.Id.Value);

            MessagesSendParams param = new MessagesSendParams();
            param.ChatId = 1;
            param.Message = "test";
            api.Messages.Send(param);
        }
    }
}
