using Microsoft.EntityFrameworkCore;
using Recipes.Application.DependencyInjection;
using Recipes.Infrustructure;
using Recipes.Persistance;
using Recipes.Persistance.DependencyInjection;
using Recipes.Web.DependencyInjection;
using Recipes.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .UseApplication(builder.Configuration)
        .UsePersistance(builder.Configuration, 
            builder.Environment.IsProduction())
        .UseInfrustructure(builder.Configuration)
        .UseWebApp(builder.Configuration);
}
var app = builder.Build();
{
    await Migrate(app.Services);

    app.UseMiddleware<ExceptionHandler>();
    app.UseCors(opts =>
    {
        opts.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(opts => opts.SerializeAsV2 = true);
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
    }

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
    app.MapControllers();
    app.Run();
}

async Task Migrate(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogCritical($"Fail migrate Db");
            logger.LogCritical(ex.Message);
        }
    }
}