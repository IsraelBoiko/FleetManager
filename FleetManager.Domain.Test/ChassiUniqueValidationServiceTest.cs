using FleetManager.Data;
using FleetManager.Domain.Concrete;
using FleetManager.Model.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FleetManager.Domain.Test
{
    [TestClass]
    public class ChassiUniqueValidationServiceTest
    {
        public Mock<IVehicleRepository> RepositoryMock { get; set; }

        public IChassiUniqueValidationService Service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RepositoryMock = new Mock<IVehicleRepository>();
            Service = new ChassiUniqueValidationService(RepositoryMock.Object);
        }

        [TestMethod]
        public void No_Check_Without_Chassi()
        {
            // Arrange
            string chassi = null;

            RepositoryMock.Setup(r => r.ChassiExists(It.IsAny<string>())).Returns(false);

            // Act
            var valid = Service.IsValid(chassi);

            // Assert
            Assert.IsTrue(valid, "Não valida sem chassi.");
            RepositoryMock.Verify(r => r.ChassiExists(It.IsAny<string>()), Times.Never(), "Não verifica a existência de chassi.");
        }

        [TestMethod]
        public void No_Check_Empty_Chassi()
        {
            // Arrange
            var chassi = string.Empty;

            RepositoryMock.Setup(r => r.ChassiExists(It.IsAny<string>())).Returns(false);

            // Act
            var valid = Service.IsValid(chassi);

            // Assert
            Assert.IsTrue(valid, "Não valida chassi vazio.");
            RepositoryMock.Verify(r => r.ChassiExists(It.IsAny<string>()), Times.Never(), "Não verifica a existência de chassi.");
        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Check_Chassi_In_Repository(bool exists)
        {
            // Arrange
            var chassi = "ABC";

            RepositoryMock.Setup(r => r.ChassiExists(It.IsAny<string>())).Returns(exists);

            // Act
            var valid = Service.IsValid(chassi);

            // Assert
            Assert.AreEqual(!exists, valid, "Verifica se o chassi existe no repositório.");
            RepositoryMock.Verify(r => r.ChassiExists(It.IsAny<string>()), Times.Once(), "Verifica a existência de chassi.");
            RepositoryMock.Verify(r => r.ChassiExists(chassi), Times.Once(), "Verifica a existência apenas do chassi solicitado.");
        }
    }
}
