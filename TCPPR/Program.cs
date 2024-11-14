using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using TCPPR.Data.DatabaseContext;
using TCPPR.Server;
using TCPPR.Services.Interfaces;
using TCPPR.Services.Services;
using TCPPR.Handlers;

namespace TCPPR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Log.Information("Запуск програми...");

            try
            {
                var host = CreateHostBuilder(args).Build();
                var serviceProvider = host.Services;

                // Отримуємо конфігурацію для адреси і порту
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var ipAddress = config["Server:IPAddress"];
                var port = int.Parse(config["Server:Port"]);

                // Створюємо TCP-сервер і запускаємо його вручну
                var logger = serviceProvider.GetRequiredService<ILogger<TcpServer>>();
                var tcpServer = new TcpServer(serviceProvider, logger, ipAddress, port);

                // Запуск TCP-сервера
                tcpServer.StartAsync().GetAwaiter().GetResult();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddSerilog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                    services.AddScoped<RequestHandler>();
                    services.AddScoped<UserRequestHandler>();
                    services.AddScoped<EquipmentRequestHandler>();
                    services.AddScoped<InventoryRequestHandler>();
                    services.AddScoped<MaintenanceRequestHandler>();

                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<IEquipmentService, EquipmentService>();
                    services.AddScoped<IInventoryService, InventoryService>();
                    services.AddScoped<IMaintenanceService, MaintenanceService>();

                    // Реєстрація ConnectionHandler для кожного клієнта
                    services.AddScoped<ConnectionHandler>();
                });
    }
}
