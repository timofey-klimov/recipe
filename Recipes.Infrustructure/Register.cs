using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Recipes.Application.Core.Auth;
using Recipes.Application.Core.Files;
using Recipes.Application.Core.Identity;
using Recipes.Application.Core.Pagination;
using Recipes.Domain.Core.Services;
using Recipes.Domain.Entities;
using Recipes.Infrustructure.Auth;
using Recipes.Infrustructure.Auth.Models;
using Recipes.Infrustructure.BackgroundJobs;
using Recipes.Infrustructure.Caching;
using Recipes.Infrustructure.Files;
using Recipes.Infrustructure.Identity;
using Recipes.Infrustructure.Pagination;
using Recipes.Infrustructure.Security;
using Recipes.Persistance.Repositories.Cache.Core;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.Configure<JwtSecuritySettings>(
                options => configuration.GetSection(nameof(JwtSecuritySettings))
                .Bind(options));
            services.AddScoped<IPhysicalFileProvider, PhysicalFileProvider>();
            services.AddScoped<IFileProviderFactory, FileProviderFactory>();
            services.AddScoped<ICachingProvider, CachingProvider>();
            services.AddSingleton<IGuidProvider, GuidProvider>();
            services.AddScoped<IPermissionProvider, PermissionProvider>();
            services.AddMemoryCache();
            services.AddQuartz(conf =>
            {
                conf.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey(nameof(ProccessOutboxMessagesJob));

                conf.AddJob<ProccessOutboxMessagesJob>(jobKey)
                    .AddTrigger(trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(shedule =>
                                shedule.WithIntervalInSeconds(60)
                                    .RepeatForever()));
            });
            services.AddQuartzHostedService();
            return services;
        }
    }
}
