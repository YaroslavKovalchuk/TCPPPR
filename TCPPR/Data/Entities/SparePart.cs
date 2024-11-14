using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCPPR.Data.Entities
{
    /// <summary>
    /// Модель запасної частини, що представляє структуру таблиці SpareParts.
    /// </summary>
    public class SparePart
    {
        [Key]
        public int Id { get; set; } // Унікальний ідентифікатор запасної частини

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // Назва запасної частини

        [Required]
        [MaxLength(50)]
        public string PartNumber { get; set; } // Номер запасної частини

        [Required]
        [MaxLength(100)]
        public string Manufacturer { get; set; } // Виробник запасної частини

        [Required]
        public string Barcode { get; set; } // Штрих-код для ідентифікації запасної частини

        public int Quantity { get; set; } // Кількість запасних частин у наявності

        [Required]
        public string StorageLocation { get; set; } // Місце зберігання запасної частини (наприклад, номер полиці)
    }
}