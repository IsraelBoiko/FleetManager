using FleetManager.Model;

namespace FleetManager.Data
{
    /// <summary>
    /// Repositório de veículos.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Verifica existência de um determinado chassi.
        /// </summary>
        /// <param name="chassi">O chassi a ser verificado.</param>
        /// <returns>True se o chassi existe, senão False.</returns>
        bool ChassiExists(string chassi);

        /// <summary>
        /// Adiciona um veículo.
        /// </summary>
        /// <param name="model">O veículo a ser adicionado.</param>
        void Add(Vehicle model);

        /// <summary>
        /// Busca um veículo pelo chassi.
        /// </summary>
        /// <param name="chassi">O chassi a ser procurado.</param>
        /// <returns>O veículo localizado, senão nulo.</returns>
        Vehicle ChassiFind(string chassi);

        /// <summary>
        /// Atualiza um veículo.
        /// </summary>
        /// <param name="model">O veículo a ser atualizado.</param>
        void Update(Vehicle model);

        /// <summary>
        /// Remove um veículo.
        /// </summary>
        /// <param name="model">O veículo a ser removido.</param>
        void Remove(Vehicle model);
    }
}
