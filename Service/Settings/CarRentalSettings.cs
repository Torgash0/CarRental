namespace Service.Settings;

public class CarRentalSettings
{
    public required Uri ServiceUri { get; set; }
    public required string CarRentalDbContextConnectionString { get; set; }
    public required string IdentityServerUri { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}
