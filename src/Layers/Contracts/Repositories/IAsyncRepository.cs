using System.Linq.Expressions;

using Contracts.DataBase;

namespace Contracts
{
    /// <summary>
    /// Определяет методы для работы с таблицей БД, соответствующей модели <see cref="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Модель данных.</typeparam>
    /// <seealso cref="IEntity" />
    public interface IAsyncRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Запрос.
        /// </summary>
        IQueryable<TEntity> Query { get; }


        /// <summary>
        /// Выполняет SQL запрос, не подразумевающий возврата значения, в асинхронном режиме.
        /// </summary>
        /// <param name="query">SQL запрос.</param>
        /// <param name="entity">Сущность, над которой исполняется запрос.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task ExecuteAsync(string query, TEntity entity);

        /// <summary>
        /// Запрашивает данные по указанному SQL запросу в асинхронном режиме.
        /// </summary>
        /// <param name="query">SQL запрос.</param>
        /// <param name="parameters">Объект, содержащий параметры запроса.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список экземпляров сущностей.</returns>
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string query, object parameters);


        /// <summary>
        /// Возвращает список сущностей по заданному условию в асинхронном режиме.
        /// </summary>
        /// <param name="expr">Условие.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список экземпляров сущностей.</returns>
        Task<List<TEntity>> ManyAsync(Expression<Func<TEntity, bool>> expr);

        /// <summary>
        /// Возвращает список всех сущностей в асинхронном режиме.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список экземпляров сущностей.</returns>
        Task<List<TEntity>> AllAsync();

        /// <summary>
        /// Возвращает сущность по условию в асинхронном режиме.
        /// </summary>
        /// <param name="expr">Условие.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр сущности.</returns>
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expr);

        /// <summary>
        /// Возвращает сущность по индетификатору в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр сущности.</returns>
        Task<TEntity?> GetAsync(int id);


        /// <summary>
        /// Возвращает количество сущностей по условию в асинхронном режиме.
        /// </summary>
        /// <param name="expr">Условие.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит количество сущностей.</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expr);

        /// <summary>
        /// Возвращает количество сущностей в асинхронном режиме.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит количество сущностей.</returns>
        Task<int> CountAsync();


        /// <summary>
        /// Создаёт экземпляры сущностей в асинхронном режиме.
        /// </summary>
        /// <param name="entities">Создаваемые сущности.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список созданных сущностей.</returns>
        Task<IEnumerable<int>> CreateManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Создает экземпляр сущности в асинхронном режиме.
        /// </summary>
        /// <param name="entity">Создаваемая сущность.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит идентификатор созданной сущности.</returns>
        Task<int> CreateAsync(TEntity entity);


        /// <summary>
        /// Обновляет состояния сущностей в асинхронном режиме.
        /// </summary>
        /// <param name="entities">Редактируемые сущностей.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task UpdateManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Обновляет состояния сущности в асинхронном режиме.
        /// </summary>
        /// <param name="entity">Редактируемая сущность.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task UpdateAsync(TEntity entity);


        /// <summary>
        /// Удаляет экземпляры сущностей в асинхронном режиме.
        /// </summary>
        /// <param name="entities">Удаляемые сущности.</param>
        Task DeleteManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удаляет экземпляры сущностей по набору ключей в асинхронном режиме.
        /// </summary>
        /// <param name="keys">Идентификаторы удаляемых сущностей.</param>
        Task DeleteManyAsync(IEnumerable<int> keys);

        /// <summary>
        /// Удаляет экземпляр сущности в асинхронном режиме.
        /// </summary>
        /// <param name="entity">Удаляемая сущность.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Удаляет экземпляр сущности по идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности.</param>
        Task DeleteAsync(int id);
    }
}