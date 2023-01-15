using System.Net;
using System.Text;
using System.Net.Sockets;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

IPAddress IP_ADDRESS = IPAddress.Any;
int port = 6379;
TcpListener server = new TcpListener(IP_ADDRESS, port);

try 
{    
    server.Start();
    Console.WriteLine($"Server started with IP Address : {IP_ADDRESS} and port: {port}");
    while (true)
    {
        using var socket = await server.AcceptSocketAsync();
        var pingResponse = Encoding.ASCII.GetBytes("+PONG\r\n");
        await socket.SendAsync(pingResponse, SocketFlags.None);
    }
}
finally
{
    server.Stop();
}