namespace Contracts.DataBase.Extensions
{
    /// <summary>
    /// Содержит методы расширения для формирования различных строковых представлений из полного имени (ФИО).
    /// </summary>
    public static class IHaveFullNameExtensions
    {
        /// <summary>
        /// Формирует и возвращает строку в формате "Фамилия И. О.".
        /// </summary>
        /// <param name="source">Тип имеющий поля, содержащие значения Фамилии, Имя и Отчества.</param>
        /// <returns>Возвращает полное имя в формате "Фамилия И. О.".</returns>
        public static string ToShortName(this IHaveFullName source)
            => $"{source.LastName} {source.FirstName}. {source.MiddleName}.";

        /// <summary>
        /// Формирует и возвращает строку в формате "Фамилия Имя Отчество".
        /// </summary>
        /// <param name="source">Тип имеющий поля, содержащие значения Фамилии, Имя и Отчества.</param>
        /// <returns>Возвращает полное имя в формате "Фамилия Имя Отчество".</returns>
        public static string ToFullName(this IHaveFullName source)
            => $"{source.LastName} {source.FirstName} {source.MiddleName}";
    }
}