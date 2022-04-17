using System;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Clients;
using Client.Domain.Common;
using Client.Handlers;

namespace Client.Menu
{
    public class RoomMenu : Menu
    {
        private string _seriesRoute;
        private readonly GameClient _gameClient;
        private readonly SeriesClient _seriesClient;
        public RoomMenu(HttpClient httpClient)
        {
            _gameClient = new GameClient(httpClient);
            _seriesClient = new SeriesClient(httpClient);

        }
        public override async Task Start()
        {
            if (_seriesRoute.Contains("Private"))
            {
                var privateMenu = await PrivateMenuAsync();
                if (!privateMenu) return;
            }
            var exit = await _seriesClient.HandleSeries(_seriesRoute);
            if (exit == null) return;
            _gameClient.SetSeriesId(exit);


            PrintMenu(MenuConst.Room, MenuConst.RoomArgs);

            while (true)
            {
                var answer = KeyHandler.GetGameMove();
                if (answer == null) return;
                if (answer == "default") continue;

                var result = await _gameClient.MakeMove(answer);
                if (!result) return;
            }
        }

        private async Task<bool> PrivateMenuAsync()
        {
            PrintMenu(MenuConst.PrivateRoom, MenuConst.PrivateRoomArgs);
            while (true)
            {
                Console.Write("\rKey: ");
                var key = Console.ReadKey().Key;
                var enter = false;
                switch (key)
                {
                    case ConsoleKey.D1:
                        await _seriesClient.HandlePrivateSeries();
                        break;
                    case ConsoleKey.D2:
                        Console.Write("\nEnter room's code: ");
                        var entranceCode = Console.ReadLine();
                        _seriesClient.SetRoomCode(entranceCode);
                        enter = true;
                        break;
                    case ConsoleKey.E:
                        return false;
                }

                if (enter) return true;
            } 
        }

        public void SetRoutes(string seriesRoute, string gameRoute)
        {
            _seriesRoute = seriesRoute;
            _gameClient.SetRoute(gameRoute);
        }
    }
}