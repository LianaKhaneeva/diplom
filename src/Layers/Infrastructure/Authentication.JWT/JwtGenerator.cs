using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Contracts;

namespace Authentication.JWT
{
    /// <summary>
    /// Генератор токенов доступа стандарта JWT.
    /// </summary>
    public sealed class JwtGenerator
    {
        private SymmetricSecurityKey Key =>
            new(Encoding.UTF8.GetBytes(this.AppSettings.Value.Secret));

        /// <summary>
        /// Возвращает экземпляр класса <see cref="Settings.Authentication"/>.
        /// </summary>
        public IOptions<Settings.Authentication> AppSettings { private get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="JwtGenerator"/>.
        /// </summary>
        /// <param name="appSettings">Содержит элементы конфигурации, представляемые экземпляром <see cref="Settings.Authentication"/>.</param>
        public JwtGenerator(IOptions<Settings.Authentication> appSettings)
        {
            AppSettings = appSettings;
        }

        /// <summary>
        /// Генерирует для указанного пользователя JWT.
        /// </summary>
        /// <param name="claims">Атрибуты, полезные в контексте операций проверки подлинности и авторизации.</param>
        /// <returns>JWE или JWS в «формате компактной сериализации».</returns>
        public string CreateToken(IEnumerable<Claim> claims)
        {
            var credentials = new SigningCredentials(this.Key, SecurityAlgorithms.HmacSha512Signature);

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject            = new ClaimsIdentity(claims),
                Expires            = DateTime.Now.AddDays(this.AppSettings.Value.ExpiresInDays)
            };

            var generator = new JwtSecurityTokenHandler();

            var token = generator.CreateToken(descriptor);

            return generator.WriteToken(token);
        }
    }
}