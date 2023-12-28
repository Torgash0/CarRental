using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("rents")]
public class RentEntity : BaseEntity
{
    public required UserEntity User { get; set; }
    public virtual required ICollection<CarEntity> Cars { get; set; }
    public required DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public double? TotalPrice { get; set; }
    public ReviewEntity? Review { get; set; }
}
