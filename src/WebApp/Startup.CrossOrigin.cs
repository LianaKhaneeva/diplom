using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WebApp
{
    public sealed partial class Startup
    {
        private readonly string AllowAnyPolicyName = "Any";

        private Action<CorsPolicyBuilder> AllowAny
         => new Action<CorsPolicyBuilder>(builder =>
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .SetIsOriginAllowed(origin => true) // allow any origin
                       .AllowCredentials());
    }
}
