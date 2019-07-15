using FleetManager.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FleetManager.Domain
{
    /// <summary>
    /// Serviços de manutenção de veículo.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Adiciona veículo.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns>Lista vazia quando sucesso, senão lista com os resultados de validação.</returns>
        IList<ValidationResult> Add(Vehicle vehicle);

        /// <summary>
        /// Busca um veículo pelo chassi.
        /// </summary>
        /// <param name="chassi"></param>
        /// <returns>O veículo encontraso, senão nullo.</returns>
        Vehicle ChassiFind(string chassi);

        /// <summary>
        /// Atualiza um veículo.
        /// </summary>
        /// <param name="vehicle">Veículo a ser atualizado.</param>
        /// <returns>Lista vazia quando sucesso, senão lista com os resultados de validação.</returns>
        IList<ValidationResult> Update(Vehicle vehicle);

        /// <summary>
        /// Remove um veículo
        /// </summary>
        /// <param name="vehicle">Veículo a ser removido.</param>
        void Remove(Vehicle vehicle);

        /// <summary>
        /// Lista todos os veículos
        /// </summary>
        /// <returns></returns>
        IQueryable<Vehicle> All();
    }
}
