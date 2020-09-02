using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PingPongClient
{
    public class Client
    {
        private string _ip;
        private int _port;
        private Byte[] _bytesReceived;
        private Socket _client;

        public Client()
        {
            _bytesReceived = new Byte[256];
        }

        public void RunClient()
        {
            //string userInput = Console.ReadLine();
            //DefineIpPort(userInput);
            var ipa = GetServerDetails();
            
            while(true)
            {
                ConnectToServer(ipa);
                SendValueToServer();
                WaitToValueFromServer();
            }
        }

        public IPAddress GetServerDetails()
        {
            Console.WriteLine("Enter IP");
            _ip = Console.ReadLine();
            IPAddress ipa = IPAddress.Parse(_ip);

            Console.WriteLine("Enter PORT");
            _port = int.Parse(Console.ReadLine());

            return ipa;
        }

        public void ConnectToServer(IPAddress ipa)
        {
            IPEndPoint ipe = new IPEndPoint(ipa, _port);

            _client = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _client.Connect(ipe);
        }

        

        public void SendValueToServer()
        {
            Console.WriteLine("Enter data to send");
            string dataToSend = Console.ReadLine();

            byte[] data = Encoding.ASCII.GetBytes(dataToSend);

            _client.Send(data);

        }

        public void WaitToValueFromServer()
        {
            _client.Receive(_bytesReceived, _bytesReceived.Length, 0);
            var valueFromServer = Encoding.ASCII.GetString(_bytesReceived);
            Console.WriteLine($"From Server: {valueFromServer}");
            _client.Close();
        }
    }
}
