using System;
using System.IO;

namespace TCPPR.Helpers
{
    /// <summary>
    /// Допоміжний клас для логування подій і помилок.
    /// </summary>
    public static class Logger
    {
        private static readonly string LogFilePath = "log.txt";

        /// <summary>
        /// Логує повідомлення про інформацію.
        /// </summary>
        /// <param name="message">Повідомлення для логування.</param>
        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        /// <summary>
        /// Логує повідомлення про помилку.
        /// </summary>
        /// <param name="message">Повідомлення про помилку для логування.</param>
        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        /// <summary>
        /// Логує повідомлення з вказаним рівнем важливості.
        /// </summary>
        /// <param name="level">Рівень важливості (INFO, ERROR і т.д.).</param>
        /// <param name="message">Повідомлення для логування.</param>
        private static void Log(string level, string message)
        {
            try
            {
                var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при логуванні: {ex.Message}");
            }
        }
    }
}