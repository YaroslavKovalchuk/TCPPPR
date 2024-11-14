using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TCPPR.Data.DatabaseContext;
using TCPPR.Data.Entities;
using TCPPR.Helpers;
using TCPPR.Services.Interfaces;

namespace TCPPR.Services.Services
{
    /// <summary>
    /// Сервіс для управління користувачами, що включає реєстрацію, аутентифікацію
    /// та отримання інформації про доступ та цех.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Конструктор, що приймає контекст бази даних для взаємодії з таблицею користувачів.
        /// </summary>
        /// <param name="dbContext">Контекст бази даних</param>
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Реєструє нового користувача в базі даних з використанням хешування пароля.
        /// </summary>
        public async Task<bool> RegisterUserAsync(string username, string password, string role, string accessLevel, string workshop)
        {
            try
            {
                if (await _dbContext.Users.AnyAsync(u => u.Username == username.Trim()))
                {
                    Console.WriteLine($"Користувач з ім'ям '{username}' вже існує.");
                    return false;
                }

                var hashedPassword = PasswordHelper.HashPassword(password.Trim());
                Console.WriteLine("Пароль успішно захешовано.");

                var user = new User
                {
                    Username = username.Trim(),
                    PasswordHash = hashedPassword,
                    Role = role.Trim(),
                    AccessLevel = accessLevel.Trim(),
                    Workshop = workshop.Trim()
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine($"Користувач '{username}' успішно зареєстрований.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка реєстрації користувача '{username}': {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Аутентифікує користувача шляхом перевірки хешованого пароля.
        /// </summary>
        public async Task<AuthenticationResult> AuthenticateUserAsync(string username, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username.Trim());
            if (user == null)
            {
                Console.WriteLine($"Користувач '{username}' не знайдений.");
                return new AuthenticationResult { IsAuthenticated = false };
            }

            bool passwordMatch = PasswordHelper.VerifyPassword(password.Trim(), user.PasswordHash.Trim());
            Console.WriteLine(passwordMatch ? "Пароль вірний." : "Пароль невірний.");

            return new AuthenticationResult
            {
                IsAuthenticated = passwordMatch,
                Role = passwordMatch ? user.Role : null,
                Workshop = passwordMatch ? user.Workshop : null
            };
        }

        /// <summary>
        /// Отримує роль користувача за його ім'ям.
        /// </summary>
        public async Task<string> GetUserRoleAsync(string username)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username.Trim());
            Console.WriteLine(user != null ? $"Роль користувача: {user.Role}" : "Користувача не знайдено.");
            return user?.Role;
        }

        /// <summary>
        /// Отримує рівень доступу користувача за його ім'ям.
        /// </summary>
        public async Task<string> GetUserAccessLevelAsync(string username)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username.Trim());
            Console.WriteLine(user != null ? $"Рівень доступу користувача: {user.AccessLevel}" : "Користувача не знайдено.");
            return user?.AccessLevel;
        }

        /// <summary>
        /// Отримує цех користувача за його ім'ям.
        /// </summary>
        public async Task<string> GetUserWorkshopAsync(string username)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username.Trim());
            Console.WriteLine(user != null ? $"Цех користувача: {user.Workshop}" : "Користувача не знайдено.");
            return user?.Workshop;
        }
    }
}
