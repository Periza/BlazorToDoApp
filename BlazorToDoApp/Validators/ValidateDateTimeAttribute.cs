using System.ComponentModel.DataAnnotations;

namespace BlazorToDoApp.Validators;

public class ValidateDateTimeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

        if (value is DateTime dateTime && dateTime != DateTime.MinValue)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Both Date and Time must be provided.");
    }

}
