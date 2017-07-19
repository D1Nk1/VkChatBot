using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.RequestParams;

namespace VkChatBot.BotModules
{
    class HelpModule
    {
        public HelpModule(VkApi api, VkNet.Model.Message msg)
        {
            api.Messages.MarkAsRead(msg.Id.Value);

            MessagesSendParams help = new MessagesSendParams();
            help.ChatId = 1;
            help.Message = "!test - тестовая команда, которая в ответ выведет сообщение 'test' от имени бота\n !say - выводит в чат сообщение с текстом указанным после команды\n !dotabuff - выводит ссылку на Ваш последний матч и статус этого матча\n !boobs - сиськи, хех\n !weather - выводит текущую погоду\n !schedule - выводит расписание на сегодня и завтра\n !skype - собирает конференцию в Skype из указанных участников беседы\n !advice - выводит случайный, блять, совет\n !roll - выводит случайное число от 1 до 100\n !mail *Имя получателя* *Сообщение* - отправить сообщение указанному пользователю\n !mail check - проверить последнее сообщение, отправленное Вам\n !audio - выводит случайную из последних ста добавленных песен одного из участников конференции";
            api.Messages.Send(help);
        }
    }
}
