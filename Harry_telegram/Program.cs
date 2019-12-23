using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Harry_telegram
{
    public class Program
    {
        public static TelegramBotClient bot;
        public static string userName = string.Empty;
        public static int chatId = 0;
        static void Main(string[] args)
        {
            bot = new TelegramBotClient("1024216247:AAElTDvwfbrfzeHu8Zy9fIW2PEnjdp1XTfw");
            bot.StartReceiving();

            bot.OnMessage += Start;
            Console.ReadLine();

            bot.StopReceiving();
        }

        private static async void Start(object sender, MessageEventArgs ev)
        {
            bot.OnMessage -= Start;

            Message message = ev.Message;
            userName = message.From.FirstName;
            chatId = message.From.Id;

            Adventure.StartAdventure(chatId);
        }
    }
}
