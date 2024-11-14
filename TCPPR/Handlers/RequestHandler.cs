using System;
using System.Threading.Tasks;

namespace TCPPR.Handlers
{
    /// <summary>
    /// Основний обробник запитів, що маршрутизує запити до відповідних обробників 
    /// на основі команди. Керує запитами, пов'язаними з користувачами, обладнанням, інвентарем 
    /// і технічним обслуговуванням.
    /// </summary>
    public class RequestHandler
    {
        // Поля для зберігання обробників, які обробляють специфічні запити
        private readonly UserRequestHandler _userHandler;
        private readonly EquipmentRequestHandler _equipmentHandler;
        private readonly InventoryRequestHandler _inventoryHandler;
        private readonly MaintenanceRequestHandler _maintenanceHandler;

        /// <summary>
        /// Конструктор для ініціалізації всіх залежностей, переданих через DI
        /// </summary>
        /// <param name="userHandler">Обробник для запитів, пов'язаних з користувачами</param>
        /// <param name="equipmentHandler">Обробник для запитів, пов'язаних з обладнанням</param>
        /// <param name="inventoryHandler">Обробник для запитів, пов'язаних з інвентарем</param>
        /// <param name="maintenanceHandler">Обробник для запитів, пов'язаних з технічним обслуговуванням</param>
        public RequestHandler(
            UserRequestHandler userHandler,
            EquipmentRequestHandler equipmentHandler,
            InventoryRequestHandler inventoryHandler,
            MaintenanceRequestHandler maintenanceHandler)
        {
            _userHandler = userHandler;
            _equipmentHandler = equipmentHandler;
            _inventoryHandler = inventoryHandler;
            _maintenanceHandler = maintenanceHandler;
        }

        /// <summary>
        /// Метод для обробки вхідного текстового запиту, визначає команду та викликає 
        /// відповідний обробник для її виконання.
        /// </summary>
        /// <param name="request">Текстовий запит від клієнта</param>
        /// <returns>Результат виконання запиту у вигляді рядка</returns>
        public async Task<string> HandleRequestAsync(string request)
        {
            // Розділення запиту на слова, щоб виділити команду
            var parts = request.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                return "Invalid Command: No command found"; // Перевірка на порожній запит
            }

            // Виділення команди (перше слово) і переведення в верхній регістр для уніфікації
            var command = parts[0].Trim().ToUpper();
            Console.WriteLine($"Отримано команду: {command}");  // Логування отриманої команди

            try
            {
                // Вибір дії на основі команди, яка визначає обробник для виклику
                switch (command)
                {
                    // Команди для обробки користувачів (реєстрація та вхід)
                    case "REGISTER":
                    case "LOGIN":
                        Console.WriteLine("Маршрутизація запиту до UserRequestHandler");
                        return await _userHandler.HandleUserRequestAsync(request);

                    // Команди для обробки обладнання (додавання, отримання, оновлення, видалення)
                    case "ADD_EQUIPMENT":
                    case "GET_EQUIPMENT":
                    case "UPDATE_EQUIPMENT":
                    case "GET_ALL_EQUIPMENT":
                    case "DELETE_EQUIPMENT":
                        Console.WriteLine("Маршрутизація запиту до EquipmentRequestHandler");
                        return await _equipmentHandler.HandleEquipmentRequestAsync(request);

                    // Команди для обробки інвентарю (додавання, отримання, оновлення, списання)
                    case "ADD_SPARE_PART":
                    case "GET_SPARE_PART":
                    case "DEDUCT_SPARE_PART":
                    case "GET_ALL_SPARE_PARTS":            // Нова команда для отримання всіх запчастин
                    case "UPDATE_SPARE_PART_QUANTITY":     // Нова команда для оновлення кількості запасів
                        Console.WriteLine("Маршрутизація запиту до InventoryRequestHandler");
                        return await _inventoryHandler.HandleInventoryRequestAsync(request);

                    // Команди для обробки заявок на технічне обслуговування (додавання, отримання, оновлення)
                    case "ADD_MAINTENANCE_REQUEST":
                    case "GET_MAINTENANCE_REQUEST":
                    case "UPDATE_MAINTENANCE_STATUS":
                        Console.WriteLine("Маршрутизація запиту до MaintenanceRequestHandler");
                        return await _maintenanceHandler.HandleMaintenanceRequestAsync(request);

                    default:
                        // Якщо команда не розпізнана
                        Console.WriteLine($"Невідома команда: {command}");
                        return "Unknown Command";
                }
            }
            catch (Exception ex)
            {
                // Логування та повернення помилки у разі винятку
                Console.WriteLine($"Помилка під час обробки запиту: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }
    }
}
