using DataAccess;

using Microsoft.EntityFrameworkCore;

using Service.Settings;

namespace Service.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services, CarRentalSettings settings)
    {
        services.AddDbContextFactory<CarRentalDbContext>(
            options => options.UseSqlServer(settings.CarRentalDbContextConnectionString),
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CarRentalDbContext>>();
        using var context = contextFactory.CreateDbContext();

        context.Database.Migrate();
    }
}
