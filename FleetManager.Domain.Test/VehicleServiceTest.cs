using FleetManager.Data;
using FleetManager.Domain.Concrete;
using FleetManager.Model;
using FleetManager.Model.Validation;
using FleetManager.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace FleetManager.Domain.Test
{
    [TestClass]
    public class VehicleServiceTest
    {
        public Mock<IVehicleRepository> RepositoryMock { get; set; }

        public Mock<IVehicleChassiValidationService> ChassiValidationServiceMock { get; set; }

        public Mock<IServiceProvider> ServiceProviderMock { get; set; }

        public IVehicleService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RepositoryMock = new Mock<IVehicleRepository>();
            ServiceProviderMock = new Mock<IServiceProvider>();

            Service = new VehicleService(RepositoryMock.Object, ServiceProviderMock.Object);
        }

        #region Add

        private void Config(bool isValidChassi)
        {
            ChassiValidationServiceMock = new Mock<IVehicleChassiValidationService>();
            ServiceProviderMock.Setup(p => p.GetService(typeof(IVehicleChassiValidationService))).Returns(ChassiValidationServiceMock.Object);
            ChassiValidationServiceMock.Setup(r => r.IsValid(It.IsAny<string>())).Returns(isValidChassi);
        }

        [TestMethod]
        public void Add_Accept_New_Vehicle()
        {
            // Arrange
            var vehicle = new Vehicle(chassi: "ABC", VehicleType.Bus, color: "Azul");

            Config(isValidChassi: true);

            // Act
            var result = Service.Add(vehicle);

            // Assert
            Assert.IsNotNull(result, "Haver retorno.");
            Assert.IsTrue(result.Count == 0, "Nenhum resultado de validação.");
            RepositoryMock.Verify(r => r.Add(It.IsAny<Vehicle>()), Times.Once(), "Adicionado ao repositório.");
        }

        [TestMethod]
        public void Add_Not_Accept_When_Exist_Chassi()
        {
            // Arrange
            var vehicle = new Vehicle(chassi: "ABC", VehicleType.Bus, color: "Azul");

            Config(isValidChassi: false);

            // Act
            var result = Service.Add(vehicle);

            // Assert
            Assert.IsNotNull(result, "Haver retorno.");
            Assert.IsFalse(result.Count == 0, "Possuir resultados de validação.");

            var validation = result.FirstOrDefault(v => v.MemberNames.Any(p => p == nameof(Vehicle.Chassi)));
            Assert.IsNotNull(validation, "Haver mensagem de validação de chassi.");

            var errorMessage = string.Format(Messages.Vehicle_Chassi_Exists, Names.Vehicle_Chassi);
            Assert.AreEqual(errorMessage, validation.ErrorMessage, "Mensagem de validação de chassi existente.");
        }

        [TestMethod]
        public void Add_Not_Accept_Without_Chassi()
        {
            // Arrange
            var vehicle = new Vehicle(chassi: null, VehicleType.Bus, color: "Azul");

            Config(isValidChassi: true);

            // Act
            var result = Service.Add(vehicle);

            // Assert
            Assert.IsNotNull(result, "Haver retorno.");
            Assert.IsFalse(result.Count == 0, "Possuir resultados de validação.");

            var validation = result.FirstOrDefault(v => v.MemberNames.Any(p => p == nameof(Vehicle.Chassi)));
            Assert.IsNotNull(validation, "Haver mensagem de validação de chassi.");

            var errorMessage = string.Format(Messages.Required, Names.Vehicle_Chassi);
            Assert.AreEqual(errorMessage, validation.ErrorMessage, "Mensagem de validação de chassi obrigatório.");
        }

        [TestMethod]
        public void Add_Not_Accept_Empty_Chassi()
        {
            // Arrange
            var vehicle = new Vehicle(chassi: "", VehicleType.Bus, color: "Azul");

            Config(isValidChassi: true);

            // Act
            var result = Service.Add(vehicle);

            // Assert
            Assert.IsNotNull(result, "Haver retorno.");
            Assert.IsFalse(result.Count == 0, "Possuir resultados de validação.");

            var validation = result.FirstOrDefault(v => v.MemberNames.Any(p => p == nameof(Vehicle.Chassi)));
            Assert.IsNotNull(validation, "Haver mensagem de validação de chassi.");

            var errorMessage = string.Format(Messages.Required, Names.Vehicle_Chassi);
            Assert.AreEqual(errorMessage, validation.ErrorMessage, "Mensagem de validação de chassi obrigatório.");
        }

        [TestMethod]
        public void Add_Not_Accept_Invalid_Type()
        {
            // Arrange
            var vehicle = new Vehicle(chassi: "ABC", type: null, color: "Azul");

            Config(isValidChassi: true);

            // Act
            var result = Service.Add(vehicle);

            // Assert
            Assert.IsNotNull(result, "Haver retorno.");
            Assert.IsFalse(result.Count == 0, "Possuir resultados de validação.");

            var validation = result.FirstOrDefault(v => v.MemberNames.Any(p => p == nameof(Vehicle.Type)));
            Assert.IsNotNull(validation, "Haver mensagem de validação de tipo.");

            var errorMessage = string.Format(Messages.Required, Names.Vehicle_Type);
            Assert.AreEqual(errorMessage, validation.ErrorMessage, "Mensagem de validação de tipo obrigatório.");
        }

        #endregion

        #region ChassiFind

        [TestMethod]
        [DataRow("ABC")]
        [DataRow("DEF")]
        public void ChassiFind_Not_Found_By_The_Repository(string chassi)
        {
            // Arrange
            RepositoryMock.Setup(r => r.ChassiFind(It.IsAny<string>())).Returns<Vehicle>(null);

            // Act
            var result = Service.ChassiFind(chassi);

            // Assert
            Assert.IsNull(result, "Veículo não encontrado.");
            RepositoryMock.Verify(r => r.ChassiFind(It.IsAny<string>()), Times.Once(), "Realizou a pesquisa no repositório.");
            RepositoryMock.Verify(r => r.ChassiFind(chassi), Times.Once(), "Pesquisou apenas o chassi solicitado.");
        }

        [TestMethod]
        [DataRow("ABC", VehicleType.Bus, "Azul")]
        [DataRow("DEF", VehicleType.Truck, "Branco")]
        public void ChassiFind_Found_By_The_Repository(string chassi, VehicleType type, string color)
        {
            // Arrange
            var expected = new Vehicle(chassi, type, color);
            RepositoryMock.Setup(r => r.ChassiFind(chassi)).Returns(expected);

            // Act
            var result = Service.ChassiFind(chassi);

            // Assert
            Assert.IsNotNull(result, "Veículo encontrado.");
            Assert.AreEqual(expected, result, "Retorna o veículo do repositório.");
            Assert.AreEqual(chassi, result.Chassi, "Mesmo chassi.");
            Assert.AreEqual(type, result.Type, "Mesmo tipo.");
            Assert.AreEqual(color, result.Color, "Mesma cor.");
            RepositoryMock.Verify(r => r.ChassiFind(It.IsAny<string>()), Times.Once(), "Realizou a pesquisa no repositório.");
            RepositoryMock.Verify(r => r.ChassiFind(chassi), Times.Once(), "Pesquisou apenas o chassi solicitado.");
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void ChassiFind_Not_Find_Repositori_Without_Chassi(string chassi)
        {
            // Arrange

            // Act
            var result = Service.ChassiFind(chassi);

            // Assert
            Assert.IsNull(result, "Veículo não encontrado.");
            RepositoryMock.Verify(r => r.ChassiFind(It.IsAny<string>()), Times.Never(), "Não realizou a pesquisa no repositório.");
        }

        #endregion

        [TestMethod]
        public void Update_Save_To_Repository()
        {
            // Arrance
            var vehicle = new Vehicle("ABC", VehicleType.Bus, "Azul");

            // Act
            var result = Service.Update(vehicle);

            // Assert
            Assert.IsNotNull(result, "Haver retorno.");
            Assert.IsTrue(result.Count == 0, "Nenhum resultado de validação.");
        }
    }
}
