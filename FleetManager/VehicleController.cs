using FleetManager.Domain;
using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            var continuar = true;

            while (continuar)
            {
                Console.Write(@"
Opções:
 1. Inserir veículo
 2. Editar veículo
 3. Deletar veículo
 4. Listar veículos
 5. Pesquisa por chassi
 ESC/0 - Sair

Entrada: ");

                var key = ReadValidKey("012345", allowEscape: true);

                switch (key)
                {
                    case '0':
                    case '\u001b':
                        // Sair
                        continuar = false;
                        break;
                    case '1':
                        Console.WriteLine("* Inserir veículo *\n");
                        Inserir();
                        break;
                    case '2':
                        Console.WriteLine("* Editar veículo *\n");
                        //Editar();
                        break;
                    case '3':
                        Console.WriteLine("* Deletar veículo *\n");
                        //Deletar();
                        break;
                    case '4':
                        Console.WriteLine("* Listar veículos *\n");
                        //Listar();
                        break;
                    case '5':
                        Console.WriteLine("* Pesquisar veículo por chassi *\n");
                        Pesquisar();
                        break;
                    default:
                        break;
                }
            }
        }

        private Vehicle Pesquisar()
        {
            Console.Write("Informe o chassi do veículo: ");
            var chassi = Console.ReadLine();

            var veiculo = Service.ChassiFind(chassi);

            Console.WriteLine();

            if (veiculo == null)
            {
                Console.Error.WriteLine("Veículo não encontrado!");

                return veiculo;
            }

            Console.Out.ShowVehicle(veiculo);

            return veiculo;
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
        }

        private void Inserir()
        {
            Console.WriteLine("Informe os dados do veículo:");
            Console.Write("Informe o chassi: ");
            var chassi = Console.ReadLine();

            Console.Write("Informe o tipo (1 - Ônibus, 2 - Caminhão): ");

            var option = ReadValidKey("12", showValidKey: false);
            var tipo = option == '1' ? VehicleType.Bus : VehicleType.Truck;

            if (tipo == VehicleType.Bus)
            {
                Console.WriteLine("1 - Ônibus");
            }
            else
            {
                Console.WriteLine("2 - Caminhão");
            }

            Console.Write("Informe a cor: ");
            var cor = Console.ReadLine();
            var veiculo = new Vehicle(chassi, tipo, cor);

            var resposta = Service.Add(veiculo);

            Console.WriteLine();

            if (!resposta.HasErros())
            {
                Console.WriteLine("Veículo adicionado com sucesso!");

                // Finaliza
                return;
            }

            Console.WriteLine("O veículo informado possui os seguintes erros:");
            Console.Error.ShowErrors(resposta);

            Console.WriteLine();
        }

        private char ReadValidKey(string validKeys, bool showValidKey = true, bool allowEscape = false)
        {
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Modifiers != 0)
                {
                    continue;
                }

                if (allowEscape && key.Key == ConsoleKey.Escape)
                {
                    if (showValidKey)
                    {
                        Console.WriteLine("ESC"); 
                    }

                    return key.KeyChar;
                }

                if (validKeys.Contains(key.KeyChar))
                {
                    if (showValidKey)
                    {
                        Console.WriteLine(key.KeyChar); 
                    }

                    return key.KeyChar;
                }
            }
        }
    }
}
