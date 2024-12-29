using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.Validators.Utils;

public class MatchListAttribute : ValidationAttribute
{
    private readonly List<string> _validStrings;

    public MatchListAttribute(params string[] validStrings)
    {
        _validStrings = new List<string>(validStrings);
    }

    public override bool IsValid(object value)
    {
        var stringValue = value as string;
        return _validStrings.Contains(stringValue);
    }
}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Get the current date
        DateTime currentDate = DateTime.Today;

        if (value != null)
        {
            DateTime inputDate = (DateTime)value;

            // Check if the input date is greater than the current date
            if (inputDate > currentDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The date must be greater than the current date.");
            }
        }

        return ValidationResult.Success; // Allow null values if needed
    }
}