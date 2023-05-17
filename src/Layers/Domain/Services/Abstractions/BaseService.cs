using AutoMapper;

using Contracts;
using Contracts.DataBase;

namespace Domain.Services.Abstractions
{
    /// <summary>
    /// Определяет базовое поведение сервисов.
    /// </summary>
    /// <typeparam name="T">Модель данных</typeparam>
    public abstract class BaseService<T>
                    where T : IEntity
    {
        /// <summary>
        /// Возвращает репозитоий для работы с таблицей <see cref="T"/>.
        /// </summary>
        public IAsyncRepository<T> Repository { protected get; init; } = null!;

        /// <summary>
        /// Экземпляр маппера <see cref="Lazy{IMapper}"/>.
        /// </summary>
        public Lazy<IMapper> Mapper { protected get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="BaseService"/>.
        /// </summary>
        public BaseService(IAsyncRepository<T> repository,
                           Lazy<IMapper> mapper)
        {
            this.Repository = repository;
            this.Mapper     = mapper;
        }
    }
}