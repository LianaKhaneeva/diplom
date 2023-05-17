using System.ComponentModel;

using Contracts.Enums;

namespace ASP.Models.Views.User
{
    /// <summary>
    /// Модель отображения детализации.
    /// </summary>
    /// <seealso cref="ASP.Models.Views.Abstractions.User.UserDetails" />
    [DisplayName("User.Details")]
    public sealed partial class Details
    {
        /// <summary>
        /// Тип пользователя.
        /// </summary>
        public UserType Type { get; set; }

        /// <summary>
        /// Полное имя пользователя в формате "Фамилия Имя Отчество".
        /// </summary>
        public string FullName { get; set; } = null!;

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName { get; set; } = null!;

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Мобильный телефонный номер.
        /// </summary>
        public string Phone { get; set; } = null!;
    }
}