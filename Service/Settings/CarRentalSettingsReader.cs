namespace Service.Settings;

public static class CarRentalSettingsReader
{
    public static CarRentalSettings Read(IConfiguration configuration) => new()
    {
        ServiceUri = configuration.GetValue<Uri>("Uri"),
        CarRentalDbContextConnectionString = configuration.GetValue<string>("CarRentalDbContext"),
        IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
        ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
        ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
    };
}
