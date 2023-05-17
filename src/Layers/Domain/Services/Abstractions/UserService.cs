using AutoMapper;

using Common.Exceptions;
using Common.Extensions;

using Contracts;

using Identity;

using Microsoft.AspNetCore.Identity;
using Models.DataBase.Abstract;
using user = ASP.Models.Views.User;

namespace Domain.Services.Abstractions
{
    /// <summary>
    /// Сервис работы с пользователями.
    /// </summary>
    public abstract class UserService<T> : BaseService<T>
        where T : User
    {
        /// <summary>
        /// Провайдер управления пользователями системы.
        /// </summary>
        public Lazy<UserManager<AppUser>> Users { protected get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="UserService"/>.
        /// </summary>
        public UserService(IAsyncRepository<T> repository,
                               Lazy<IMapper> mapper,
                               Lazy<UserManager<AppUser>> users)
            : base(repository, mapper)
        {
            Users = users;
        }

        /// <summary>
        /// Регистрирует клиента в асинхронном режиме.
        /// </summary>
        /// <param name="user">Регистрируемый клиент.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит идентификатор зарегистрированного клиента.</returns>
        public async Task<int> RegisterAsync(user.New user)
        {
            user.Phone = user.Phone.ClearPhone();

            var appUser = new AppUser
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                PhoneNumber = user.Phone,
                Email = user.UserName,
                UserName = user.UserName,
                Type = user.Type
            };

            var registerResult = await Users.Value.CreateAsync(appUser, user.Password);

            if (!registerResult.Succeeded)
            {
                throw
                  new InvalidOperationException("Не удалось зарегестрировать");
            }

            return appUser.Id;
        }

        /// <summary>
        /// Возвращает типизированного пользователя по его идентификатору системного пользователя в асинхронном режиме.
        /// </summary>
        /// <param name="id">Индетификатор системного пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр <see cref="T" />.</returns>
        public async Task<int> GetByIdentityAsync(int id)
        {
            var user =
                await Repository
                          .GetAsync(x => x.IdentityId == id)
                ?? throw new NotFoundException(id);

            return user.Id;
        }
    }
}
