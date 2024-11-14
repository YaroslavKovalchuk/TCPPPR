using System;
using System.Threading.Tasks;
using TCPPR.Services.Interfaces;
using TCPPR.Data.Entities;

namespace TCPPR.Handlers
{
    /// <summary>
    /// Обробник запитів, пов'язаних із заявками на ремонт, таких як створення, отримання інформації та оновлення статусу заявки.
    /// </summary>
    public class MaintenanceRequestHandler
    {
        private readonly IMaintenanceService _maintenanceService;

        /// <summary>
        /// Конструктор для ін'єкції сервісу роботи із заявками на ремонт.
        /// </summary>
        /// <param name="maintenanceService">Сервіс для роботи із заявками на ремонт</param>
        public MaintenanceRequestHandler(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        /// <summary>
        /// Метод для обробки запитів, пов'язаних із заявками на ремонт.
        /// </summary>
        /// <param name="request">Текстовий запит від клієнта</param>
        /// <returns>Відповідь на запит</returns>
        public async Task<string> HandleMaintenanceRequestAsync(string request)
        {
            // Розбиваємо запит на частини для визначення команди та аргументів
            var parts = request.Split(' ');
            var command = parts[0].ToUpper();

            try
            {
                switch (command)
                {
                    // Додавання нової заявки на ремонт
                    case "ADD_MAINTENANCE_REQUEST":
                        if (parts.Length < 3) return "Помилка: недостатньо аргументів для створення заявки";
                        if (!int.TryParse(parts[1], out int equipmentId))
                            return "Помилка: невірний формат ID обладнання";

                        var description = string.Join(' ', parts, 2, parts.Length - 2);

                        var maintenanceRequest = new MaintenanceRequest
                        {
                            EquipmentId = equipmentId,
                            Description = description,
                            Status = "Новий",
                            DateCreated = DateTime.Now,
                            CreatedBy = "Бригадир"
                        };

                        // Викликаємо сервіс для додавання нової заявки на ремонт
                        return await _maintenanceService.AddMaintenanceRequestAsync(maintenanceRequest) 
                            ? "Maintenance Request Added Successfully" 
                            : "Failed to Add Maintenance Request";

                    // Отримання інформації про заявку на ремонт за ID
                    case "GET_MAINTENANCE_REQUEST":
                        if (parts.Length < 2) return "Помилка: недостатньо аргументів для отримання заявки";
                        if (!int.TryParse(parts[1], out int requestId))
                            return "Помилка: невірний формат ID заявки";

                        var maintenanceInfo = await _maintenanceService.GetMaintenanceRequestByIdAsync(requestId);
                        return maintenanceInfo != null 
                            ? $"Заявка ID: {maintenanceInfo.Id}, Опис: {maintenanceInfo.Description}, Статус: {maintenanceInfo.Status}" 
                            : "Maintenance Request Not Found";

                    // Оновлення статусу заявки на ремонт
                    case "UPDATE_MAINTENANCE_STATUS":
                        if (parts.Length < 3) return "Помилка: недостатньо аргументів для оновлення статусу заявки";
                        if (!int.TryParse(parts[1], out requestId))
                            return "Помилка: невірний формат ID заявки";

                        var newStatus = parts[2];

                        maintenanceInfo = await _maintenanceService.GetMaintenanceRequestByIdAsync(requestId);
                        if (maintenanceInfo == null) return "Maintenance Request Not Found";

                        maintenanceInfo.Status = newStatus;

                        // Викликаємо сервіс для оновлення статусу заявки
                        return await _maintenanceService.UpdateMaintenanceRequestAsync(maintenanceInfo) 
                            ? "Maintenance Request Status Updated Successfully" 
                            : "Failed to Update Maintenance Request Status";

                    // Невідома команда для обробника заявок на ремонт
                    default:
                        return "Unknown Command in Maintenance Handler";
                }
            }
            catch (Exception ex)
            {
                // Повертаємо повідомлення про помилку у разі винятку
                return $"Error: {ex.Message}";
            }
        }
    }
}
