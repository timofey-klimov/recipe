using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Core.Files;
using Recipes.Domain.Core.Services;
using Recipes.Infrustructure.Files;
using Recipes.Infrustructure.Security;

namespace Recipes.Infrustructure
{
    public static class Register
    {
        public static IServiceCollection UseInfrustructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ISaltGenerator, SaltGenerator>();
            services.AddSingleton<IFileProvider, FilesInDbProvider>();
            return services;
        }
    }
}
