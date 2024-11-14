using System;
using System.Text;
using System.Threading.Tasks;
using TCPPR.Services.Interfaces;
using TCPPR.Data.Entities;

namespace TCPPR.Handlers
{
    /// <summary>
    /// Обробник запитів, пов'язаних з інвентарем, таких як додавання, отримання інформації, 
    /// списання та оновлення кількості запасних частин.
    /// </summary>
    public class InventoryRequestHandler
    {
        private readonly IInventoryService _inventoryService; // Сервіс для управління інвентарем

        /// <summary>
        /// Конструктор для ініціалізації InventoryRequestHandler з використанням ін'єкції залежностей.
        /// </summary>
        /// <param name="inventoryService">Сервіс для роботи з інвентарем</param>
        public InventoryRequestHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Основний метод для обробки запитів інвентарю.
        /// </summary>
        /// <param name="request">Текстовий запит від клієнта</param>
        /// <returns>Відповідь на запит у вигляді рядка</returns>
        public async Task<string> HandleInventoryRequestAsync(string request)
        {
            // Розділяємо запит на слова для визначення команди та параметрів
            var parts = request.Split(' ');
            var command = parts[0].ToUpper(); // Приводимо команду до верхнього регістру для уніфікації

            try
            {
                switch (command)
                {
                    // Додавання нової запасної частини до інвентарю
                    case "ADD_SPARE_PART":
                        if (parts.Length < 5) return "Помилка: недостатньо аргументів для додавання запасної частини";

                        var name = parts[1];
                        var partNumber = parts[2];
                        var manufacturer = parts[3];
                        if (!int.TryParse(parts[4], out int quantity)) // Перевіряємо кількість
                            return "Помилка: невірний формат кількості запасної частини";

                        var barcode = parts.Length > 5 ? parts[5] : string.Empty; // Якщо є, зберігаємо штрих-код

                        // Створення об'єкта SparePart для додавання до інвентарю
                        var sparePart = new SparePart
                        {
                            Name = name,
                            PartNumber = partNumber,
                            Manufacturer = manufacturer,
                            Quantity = quantity,
                            Barcode = barcode,
                            StorageLocation = "Не визначено" // Початкове місце зберігання
                        };

                        // Викликаємо сервіс для додавання запасної частини
                        return await _inventoryService.AddSparePartAsync(sparePart) 
                            ? "Spare Part Added Successfully" 
                            : "Failed to Add Spare Part";

                    // Отримання інформації про конкретну запасну частину за її ID
                    case "GET_SPARE_PART":
                        if (parts.Length < 2) return "Помилка: недостатньо аргументів для отримання інформації про запасну частину";
                        if (!int.TryParse(parts[1], out int sparePartId)) // Перевірка ID запасної частини
                            return "Помилка: невірний формат ID запасної частини";

                        var sparePartInfo = await _inventoryService.GetSparePartByIdAsync(sparePartId);
                        return sparePartInfo != null 
                            ? $"Запасна частина: {sparePartInfo.Name}, Виробник: {sparePartInfo.Manufacturer}, Кількість: {sparePartInfo.Quantity}" 
                            : "Spare Part Not Found";

                    // Списання певної кількості запасної частини з інвентарю
                    case "DEDUCT_SPARE_PART":
                        if (parts.Length < 3) return "Помилка: недостатньо аргументів для списання запасної частини";
                        if (!int.TryParse(parts[1], out sparePartId)) // Перевірка ID запасної частини
                            return "Помилка: невірний формат ID запасної частини";
                        if (!int.TryParse(parts[2], out quantity)) // Перевірка кількості для списання
                            return "Помилка: невірний формат кількості для списання";

                        // Викликаємо сервіс для списання запасної частини
                        return await _inventoryService.DeductSparePartAsync(sparePartId, quantity) 
                            ? "Spare Part Deducted Successfully" 
                            : "Failed to Deduct Spare Part";

                    // Отримання списку всіх доступних запасних частин
                    case "GET_ALL_SPARE_PARTS":
                        var spareParts = await _inventoryService.GetAllSparePartsAsync();
                        if (spareParts == null || !spareParts.Any()) // Перевірка наявності запчастин
                            return "No Spare Parts Found";

                        // Формування списку запасних частин для відповіді
                        var result = new StringBuilder("Spare Parts List:\n");
                        foreach (var part in spareParts)
                        {
                            result.AppendLine($"- Id: {part.Id}, Назва: {part.Name}, Кількість: {part.Quantity}, Виробник: {part.Manufacturer}");
                        }
                        return result.ToString();

                    // Оновлення кількості запасної частини за її ID
                    case "UPDATE_SPARE_PART_QUANTITY":
                        if (parts.Length < 3) return "Помилка: недостатньо аргументів для оновлення кількості запасної частини";
                        if (!int.TryParse(parts[1], out sparePartId)) // Перевірка ID запасної частини
                            return "Помилка: невірний формат ID запасної частини";
                        if (!int.TryParse(parts[2], out quantity)) // Перевірка нової кількості
                            return "Помилка: невірний формат кількості";

                        // Викликаємо сервіс для оновлення кількості запасної частини
                        return await _inventoryService.UpdateSparePartQuantityAsync(sparePartId, quantity)
                            ? "Spare Part Quantity Updated Successfully"
                            : "Failed to Update Spare Part Quantity";

                    // Обробка невідомих команд
                    default:
                        return "Unknown Command in Inventory Handler";
                }
            }
            catch (Exception ex)
            {
                // Обробка виключень і повернення повідомлення про помилку
                return $"Error: {ex.Message}";
            }
        }
    }
}
