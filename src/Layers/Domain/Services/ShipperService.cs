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
    /// Сервис работы с грузоотправителями.
    /// </summary>
    public sealed class ShipperService : UserService<Shipper>
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ShipperService"/>.
        /// </summary>
        public ShipperService(IAsyncRepository<Shipper> repository,
                               Lazy<IMapper> mapper,
                               Lazy<UserManager<AppUser>> users)
            : base(repository, mapper, users)
        {}

        /// <summary>
        /// Возвращает всех грозоотправителей в асинхронном режиме.
        /// </summary>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список грозоотправителей.</returns>
        public Task<List<Shipper>> AllAsync()
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .AllAsync();

        /// <summary>
        /// Возвращает грозоотправителя по ключу в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит грозоотправителя.</returns>
        public async Task<Shipper> GetAsync(int id)
        {
            var user =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            return user;
        }

        /// <summary>
        /// Регистрирует грозоотправителя в асинхронном режиме.
        /// </summary>
        /// <param name="user">Регистрируемый грозоотправитель.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит идентификатор зарегистрированного грозоотправителя.</returns>
        public async Task<int> RegisterAsync(user.New user)
        {
            user.Type = UserType.Shipper;

            var userId = await base.RegisterAsync(user);

            var entity = new Shipper
            {
                IdentityId = userId
            };


            var id = await Repository.CreateAsync(entity);

            return id;
        }
    }
}
