using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Recipes.Infrustructure.Auth.Models;
using System.Reflection;

namespace Recipes.Web.DependencyInjection
{
    public static class Register
    {
        public static IServiceCollection UseWebApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            AddSwagger(services);
            AddJwtAuth(services, configuration);

            return services;
        }
        #region helpers
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Recipes App",
                    Description = "Asp.Net core app for managing recipes",
                });
                opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer auth",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                opts.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }});

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        private static void AddJwtAuth(IServiceCollection services, IConfiguration conf)
        {
            var jwtSettings = new JwtSecuritySettings();
            conf.GetSection(nameof(JwtSecuritySettings)).Bind(jwtSettings);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true,
                        IssuerSigningKey = jwtSettings.GetSymmetricKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
        }
        #endregion
    }
}
