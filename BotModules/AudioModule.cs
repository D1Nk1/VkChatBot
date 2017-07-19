using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot.BotModules
{
    class AudioModule
    {
        Dictionary<long, string> audiodic = new Dictionary<long, string>();

        public AudioModule (VkApi api, VkNet.Model.Message msg)
        {
            audiodic[20091500] = "Игорь";
            audiodic[18214144] = "Максим";
            audiodic[19372999] = "Роман";
            audiodic[79545461] = "Семён";
            audiodic[76822135] = "Александр";
            audiodic[50062594] = "Алексей";
            audiodic[62356323] = "Михаил";
            audiodic[14321060] = "Заяц";
            audiodic[23856720] = "Захар";


            int[] audioids1 = new int[] { 20091500, 79545461, 18214144, 19372999, 50062594, 14321060, 62356323, 76822135, 23856720 };

            Random random = new Random();

            int index = random.Next(0, 100);

            long ids = random.Next(0, audioids1.Length);

            var audios = api.Audio.Get(audioids1[ids]);

            VkNet.Model.Attachments.Audio audioatt = new VkNet.Model.Attachments.Audio();
            audioatt.Url = audios[index].Url;
            audioatt.Id = audios[index].Id;
            audioatt.OwnerId = audioids1[ids];

            if (audiodic.Keys.Contains(audioatt.OwnerId.Value))
            {
                MessagesSendParams param = new MessagesSendParams();
                param.Attachments = new[] { audioatt };
                param.Message = "Владелец аудиозаписи: " + audiodic[audioatt.OwnerId.Value];
                param.ChatId = 1;
                api.Messages.Send(param);
            }

            /*MessagesSendParams param = new MessagesSendParams();
            param.Attachments = new[] { audioatt };
            param.Message = " ";
            param.ChatId = 1;*/

            //api.Messages.Send(param);


        }
    }
}
