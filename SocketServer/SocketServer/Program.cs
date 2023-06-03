using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Any;
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 23000);
            listener.Bind(ipEndPoint);
            Console.WriteLine("About to accept to incoming connection");
            listener.Listen(5);
            Socket client = listener.Accept();
            Console.WriteLine("Client connected " + client.ToString() + "Ip Adress: " + client.RemoteEndPoint.ToString());
            byte[] buffer = new byte[128];
            while (true)
            {
                int numberOfRecivedByte = 0;
                numberOfRecivedByte = client.Receive(buffer);
                Console.WriteLine("Number of recived bytes: " + numberOfRecivedByte);
                string data = Encoding.UTF8.GetString(buffer);
                Console.WriteLine("Data send by client: " + data);
                client.Send(buffer);

                if (data == "x")
                {
                    break;
                }
                Array.Clear(buffer, 0, numberOfRecivedByte);
                numberOfRecivedByte = 0;
            }
        }
    }
}
