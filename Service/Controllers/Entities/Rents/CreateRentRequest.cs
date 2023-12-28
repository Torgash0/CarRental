using System.ComponentModel.DataAnnotations;

namespace Service.Controllers.Entities.Rents;

public class CreateRentRequest : IValidatableObject
{
    public double? TotalPrice { get; set; }
    public required DateTime Start { get; set; }
    public DateTime? End { get; set; }

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
