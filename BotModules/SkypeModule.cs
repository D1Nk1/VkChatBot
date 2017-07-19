using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;
//using SKYPE4COMLib;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VkChatBot.BotModules
{
    class SkypeModule
    {
    //    private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
    //    private const int WM_APPCOMMAND = 0x319;

    //    [DllImport("user32.dll")]
    //    public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
    //        IntPtr wParam, IntPtr lParam);

        public SkypeModule(VkApi api, VkNet.Model.Message msg, IntPtr handle)
        {
            
            //SendMessageW(handle, WM_APPCOMMAND, handle,
            //    (IntPtr)APPCOMMAND_VOLUME_MUTE);

            Dictionary<string, string> skypedic = new Dictionary<string, string>();
            
            skypedic["Максим"] = "incognita2012";
            skypedic["Роман"] = "haidjeker";
            skypedic["Семён"] = "sem1e1n";
            skypedic["Александр"] = "but3nko";
            skypedic["Захар"] = "zeratyl9";
            skypedic["Михаил"] = "mixan007892";
            skypedic["Заяц"] = "s_d_s_";
            skypedic["Демид"] = "xdelierx";

            string test = String.Join("", msg.Body.Split(' ').Skip(1));
            string[] arguments = test.Split(',');

            string call = "";

            foreach (var item in arguments)
            {
                if (skypedic.ContainsKey(item))
                {
                    call += skypedic[item] + ";";
                }
            }

            MessagesSendParams skypeparams = new MessagesSendParams();
            skypeparams.ChatId = 1;
            skypeparams.Message = "Конференция успешно создана!";
             

            Process skypecall = new Process();

            skypecall.StartInfo.FileName = @"skype:" + call;
            skypecall.Start();

            api.Messages.Send(skypeparams);


        }
    }
}
