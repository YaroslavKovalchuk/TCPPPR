using QRCoder;
using System.Drawing;

namespace TCPPR.Helpers
{
    /// <summary>
    /// Допоміжний клас для генерації QR-кодів.
    /// </summary>
    public static class QRCodeHelper // Модифікатор "public" для класу
    {/*
        // Модифікатор "public" для методу в класі
        public static Bitmap GenerateQRCode(string text)
        {
            try
            {
                // Генератор QR-коду
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    // Створення даних QR-коду
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

                    // Створення QR-коду на основі отриманих даних
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        return qrCodeImage;
                    }
                }
            }
            catch (Exception ex)
            {
                // Виведення повідомлення про помилку для діагностики
                Console.WriteLine($"Помилка при генерації QR-коду: {ex.Message}");
                throw;
            }
        }*/
    }
}