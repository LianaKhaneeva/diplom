using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.JWT
{
    /// <summary>
    /// Конфигурирующие методы, предоставляемые проектом.
    /// </summary>
    public static class Module
    {
        /// <summary>
        /// Добавляет службу аутентификации.
        /// </summary>
        /// <param name="services">Коллекция <see cref="IServiceCollection"/>, в которую добавляются службы.</param>
        /// <param name="secret">Секретный ключ.</param>
        /// <returns><see cref="IServiceCollection"/> для объединения дополнительных вызовов в цепочку.</returns>
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, string secret)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey         = key,
                            ValidateAudience         = false,
                            ValidateIssuer           = false,
                        };
                    });

            return services;
        }

        /// <summary>
        /// Регистрирует типы в IoC-контейнере.
        /// </summary>
        /// <param name="container">Используемый IoC-контейнер</param>
        public static void Register(IServiceCollection container)
        {
            container.AddScoped(typeof(AuthorizationService));

            container.AddScoped(typeof(JwtGenerator));
        }
    }
}