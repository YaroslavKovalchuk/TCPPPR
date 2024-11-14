using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TCPPR.Handlers;

namespace TCPPR.Server
{
    public class TcpServer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TcpListener _listener;
        private readonly ILogger<TcpServer> _logger;
        private readonly string _ipAddress;
        private readonly int _port;

        public TcpServer(IServiceProvider serviceProvider, ILogger<TcpServer> logger, string ipAddress, int port)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _ipAddress = ipAddress;
            _port = port;
            _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
        }

        public async Task StartAsync()
        {
            _listener.Start();
            _logger.LogInformation($"TCP-сервер запущено на адресі {_ipAddress}:{_port}");
            Console.WriteLine($"TCP-сервер запущено на адресі {_ipAddress}:{_port}");

            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                var clientEndpoint = (IPEndPoint)client.Client.RemoteEndPoint;
                
                _logger.LogInformation($"Новий клієнт підключився: {clientEndpoint?.Address}:{clientEndpoint?.Port}");
                Console.WriteLine($"Новий клієнт підключився: {clientEndpoint?.Address}:{clientEndpoint?.Port}");
                
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using var scope = _serviceProvider.CreateScope();
            var connectionHandler = scope.ServiceProvider.GetRequiredService<ConnectionHandler>();
            await connectionHandler.HandleClientAsync(client);
        }

        public void Stop()
        {
            _listener.Stop();
            _logger.LogInformation("TCP-сервер зупинено");
            Console.WriteLine("TCP-сервер зупинено");
        }
    }
}
