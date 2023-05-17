using System.ComponentModel;

using Contracts.Enums;

namespace ASP.Models.Views.User
{
    /// <summary>
    /// Модель создания.
    /// </summary>
    [DisplayName("User.New")]
    public sealed partial class New
    {
        /// <summary>
        /// Тип пользователя.
        /// </summary>
        public UserType Type { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName { get; set; } = null!;

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Мобильный телефонный номер.
        /// </summary>
        public string Phone { get; set; } = null!;
}
}