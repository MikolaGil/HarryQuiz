using System;
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
                await Dialog.SendMessage(Program.chatId, "Верно!");
                SecondQuestion();
                Program.bot.OnCallbackQuery -= FirstAnswer;
                Program.bot.OnMessage += SecondAnswer;
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
            await Dialog.SendMessage(Program.chatId, "Зал заполняется учениками. Все ждут учетелей.\n" +
                "Ожидание затягивается, преподавателей все нет и нет.\n" +
                "Наконец появляется Дамблдор с испуганными глазами и объявляет что шляпа пропала."
                );

            await Program.bot.SendDocumentAsync(Program.chatId, "https://thumbs.gfycat.com/RecklessSharpChihuahua-small.gif");
            await Dialog.SendMessage(Program.chatId, "Кажется это твой звездный час! Ты ведь читала много книг Донцовой и Агаты Кристи.\n" +
                "Тебе точно под силу разгадать это преступление. Последний раз шляпу видели на кухне. Попробуй расспросить поваров.");
        }

        private static async void SecondAnswer(object sender, MessageEventArgs e)
        {
            var message = e.Message.Text.ToLower();

            if (message.Equals("apistogramma panduro"))
            {
                await Dialog.SendMessage(Program.chatId, "Думаю это название какого-то существа, которое даст тебе подсказку.");

                Program.bot.OnMessage -= SecondAnswer;
                ThirdQuestion();
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Неверно, попробуй ещё");
            }
        }
        //Third Question--------------
        private static async void ThirdQuestion()
        {
            Program.bot.OnMessage += ThirdAnswer;
        }

        private static async void ThirdAnswer(object sender, MessageEventArgs e)
        {
            var message = e.Message.Text.ToLower();

            if (message.Equals("буль-буль-ом-ом"))
            {
                await Dialog.SendMessage(Program.chatId, "Я знаю эту фразу: 'Книга - друг одинокого, а библиотека убежище бездомного'.");

                Program.bot.OnMessage -= ThirdAnswer;

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
            Program.bot.OnMessage += FourthAnswer;
        }

        private static async void FourthAnswer(object sender, MessageEventArgs e)
        {
            var message = e.Message.Text.ToLower();

            if (message.Equals("метла"))
            {
                await Program.bot.SendDocumentAsync(Program.chatId, "https://i.pinimg.com/originals/51/06/97/5106974384f040feccb5304a0d91f0c6.jpg");
                await Dialog.SendMessage(Program.chatId, "Мы видели метлу в коморке но сейчас она закрыта, вспомни заклинание что бы открыть дверь");

                Program.bot.OnMessage -= FourthAnswer;
                Program.bot.OnMessage += FifthAnswer;

                FifthQuestion();
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Не-а, попробуй ещё.");
            }
        }

        static async void FifthQuestion()
        {
            var keyboardArr = new[]
{
                new[]{
                    new KeyboardButton("Аларте Аскендаре") { Text = "Аларте Аскендаре"},
                    new KeyboardButton("Окулус Репаро") { Text = "Окулус Репаро"}
                },
                new[]{
                    new KeyboardButton("Люмус"){ Text = "Люмус"},
                    new KeyboardButton("Алохомора"){ Text = "Алохомора"}
                },
            };

            var keyboard = new ReplyKeyboardMarkup(keyboardArr);
            await Program.bot.SendTextMessageAsync(Program.chatId, "Заклинания", replyMarkup: keyboard);
        }

        private static async void FifthAnswer(object sender, MessageEventArgs e)
        {
            var message = e.Message.Text;
            
            if (message.Equals("Алохомора"))
            {
                Program.bot.OnMessage -= FifthAnswer;
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
            Program.bot.OnMessage += SixAnswer;
        }

        private static async void SixAnswer(object sender, MessageEventArgs e)
        {
            var message = e.Message.Text.ToLower();

            if (message.Equals("люмус") || message.Equals("люмос"))
            {
                Program.bot.OnMessage -= SixAnswer;

                await Program.bot.SendDocumentAsync(Program.chatId, "https://vignette.wikia.nocookie.net/harrypotter/images/4/43/Wand-lighting_charm.gif/revision/latest?cb=20141024183345&path-prefix=ru");
                await Dialog.SendMessage(Program.chatId, "Нужно изучить метлу поближе, возможно она подписана. ");

                Program.bot.OnMessage += SevenAnswer;
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Пыф. Нет, только искра появилась");
            }
        }

        private static async void SevenAnswer(object sender, MessageEventArgs e)
        {
            var message = e.Message.Text.ToLower();

            if (message.Equals("harry p"))
            {
                await Dialog.SendMessage(Program.chatId, "Должно быть Гарри Поттера, он поможет тебе...");
                Program.bot.OnMessage -= SevenAnswer;
                Program.bot.OnMessage += EightAnswer;
                SevenQuestion();
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Не знаю такого :( ...");
            }
        }

        private static async void SevenQuestion()
        {
            await Dialog.SendMessage(Program.chatId, "Гарри: -Просто так не подскажу. Ответь на вопрос.\n Сколько человек помещается в главном зале Хогвартс?");
        }

        private static async void EightAnswer(object sender, MessageEventArgs e)
        {
            try
            {
                var message = Convert.ToInt32(e.Message.Text);

                if (message < 280)
                    await Dialog.SendMessage(Program.chatId, "Не-а. Больше");

                if (message > 280)
                    await Dialog.SendMessage(Program.chatId, "Нет. По-меньше");

                if (message == 280)
                {
                    await Dialog.SendMessage(Program.chatId, "Верно! Я видел Хагрида со шляпой у него в хижине. Загляни к нему");
                    await Program.bot.SendDocumentAsync(Program.chatId, "https://media1.giphy.com/media/50YgxbPnjA1LG/giphy.gif");

                    Program.bot.OnMessage -= EightAnswer;
                    EightQuestion();
                }
            }
            catch (Exception)
            {
                await Dialog.SendMessage(Program.chatId, "Нет такого числа!");
            }
        }

        private static async void EightQuestion()
        {
            await Dialog.SendMessage(Program.chatId, "Хагрид! Что шляпа делает у тебя? У нас сегодня распределение.");
            await Dialog.SendMessage(Program.chatId, "Ой, я совсем забыл её вернуть. Мне было так скучно на каникулах, и я решил поболтать хоть с кем-то.");
            await Dialog.SendMessage(Program.chatId, "Я отдам тебе шляпу если ты вспомнишь на что у меня аллергия?");

            var inlineMenu = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Единороги", "1"),
                            InlineKeyboardButton.WithCallbackData("Первокурсники", "2")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Кошки", "3"),
                            InlineKeyboardButton.WithCallbackData("Мандрагоры", "4")
                        }
                    });

            await Program.bot.SendTextMessageAsync(Program.chatId, "Варианты аллергии:", replyMarkup: inlineMenu);
            Program.bot.OnCallbackQuery += NineAnswer;
        }

        private static async void NineAnswer(object sender, CallbackQueryEventArgs e)
        {
            var answer = e.CallbackQuery.Data;

            if (answer == "3")
            {
                await Dialog.SendMessage(Program.chatId, "Именно! Держи шляпу!");
                await Dialog.SendMessage(Program.chatId, $"Дамблдор: О, ты должно быть {Program.userName}, наш новый ученик. Спасибо тебе! И за первое полезное дело для Хогвартса, возьми себе подарок из коморки, только бери справа=)");
                await Program.bot.SendDocumentAsync(Program.chatId, "http://media.tumblr.com/tumblr_lo5vbgRbdy1qbknh7.gif");
                Program.bot.OnCallbackQuery -= NineAnswer;


                Program.bot.OnMessage += Program.Start;
            }
            else
            {
                await Dialog.SendMessage(Program.chatId, "Нет, этих я люблю =)");
            }
        }
    }
}
