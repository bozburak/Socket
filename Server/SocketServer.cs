using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class SocketServer
    {
        private static int port = 500;
        private static IPHostEntry host = Dns.GetHostEntry("localhost");
        private static IPAddress ipAddress = host.AddressList[1];
        private static IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
        private static byte[] bytes = new byte[1024];
        public static void Main()
        {
            CreateServer();
        }

        private static void CreateServer()
        {
            try
            {
                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(localEndPoint);
                socket.Listen(10);
                Console.WriteLine("Waiting connection");
                Socket handler = socket.Accept();

                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if ("Exist".Equals(data))
                    {
                        break;
                    }
                    Console.WriteLine("Text received : {0}", data);

                    byte[] msg = Encoding.ASCII.GetBytes("Server == " + data);
                    handler.Send(msg);
                }

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
