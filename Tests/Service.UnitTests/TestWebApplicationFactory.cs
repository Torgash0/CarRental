using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Service.UnitTests;

public class TestWebApplicationFactory(Action<IServiceCollection>? overrideDependencies = null) : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _overrideDependencies = overrideDependencies;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => _overrideDependencies?.Invoke(services));
    }
}
