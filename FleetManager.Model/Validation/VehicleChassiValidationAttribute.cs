using System.ComponentModel.DataAnnotations;

namespace FleetManager.Model.Validation
{
    public class VehicleChassiValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationService = (IVehicleChassiValidationService)validationContext.GetService(typeof(IVehicleChassiValidationService));
            var strValue = value as string;

            if (validationService.IsValid(strValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { nameof(Vehicle.Chassi) });
        }
    }
}
