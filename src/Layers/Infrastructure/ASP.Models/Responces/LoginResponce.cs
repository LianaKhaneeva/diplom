using Contracts.Enums;

namespace ASP.Models.Responces
{
    /// <summary>
    /// Ответ на запрос авторизации.
    /// </summary>
    public sealed class LoginResponce
    {
        /// <summary>
        /// Полное ФИО пользователя.
        /// </summary>
        public string FullName { get; set; } = null!;


        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Телефон.
        /// </summary>
        public string Phone { get; set; } = null!;


        /// <summary>
        /// Роль.
        /// </summary>
        public UserType Role { get; set; }

        /// <summary>
        /// Токен аутентификации.
        /// </summary>
        public string Token { get; set; } = null!;
    }
}