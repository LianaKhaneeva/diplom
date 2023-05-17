using AutoMapper;

using Common.Exceptions;

using Contracts;
using Contracts.Enums;
using Domain.Services.Abstractions;
using Identity;

using Microsoft.AspNetCore.Identity;

using Models.DataBase;

using Store.Decorators;
using Store.Extensions;

using user = ASP.Models.Views.User;

namespace Domain.Services
{
    /// <summary>
    /// Сервис работы с грузоперевозчиками.
    /// </summary>
    public sealed class CarrierService : UserService<Carrier>
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CarrierService"/>.
        /// </summary>
        public CarrierService(IAsyncRepository<Carrier> repository,
                               Lazy<IMapper> mapper,
                               Lazy<UserManager<AppUser>> users)
            : base(repository, mapper, users)
        {}

        /// <summary>
        /// Возвращает всех грузоперевозчиков в асинхронном режиме.
        /// </summary>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список грузоперевозчиков.</returns>
        public Task<List<Carrier>> AllAsync()
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .AllAsync();

        /// <summary>
        /// Возвращает грузоперевозчика по ключу в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит грузоперевозчика.</returns>
        public async Task<Carrier> GetAsync(int id)
        {
            var user =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            return user;
        }

        /// <summary>
        /// Регистрирует грузоперевозчика в асинхронном режиме.
        /// </summary>
        /// <param name="user">Регистрируемый грузоперевозчик.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит идентификатор зарегистрированного грузоперевозчика.</returns>
        public async Task<int> RegisterAsync(user.New user)
        {
            user.Type = UserType.Carrier;

            var userId = await base.RegisterAsync(user);

            var entity = new Carrier
            {
                IdentityId = userId
            };

            var id = await Repository.CreateAsync(entity);

            return id;
        }
    }
}
