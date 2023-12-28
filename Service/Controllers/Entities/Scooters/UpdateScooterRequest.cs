﻿using System.ComponentModel.DataAnnotations;

namespace Service.Controllers.Entities.Scooters;

public class UpdateScooterRequest : IValidatableObject
{
    public double? Price { get; set; }
    public double? ChargePercentage { get; set; }
    public string? Location { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (Price < 0)
        {
            errors.Add(new ValidationResult($"{nameof(Price)} must have more 0"));
        }

        if (ChargePercentage < 0)
        {
            errors.Add(new ValidationResult($"{nameof(ChargePercentage)} must have more 0"));
        }

        return errors;
    }
}
