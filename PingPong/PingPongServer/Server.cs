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
        private TcpListener _server;
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

            _server = new TcpListener(ipa, _port);

            _server.Start(100);

            Console.WriteLine("Server started");

            while(true)
            {
                var connectionSocket = _server.AcceptTcpClient();
                Task task = new Task(() => ChatWithClient(connectionSocket));
                task.Start();
            }
        }
        public void ChatWithClient(TcpClient clientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                NetworkStream nwStream = clientSocket.GetStream();
                int bytesRecieved = nwStream.Read(buffer);
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

        public void SendToClient(string dataToSend, TcpClient clientSocket)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataToSend);
            NetworkStream nwStream = clientSocket.GetStream();
            nwStream.Write(data);
        }
    }
}
