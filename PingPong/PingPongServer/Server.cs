using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPongServer
{
    public class Server
    {
        private int _port;
        private int _counter;
        private Socket _server;
        private const string IP = "10.1.0.17";

        public Server()
        {
        }

        public void CreateServerSocket()
        {
            IPAddress ipa = IPAddress.Parse(IP);
            Console.WriteLine("Enter PORT");
            _port = int.Parse(Console.ReadLine());
            IPEndPoint ipe = new IPEndPoint(ipa, _port);

            _server = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _server.Bind(ipe);
            _server.Listen(100);

            Console.WriteLine("Server started");

            while(true)
            {
                var connectionSocket = _server.Accept();
                
                Task task = new Task(() => ChatWithClient(connectionSocket));
                task.Start();
            }
        }
        public void ChatWithClient(Socket clientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRecieved = clientSocket.Receive(buffer);
                string stringData = Encoding.ASCII.GetString(buffer);
                PrintContent(stringData);
                SendToClient(stringData, clientSocket);
            }catch(Exception e)
            {
                Console.WriteLine("Client disconnected");
            }
        }

        public void PrintContent(string dataFromClient)
        {
            Console.WriteLine($"From Client: {dataFromClient}");
        }

        public void SendToClient(string dataToSend, Socket clientSocket)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataToSend);
            clientSocket.Send(data);
        }
    }
}
