using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel;


namespace Vox_Bot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("app-settings.json")
                .AddEnvironmentVariables()
                .Build();

            Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();

            var token = settings?.Token;

            var Client = new TelegramBotClient(token);
            Client.StartReceiving(Update, Error);
            Console.WriteLine("Bot ready");
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message.Text != null)
            {
                if (message.Text.ToLower().Contains("привет"))
                {
                    botclient.SendTextMessageAsync(message.Chat.Id, "Привет");
                }
                else
                {
                    botclient.SendTextMessageAsync(message.Chat.Id, "я понимаю только привет х.х");
                }
            }
        }

        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

    }
}
