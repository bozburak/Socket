using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Helper
{
    public static class Helper
    {

        public static Socket CreateSocket()
        {
            Socket socket = new Socket(host.AddressList[0].AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            return socket;
        }

        public static void ConnectSocket(Socket socket)
        {
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
            socket.Connect(remoteEP);
        }

        public static void ReleaseSocket(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public static string ConvertReceiveMessage(Socket socket)
        {
            var bytes = new byte[1024];
            int bytesRec = socket.Receive(bytes);
            var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            return data;
        }
    }
}
