using FleetManager.Data;
using FleetManager.Model.Validation;

namespace FleetManager.Domain.Concrete
{
    public class VehicleChassiValidationService : IVehicleChassiValidationService
    {
        public VehicleChassiValidationService(IVehicleRepository repository)
        {
            Repository = repository;
        }

        public IVehicleRepository Repository { get; }

        public bool IsValid(string chassi) =>
            // Não valida quando não foi preenchido
            string.IsNullOrEmpty(chassi) || !Repository.ChassiExists(chassi);
    }
}
