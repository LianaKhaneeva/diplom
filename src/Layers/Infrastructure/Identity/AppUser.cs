using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using Contracts.DataBase;
using Contracts.Enums;

namespace Identity
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public sealed class AppUser : IdentityUser<int>, IHaveFullName
    {
        /// <summary>
        /// Тип пользователя.
        /// </summary>
        [Required]
        public UserType Type { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required,
            StringLength(50)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Имя.
        /// </summary>
        [Required,
            StringLength(50)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Отчество.
        /// </summary>
        [Required,
            StringLength(50)]
        public string MiddleName { get; set; } = null!;
    }
}