using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCPPR.Data.Entities
{
    /// <summary>
    /// Модель обладнання, що представляє структуру таблиці Equipments.
    /// </summary>
    public class Equipment
    {
        [Key]
        public int Id { get; set; } // Унікальний ідентифікатор обладнання

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // Назва обладнання

        [Required]
        public string InventoryNumber { get; set; } // Інвентарний номер обладнання (для всієї лінії)

        [Required]
        public string QRCode { get; set; } // Унікальний QR-код для ідентифікації модуля

        [Required(ErrorMessage = "Description cannot be null")] // Додаємо [Required] для запобігання помилкам
        public string Description { get; set; } = ""; // Ініціалізуємо як порожній рядок

        [Required]
        public DateTime DateAdded { get; set; } // Дата додавання обладнання

        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } // Список заявок на ремонт, пов'язаних із цим обладнанням
    }
}