using System;

namespace PingPongServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Server server = new Server(9000);
            server.CreateServerSocket();
            Console.WriteLine("hi");
        }
    }
}
