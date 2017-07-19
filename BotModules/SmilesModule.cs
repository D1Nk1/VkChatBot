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
    class SmilesModule
    {
        public SmilesModule(VkApi api, VkNet.Model.Message msg, int smile)
        {

            api.Messages.MarkAsRead(msg.Id.Value);

            MessagesSendParams kappa = new MessagesSendParams();

            VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();


            pht.AlbumId = 228305996;
            pht.Id = smile;
            pht.OwnerId = 349546400;

            kappa.ChatId = 1;
            kappa.Attachments = new[] { pht };
            kappa.Message = " ";
            api.Messages.Send(kappa);
        }
    }
}
