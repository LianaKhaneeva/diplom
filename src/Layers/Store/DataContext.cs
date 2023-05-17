using Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Store.Setups;

namespace Store
{
    /// <summary>
    /// Определяет методы для создания контекста БД.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory&lt;Store.AppDataContext&gt;" />
    internal class DataContextFactory : IDesignTimeDbContextFactory<AppDataContext>
    {
        /// <summary>
        /// Создает новый экземпляр контекста БД.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        /// <returns>
        /// Экземпляр <typeparamref name="AppDataContext" />.
        /// </returns>
        public AppDataContext CreateDbContext(string[] args)
        {
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            var connection = configuration.GetConnectionString("Default");

            var setup =
                new DbContextOptionsBuilder<AppDataContext>()
                    .UseNpgsql(connection);

            return new AppDataContext(setup.Options);
        }
    }

    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext&lt;Identity.AppUser, Microsoft.AspNetCore.Identity.IdentityRole&lt;System.Int32&gt;, System.Int32&gt;" />
    internal sealed partial class AppDataContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        /// <summary>
        /// Регистратор.
        /// </summary>
        public ILoggerFactory Logger { private get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="AppDataContext"/>.
        /// </summary>
        /// <param name="options">Опции контекста.</param>
        public AppDataContext(DbContextOptions<AppDataContext> options)
             : base(options)
        {
        }

        /// <summary>
        /// Настраивает контекст БД.
        /// </summary>
        /// <param name="options">Опции контекста.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            // options.ConfigureWarnings(warnings => warnings.Default(WarningBehavior.Throw);

            options.EnableSensitiveDataLogging(false)
                   .UseLoggerFactory(this.Logger);
        }

        /// <summary>
        /// Конфигурация схемы.
        /// </summary>
        /// <param name="self">Строитель, который будет использоваться для насройки моделей.</param>
        protected override void OnModelCreating(ModelBuilder self)
        {
            base.OnModelCreating(self);

            self.ApplyConfiguration(new CarrierSetup());
            self.ApplyConfiguration(new ShipperSetup());
        }
    }
}