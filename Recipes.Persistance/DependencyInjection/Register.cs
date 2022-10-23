using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Domain.Core;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.DependencyInjection
{
    public static class Register
    {
        public static IServiceCollection UsePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
