using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Client.Clients;
using Server.Models;

namespace Client.Handlers;

public static class AuthHandler
{
    public static async Task HandleRegister(this AuthClient authClient)
    {
        var response = await authClient.Register(GetContent());
        Console.WriteLine(response.IsSuccessStatusCode ? "Now you can login" : "Login exists already");
    }

    public static async Task<string> HandleLogin(this AuthClient authClient)
    {
        try
        {
            var token = await authClient.Login(GetContent());
            Console.WriteLine("Your LogIn was successful.");
            await Task.Delay(1000);
            return token.Substring(1, token.Length - 2);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.StatusCode == HttpStatusCode.BadRequest
                ? "You cannot login in several devices at the same time."
                : "Such user doesn't exist");
            return null;
        }
    }

    private static string GetField(string name, int min, int max)
    {
        var cursor = Console.CursorTop;
        Console.Write($"Enter {name}: ");
        var field = Console.ReadLine()?.Trim();

        while (field != null && (field.Length < min || field.Length > max))
        {
            Console.SetCursorPosition(0, cursor);
            Console.Write("\r" + new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursor);
            Console.Write($"\rEnter {name}(min length = {min}, max length = {max}): ");
            field = Console.ReadLine()?.Trim();
        }
        return field;
    }

    private static StringContent GetContent()
    {
        Console.WriteLine();
        var login = GetField("login", 3, 20);
        var password = GetField("password", 6, 64);

        var account = new Account()
        {
            Login = login,
            Password = password
        };
        var json = JsonSerializer.Serialize(account);

        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return content;
    }
}