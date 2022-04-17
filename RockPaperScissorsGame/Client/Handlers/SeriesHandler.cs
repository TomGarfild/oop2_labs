using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Client.Clients;
using Server.Models;

namespace Client.Handlers;

public static class SeriesHandler
{
    public static async Task<string> HandleSeries(this SeriesClient client, string route)
    {
        var seriesTask = client.GetSeries(route);

        Console.WriteLine("\rTrying to find your opponent. Press E to exit.");
        Console.Write("\rKey: ");
        while (seriesTask.Status != TaskStatus.RanToCompletion)
        {
            if (Console.KeyAvailable)
            {
                Console.Write("\rKey: ");
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.E)
                {
                    Console.WriteLine("\nYou exit from session.");
                    await Task.Delay(1000);
                    return null;
                }

                Console.Write("\b");
            }
        }

        Console.WriteLine("\nYour opponent was found.");
        await Task.Delay(1000);

        var response = await seriesTask;
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("\nRoom is full or not found");
            await Task.Delay(2000);
            return null;
        }

        var seriesJson = await (response).Content.ReadAsStringAsync();
        var seriesId = JsonSerializer.Deserialize<Series>(seriesJson, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })?.Id;

        return seriesId;
    }

    public static async Task HandlePrivateSeries(this SeriesClient client)
    {
        var response = await client.GetPrivateSeries();
        var code = JsonSerializer.Deserialize<PrivateSeries>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })?.Code;
        Console.WriteLine($"\nCode: {code}. Now you and your friend can login this room.");
    }
}