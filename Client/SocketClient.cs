using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class SocketClient
    {
        private static int port = 500;
        private static IPHostEntry host = Dns.GetHostEntry("localhost");
        private static IPAddress ipAddress = host.AddressList[1];
        private static IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
        private static byte[] bytes = new byte[1024];
        public static void Main()
        {
            CreateClient();
        }

        private static void CreateClient()
        {
            try
            {
                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(localEndPoint);
                Console.WriteLine($"Socket connected to {socket.RemoteEndPoint.ToString()}");

                while (true)
                {
                    Console.WriteLine("Write Message");
                    var message = Console.ReadLine();
                    if ("Exist".Equals(message))
                    {
                        break;
                    }

                    byte[] msg = Encoding.ASCII.GetBytes(message);
                    int bytesSent = socket.Send(msg);
                    int bytesRec = socket.Receive(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine($"Server Message {data}");
                }

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}
