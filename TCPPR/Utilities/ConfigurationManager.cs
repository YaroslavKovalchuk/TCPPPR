using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TCPPR.Helpers
{
    /// <summary>
    /// Допоміжний клас для управління конфігурацією програми.
    /// </summary>
    public static class ConfigurationManager
    {
        private static IConfigurationRoot _configuration;

        /// <summary>
        /// Ініціалізує конфігурацію з файлу appsettings.json.
        /// </summary>
        static ConfigurationManager()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                _configuration = builder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні конфігурації: {ex.Message}");
            }
        }

        /// <summary>
        /// Отримує значення конфігурації за ключем.
        /// </summary>
        /// <param name="key">Ключ конфігурації.</param>
        /// <returns>Значення конфігурації.</returns>
        public static string GetConfigurationValue(string key)
        {
            return _configuration[key];
        }

        /// <summary>
        /// Отримує IP-адресу сервера з конфігурації.
        /// </summary>
        /// <returns>IP-адреса сервера.</returns>
        public static string GetServerIPAddress()
        {
            return _configuration["Server:IPAddress"];
        }

        /// <summary>
        /// Отримує порт сервера з конфігурації.
        /// </summary>
        /// <returns>Порт сервера.</returns>
        public static int GetServerPort()
        {
            int.TryParse(_configuration["Server:Port"], out int port);
            return port;
        }
    }
}