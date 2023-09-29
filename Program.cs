using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerConApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int port = 8080;
            string ip = "127.0.0.1";
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port); // ipPoint - точка подключения
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(ipPoint);
                socket.Listen();
                Console.WriteLine("Server started. Waiting for connections...");
                Socket client = await socket.AcceptAsync();
                Console.WriteLine("Client connected. Waiting for data...");
                do
                {
                   
                    byte[] data = new byte[256];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = await client.ReceiveAsync(data, SocketFlags.None);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                        Console.WriteLine("Data received");
                        Console.WriteLine(builder.ToString());
                    } while (client.Available > 0);
                } while (true);
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}