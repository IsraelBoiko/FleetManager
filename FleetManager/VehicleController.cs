using FleetManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManager
{
    public class VehicleController
    {
        public VehicleController(IVehicleService service)
        {
            Service = service;
        }

        public IVehicleService Service { get; }

        /// <summary>
        /// Apresenta o menu principal.
        /// </summary>
        public void Show()
        {
            var validKey = true;

            while (true)
            {
                if (validKey)
                {
                    Console.WriteLine(@"
Opções:
 1. Inserir veículo
 2. Editar veículo
 3. Deletar veículo
 4. Listar veículos
 5. Pesquisa por chassi
 ESC/0. Sair");
                }

                var key = Console.ReadKey(true);

                if (key.Modifiers != 0)
                {
                    continue;
                }

                if (key.Key == ConsoleKey.Escape || key.KeyChar == '0')
                {
                    break;
                }

                validKey = true;

                switch (key.KeyChar)
                {
                    case '1':
                        Inserir();
                        break;
                    case '2':
                        Editar();
                        break;
                    case '3':
                        Deletar();
                        break;
                    case '4':
                        Listar();
                        break;
                    case '5':
                        Pesquisar();
                        break;
                    default:
                        validKey = false;
                        break;
                }
            }
        }

        private void Pesquisar()
        {
            throw new NotImplementedException();
        }

        private void Listar()
        {
            throw new NotImplementedException();
        }

        private void Deletar()
        {
            throw new NotImplementedException();
        }

        private void Editar()
        {
            throw new NotImplementedException();
        }

        private void Inserir()
        {
            throw new NotImplementedException();
        }
    }
}
