using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Core.Identity;
using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Interceptors;
using Recipes.Persistance.Repositories;
using Recipes.Persistance.Repositories.Cache;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.DependencyInjection
{
    public static class Register
    {
        public static IServiceCollection UsePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.AddInterceptors(new CreateOutboxMessageInterceptor());
                x.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRecipeCardRepository, RecipeCardRepository>();
            services.Decorate<IRecipeCardRepository, CacheRecipeCardRepository>();
            services.AddScoped<ICookingStageRepository, CookingStageRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IFavouriteRecipeRepository, FavouriteRecipeRepository>();
            services.AddScoped<IConfirmationRequestRepository, ConfirmationRequestRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();

            return services;
        }
    }
}
