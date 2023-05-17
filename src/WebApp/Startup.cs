using Authentication.JWT;

using Contracts;

using Store;

using WebApp.Middleware;

using models = ASP.Models.Views.Abstractions;

namespace WebApp
{
    public sealed partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddEndpointsApiExplorer()
                    .AddSwaggerGen(options =>
                    {
                        options.AddSecurityRequirement(this.SecurityRequirement);

                        options.AddSecurityDefinition("Bearer", this.DefinitionSecurityScheme);

                        options.CustomSchemaIds(type => this.GetTypeName(type));
                    });


            var authentication = this.Configuration.GetSection(Settings.Authentication.Key);
            
            services.Configure<Settings.Authentication>(authentication);

            services.AddTokenAuthentication(
                authentication.Get<Settings.Authentication>()
                              .Secret);
            services.AddAuthorization(options =>
                    {
                        // Register policies here
                    });


            var connection = this.Configuration.GetConnectionString("Default");

            services.AddDataStore(connection);


            services.AddAutoMapper(typeof(models.BaseMap));

            services.AddCors(options => options.AddPolicy(this.AllowAnyPolicyName, this.AllowAny));

            services.AddLocalization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app.UseMiddleware<ErrorHandling>()
                  .UseHttpsRedirection()
                  .UseCors(this.AllowAnyPolicyName)
                  .UseAuthentication()
                  .UseRouting()
                  .UseAuthorization()
                  .UseEndpoints(endpoints =>
                  {
                      endpoints.MapControllers()
                               .RequireAuthorization();
                  })
                  .UseSwagger()
                  .UseSwaggerUI();
    }
}