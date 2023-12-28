using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Service.Controllers.Entities.Users;

public class CreateUserRequest : IValidatableObject
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add(new ValidationResult($"{nameof(Name)} must have at least 1 character."));
        }

        if (string.IsNullOrWhiteSpace(Surname))
        {
            errors.Add(new ValidationResult($"{nameof(Surname)} must have at least 1 character."));
        }

        if (!IsValidEmail())
        {
            errors.Add(new ValidationResult($"{nameof(Email)} is invalid."));
        }

        if (PhoneNumber is null || PhoneNumber.Length < 8)
        {
            errors.Add(new ValidationResult($"{nameof(PhoneNumber)} is invalid."));
        }

        if (Password is null || Password.Length < 8)
        {
            errors.Add(new ValidationResult($"{nameof(Password)} must have at least 8 character."));
        }

        return errors;
    }

    private bool IsValidEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            return false;
        }

        string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        Regex regex = new(emailPattern, RegexOptions.Compiled);

        return regex.IsMatch(Email);
    }
}
