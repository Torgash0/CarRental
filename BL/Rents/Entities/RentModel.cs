using BL.Cars.Entities;

namespace BL.Rents.Entities;

public class RentModel
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public virtual required ICollection<CarModel> Cars { get; set; }
    public required DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public double? TotalPrice { get; set; }
    public Guid? ReviewId { get; set; }
}
