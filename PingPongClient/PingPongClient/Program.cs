using System;

namespace PingPongClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Client client = new Client();

            client.RunClient();
        }
    }
}
