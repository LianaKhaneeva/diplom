using Contracts;
using Contracts.DataBase;

namespace Store.Extensions
{
    /// <summary>
    /// Содержит методы расширения для работы с репозиторием.
    /// </summary>
    public static class IAsyncRepositoryExtensions
    {
        /// <summary>
        /// Изменяет запрос.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="source">Репозиторий сущности.</param>
        /// <param name="mutaplasmid">Методы изменения запроса.</param>
        public static IAsyncRepository<T> MutateQuery<T>(
                 this IAsyncRepository<T> source,
                       Func<IQueryable<T>,
                            IQueryable<T>> mutaplasmid)
                where T : class, IEntity
        {
            var store = (AsyncRepository<T>)source;

            var query = mutaplasmid.Invoke(store.Query);

            store.Query = query;

            return store;
        }
    }
}