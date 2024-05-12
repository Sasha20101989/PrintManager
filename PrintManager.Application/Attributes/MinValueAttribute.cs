using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Attributes
{
    public class MinValueAttribute(int minValue) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || (int)value >= minValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The field {validationContext.DisplayName} must be greater than or equal to {minValue}.");
        }
    }
}
