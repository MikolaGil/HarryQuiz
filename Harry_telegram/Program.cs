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
            bot = new TelegramBotClient("");
            bot.StartReceiving();
            bot.OnMessage += Start;
            
            Console.ReadLine();

            bot.StopReceiving();
        }

        public static async void Start(object sender, MessageEventArgs ev)
        {
            Message message = ev.Message;
            userName = message.From.FirstName;
            chatId = message.From.Id;

            if (message.Text.Equals("/startus"))
            {
                bot.OnMessage -= Start;
                Adventure.StartAdventure(chatId);

            }
            else
            {
                await Dialog.SendMessage(ev.Message.From.Id, "Команды чата:\n /startus - начать приключение\n /help - помощь");
            }
        }
    }
}
