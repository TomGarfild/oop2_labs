using System;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Clients;
using Client.Domain.Common;
using Client.Handlers;

namespace Client.Menu
{
    public class RegistrationMenu : Menu
    {
        private readonly HttpClient _httpClient;
        private readonly AuthClient _authClient;
        private bool _changed = true;
        public RegistrationMenu(HttpClient httpClient)
        {

            _httpClient = httpClient;
            _authClient = new AuthClient(httpClient);
        }

        public override async Task Start()
        {
            do
            {
                if (_changed)
                {
                    PrintMenu(MenuConst.Main, MenuConst.AuthArgs);
                    _changed = false;
                }
                Console.Write("\rKey: ");
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.R:
                        await _authClient.HandleRegister();
                        break;
                    case ConsoleKey.L:
                        var token = await _authClient.HandleLogin();
                        if (token == null) break;
                        var gameMenu = new GameMenu(_httpClient, token);
                        await gameMenu.Start();
                        await _authClient.LogOut(token);
                        _changed = true;
                        break;
                    case ConsoleKey.E:
                        return;
                }

                Console.Write('\b');
            } while (true);
        }
        
    }
}