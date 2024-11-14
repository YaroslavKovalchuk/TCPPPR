using System.ComponentModel.DataAnnotations;

namespace TCPPR.Data.Entities
{
    /// <summary>
    /// Модель користувача, що зберігає інформацію про облікові дані, роль, рівень доступу та цех.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Ім'я користувача (унікальне).
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Хеш пароля користувача.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Роль користувача (наприклад, директор, бригадир).
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        /// <summary>
        /// Рівень доступу користувача.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string AccessLevel { get; set; }

        /// <summary>
        /// Цех, до якого належить користувач (наприклад, 6 або 8).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Workshop { get; set; }  // Нова властивість для зберігання цеху
    }
}