using FleetManager.Resources;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Model
{
    public class Vehicle
    {
        public Vehicle(string chassi, VehicleType? type, string color)
        {
            Chassi = chassi;
            Type = type;
            Color = color;
        }

        // For ORM
        protected Vehicle()
        {
        }

        public int Id { get; protected set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Validation.VehicleChassiValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Vehicle_Chassi_Exists")]
        [Display(ResourceType = typeof(Names), Name = nameof(Vehicle) + "_" + nameof(Chassi))]
        public string Chassi { get; protected set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Names), Name = nameof(Vehicle) + "_" + nameof(Type))]
        public VehicleType? Type { get; protected set; }

        public int Passengers
        {
            get
            {
                switch (Type)
                {
                    case VehicleType.Bus:
                        return 42;
                    case VehicleType.Truck:
                        return 2;
                    default:
                        return 0;
                }
            }
        }

        public string Color { get; set; }
    }
}
