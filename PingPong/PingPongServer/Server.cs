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

        public Server(int port)
        {
            _port = port;
            _counter = 0;
        }

        public void CreateServerSocket()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            IPEndPoint ipe = new IPEndPoint(ipAddress, _port);

            _server = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _server.Bind(ipe);
            _server.Listen(100);

            Console.WriteLine("Server started");

            while(true)
            {
                var connectionSocket = _server.Accept();
                byte[] buffer = new byte[1024];
                int bytesRecieved = connectionSocket.Receive(buffer);
                //string dataRecieved = Encoding.ASCII.GetString(buffer, 0, bytesRecieved);
                ChatWithClient(buffer, connectionSocket);
                Console.WriteLine("8");
            }
        }


        //public Task Start()
        //{

        //}

        public void RunServer()
        {
            while(true)
            {
                _counter++;
                
                
            }
        }

        public void StartListening()
        {
            
        }

        public void ChatWithClient(byte[] dataFromClient, Socket clientSocket)
        {
            string stringData = Encoding.ASCII.GetString(dataFromClient);
            PrintContent(stringData);
            SendToClient(stringData, clientSocket);
        }

        public void PrintContent(string dataFromClient)
        {
            Console.WriteLine($"From Client: {dataFromClient}");
        }

        public void SendToClient(string dataToSend, Socket clientSocket)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataToSend);
            clientSocket.Send(data);
            clientSocket.Close();
        }
    }
}
