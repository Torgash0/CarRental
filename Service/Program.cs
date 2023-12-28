using Service.IoC;
using Service.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var settings = CarRentalSettingsReader.Read(configuration);
var builder = WebApplication.CreateBuilder();

builder.Services.AddControllers();

AuthorizationConfigurator.ConfigureServices(builder.Services, settings);
DbContextConfigurator.ConfigureService(builder.Services, settings);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);
MapperConfigurator.ConfigureServices(builder.Services);
ServicesConfigurator.ConfigureService(builder.Services, settings);

var app = builder.Build();

SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);
DbContextConfigurator.ConfigureApplication(app);
AuthorizationConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
