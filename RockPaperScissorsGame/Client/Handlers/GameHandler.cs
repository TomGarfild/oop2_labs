using System;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Clients;

namespace Client.Handlers;

public static class GameHandler
{
    public static async Task<bool> MakeMove(this GameClient client, string move)
    {
        var result = await client.GetResult(move);
        if (result.Equals("Undefined"))
        {
            await Task.Delay(3000);
            Console.WriteLine("\nYour opponent didn't make a choice. You will be redirected to menu.");
            return false;
        }
        Console.WriteLine($"\nResult: {result}");
        return true;
    }
}