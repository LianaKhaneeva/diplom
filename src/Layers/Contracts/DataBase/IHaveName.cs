namespace Contracts.DataBase
{
    /// <summary>
    /// Определяет наличие наименования.
    /// </summary>
    public interface IHaveName
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        string Name { get; }
    }
}
