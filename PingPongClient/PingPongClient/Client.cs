using System;
using System.Collections.Generic;
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

        public void RunClient(string userInput)
        {
            DefineIpPort(userInput);
            ConnectToServer();
            SendValueToServer();
            WaitToValueFromServer();
        }

        public void DefineIpPort(string userInput)
        {
            _ip = userInput.Split(':')[0];
            _port = int.Parse(userInput.Split(':')[1]);
        }

        public void ConnectToServer()
        {
            IPAddress ipa = IPAddress.Parse(_ip);

            IPEndPoint ipe = new IPEndPoint(ipa, _port);

            _client = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _client.Connect(ipe);
        }

        public void SendValueToServer()
        {
            string dataToSend = Console.ReadLine();

            byte[] data = Encoding.ASCII.GetBytes(dataToSend);

            _client.Send(data);
        }

        public void WaitToValueFromServer()
        {
            int bytes = 0;
            do
            {
                bytes = _client.Receive(_bytesReceived, _bytesReceived.Length, 0);
                var valueFromServer = Encoding.ASCII.GetString(_bytesReceived, 0, bytes);
                Console.WriteLine($"From Server: {valueFromServer}");
            }
            while (bytes > 0);
        }
    }
}
