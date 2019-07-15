using FleetManager.Data;
using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Domain.Concrete
{
    /// <summary>
    /// Implementação dos serviços de manutenção de veículos.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        public VehicleService(IVehicleRepository repository, IServiceProvider serviceProvider)
        {
            Repository = repository;
            ServiceProvider = serviceProvider;
        }

        public IVehicleRepository Repository { get; }
        public IServiceProvider ServiceProvider { get; }

        public IList<ValidationResult> Add(Vehicle model)
        {
            var validations = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, ServiceProvider, null);
            var success = Validator.TryValidateObject(model, validationContext, validations, true);

            if (success)
            {
                Repository.Add(model);
            }

            return validations;
        }

        public Vehicle ChassiFind(string chassi) => string.IsNullOrEmpty(chassi) ? null : Repository.ChassiFind(chassi);

        public IList<ValidationResult> Update(Vehicle model)
        {
            Repository.Update(model);

            return Array.Empty<ValidationResult>();
        }
    }
}
