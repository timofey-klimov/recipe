using Recipes.Application.DependencyInjection;
using Recipes.Persistance.DependencyInjection;
using Recipes.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .UseApplication(builder.Configuration)
        .UsePersistance(builder.Configuration)
        .UseWebApp(builder.Configuration);

    builder.Services.AddControllers();
}
var app = builder.Build();
{
    app.MapControllers();
    app.Run();
}

