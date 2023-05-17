namespace Contracts.DataBase
{
    /// <summary>
    /// Определяет наличие идентификатора (первичного ключа).
    /// </summary>
    public interface IHaveId
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        int Id { get; }
    }
}