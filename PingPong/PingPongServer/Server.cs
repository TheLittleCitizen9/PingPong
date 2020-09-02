using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PingPongServer
{
    public class Server
    {
        private int _port;
        private TcpListener _server;
        private const string IP = "10.1.0.17";

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
                PrintContent(buffer);
                SendToClient(stringData, clientSocket);
            }catch(Exception e)
            {
                Console.WriteLine("Client disconnected");
            }
        }

        public void PrintContent(byte[] dataFromClient)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            var binForm = new BinaryFormatter();
            memStream.Write(dataFromClient, 0, dataFromClient.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var person = binForm.Deserialize(memStream);
            Console.WriteLine($"From Client: {person.ToString()}");
        }

        public void SendToClient(string dataToSend, TcpClient clientSocket)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataToSend);
            NetworkStream nwStream = clientSocket.GetStream();
            nwStream.Write(data);
        }
    }
}
