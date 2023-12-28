using System.ComponentModel.DataAnnotations;

using BL.Scooters.Entities;

namespace Service.Controllers.Entities.Rents;

public class UpdateRentRequest : IValidatableObject
{
    public virtual ICollection<ScooterModel>? Scooters { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public double? TotalPrice { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (TotalPrice != null && TotalPrice < 0)
        {
            errors.Add(new ValidationResult($"{nameof(TotalPrice)} must have more 0."));
        }

        return errors;
    }
}
