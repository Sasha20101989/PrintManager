using PrintManager.Application.Properties;
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

            return new ValidationResult(string.Format(AttributesResources.ErrorNonNegativeValidationMessage, validationContext.DisplayName));
        }
    }
}
