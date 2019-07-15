using FleetManager.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace FleetManager
{
    public static class ShowHelpers
    {
        public static void ShowErrors(this TextWriter writer, IList<ValidationResult> results)
        {
            foreach (var result in results)
            {
                writer.WriteLine(result.ErrorMessage);
            }
        }

        public static void ShowVehicle(this TextWriter writer, Vehicle vehicle)
        {
            var tipo = vehicle.Type == VehicleType.Bus ? "Ônibus" : "Caminhão";

            writer.WriteLine("Dados do veículo:\n");
            writer.Write("Chassi: ");
            writer.WriteLine(vehicle.Chassi);
            writer.WriteLine($"Tipo: {tipo}");
            writer.Write("Cor: ");
            writer.WriteLine(vehicle.Color);
            writer.WriteLine();
        }
    }
}
