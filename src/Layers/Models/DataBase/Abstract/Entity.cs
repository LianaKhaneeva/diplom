using System.ComponentModel.DataAnnotations;

using Contracts.DataBase;

namespace Models.DataBase.Abstract
{
    /// <summary>
    /// Абстракция над всеми сущностями слоя данных.
    /// </summary>
    /// <seealso cref="IEntity" />
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        public int Id { get; set; } = default!;
    }
}