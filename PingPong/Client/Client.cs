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

        public Client()
        {

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

            Socket client = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(ipe);
        }
    }
}
