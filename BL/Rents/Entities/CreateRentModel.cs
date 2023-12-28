namespace BL.Rents.Entities;

public class CreateRentModel
{
    public required Guid UserId { get; set; }
    public virtual required ICollection<Guid> CarsId { get; set; }
    public required DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public double? TotalPrice { get; set; }
    public Guid? ReviewId { get; set; }
}
