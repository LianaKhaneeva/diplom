using System.ComponentModel.DataAnnotations;

namespace ASP.Models.Requests
{
    /// <summary>
    /// Предоставляет имя пользователя с его свойствами.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string UserName { get; set; } = null!;


        /// <summary>
        /// Возвращает нормализованное имя пользователя (в верхнем регистре).
        /// </summary>
        /// <value>
        /// Имя пользователя, все символы в которого приведены к верхнему регистру.
        /// </value>
        public string NormalizedUserName
            => this.UserName.ToUpperInvariant();
    }
}