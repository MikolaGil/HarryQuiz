using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Harry_telegram
{
    public static class Dialog
    {
        public static async Task SendMessage(int chatId, string message)
        {
            await Program.bot.SendTextMessageAsync(chatId, message);
        }
    }
}
