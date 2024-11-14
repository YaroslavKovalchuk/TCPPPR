using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPPR.Services.Interfaces;
using TCPPR.Data.Entities;

namespace TCPPR.Handlers
{
    /// <summary>
    /// Обробник запитів, пов'язаних з обладнанням, таких як додавання, отримання інформації, оновлення та видалення.
    /// </summary>
    public class EquipmentRequestHandler
    {
        private readonly IEquipmentService _equipmentService;

        /// <summary>
        /// Конструктор для ін'єкції сервісу роботи з обладнанням.
        /// </summary>
        /// <param name="equipmentService">Сервіс для роботи з обладнанням</param>
        public EquipmentRequestHandler(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        /// <summary>
        /// Метод для обробки запитів, пов'язаних з обладнанням.
        /// </summary>
        /// <param name="request">Текстовий запит від клієнта</param>
        /// <returns>Відповідь на запит</returns>
        public async Task<string> HandleEquipmentRequestAsync(string request)
        {
            var parts = request.Split(' ');
            var command = parts[0].Trim().ToUpper();

            try
            {
                switch (command)
                {
                    // Додавання нового обладнання
                    case "ADD_EQUIPMENT":
                        if (parts.Length < 4) return "Помилка: недостатньо аргументів для додавання обладнання";
                        var name = parts[1];
                        var inventoryNumber = parts[2];
                        var qrCode = parts[3];

                        var equipment = new Equipment
                        {
                            Name = name,
                            InventoryNumber = inventoryNumber,
                            QRCode = qrCode,
                            DateAdded = DateTime.Now
                        };

                        return await _equipmentService.AddEquipmentAsync(equipment)
                            ? "Equipment Added Successfully"
                            : "Failed to Add Equipment";

                    // Отримання інформації про обладнання за ID
                    case "GET_EQUIPMENT":
                        if (parts.Length < 2) return "Помилка: недостатньо аргументів для отримання інформації про обладнання";
                        if (!int.TryParse(parts[1], out int equipmentId))
                            return "Помилка: невірний формат ID обладнання";

                        var equipmentInfo = await _equipmentService.GetEquipmentByIdAsync(equipmentId);
                        return equipmentInfo != null
                            ? $"Обладнання: {equipmentInfo.Name}, Інвентарний номер: {equipmentInfo.InventoryNumber}, QR-код: {equipmentInfo.QRCode}"
                            : "Equipment Not Found";

                    // Оновлення інформації про обладнання
                    case "UPDATE_EQUIPMENT":
                        if (parts.Length < 5) return "Помилка: недостатньо аргументів для оновлення обладнання";
                        if (!int.TryParse(parts[1], out equipmentId))
                            return "Помилка: невірний формат ID обладнання";

                        name = parts[2];
                        inventoryNumber = parts[3];
                        qrCode = parts[4];

                        equipmentInfo = await _equipmentService.GetEquipmentByIdAsync(equipmentId);
                        if (equipmentInfo == null) return "Equipment Not Found";

                        equipmentInfo.Name = name;
                        equipmentInfo.InventoryNumber = inventoryNumber;
                        equipmentInfo.QRCode = qrCode;

                        return await _equipmentService.UpdateEquipmentAsync(equipmentInfo)
                            ? "Equipment Updated Successfully"
                            : "Failed to Update Equipment";

                    // Отримання всіх одиниць обладнання
                    case "GET_ALL_EQUIPMENT":
                        var equipmentList = await _equipmentService.GetAllEquipmentAsync();
                        if (equipmentList == null || !equipmentList.Any()) return "No Equipment Found";

                        var result = new StringBuilder("Equipment List:\n");
                        foreach (var item in equipmentList)
                        {
                            result.AppendLine($"- Id: {item.Id}, Назва: {item.Name}, Інвентарний номер: {item.InventoryNumber}, QR-код: {item.QRCode}");
                        }
                        return result.ToString();

                    // Видалення обладнання за ID
                    case "DELETE_EQUIPMENT":
                        if (parts.Length < 2) return "Помилка: недостатньо аргументів для видалення обладнання";
                        if (!int.TryParse(parts[1], out equipmentId))
                            return "Помилка: невірний формат ID обладнання";

                        var isDeleted = await _equipmentService.DeleteEquipmentAsync(equipmentId);
                        return isDeleted ? "Equipment Deleted Successfully" : "Failed to Delete Equipment";

                    // Невідома команда
                    default:
                        return "Unknown Command in Equipment Handler";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
