using System.ComponentModel.DataAnnotations;

namespace FleetManager.Model.Validation
{
    public class ChassiUniqueValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationService = (IChassiUniqueValidationService)validationContext.GetService(typeof(IChassiUniqueValidationService));
            var strValue = value as string;

            if (validationService.IsValid(strValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { nameof(Vehicle.Chassi) });
        }
    }
}
