using BL.Rents.Entities;

namespace BL.Cars.Entities;

public class CarModel
{
    public required Guid Id { get; set; }
    public required double Price { get; set; }
    public required double ChargePercentage { get; set; }
    public string? Location { get; set; }
    public virtual ICollection<RentModel>? Rents { get; set; }
}
