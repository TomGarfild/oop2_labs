using System;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Clients;
using Client.Domain.Common;
using Client.Handlers;

namespace Client.Menu
{
    public class StatisticMenu : Menu
    {
        private readonly StatisticClient _statisticClient;
        public StatisticMenu(HttpClient httpClient)
        {
            _statisticClient = new StatisticClient(httpClient);
        }
        public override async Task Start()
        {
            PrintMenu(MenuConst.Stat, MenuConst.StatArgs);
            do
            {
                Console.Write("\rKey: ");
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        await _statisticClient.HandleLocalStatistic();
                        break;
                    case ConsoleKey.D2:
                        await _statisticClient.HandleGlobalStatistic();
                        break;
                    case ConsoleKey.E:
                        return;
                }
            } while (true);
        }
    }
}