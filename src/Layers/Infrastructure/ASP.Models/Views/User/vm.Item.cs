using System.ComponentModel;


namespace ASP.Models.Views.User
{
    /// <summary>
    /// Модель отображения списка.
    /// </summary>
    /// <seealso cref="ASP.Models.Views.Abstractions.User.UserItem" />
    [DisplayName("User.Item")]
    public sealed partial class Item
    {
        /// <summary>
        /// Полное имя пользователя в формате "Фамилия Имя Отчество".
        /// </summary>
        public string FullName { get; set; } = null!;

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
        /// Мобильный телефонный номер.
        /// </summary>
        public string Phone { get; set; } = null!;
    }
}