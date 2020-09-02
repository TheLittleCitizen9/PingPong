using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

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
            Console.WriteLine("Enter person name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter person age");
            int age = int.Parse(Console.ReadLine());

            Person.Person person = new Person.Person(name, age);

            BinaryFormatter bf = new BinaryFormatter();
            NetworkStream nwStream = _client.GetStream();

            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, person);

            byte[] data = ms.ToArray();

            nwStream.Write(data);
        }

        public void WaitToValueFromServer()
        {
            NetworkStream nwStream = _client.GetStream();
            nwStream.Read(_bytesReceived, 0, _bytesReceived.Length);

            MemoryStream memStream = new MemoryStream();
            var binForm = new BinaryFormatter();
            memStream.Write(_bytesReceived, 0, _bytesReceived.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var person = binForm.Deserialize(memStream);

            Console.WriteLine($"From Server: {person.ToString()}");
            _client.Close();
        }
    }
}
