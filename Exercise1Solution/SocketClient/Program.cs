using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Programm
    {
        static Socket socket;
        static void Main(string[] args)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            try
            {
                socket.Connect(localEndPoint);
            }
            catch
            {
                Console.WriteLine("Client is unable to connect");
                socket.Close();
                System.Environment.Exit(0);
            }
            Console.WriteLine("Enter text:");
            while (true)
            {
                string text = Console.ReadLine();
                if (text.StartsWith("#"))
                {
                    if (text == "#quit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command.");
                        continue;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(text);
                socket.Send(data);
            }
            socket.Close();
        }
    }
}
