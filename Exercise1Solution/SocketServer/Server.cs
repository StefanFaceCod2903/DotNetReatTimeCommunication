using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SockerServer
{
    class Program
    {
        static Byte[] Buffer { get; set; }
        static Socket sck;
        static void Main(string[] args)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Any, 1234));
            sck.Listen(100);
            bool open = true;
            Socket accepted = sck.Accept();
            Thread backgroundThread = new Thread(new ThreadStart(()=>
            {
                while (open) {
                    string command = Console.ReadLine();
                    if (command == "#quit") {
                        open = false;
                    } else
                    {
                        Console.WriteLine("Command not valid! Write '#quit to quit'");
                    }
                }
            }));
            backgroundThread.Start();
            while (open)
            {
                Buffer = new Byte[accepted.SendBufferSize];
                int bytesRead = accepted.Receive(Buffer);

                byte[] formatted = new byte[bytesRead];
                for (int i = 0; i < formatted.Length; i++)
                {
                    formatted[i] = Buffer[i];
                }
                string data = Encoding.ASCII.GetString(formatted);
                Console.WriteLine(data);
            }
            accepted.Close();
            sck.Close();
        }
    }
}
