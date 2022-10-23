namespace Recipes.Web.DependencyInjection
{
    public static class Register
    {
        public static IServiceCollection UseWebApp(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
