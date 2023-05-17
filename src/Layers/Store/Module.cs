using Contracts;

using Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace Store
{
    public static class Module
    {
        /// <summary>
        /// Добавляет сконфигурированное хранилище данных.
        /// </summary>
        /// <param name="services">Коллекция <see cref="IServiceCollection"/>, в которую нужно добавить службы.</param>
        /// <param name="connection">Строка подключения к базе данных.</param>
        /// <returns>Контракт для коллекции дескрипторов служб.</returns>
        public static IServiceCollection AddDataStore(this IServiceCollection services, string connection)
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<AppDataContext>();

            services.AddDbContext<AppDataContext>(options =>
            {
                _ = options.UseNpgsql(connection, x => x.MigrationsHistoryTable("_Migrations"));
            });


            services.AddScoped<AppUserStore>();


            services.Configure<PasswordHasherOptions>(option =>
            {
                option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                option.IterationCount    = 12000;
            });

            services.AddIdentityCore<AppUser>(config =>
            {
                config.Password.RequireNonAlphanumeric    = false;
                config.Password.RequireLowercase          = false;
                config.Password.RequireUppercase          = false;
                config.Password.RequireDigit              = false;
                config.Password.RequiredUniqueChars       = 1;
                config.Password.RequiredLength            = 4;

                config.Lockout.MaxFailedAccessAttempts    = 5;
                config.Lockout.DefaultLockoutTimeSpan     = TimeSpan.FromMinutes(5);
                config.Lockout.AllowedForNewUsers         = false;

                config.SignIn.RequireConfirmedPhoneNumber = false;
                config.SignIn.RequireConfirmedEmail       = false;

                config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                config.User.RequireUniqueEmail = true;
            })
                    .AddEntityFrameworkStores<AppDataContext>()
                    .AddDefaultTokenProviders()
                    .AddSignInManager<SignInManager<AppUser>>()
                    .AddUserStore<AppUserStore>();


            return services;
        }

        /// <summary>
        /// Применение всех существующих миграций для контекста к базе данных. Создаст базу данных, если она ещё не существует.
        /// 
        /// Для добавления новой миграции:
        /// 1. назначить Layers/Store запускаемым проектом
        /// 2. в консоли диспетчера пакетов переключить Проект по умолчанию на Layers/Store
        /// 3. в консоли диспетчера пакетов создать новую миграцию: Add-Migration *имя миграции* (например Add-Migration InitialCreate)
        /// </summary>
        /// <param name="app">Абстракция программы.</param>
        /// <returns>Абстракция программы.</returns>
        public static IHost MigrateDatabase(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                using (var context = services.GetService<AppDataContext>())
                {
                    try
                    {
                        context?.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetService<ILogger>();
                        logger?.Error(ex, "В процессе миграции базы данных произошла ошибка.");
                        throw;
                    }
                }
            }

            return app;
        }

        /// <summary>
        /// Регистрирует типы в IoC-контейнере.
        /// </summary>
        /// <param name="container">Используемый IoC-контейнер</param>
        public static void Register(IServiceCollection container)
        {
            container.AddTransient(x => 
                x.GetService<AppDataContext>()
                !.Database
                 .GetService<IRelationalConnection>());

            container.AddTransient(x =>
                x.GetService<AppDataContext>()
                !.Database
                 .GetService<IConcurrencyDetector>());

            container.AddTransient(x =>
                x.GetService<AppDataContext>()
                !.Database
                 .GetService<ICurrentDbContext>());

            container.AddScoped<DbContext, AppDataContext>();

            container.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
        }
    }
}