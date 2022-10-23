using Microsoft.OpenApi.Models;

namespace Recipes.Web.DependencyInjection
{
    public static class Register
    {
        public static IServiceCollection UseWebApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Recipes App",
                    Description = "Asp.Net core app for managing recipes",
                });
            });
            return services;
        }
    }
}
