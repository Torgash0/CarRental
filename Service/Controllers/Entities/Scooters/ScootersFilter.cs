namespace Service.Controllers.Entities.Scooters;

public class ScootersFilter
{
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public double? MinChargePercentage { get; set; }
    public double? MaxChargePercentage { get; set; }
    public string? Location { get; set; }
}
