using AutoMapper;

using Contracts;
using Contracts.Enums;

using Domain.Services;

using Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using NuGet.Common;

namespace WebApp.Controllers.Abstractions
{
    public partial class BaseController : ControllerBase
    {
        /// <summary>
        /// Авторизованный системный пользователь.
        /// </summary>
        protected AsyncLazy<AppUser> AppUser { get; private set; }

        /// <summary>
        /// Возвращает экземпляр <see cref="UserManager{AppUser}"/>.
        /// </summary>
        public Lazy<UserManager<AppUser>> Users { protected get; init; } = null!;

        /// <summary>
        /// Возвращает экземпляр <see cref="CarrierService"/>.
        /// </summary>
        public Lazy<CarrierService> CarrierService { protected get; init; } = null!;
        
        /// <summary>
        /// Возвращает экземпляр <see cref="CarrierService"/>.
        /// </summary>
        public Lazy<ShipperService> ShipperService { protected get; init; } = null!;

        /// <summary>
        /// Предоставляет экземпляр класса, реализующего <see cref="IMapper"/>.
        /// </summary>
        public Lazy<IMapper> Mapper { protected get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="BaseController"/>.
        /// </summary>
        public BaseController(Lazy<UserManager<AppUser>> users,
                              Lazy<IMapper> mapper,
                              Lazy<CarrierService> carrierService,
                              Lazy<ShipperService> shipperService)
        {
            this.Mapper      = mapper;
            this.Users       = users;
            this.CarrierService = carrierService;
            this.ShipperService = shipperService;

            this.AppUser =
               new AsyncLazy<AppUser>(async () =>
               {
                   var value =
                       base.User
                           .Claims
                           .FirstOrDefault(x => x.Type == Claims.AppUserId)
                          ?.Value;

                   var user = await this.Users.Value.FindByIdAsync(value);

                   return user;
               });

        }

        /// <summary>
        /// Возвращает идентификатор авторизованного клиента в асинхронном режиме.
        /// </summary>
        /// <returns>Идентификатор авторизованного клиента</returns>
        protected async Task<int> GetAuthorizedUserIdAsync()
            => (await this.AppUser).Type == UserType.Carrier
                    ? await this.CarrierService
                                .Value
                                .GetByIdentityAsync((await this.AppUser).Id)
                    : await this.ShipperService
                                .Value
                                .GetByIdentityAsync((await this.AppUser).Id);
    }
}