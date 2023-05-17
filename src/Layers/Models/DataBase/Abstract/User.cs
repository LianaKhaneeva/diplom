using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Contracts.DataBase;
using Contracts.Enums;

using Identity;
using Identity.Extensions;

namespace Models.DataBase.Abstract
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    public abstract class User : Entity, IHaveFullName
    {
        protected Action<object, string> Lazy { get; init; } = null!;

        private AppUser identity = null!;

        /// <summary>
        /// Личность пользователя в системе идентификации.
        /// </summary>
        [Required,
            ForeignKey(nameof(IdentityId))]
        public AppUser Identity
        {
            get => Lazy.Load(this, ref identity);
            set => identity = value;
        }

        /// <summary>
        /// Идентификатор личности пользователя в системе идентификации.
        /// </summary>
        [Required,
            ForeignKey(nameof(IdentityId))]
        public int IdentityId { get; set; }

        /// <summary>
        /// Роль.
        /// </summary>
        [NotMapped]
        public UserType Role
            => Identity.Type;


        /// <summary>
        /// Мобильный телефонный номер.
        /// </summary>
        [NotMapped]
        public string Phone
            => Identity.PhoneNumber;

        /// <summary>
        /// Фамилия.
        /// </summary>
        [NotMapped]
        public string LastName
            => Identity.LastName;

        /// <summary>
        /// Имя.
        /// </summary>
        [NotMapped]
        public string FirstName
            => Identity.FirstName;

        /// <summary>
        /// Отчество.
        /// </summary>
        [NotMapped]
        public string MiddleName
            => Identity.MiddleName;
    }
}