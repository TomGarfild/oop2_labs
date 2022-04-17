using System;
using System.Threading.Tasks;
using Client.Clients;

namespace Client.Handlers;

public static class StaticHandler
{
    public static async Task HandleLocalStatistic(this StatisticClient client)
    {
        var response = await client.GetLocalStatistic();
        Console.WriteLine($"\nLocale statistic: {response}");
    }

    public static async Task HandleGlobalStatistic(this StatisticClient client)
    {
        var response = await client.GetGlobalStatistic();
        Console.WriteLine($"\nGlobal statistic: {response}");
    }
}