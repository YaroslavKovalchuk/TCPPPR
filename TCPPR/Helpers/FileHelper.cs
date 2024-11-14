using System;
using System.IO;

namespace TCPPR.Helpers
{
    /// <summary>
    /// Допоміжний клас для роботи з файлами: збереження, завантаження, видалення.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Зберігає файл за вказаним шляхом.
        /// </summary>
        /// <param name="filePath">Шлях до файлу.</param>
        /// <param name="content">Вміст файлу у вигляді масиву байтів.</param>
        public static void SaveFile(string filePath, byte[] content)
        {
            try
            {
                File.WriteAllBytes(filePath, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при збереженні файлу: {ex.Message}");
            }
        }

        /// <summary>
        /// Завантажує файл з вказаного шляху.
        /// </summary>
        /// <param name="filePath">Шлях до файлу.</param>
        /// <returns>Вміст файлу у вигляді масиву байтів.</returns>
        public static byte[] LoadFile(string filePath)
        {
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні файлу: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Видаляє файл за вказаним шляхом.
        /// </summary>
        /// <param name="filePath">Шлях до файлу.</param>
        public static void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при видаленні файлу: {ex.Message}");
            }
        }
    }
}