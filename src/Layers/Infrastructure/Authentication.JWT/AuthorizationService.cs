using System.Net;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using ASP.Models.Requests;
using ASP.Models.Responces;
using Common;
using Contracts;
using Contracts.DataBase.Extensions;
using Identity;

namespace Authentication.JWT
{
    /// <summary>
    /// Сервис, отвечающий за авторизацию пользователя.
    /// </summary>
    public sealed class AuthorizationService
    {
        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="SignInManager{T}"/>.
        /// </summary>
        /// <value>
        /// Сервис, предоставляющий методы для авторизации пользователя.
        /// </value>
        public Lazy<SignInManager<AppUser>> SignIn { private get; init; } = null!;

        /// <summary>
        /// Возвращает экземпляр класса <see cref="JwtGenerator"/>.
        /// </summary>
        /// <value>
        /// Представляет объект, генерирующий JWT токены.
        /// </value>
        public Lazy<JwtGenerator> Jwt { private get; init; } = null!;

        /// <summary>
        /// Возвращает экземпляр класса <see cref="AppUserStore"/>.
        /// </summary>
        /// <value>
        /// Представляет экземпляр персистентного хранилища пользователей.
        /// </value>
        public AppUserStore User { private get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="AuthorizationService"/>.
        /// </summary>
        public AuthorizationService(Lazy<SignInManager<AppUser>> signIn,
                                    Lazy<JwtGenerator> jwt, 
                                    AppUserStore user)
        {
            this.SignIn = signIn;
            this.Jwt    = jwt;
            this.User   = user;
        }

        private LoginResponce PrepareResponce(AppUser user)
        {
            var claims =
                new List<Claim>
                {
                    new Claim(ClaimTypes.Role, $"{user.Type}"),

                    new Claim(Claims.AppUserId, $"{user.Id}")
                };

            return
               new LoginResponce
               {
                   FullName       = user.ToFullName(),
                   Email          = user.Email,
                   Phone          = user.PhoneNumber,
                   Role           = user.Type,
                   Token          = this.Jwt.Value.CreateToken(claims)
               };
        }

        /// <summary>
        /// Производит попытку авторизации в асинхронном режиме.
        /// </summary>
        /// <param name="request">Авторизационные данные.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр <see cref="LoginResponce"/>, содержащий сведения об авторизованном пользователе.</returns>
        public async Task<LoginResponce> MakeAttemptAsync(LoginRequest request)
        {
            var appUser = await this.User.FindByNameAsync(request.NormalizedUserName);

            if (appUser == null)
            {
                throw
                  new WebAppException(
                      HttpStatusCode.Unauthorized,
                     "Неверный логин или пароль");
            }


            var result = await this.SignIn.Value.CheckPasswordSignInAsync(appUser, request.Password, false);

            if (!result.Succeeded)
            {
                throw
                  new WebAppException(
                      HttpStatusCode.Unauthorized,
                     "Неверный логин или пароль");
            }


            return this.PrepareResponce(appUser);
        }

        /// <summary>
        /// Производит попытку авторизации в асинхронном режиме. Используется в случае необходимости смены токена, в следствии изменения идентифицирующих пользователя данных.
        /// </summary>
        /// <param name="user">Авторизованный пользователь.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит экземпляр <see cref="LoginResponce"/>, содержащий сведения об авторизованном пользователе.</returns>
        public LoginResponce MakeAttemptAsync(AppUser user)
            => this.PrepareResponce(user);
    }
}