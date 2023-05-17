namespace Contracts.DataBase
{
    /// <summary>
    /// Определяет наличие ФИО (Фамилии, Имени и Отчества).
    /// </summary>
    public interface IHaveFullName
    {
        /// <summary>
        /// Фамилия.
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Имя.
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Отчество.
        /// </summary>
        string MiddleName { get; }
    }
}