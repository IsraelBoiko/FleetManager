using FleetManager.Resources;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Model
{
    public enum VehicleType
    {
        [Display(ResourceType = typeof(Names), Name = nameof(VehicleType) + "_" + nameof(Bus))]
        Bus,
        [Display(ResourceType = typeof(Names), Name = nameof(VehicleType) + "_" + nameof(Truck))]
        Truck,
    }
}
