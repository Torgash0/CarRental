using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Service.Settings;

namespace DataAccess.UnitTests;

public class RepositoryTestsBaseClass
{
    protected readonly CarRentalSettings _settings;
    protected readonly IDbContextFactory<CarRentalDbContext> _dbContextFactory;
    protected readonly IServiceProvider _serviceProvider;

    public RepositoryTestsBaseClass()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();

        _settings = CarRentalSettingsReader.Read(configuration);
        _serviceProvider = ConfigureServiceProvider;
        _dbContextFactory = _serviceProvider.GetRequiredService<IDbContextFactory<CarRentalDbContext>>();
    }

    private IServiceProvider ConfigureServiceProvider
    {
        get
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContextFactory<CarRentalDbContext>(
                options => options.UseSqlServer(_settings.ScooterRentalDbContextConnectionString),
                        ServiceLifetime.Scoped);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
