using dotenv.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp;

namespace DVBot
{
    public class Client
    {
        private TelegramClient _client;

        public Client()
        {
            var env = DotEnv.Read();
            _client = new TelegramClient(new
            (
                "dv_bot",
                int.Parse(env["DV_APIID"]),
                env["DV_APIHASH"],
                null,
                env["DV_BOTTOKEN"]
            ));
            _client.AuthorizationStateChanged += OnClientAuthorizationStateChanged;
        }

        private async void OnClientAuthorizationStateChanged(object? sender, TeleSharp.Types.AuthorizationStateChangedEventArgs e)
        {
            Console.WriteLine(e.State);
            if (e.State == TeleSharp.Enums.AuthorizationState.Ready)
            {
                await _client.SendTextMessageAsync(chatId: 0, message: "I'm ready!");
            }
            await e.Respond(Console.ReadLine());
        }
    }
}
