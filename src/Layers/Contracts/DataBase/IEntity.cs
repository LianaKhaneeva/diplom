namespace Contracts.DataBase
{
    /// <summary>
    /// Общий контракт, реализуемый всеми сущностями слоя данных.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        int Id { get; set; }
    }
}