using System.Net;
using System.Text;
using System.Net.Sockets;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

IPAddress IP_ADDRESS = IPAddress.Any;
int port = 6379;
TcpListener server = new TcpListener(IP_ADDRESS, port);
var pingResponse = Encoding.ASCII.GetBytes("+PONG\r\n");

try
{    
    server.Start();
    Console.WriteLine($"Server started with IP Address : {IP_ADDRESS} and port: {port}");
    using var tcpClient = server.AcceptTcpClient();
    tcpClient.Client.Send(pingResponse, SocketFlags.None);
    using var streamReader = new StreamReader(tcpClient.GetStream());
    while (true)
    {
        var receivedMsg = streamReader.ReadLine();
        if (!string.IsNullOrWhiteSpace(receivedMsg))
        {
            // Send "PONG" to any incoming message on the socket.        
            tcpClient.Client.Send(pingResponse, SocketFlags.None);
        }
    }
}
finally
{
    server.Stop();
}