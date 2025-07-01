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
            Assert.AreEqual(2.0m, costPerHour);
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
            Assert.AreEqual(1, printer.PrinterId);
            Assert.AreEqual("TestPrinter", printer.Name);
            Assert.AreEqual("TestManufacturer", printer.ManufacturerName);
            Assert.AreEqual(500, printer.Price);
            Assert.AreEqual(250, printer.PeriotOfAmortisation);
            Assert.AreEqual(1.75f, printer.FilamentDiameter);
            Assert.AreEqual(120, printer.EnergyConsumptionW);
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