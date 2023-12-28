using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("cars")]
public class CarsEntity : BaseEntity
{
    public required double Price { get; set; }
    public required double ChargePercentage { get; set; }
    public string? Location { get; set; }
    public virtual ICollection<RentEntity>? Rents { get; set; }
}
