using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TCPPR.Handlers;

namespace TCPPR.Server
{
    public class ConnectionHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ConnectionHandler> _logger;

        public ConnectionHandler(IServiceProvider serviceProvider, ILogger<ConnectionHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task HandleClientAsync(TcpClient client)
        {
            using var scope = _serviceProvider.CreateScope();
            var requestHandler = scope.ServiceProvider.GetRequiredService<RequestHandler>();

            var buffer = new byte[256];
            var stream = client.GetStream();

            try
            {
                while (client.Connected)
                {
                    _logger.LogInformation("Очікування повідомлення від клієнта...");
                    Console.WriteLine("Очікування повідомлення від клієнта...");

                    // Читання даних від клієнта
                    var byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (byteCount <= 0)
                    {
                        _logger.LogInformation("Клієнт завершив підключення.");
                        Console.WriteLine("Клієнт завершив підключення.");
                        break;
                    }

                    // Логування отриманих даних
                    var request = Encoding.UTF8.GetString(buffer, 0, byteCount).Trim();
                    _logger.LogInformation($"Отримано від клієнта: {request}");
                    Console.WriteLine($"Отримано від клієнта: {request}");

                    // Обробка запиту
                    string response;
                    if (request.Equals("PING", StringComparison.OrdinalIgnoreCase))
                    {
                        response = "PONG";
                        _logger.LogInformation("Відповідь на 'PING' підготовлена: 'PONG'");
                    }
                    else
                    {
                        try
                        {
                            _logger.LogInformation($"Передача запиту до RequestHandler: {request}");
                            response = await requestHandler.HandleRequestAsync(request);
                            _logger.LogInformation($"Відповідь від RequestHandler: {response}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Помилка в RequestHandler: {ex.Message}");
                            response = "Error processing request";
                        }
                    }

                    // Відправка відповіді клієнту
                    if (client.Connected)
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        _logger.LogInformation($"Відправлення відповіді клієнту: {response}");
                        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await stream.FlushAsync();
                        _logger.LogInformation("Відповідь успішно надіслана клієнту.");
                    }
                }
            }
            catch (IOException ex)
            {
                _logger.LogError($"Помилка при обробці клієнта (IOException): {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Неочікувана помилка: {ex.Message}");
            }
            finally
            {
                client.Close();
                _logger.LogInformation("Клієнт відключився");
                Console.WriteLine("Клієнт відключився");
            }
        }
    }
}
