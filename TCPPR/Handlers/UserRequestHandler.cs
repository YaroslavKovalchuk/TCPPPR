using System;
using System.Threading.Tasks;
using TCPPR.Services.Interfaces;

namespace TCPPR.Handlers
{
    /// <summary>
    /// Обробник запитів, пов'язаних з користувачами, таких як реєстрація та авторизація.
    /// </summary>
    public class UserRequestHandler
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Конструктор, який приймає сервіс для роботи з користувачами через ін'єкцію залежностей.
        /// </summary>
        /// <param name="userService">Сервіс для роботи з користувачами</param>
        public UserRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Метод для обробки запитів, пов'язаних з користувачами.
        /// </summary>
        /// <param name="request">Текстовий запит від клієнта</param>
        /// <returns>Відповідь на запит</returns>
        public async Task<string> HandleUserRequestAsync(string request)
        {
            // Розбиваємо запит на частини для отримання команди та аргументів
            var parts = request.Split(' ');
            var command = parts[0].ToUpper(); // Команда, наприклад, "REGISTER" або "LOGIN"

            try
            {
                switch (command)
                {
                    // Реєстрація користувача
                    case "REGISTER":
                        // Перевірка, що надано достатню кількість параметрів для реєстрації
                        if (parts.Length < 6) return "Помилка: недостатньо аргументів для реєстрації";
                        var username = parts[1];
                        var password = parts[2];
                        var role = parts[3];
                        var accessLevel = parts[4];
                        var workshop = parts[5]; // Додано аргумент workshop
                
                        // Викликаємо сервіс для реєстрації користувача з параметрами
                        return await _userService.RegisterUserAsync(username, password, role, accessLevel, workshop) 
                            ? "User Registered Successfully" 
                            : "User Registration Failed";

                    // Авторизація користувача
                    case "LOGIN":
                        // Перевірка, що надано достатню кількість параметрів для авторизації
                        if (parts.Length < 3) return "Помилка: недостатньо аргументів для авторизації";
                        username = parts[1];
                        password = parts[2];
                
                        // Викликаємо сервіс для аутентифікації
                        var authResult = await _userService.AuthenticateUserAsync(username, password);
                        if (authResult.IsAuthenticated)
                        {
                            return $"Login Successful. Role: {authResult.Role}, Workshop: {authResult.Workshop}";
                        }
                        else
                        {
                            return "Login Failed";
                        }

                    // Невідома команда
                    default:
                        return "Unknown Command in User Handler";
                }
            }
            catch (Exception ex)
            {
                // Повертаємо повідомлення про помилку у разі виникнення винятку
                return $"Error: {ex.Message}";
            }
        }
    }
}
