using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Recipes.Application.DependencyInjection
{
    public static class Register
    {
        public static IServiceCollection UseApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    } 
}
