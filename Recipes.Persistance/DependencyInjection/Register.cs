using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Domain.Core.Repositories;
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
            services.AddScoped<IRecipeCardIDetailRepository, RecipeCardDetailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRecipeCardRepository, RecipeCardRepository>();

            return services;
        }
    }
}
