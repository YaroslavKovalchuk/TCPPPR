using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCPPR.Data.Entities
{
    /// <summary>
    /// Модель заявки на ремонт, що представляє структуру таблиці MaintenanceRequests.
    /// </summary>
    public class MaintenanceRequest
    {
        [Key]
        public int Id { get; set; } // Унікальний ідентифікатор заявки на ремонт

        [Required]
        public int EquipmentId { get; set; } // Ідентифікатор обладнання, для якого створена заявка

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; } // Обладнання, пов'язане із заявкою

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } // Опис проблеми

        [Required]
        public string Status { get; set; } // Статус заявки (наприклад, "Критичний", "Помірний", "Виконано")

        public DateTime? DateResolved { get; set; } // Дата завершення ремонту (якщо є)

        [Required]
        public DateTime DateCreated { get; set; } // Дата створення заявки

        public string PhotoPath { get; set; } // Шлях до фото, прикріпленого до заявки

        [Required]
        public string CreatedBy { get; set; } // Ким створена заявка (користувач)
    }
}