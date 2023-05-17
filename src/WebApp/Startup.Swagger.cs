using System.ComponentModel;
using System.Reflection;
using System.Text;

using Microsoft.OpenApi.Models;

using CSharpTypePrinter;

namespace WebApp
{
    public sealed partial class Startup
    {
        /// <summary>
        /// Возвращает требования безопасности API.
        /// </summary>
        private OpenApiSecurityRequirement SecurityRequirement
            => new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id   = "Bearer"
                        },
                        Scheme    = "oauth2",
                        Name      = "Bearer",
                        In        = ParameterLocation.Header
                    },
                    new List<string>()
                }
            };


        /// <summary>
        /// Возвращает описание схемы авторизации для Swagger.
        /// </summary>
        private OpenApiSecurityScheme DefinitionSecurityScheme
            => new()
            {
                Description = $"JWT Authorization header using the Bearer scheme.\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: 'Bearer 12345abc'",
                Scheme      = "Bearer",
                Name        = "Authorization",
                Type        = SecuritySchemeType.ApiKey,
                In          = ParameterLocation.Header
            };


        private readonly List<string> replaceable = new() { "New", "Edit", "Item", "Details" };

        /// <summary>
        /// Возвращает нормализованное имя типа.
        /// </summary>
        /// <param name="type">Тип, имя которого ищется.</param>
        /// <returns>Строка, содержащая нормализованное имя переданного типа. Для форирования имени используется содержимое аттрибута <see cref="DisplayNameAttribute"/>, при его отсутствии применяется <see cref="TypePrinter.ToCSharpCode"/>. Нормализуются, в том числе, имена generic параметров.</returns>
        /// <seealso cref="DisplayNameAttribute"/>
        /// <seealso cref="TypePrinter.ToCSharpCode"/>
        private string GetTypeName(Type type)
        {
            string DisplayName(Type type)
                => type.GetCustomAttribute<DisplayNameAttribute>(false)
                      ?.DisplayName
                ?? type.ToCSharpCode(stripNamespace: true);

            void TryReplace(Type type, StringBuilder name)
            {
                var generic = type.GetGenericArguments();

                foreach (var arg in generic)
                {
                    if (replaceable.Any(x => arg.Name.Contains(x)))
                    {
                        name.Replace(arg.Name, DisplayName(arg));
                    }

                    TryReplace(arg, name);
                }
            };

            var name = new StringBuilder(DisplayName(type));

            TryReplace(type, name);

            return name.ToString();
        }
    }
}