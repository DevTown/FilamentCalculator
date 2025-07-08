using FilamentCalculator.Models;
using NUnit.Framework;

namespace FilamentCalcTest.Models
{
    [TestFixture]
    public class PrinterTest
    {
        [Test]
        public void AmotationCostPerHour_CorrectCalculation()
        {
            // Arrange
            var printer = new Printer
            {
                Price = 1000,
                PeriotOfAmortisation = 500m
            };

            // Act
            var costPerHour = printer.AmotationCostPerHour;

            // Assert
            Assert.That(2.0m, Is.EqualTo(costPerHour));
        }

        [Test]
        public void Properties_AreSetAndGetCorrectly()
        {
            // Arrange
            var printer = new Printer
            {
                PrinterId = 1,
                Name = "TestPrinter",
                ManufacturerName = "TestManufacturer",
                Price = 500,
                PeriotOfAmortisation = 250,
                FilamentDiameter = 1.75f,
                EnergyConsumptionW = 120
            };

            // Assert
            Assert.That(1,Is.EqualTo(printer.PrinterId));
            Assert.That("TestPrinter", Is.EqualTo(printer.Name));
            Assert.That("TestManufacturer", Is.EqualTo(printer.ManufacturerName));
            Assert.That(500, Is.EqualTo(printer.Price));
            Assert.That(250, Is.EqualTo(printer.PeriotOfAmortisation));
            Assert.That(1.75f, Is.EqualTo(printer.FilamentDiameter));
            Assert.That(120, Is.EqualTo(printer.EnergyConsumptionW));
        }

        [Test]
        public void AmotationCostPerHour_DivideByZero_ReturnsInfinity()
        {
            // Arrange
            var printer = new Printer
            {
                Price = 1000,
                PeriotOfAmortisation = 0m
            };

            // Act & Assert
            Assert.Throws<System.DivideByZeroException>(() => { var _ = printer.AmotationCostPerHour; });
        }
    }
}