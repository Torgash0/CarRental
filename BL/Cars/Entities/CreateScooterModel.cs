namespace BL.Scooters.Entities;

public class CreateScooterModel
{
    public required double Price { get; set; }
    public required double ChargePercentage { get; set; }
    public string? Location { get; set; }
}
