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
        private TcpClient _client;
        private IPEndPoint _ipe;

        public Client()
        {
            _bytesReceived = new Byte[256];
        }

        public void RunClient()
        {
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
            _ipe = new IPEndPoint(ipa, _port);

            _client = new TcpClient(_ip, _port);
        }

        

        public void SendValueToServer()
        {
            Console.WriteLine("Enter data to send");
            string dataToSend = Console.ReadLine();

            byte[] data = Encoding.ASCII.GetBytes(dataToSend);
            NetworkStream nwStream = _client.GetStream();

            nwStream.Write(data);
        }

        public void WaitToValueFromServer()
        {
            NetworkStream nwStream = _client.GetStream();
            nwStream.Read(_bytesReceived, 0, _bytesReceived.Length);
            var valueFromServer = Encoding.ASCII.GetString(_bytesReceived);
            Console.WriteLine($"From Server: {valueFromServer}");
            _client.Close();
        }
    }
}
