using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Core.Auth;
using Recipes.Application.Core.Files;
using Recipes.Application.Core.Pagination;
using Recipes.Domain.Core.Services;
using Recipes.Domain.Entities;
using Recipes.Infrustructure.Auth;
using Recipes.Infrustructure.Auth.Models;
using Recipes.Infrustructure.Files;
using Recipes.Infrustructure.Pagination;
using Recipes.Infrustructure.Security;
using System.Runtime;

namespace Recipes.Infrustructure
{
    public static class Register
    {
        public static IServiceCollection UseInfrustructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ISaltGenerator, SaltGenerator>();
            services.AddSingleton<IFileProvider, FilesInDbProvider>();
            services.AddSingleton<IPaginationProvider<RecipeCard>, RecipeCardPaginationProvider>();
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.Configure<JwtSecuritySettings>(
                options => configuration.GetSection(nameof(JwtSecuritySettings))
                .Bind(options));
            return services;
        }
    }
}
