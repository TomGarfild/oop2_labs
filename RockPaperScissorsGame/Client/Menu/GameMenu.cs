using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Client.Domain.Common;

namespace Client.Menu
{
    public class GameMenu : Menu
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;
        private readonly MediaTypeWithQualityHeaderValue _mediaType;
        public GameMenu(HttpClient httpClient, string token)
        {
            _httpClient = httpClient;
            _token = token;
            _mediaType = new MediaTypeWithQualityHeaderValue("application/json");
        }
        public override async Task Start()
        {
            var changed = true;
            SetHeaders(_token);
            var roomMenu = new RoomMenu(_httpClient);

            do
            {
                if (changed)
                {
                    PrintMenu(MenuConst.Main, MenuConst.GameArgs);
                }

                changed = true;
                Console.Write("\rKey: ");
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        roomMenu.SetRoutes("/series/NewPublicSeries", "/round/Play");
                        break;
                    case ConsoleKey.D2:
                        roomMenu.SetRoutes("/series/SearchPrivateSeries", "/round/Play");
                        break;
                    case ConsoleKey.D3:
                        roomMenu.SetRoutes("/series/NewTrainingSeries", "/round/TrainingPlay");
                        break;
                    case ConsoleKey.E:
                        return;
                    default:
                        changed = false;
                        continue;
                }
                await roomMenu.Start();
            } while (true);
            
        }

        private void SetHeaders(string token)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("x-token"))
            {
                _httpClient.DefaultRequestHeaders.Add("x-token", _token);
            }
            if (!_httpClient.DefaultRequestHeaders.Accept.Contains(_mediaType))
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(_mediaType);
            }
        }
    }
}