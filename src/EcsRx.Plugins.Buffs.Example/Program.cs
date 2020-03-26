using System;

namespace EcsRx.Plugins.Buffs.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new Application();
            application.StartApplication();

            Console.ReadKey();
            application.StopApplication();
        }
    }
}