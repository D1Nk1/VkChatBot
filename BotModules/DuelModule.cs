using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot.BotModules
{
    public class DuelModule
    {
        GameModule.Player player1 = new GameModule.Player();
        Random r = new Random();
        MessagesGetParams param1 = new MessagesGetParams();
        public VkApi api = new VkApi();

        public DuelModule(VkApi api, VkNet.Model.Message msg, MainForm parent)
        {
            
            player1.MaximumHitPoints = 100;
            player1.CurrentHitPoints = 100;

            player1.DMG = 10; //r.Next(10, 20);

            //parent.label4.Text = player1.DMG.ToString();
            //parent.label5.Text = player1.MaximumHitPoints.ToString();


            //parent.button3.Click += Button3_Click;

            ////player1.CurrentHitPoints = player1.CurrentHitPoints - player1.DMG;

            //parent.label7.Text = player1.CurrentHitPoints.ToString();

            //api.Messages.MarkAsRead(msg.Id.Value);

            MessagesSendParams param = new MessagesSendParams();
            param.ChatId = 1;
            param.Message = player1.CurrentHitPoints.ToString();
            api.Messages.Send(param);
            
            parent.timer1.Tick += Timer1_Tick;

            param1.Out = VkNet.Enums.MessageType.Received;
            param1.Count = 1;
            
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try {
                VkNet.Model.MessagesGetObject messages = api.Messages.Get(param1);

                foreach (VkNet.Model.Message msg in messages.Messages)
                {
                    if (msg.Body == "!test3" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        api.Messages.MarkAsRead(msg.Id.Value);
                        player1.CurrentHitPoints = player1.CurrentHitPoints - player1.DMG;

                        MessagesSendParams param = new MessagesSendParams();
                        param.ChatId = 1;
                        param.Message = player1.CurrentHitPoints.ToString();
                        api.Messages.Send(param);
                    }
                }
            }
            catch (Exception ex)
            {
                ApiAuthParams authorize = new ApiAuthParams();
                authorize.Login = "gari4ekkoryakin@gmail.com";
                authorize.Password = "Kompasogyrec123";
                authorize.ApplicationId = 5292881;
                authorize.Settings = VkNet.Enums.Filters.Settings.All;
                api.Authorize(authorize);
            }

}
            
            


        private void Button3_Click(object sender, EventArgs e)
        {
            VkApi api = new VkApi();
            ApiAuthParams authorize = new ApiAuthParams();

            player1.CurrentHitPoints = player1.CurrentHitPoints - player1.DMG;
            
            

            authorize.Login = "gari4ekkoryakin@gmail.com";
            authorize.Password = "Kompasogyrec123";
            authorize.ApplicationId = 5292881;
            authorize.Settings = VkNet.Enums.Filters.Settings.All;
            api.Authorize(authorize);

            MessagesSendParams param = new MessagesSendParams();
            param.ChatId = 1;
            param.Message = player1.CurrentHitPoints.ToString();
            api.Messages.Send(param);
        }

    }
}
