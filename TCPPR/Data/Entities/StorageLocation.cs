using System.ComponentModel.DataAnnotations;

namespace TCPPR.Data.Entities
{
    /// <summary>
    /// Модель місця зберігання, що представляє структуру таблиці StorageLocations.
    /// </summary>
    public class StorageLocation
    {
        [Key]
        public int Id { get; set; } // Унікальний ідентифікатор місця зберігання

        [Required]
        [MaxLength(100)]
        public string LocationName { get; set; } // Назва або номер місця зберігання (наприклад, "Полиця 1")

        [Required]
        public string QRCode { get; set; } // QR-код для ідентифікації місця зберігання

        [MaxLength(500)]
        public string Description { get; set; } // Опис місця зберігання або його вмісту
    }
}