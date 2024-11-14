using System.Threading.Tasks;

namespace TCPPR.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс для управління користувачами: реєстрація, авторизація, перевірка доступу.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Реєструє нового користувача у системі з хешуванням пароля та визначає цех.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <param name="password">Пароль користувача.</param>
        /// <param name="role">Роль користувача (наприклад, керівник, механік).</param>
        /// <param name="accessLevel">Рівень доступу користувача (наприклад, доступ до керування технічними даними).</param>
        /// <param name="workshop">Цех, до якого належить користувач (наприклад, 6 або 8).</param>
        /// <returns>True, якщо реєстрація пройшла успішно; інакше - False.</returns>
        Task<bool> RegisterUserAsync(string username, string password, string role, string accessLevel, string workshop);

        /// <summary>
        /// Аутентифікує користувача за допомогою перевірки хешованого пароля.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <param name="password">Пароль користувача.</param>
        /// <returns>Об'єкт AuthenticationResult з інформацією про результат аутентифікації.</returns>
        Task<AuthenticationResult> AuthenticateUserAsync(string username, string password);

        /// <summary>
        /// Отримує роль користувача за його ім'ям.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <returns>Роль користувача як рядок або null, якщо користувач не знайдений.</returns>
        Task<string> GetUserRoleAsync(string username);

        /// <summary>
        /// Отримує рівень доступу користувача за його ім'ям.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <returns>Рівень доступу користувача як рядок або null, якщо користувач не знайдений.</returns>
        Task<string> GetUserAccessLevelAsync(string username);

        /// <summary>
        /// Отримує інформацію про цех користувача за його ім'ям.
        /// </summary>
        /// <param name="username">Ім'я користувача.</param>
        /// <returns>Номер цеху (6 чи 8) або null, якщо користувач не знайдений.</returns>
        Task<string> GetUserWorkshopAsync(string username);
    }

    /// <summary>
    /// Результат аутентифікації, що містить інформацію про статус і дані користувача.
    /// </summary>
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; } // Чи пройшла аутентифікація успішно
        public string Role { get; set; }          // Роль користувача
        public string Workshop { get; set; }      // Цех, до якого належить користувач
    }
}
