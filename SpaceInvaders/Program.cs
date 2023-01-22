using System;

namespace SpaceInvaders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Window game = new Window(512, 512, "My window");
            game.Run();
            Console.WriteLine("Close");
        }
    }
}
