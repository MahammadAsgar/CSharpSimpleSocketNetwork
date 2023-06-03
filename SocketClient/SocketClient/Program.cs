using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = null;
            try
            {
                Console.WriteLine("Socket Client");
                Console.WriteLine("Enter Ip Adress");
                string strIp = Console.ReadLine();
                int portInput = 0;

                Console.WriteLine("Enter port");
                string strPort = Console.ReadLine();
                if (!IPAddress.TryParse(strIp, out ipAddr))
                {
                    Console.WriteLine("invalid Ip adress");
                    return;
                }

                if (!int.TryParse(strPort.Trim(), out portInput))
                {
                    Console.WriteLine("invalid port");
                    return;
                }
                Console.WriteLine(string.Format("IpAdress: {0} - Port: {1}", strIp.ToString(), portInput));
                client.Connect(ipAddr, portInput);
                Console.WriteLine("connected to the server. text ex to close");
                string inputData;
                while (true)
                {
                    inputData = Console.ReadLine();
                    if (inputData.Equals("ex"))
                    {
                        break;
                    }
                    byte[] buffSend = Encoding.ASCII.GetBytes(inputData);
                    client.Send(buffSend);

                    byte[] buffRecieve = new byte[128];
                    int recivedBytes = client.Receive(buffRecieve);
                    Console.WriteLine("Recived Data: " + Encoding.ASCII.GetString(buffRecieve, 0, recivedBytes));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                client.Dispose();
            }
        }
    }
}
