using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Harry_telegram
{
    static class Adventure
    {
        public static async void StartAdventure(int chatId)
        {
            await Dialog.SendMessage(chatId, $"Приветствую тебя {Program.userName}, кажется это твой первый учебный год в Школе Магии Хогвартс. Что ж давай собираться в путь.");
            FirstQuestion(chatId);
        }
        //First Quesdtion------------------
        public static async void FirstQuestion(int chatId)
        {
            await Dialog.SendMessage(chatId, "Нам с тобой нужно добраться на платформу 9¾. Все прекрасно знают что платформа находится на лондонском вокзале. А знаешь ли ты адрес самого вокзала?");

            Program.bot.OnCallbackQuery += FirstAnswer;

            var inlineMenu = new InlineKeyboardMarkup(new[]
{
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Privokzalnaya square, Bobruiskaya 1", "1"),
                            InlineKeyboardButton.WithCallbackData("Pancras Rd, Kings Cross, N1 9AP,", "2")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Park Avenue, 42 street", "3"),
                            InlineKeyboardButton.WithCallbackData("Marunouchi, Chiyoda-ku, Tōkyō-to", "4")
                        }
                    });

            await Program.bot.SendTextMessageAsync(chatId,"Варианты ответа", replyMarkup: inlineMenu);
        }

        private static async void FirstAnswer(object sender, CallbackQueryEventArgs e)
        {
            var answer = e.CallbackQuery.Data;

            if (answer == "2")
            {
                await Program.bot.SendDocumentAsync(Program.chatId, "https://pa1.narvii.com/6284/e6f5947ed2bd81ca95674af5268a6dc07d0c8ddd_hq.gif");
                await Dialog.SendMessage(Program.chatId, "Верно!");

                SecondQuestion();
                Program.bot.OnCallbackQuery -= FirstAnswer;
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Нет, отсюда мы в Хогвартс не попадем =(");
            }
        }
        //Second Question----------------------------
        private static async void SecondQuestion()
        {
            await Dialog.SendMessage(Program.chatId, "Дорога в поезде не заняла много времени. Всех студентов просят собраться в главном зале для распределения по факультетам. ");
            await Program.bot.SendDocumentAsync(Program.chatId, "https://media1.giphy.com/media/kojtc7CkEsXZK/giphy.gif");
            await Dialog.SendMessage(Program.chatId, "Зал заполняется учениками. Все ждут учетелей.\n" +
                "Ожидание затягивается, преподавателей все нет и нет.\n" +
                "Наконец появляется Дамблдор с испуганными глазами и объявляет что шляпа пропала."
                );

            await Program.bot.SendDocumentAsync(Program.chatId, "https://thumbs.gfycat.com/RecklessSharpChihuahua-small.gif");
            await Dialog.SendMessage(Program.chatId, "Кажется это твой звездный час! Ты ведь читала много книг Донцовой и Агаты Кристи.\n" +
                "Тебе точно под силу разгадать это преступление. Последний раз шляпу видели на кухне. Попробуй расспросить поваров.");

            Program.bot.OnCallbackQuery += SecondAnswer;
        }

        private static async void SecondAnswer(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Data;

            if (message.Equals("Apistogramma panduro"))
            {
                await Dialog.SendMessage(Program.chatId, "Что бы это могло значить, возможно всезнающий гугл поможет тебе");

                Program.bot.OnCallbackQuery -= SecondAnswer;
                ThirdQuestion();
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Неверно, попробуй ещё");
            }
        }

        private static async void ThirdQuestion()
        {
            await Dialog.SendMessage(Program.chatId, "Я понимаю рыбий, напиши мне что она тебе сказала");
            Program.bot.OnCallbackQuery += ThirdAnswer;
        }

        private static async void ThirdAnswer(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Data;

            if (message.Equals("Буль-буль-ом-ом"))
            {
                await Dialog.SendMessage(Program.chatId, "Я знаю эту фразу 'Книга - друг одинокого, а библиотека убежище бездомного'.");

                Program.bot.OnCallbackQuery -= ThirdAnswer;

                FouthQuestion();
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Чушь какая-то, давай ещё раз попробуем.");
            }
        }

        private static async void FouthQuestion()
        {
            await Dialog.SendMessage(Program.chatId, "Хмм, кажется в нашей библиотеке появилась новая книга, попробуй найти её.");
            Program.bot.OnCallbackQuery += FourthAnswer;
        }

        private static async void FourthAnswer(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Data.ToLower();

            if (message.Equals("метла"))
            {
                await Dialog.SendMessage(Program.chatId, "Мы видели метлу в коморке но сейчас она закрыта, вспомни заклинание что бы открыть дверь");

                Program.bot.OnCallbackQuery -= FourthAnswer;
                Program.bot.OnCallbackQuery += FifthAnswer;
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Не-а, попробуй ещё.");
            }
        }

        private static async void FifthAnswer(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Data;

            var keyboardArr = new[]
            {
                new[]{
                    new KeyboardButton("Аларте Аскендаре") { Text = "1"},
                    new KeyboardButton("Окулус Репаро") { Text = "2"}
                },
                new[]{
                    new KeyboardButton("Пуллус"){ Text = "3"},
                    new KeyboardButton("Алохомора"){ Text = "4"}
                },
            };

            var keyboard = new ReplyKeyboardMarkup(keyboardArr);
            await Program.bot.SendTextMessageAsync(Program.chatId, "Заклинания", replyMarkup:keyboard);
            
            if (message.Equals("4"))
            {
                Program.bot.OnCallbackQuery -= FifthAnswer;
                await Program.bot.SendTextMessageAsync(Program.chatId, "Чик-чпок, замок открылся");
                
                SixQuestion();
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Ой. Кажется, ты кого-то в жабу превратил...");
            }
        }

        private static async void SixQuestion()
        {
            await Dialog.SendMessage(Program.chatId, "За дверью совсем темно, зажги свет на палочке.");
            Program.bot.OnCallbackQuery += SixAnswer;
        }

        private static async void SixAnswer(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Data.ToLower();

            if (message.Equals("люмус"))
            {
                await Dialog.SendMessage(Program.chatId, "Нужно изучить метлу поближе, возможно она подписана. ");
                Program.bot.OnCallbackQuery += SevenAnswer;
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Пыф. Нет, только искра появилась");
            }
        }

        private static async void SevenAnswer(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Data.ToLower();

            if (message.Equals("люмус"))
            {
                await Dialog.SendMessage(Program.chatId, "Нужно изучить метлу поближе, возможно она подписана. ");
                SevenQuestion();
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Пыф. Нет, только искра появилась");
            }
        }
    }
}
