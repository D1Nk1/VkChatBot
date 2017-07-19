using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot.BotModules
{
    class ScheduleModule
    {
        public ScheduleModule(VkApi api, VkNet.Model.Message msg)
        {
            api.Messages.MarkAsRead(msg.Id.Value);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                MessagesSendParams today = new MessagesSendParams();
                MessagesSendParams tomorrow = new MessagesSendParams();


                VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();
                VkNet.Model.Attachments.Photo pht1 = new VkNet.Model.Attachments.Photo();


                pht.AlbumId = 228525570;
                pht.Id = 430091077;
                pht.OwnerId = 349546400;

                pht1.AlbumId = 228525570;
                pht1.Id = 430091079;
                pht1.OwnerId = 349546400;

                today.ChatId = 1;
                today.Attachments = new[] { pht };
                today.Message = "Расписание на сегодня:";
                api.Messages.Send(today);

                tomorrow.ChatId = 1;
                tomorrow.Attachments = new[] { pht1 };
                tomorrow.Message = "Расписание на завтра:";
                api.Messages.Send(tomorrow);
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                MessagesSendParams today = new MessagesSendParams();
                MessagesSendParams tomorrow = new MessagesSendParams();


                VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();
                VkNet.Model.Attachments.Photo pht1 = new VkNet.Model.Attachments.Photo();


                pht.AlbumId = 228525570;
                pht.Id = 430091079;
                pht.OwnerId = 349546400;

                pht1.AlbumId = 228525570;
                pht1.Id = 430091080;
                pht1.OwnerId = 349546400;

                today.ChatId = 1;
                today.Attachments = new[] { pht };
                today.Message = "Расписание на сегодня:";
                api.Messages.Send(today);

                tomorrow.ChatId = 1;
                tomorrow.Attachments = new[] { pht1 };
                tomorrow.Message = "Расписание на завтра:";
                api.Messages.Send(tomorrow);
            }

            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                MessagesSendParams today = new MessagesSendParams();
                MessagesSendParams tomorrow = new MessagesSendParams();


                VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();
                VkNet.Model.Attachments.Photo pht1 = new VkNet.Model.Attachments.Photo();


                pht.AlbumId = 228525570;
                pht.Id = 430091080;
                pht.OwnerId = 349546400;

                pht1.AlbumId = 228525570;
                pht1.Id = 430091081;
                pht1.OwnerId = 349546400;

                today.ChatId = 1;
                today.Attachments = new[] { pht };
                today.Message = "Расписание на сегодня:";
                api.Messages.Send(today);

                tomorrow.ChatId = 1;
                tomorrow.Attachments = new[] { pht1 };
                tomorrow.Message = "Расписание на завтра:";
                api.Messages.Send(tomorrow);
            }

            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                MessagesSendParams today = new MessagesSendParams();
                MessagesSendParams tomorrow = new MessagesSendParams();


                VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();
                VkNet.Model.Attachments.Photo pht1 = new VkNet.Model.Attachments.Photo();


                pht.AlbumId = 228525570;
                pht.Id = 430091081;
                pht.OwnerId = 349546400;

                pht1.AlbumId = 228525570;
                pht1.Id = 430091083;
                pht1.OwnerId = 349546400;

                today.ChatId = 1;
                today.Attachments = new[] { pht };
                today.Message = "Расписание на сегодня:";
                api.Messages.Send(today);

                tomorrow.ChatId = 1;
                tomorrow.Attachments = new[] { pht1 };
                tomorrow.Message = "Расписание на завтра:";
                api.Messages.Send(tomorrow);
            }

            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                MessagesSendParams today = new MessagesSendParams();
                MessagesSendParams tomorrow = new MessagesSendParams();


                VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();
                VkNet.Model.Attachments.Photo pht1 = new VkNet.Model.Attachments.Photo();


                pht.AlbumId = 228525570;
                pht.Id = 430091083;
                pht.OwnerId = 349546400;

                pht1.AlbumId = 228525570;
                pht1.Id = 430091084;
                pht1.OwnerId = 349546400;

                today.ChatId = 1;
                today.Attachments = new[] { pht };
                today.Message = "Расписание на сегодня:";
                api.Messages.Send(today);

                tomorrow.ChatId = 1;
                tomorrow.Attachments = new[] { pht1 };
                tomorrow.Message = "Расписание на субботу:";
                api.Messages.Send(tomorrow);
            }

            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {

                MessagesSendParams today = new MessagesSendParams();
                MessagesSendParams tomorrow = new MessagesSendParams();

                VkNet.Model.Attachments.Photo pht1 = new VkNet.Model.Attachments.Photo();
                VkNet.Model.Attachments.Photo pht = new VkNet.Model.Attachments.Photo();

                pht.AlbumId = 228525570;
                pht.Id = 430091084;
                pht.OwnerId = 349546400;

                pht1.AlbumId = 228525570;
                pht1.Id = 430091077;
                pht1.OwnerId = 349546400;

                today.ChatId = 1;
                today.Attachments = new[] { pht };
                today.Message = "Расписание на субботу:";
                api.Messages.Send(today);

                tomorrow.ChatId = 1;
                tomorrow.Attachments = new[] { pht1 };
                tomorrow.Message = "Расписание на понедельник:";
                api.Messages.Send(tomorrow);
            }
        }
    }
}
