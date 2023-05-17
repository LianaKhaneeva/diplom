using Store;

namespace WebApp
{
    public static class Program
    {
        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((context, options) =>
                   {
                       options.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddEnvironmentVariables();
                   })
                   .ConfigureWebHostDefaults(builder =>
                   {
                       builder.UseStartup<Startup>();
                   })
                   .ConfigureContainer<IServiceCollection>((context, container) =>
                   {
                       container.AddTransient(typeof(Lazy<>));

                       Authentication.JWT.Module.Register(container);

                       Store.Module.Register(container);

                       Domain.Module.Register(container);
                   });

        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .MigrateDatabase()
                .Run();
    } 
}