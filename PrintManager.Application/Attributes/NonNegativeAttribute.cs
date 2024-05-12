using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Attributes
{
    public class NonNegativeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || (int)value >= 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The field {validationContext.DisplayName} must be a non-negative number.");
        }
    }
}
