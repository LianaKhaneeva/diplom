using System.Linq.Expressions;

using Common.Exceptions;
using Common.Extensions;

using Contracts;
using Contracts.DataBase;

using Dapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Store
{
    /// <summary>
    /// Определяет базовые методы для работы с таблицей БД, соответствующей модели <see cref="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    internal abstract class BaseAsyncRepository<TEntity> : IAsyncRepository<TEntity>, IDisposable
        where TEntity : class, IEntity
    {
        private readonly ICurrentDbContext current = null!;

        private IQueryable<TEntity>? query;

        private bool wasDisposed = false;

        /// <summary>
        /// Текущий контекст БД.
        /// </summary>
        protected DbContext Context
            => this.current.Context;

        /// <summary>
        /// Запрос.
        /// </summary>
        public IQueryable<TEntity> Query
        {
            get => this.query ??= this.Context.Set<TEntity>();
            internal protected set => this.query = value;
        }

        /// <summary>
        /// Базовый конструктор, инициализирующий экземпляры наследников класса <see cref="BaseAsyncRepository{TEntity, TKey}"/>.
        /// </summary>
        /// <param name="current">Текущий контекст БД.</param>
        protected BaseAsyncRepository(ICurrentDbContext current)
        {
            this.current = current;
        }

        /// <summary>
        /// Cтроит выражение для метода, возвращающего сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Выражение для сопоставления идентификатора.</returns>
        private Expression<Func<TEntity, bool>> BuildExpression(int id)
        {
            var parameterExpr = Expression.Parameter(typeof(TEntity));
            var idPropExpr = Expression.Property(parameterExpr, "Id");
            var idExpr = Expression.Constant(id);
            var eqExpr = Expression.Equal(idPropExpr, idExpr);

            return Expression.Lambda<Func<TEntity, bool>>(eqExpr, parameterExpr);
        }

        /// <summary>
        /// Возвращает сущность по идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр сущности.</returns>
        private Task<TEntity?> GetByExpressionAsync(int id)
        {
            var expr = this.BuildExpression(id);

            return this.GetAsync(expr);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.wasDisposed)
            {
                return;
            }

            if (disposing)
            {
            }

            this.wasDisposed = true;
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с освобождением, выпуском или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Выполняет SQL запрос, не подразумевающий возврата значения, в асинхронном режиме.
        /// </summary>
        /// <param name="query">SQL запрос.</param>
        /// <param name="entity">Сущность, над которой исполняется запрос.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public Task ExecuteAsync(string query, TEntity entity)
            => this.Context.Database.GetDbConnection().ExecuteAsync(query, entity);

        /// <summary>
        /// Запрашивает данные по указанному SQL запросу в асинхронном режиме.
        /// </summary>
        /// <param name="query">SQL запрос.</param>
        /// <param name="parameters">Объект, содержащий параметры запроса.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список экземпляров сущностей.</returns>
        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string query, object parameters)
            => this.Context.Database.GetDbConnection().QueryAsync<TResult>(query, parameters);


        /// <summary>
        /// Возвращает список сущностей по заданному условию в асинхронном режиме.
        /// </summary>
        /// <param name="expr">Условие.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список экземпляров сущностей.</returns>
        public virtual Task<List<TEntity>> ManyAsync(Expression<Func<TEntity, bool>> expr)
            => this.Query.Where(expr).ToListAsync();

        /// <summary>
        /// Возвращает список всех сущностей в асинхронном режиме.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список экземпляров сущностей.</returns>
        public virtual Task<List<TEntity>> AllAsync()
            => this.Query.ToListAsync();

        /// <summary>
        /// Возвращает сущность по условию в асинхронном режиме.
        /// </summary>
        /// <param name="expr">Условие.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр сущности.</returns>
        public virtual Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expr)
            => this.Query.FirstOrDefaultAsync(expr);

        /// <summary>
        /// Возвращает сущность по индетификатору в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр сущности.</returns>
        public virtual Task<TEntity?> GetAsync(int id)
            => this.GetByExpressionAsync(id);   


        /// <summary>
        /// Возвращает количество сущностей по условию в асинхронном режиме.
        /// </summary>
        /// <param name="expr">Условие.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит количество сущностей.</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expr)
            => this.Query.CountAsync(expr);

        /// <summary>
        /// Возвращает количество сущностей в асинхронном режиме.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит количество сущностей.</returns>
        public virtual Task<int> CountAsync()
            => this.Query.CountAsync();


        /// <summary>
        /// Создаёт экземпляры сущностей в асинхронном режиме.
        /// </summary>
        /// <param name="entities">Создаваемые сущности.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит список идентификаторов созданных сущностей.</returns>
        public virtual Task<IEnumerable<int>> CreateManyAsync(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().AddRange(entities);

            var result =
                this.Context
                    .SaveChangesAsync()
                    .ContinueWith(x => entities.Select(entity => entity.Id));

            return result;
        }

        /// <summary>
        /// Создает экземпляр сущности в асинхронном режиме.
        /// </summary>
        /// <param name="entity">Создаваемая сущность.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит идентификатор созданной сущности.</returns>
        public virtual Task<int> CreateAsync(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);

            var result =
                this.Context
                    .SaveChangesAsync()
                    .ContinueWith(x => entity.Id);

            return result;
        }


        /// <summary>
        /// Обновляет состояния сущностей в асинхронном режиме.
        /// </summary>
        /// <param name="entities">Редактируемые сущностей.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public virtual Task UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }

            return this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет состояния сущности в асинхронном режиме.
        /// </summary>
        /// <param name="entity">Редактируемая сущность.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public virtual Task UpdateAsync(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;

            return this.Context.SaveChangesAsync();
        }


        /// <summary>
        /// Удаляет экземпляры сущностей в асинхронном режиме.
        /// </summary>
        /// <param name="entities">Удаляемые сущности.</param>
        public virtual Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            this.Context.RemoveRange(entities);

            return this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет экземпляры сущностей по набору ключей в асинхронном режиме.
        /// </summary>
        /// <param name="keys">Идентификаторы удаляемых сущностей.</param>
        public virtual Task DeleteManyAsync(IEnumerable<int> keys)
            => this.Query
                   .Where(x => keys.Contains(x.Id))
                   .ToListAsync()
                   .Continue(async data =>
                   {
                       var entities = await data;

                       await this.DeleteManyAsync(entities);
                   });

        /// <summary>
        /// Удаляет экземпляр сущности в асинхронном режиме.
        /// </summary>
        /// <param name="entity">Удаляемая сущность.</param>
        public virtual Task DeleteAsync(TEntity entity)
        {
            this.Context.Remove(entity);

            return this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет экземпляр сущности по идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности.</param>
        public virtual Task DeleteAsync(int id)
            => this.GetByExpressionAsync(id)
                   .Continue(async data =>
                   {
                       var entity = await data ?? throw new NotFoundException();

                       await this.DeleteAsync(entity);
                   });
    }

    /// <summary>
    /// Определяет методы для работы с таблицей БД, соответствующей модели <see cref="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Модель данных.</typeparam>
    /// <seealso cref="Store.BaseAsyncRepository&lt;TEntity, TKey&gt;" />
    internal sealed class AsyncRepository<TEntity> : BaseAsyncRepository<TEntity> 
        where TEntity : class, IEntity

    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="AsyncRepository{TEntity, TKey}"/>.
        /// </summary>
        /// <param name="current">Текущий контекст БД.</param>
        public AsyncRepository(ICurrentDbContext current)
             : base(current)
        {
        }
    }
}