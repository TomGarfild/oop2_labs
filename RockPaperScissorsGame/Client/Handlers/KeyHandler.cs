using System;

namespace Client.Handlers;

public class KeyHandler
{
    public static string GetGameMove()
    {
        Console.Write("\rKey: ");
        var key = Console.ReadKey().Key;

        switch (key)
        {
            case ConsoleKey.R:
                return "Rock";
            case ConsoleKey.P:
                return "Paper";
            case ConsoleKey.S:
                return "Scissors";
            case ConsoleKey.E:
                return null;
            default:
                return "";
        }
    }
}