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
//using SKYPE4COMLib;
using Newtonsoft.Json;
using System.Diagnostics;


namespace VkChatBot
{

    public partial class MainForm : Form
    {
        public string online;
        public bool boobscd = true;
        public bool weathercd = true;
        public bool weatherfullcd = true;
        public bool advicecd = true;

        public VkApi api = new VkApi();
        ApiAuthParams authorize = new ApiAuthParams();

        MessagesGetParams param1 = new MessagesGetParams();
        MessagesSendParams param2 = new MessagesSendParams();
        MessagesSendParams param = new MessagesSendParams();


        Dictionary<string, int> smilesdic = new Dictionary<string, int>();
        Dictionary<long, string> dotabuffdic = new Dictionary<long, string>();
        Dictionary<long, string> rolldic = new Dictionary<long, string>();
        Dictionary<long, string> maildic = new Dictionary<long, string>();


        public MainForm()
        {
            InitializeComponent();


            string login = " "; //your e-mail
            string pass = " "; //your pass
            ulong appID = 5292881;

            //var authorize = new ApiAuthParams();
            VkNet.Enums.Filters.Settings scope = VkNet.Enums.Filters.Settings.All;


            authorize.Login = login;
            authorize.Password = pass;
            authorize.ApplicationId = appID;
            authorize.Settings = scope;
            api.Authorize(authorize);

            timer1.Enabled = true;
            timer1.Interval = 1000;

            timer2.Interval = 30 * 1000;

            //timer3.Enabled = true;
            timer3.Interval = 10 * 60 * 1000;


            //timer4.Enabled = true;
            timer4.Interval = 30 * 60 * 1000;

            timer5.Interval = 15 * 1000;

            timer6.Enabled = false; //stream check
            timer6.Interval = 60 * 1000;

            smilesdic["Kappa"] = 401422352;
            smilesdic["KappaPride"] = 401537998;

            dotabuffdic[20091500] = "http://www.dotabuff.com/players/36981197"; //Корякин Игорь
            dotabuffdic[18214144] = "http://www.dotabuff.com/players/23509620"; //Макаров Максим
            dotabuffdic[19372999] = "http://www.dotabuff.com/players/241084305"; //Яндушкин Роман
            dotabuffdic[79545461] = "http://www.dotabuff.com/players/58282160"; //Муравьёв Семён
            dotabuffdic[76822135] = "http://www.dotabuff.com/players/92413647"; //Бутенко Александр
            dotabuffdic[14321060] = "http://www.dotabuff.com/players/51555786"; //Зайцев Андрей
            dotabuffdic[23856720] = "http://www.dotabuff.com/players/133703014"; //Захаров Андрей

            rolldic[20091500] = "Игорь";
            rolldic[18214144] = "Максим";
            rolldic[19372999] = "Роман";
            rolldic[79545461] = "Семён";
            rolldic[76822135] = "Александр";
            rolldic[23856720] = "Захар";
            rolldic[50062594] = "Алексей";
            rolldic[62356323] = "Михаил";
            rolldic[14321060] = "Заяц";
            rolldic[108844285] = "Демид";

            maildic[20091500] = "Игорь";
            maildic[18214144] = "Максим";
            maildic[19372999] = "Роман";
            maildic[79545461] = "Семён";
            maildic[76822135] = "Александр";
            maildic[23856720] = "Захар";
            maildic[50062594] = "Алексей";
            maildic[62356323] = "Михаил";
            maildic[14321060] = "Заяц";
            maildic[108844285] = "Демид";



            param.ChatId = 1;
            param.Message = "test";



            param2.ChatId = 1;


            param1.Out = VkNet.Enums.MessageType.Received;
            param1.Count = 3;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            MessagesSendParams param = new MessagesSendParams();

            param.ChatId = 1;
            param.Message = "test";
            api.Messages.Send(param);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                VkNet.Model.MessagesGetObject messages = api.Messages.Get(param1);

                foreach (VkNet.Model.Message msg in messages.Messages)
                {


                    if (msg.Body == "!test" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        TestModule test = new TestModule(api, msg);
                    }

                    else if (msg.Body.Contains("!say") && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        api.Messages.MarkAsRead(msg.Id.Value);
                        param2.Message = Regex.Replace(msg.Body, "!say", "").Trim();
                        api.Messages.Send(param2);
                    }

                    else if (msg.Body == "!dotabuff" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        api.Messages.MarkAsRead(msg.Id.Value);

                        if (dotabuffdic.Keys.Contains(msg.UserId.Value))
                        {
                            BotModules.DotabuffModule db = new BotModules.DotabuffModule(api, msg, dotabuffdic[msg.UserId.Value]);
                        }
                    }

                    else if (msg.Body == "!roll" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        api.Messages.MarkAsRead(msg.Id.Value);

                        if (rolldic.Keys.Contains(msg.UserId.Value))
                        {
                            BotModules.RollModule roll = new BotModules.RollModule(api, msg, rolldic[msg.UserId.Value]);
                        }
                    }



                    else if (msg.Body == "!boobs" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        if (boobscd == true)
                        {
                            timer2.Enabled = true;
                            BoobsModule boobs = new BoobsModule(api, msg, this);
                            boobscd = false;
                        }
                        else
                        {
                            MessagesSendParams boobscooldownparams = new MessagesSendParams();
                            boobscooldownparams.ChatId = 1;
                            boobscooldownparams.Message = "!boobs are on a cooldown.";
                            api.Messages.Send(boobscooldownparams);
                        }


                    }
                    else if (msg.Body == "!weather" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        if (weathercd == true)
                        {
                            timer3.Enabled = true;
                            BotModules.WeatherModule weather = new BotModules.WeatherModule(api, msg);
                            weathercd = false;
                        }
                        else
                        {
                            MessagesSendParams weathercooldownparams = new MessagesSendParams();
                            weathercooldownparams.ChatId = 1;
                            weathercooldownparams.Message = "!weather is on a cooldown.";
                            api.Messages.Send(weathercooldownparams);
                        }
                    }

                    else if (msg.Body == "!schedule" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        BotModules.ScheduleModule schedule = new BotModules.ScheduleModule(api, msg);
                    }

                    else if (smilesdic.Keys.Contains(msg.Body) && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        SmilesModule smiles = new SmilesModule(api, msg, smilesdic[msg.Body]);
                    }

                    else if (msg.Body.Contains("!mail") && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        api.Messages.MarkAsRead(msg.Id.Value);

                        if (maildic.Keys.Contains(msg.UserId.Value))
                        {
                            BotModules.MailModule mm = new BotModules.MailModule(api, msg, maildic[msg.UserId.Value]);
                        }
                    }

                    else if (msg.Body.Contains("!skype") && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        api.Messages.MarkAsRead(msg.Id.Value);

                        BotModules.SkypeModule skype = new BotModules.SkypeModule(api, msg, this.Handle);
                    }

                    else if (msg.Body == "!help" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        BotModules.HelpModule help = new BotModules.HelpModule(api, msg);
                    }

                    else if (msg.Body == "!exchangerates" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        BotModules.ExchangeRatesModule help = new BotModules.ExchangeRatesModule(api, msg);
                    }


                    else if (msg.Body == "!audio" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        BotModules.AudioModule audio = new BotModules.AudioModule(api, msg);
                    }

                    else if (msg.Body.Contains("!tts") && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        api.Messages.MarkAsRead(msg.Id.Value);

                        string ttstext = Regex.Replace(msg.Body, "!tts", "").Trim();
                        //string url = "https://tts.global.ivonacloud.com/CreateSpeech?Voice.Name=Maxim&Input.Type=text%2Fplain&OutputFormat.Codec=MP3&Voice.Language=ru-RU&Input.Data=" + ttstext + "&OutputFormat.SampleRate=22050&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Date=20160704T185236Z&X-Amz-SignedHeaders=host&X-Amz-Expires=300&X-Amz-Credential=GDNAI2EMTPXZH7ONBHZQ%2F20160704%2Fglobal%2Ftts%2Faws4_request&X-Amz-Signature=861c304ee816bbd4dae6f47c893472a9a392ff159aa167129de8d6f113ea70a7";
                        string url = "http://api.voicerss.org/?key=14358f400f2f405eb6404621990d73e0&src=" + ttstext + "&hl=ru-ru&f=8khz_16bit_stereo";
                        WebClient wc = new WebClient();
                        wc.DownloadFile(url, "ttsfile.mp3");
                        
                        var uploadServer = api.Audio.GetUploadServer();
                        
                        var responseAudio = Encoding.ASCII.GetString(wc.UploadFile(uploadServer, @"ttsfile.mp3"));
                        api.Audio.Save(responseAudio);

                        var audios = api.Audio.Get(uid: 349546400);
                        VkNet.Model.Attachments.Audio audioatt = new VkNet.Model.Attachments.Audio();
                        audioatt.Url = audios[0].Url;
                        audioatt.Id = audios[0].Id;
                        audioatt.OwnerId = 349546400;

                        MessagesSendParams param = new MessagesSendParams();
                        param.Attachments = new[] { audioatt };
                        param.Message = "Text-to-speech audio:";
                        param.ChatId = 1;
                        api.Messages.Send(param);

                    }
                    else if (msg.Body == "!advice" && msg.ReadState == VkNet.Enums.MessageReadState.Unreaded)
                    {
                        if (advicecd == true)
                        {
                            timer5.Enabled = true;
                            BotModules.AdviceModule help = new BotModules.AdviceModule(api, msg);
                            advicecd = false;
                        }
                        else
                        {
                            MessagesSendParams advicecd = new MessagesSendParams();
                            advicecd.ChatId = 1;
                            advicecd.Message = "!advice is on a cooldown.";
                            api.Messages.Send(advicecd);
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + Environment.NewLine + DateTime.Now.ToString() + ": \n" + ex.Message.ToString() + "\n";
                authorize.Login = " "; //enter your e-mail
                authorize.Password = " "; //enter your pass
                authorize.ApplicationId = 5292881;
                authorize.Settings = VkNet.Enums.Filters.Settings.All;
                api.Authorize(authorize);
            }
        }


        //public void schedulecheck()
        //{
        //    string html = "https://www.mirea.ru/education/schedule-main/schedule/";
        //    HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
        //    var web = new HtmlWeb
        //    {
        //        AutoDetectEncoding = false,
        //        OverrideEncoding = Encoding.UTF8,
        //    };
        //    HD = web.Load(html);
        //    HtmlNode NoAltElements1 = HD.DocumentNode.SelectSingleNode(".//*[@id='tab-content']/li[1]/div/div[8]/div/div[4]/table/tbody/tr[1]/td[5]/a[@href]");

        //    if (NoAltElements1 != null)
        //    {
        //        richTextBox2.Text = NoAltElements1.InnerText;
        //    }

        //    //.//[@id='tab-content']/li[1]/div/div[6]/div/div[4]/table/tbody/tr[1]/td[5]
        //}

        public void streamcheck()
        {
            string url = "https://api.twitch.tv/kraken/streams/evilarthas";

            var json = new WebClient().DownloadString(url);

            dynamic stuff = JsonConvert.DeserializeObject(json);

            try
            {
                if (stuff.stream.game.Value != null)
                {
                    online = "Стрим онлайн! " + "\n" + "http://www.twitch.tv/evilarthas";

                    MessagesSendParams stream = new MessagesSendParams();
                    stream.ChatId = 1;
                    stream.Message = online;
                    api.Messages.Send(stream);

                    timer6.Interval = 30 * 60 * 1000;
                }
            }
            catch (Exception ex)
            {
                timer6.Interval = 60 * 1000;
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //schedulecheck();
            var get = api.Users.Get(20091500);
            MessageBox.Show(get.Online.ToString());
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            boobscd = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            weathercd = true;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            weatherfullcd = true;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            advicecd = true;
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            streamcheck();
        }
    }

}

