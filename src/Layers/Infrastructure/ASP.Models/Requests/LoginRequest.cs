using System.ComponentModel.DataAnnotations;

namespace ASP.Models.Requests
{
    /// <summary>
    /// Запрос на авторизацию.
    /// </summary>
    public sealed class LoginRequest : UserData
    {
        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        public string Password { get; set; } = null!;
    }
}