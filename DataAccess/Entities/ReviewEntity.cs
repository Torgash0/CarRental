using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("reviews")]
public class ReviewEntity : BaseEntity
{
    public string? Text { get; set; }
    public required int Rating { get; set; }
    public required int RentId { get; set; }
    public required RentEntity Rent { get; set; }
}
