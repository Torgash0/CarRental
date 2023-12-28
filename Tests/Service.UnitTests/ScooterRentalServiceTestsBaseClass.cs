using DataAccess.Entities;
using DataAccess.Repository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Moq;

using Service.UnitTests.Helpers;

namespace Service.UnitTests;

public class ScooterRentalServiceTestsBaseClass
{
    public ScooterRentalServiceTestsBaseClass()
    {
        var settings = TestSettingsHelper.GetSettings();

        _testServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(TestHttpClient);

                return httpClientFactoryMock.Object;
            }));

            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(TestHttpClient)
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var scooterRepository = scope.ServiceProvider.GetRequiredService<IRepository<ScooterEntity>>();
        var scooter = scooterRepository.Save(new ScooterEntity()
        {
            Price = 1000.0,
            ChargePercentage = 100.0
        });
        _testScooterId = scooter.Id;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _testServer.Dispose();
    }

    public T? GetService<T>()
    {
        return _testServer.Services.GetRequiredService<T>();
    }

    private readonly WebApplicationFactory<Program> _testServer;
    protected int _testScooterId;
    protected HttpClient TestHttpClient => _testServer.CreateClient();
}
