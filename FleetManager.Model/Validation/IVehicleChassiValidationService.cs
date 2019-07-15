namespace FleetManager.Model.Validation
{
    public interface IVehicleChassiValidationService
    {
        bool IsValid(string chassi);
    }
}