using System;
using System.Threading.Tasks;

namespace Client.Menu
{
    public abstract class Menu
    {
        public abstract Task Start();
        protected static void PrintMenu(string header, string[] fields)
        {
            Console.Clear();
            Console.WriteLine("\n+-------------------------------+");
            Console.WriteLine(header);
            Console.WriteLine("+-------------------------------+");
            foreach (var field in fields)
            {
                Console.WriteLine(field);
            }
            Console.WriteLine("+-------------------------------+");
        }
    }
}