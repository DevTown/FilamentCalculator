using FilamentCalculator.Data;
using FilamentCalculator.Models;
using FilamentCalculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FilamentCalcTest.Models
{
    [TestFixture]
    public class PrinterViewModelTests
    {
        private FilamentCalcContext _context;
        private DbContextOptions<FilamentCalcContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FilamentCalcContext>()
                .UseInMemoryDatabase(databaseName: "TestPrinterDb")
                .Options;

            _context = new FilamentCalcContext(_options);
            
            // Testdaten vorbereiten
            var printer = new Printer
            {
                PrinterId = 1,
                Name = "TestPrinter",
                ManufacturerName = "TestManufacturer",
                Price = 1000,
                EnergyConsumptionW = 300,
                PeriotOfAmortisation = 5000
            };
            _context.Printers.Add(printer);
            _context.SaveChanges();
        }

        [Test]
        public void PrinterViewModel_DefaultConstructor_CreatesEmptyPrinter()
        {
            // Arrange & Act
            var viewModel = new PrinterViewModel();

            // Assert
            Assert.That(viewModel.Printer, Is.Not.Null);
            Assert.That(viewModel.Printer.Name, Is.Null);
        }

        [Test]
        public void PrinterViewModel_ContextConstructor_CreatesEmptyPrinter()
        {
            // Arrange & Act
            var viewModel = new PrinterViewModel(_context);

            // Assert
            Assert.That(viewModel.Printer, Is.Not.Null);
            Assert.That(viewModel.Printer.Name, Is.Null);
        }

        [Test]
        public void PrinterViewModel_IdConstructor_LoadsExistingPrinter()
        {
            // Arrange & Act
            var viewModel = new PrinterViewModel(1, _context);

            // Assert
            Assert.That(viewModel.Printer, Is.Not.Null);
            Assert.That(viewModel.Printer.Name, Is.EqualTo("TestPrinter"));
            Assert.That(viewModel.Printer.ManufacturerName, Is.EqualTo("TestManufacturer"));
            Assert.That(viewModel.Printer.Price, Is.EqualTo(1000));
            Assert.That(viewModel.Printer.EnergyConsumptionW, Is.EqualTo(300));
            Assert.That(viewModel.Printer.PeriotOfAmortisation, Is.EqualTo(5000));
        }

        [Test]
        public void PrinterViewModel_DiameterList_ContainsCorrectValues()
        {
            // Arrange & Act
            var viewModel = new PrinterViewModel();

            // Assert
            Assert.That(viewModel.DiameterList, Is.Not.Null);
            Assert.That(viewModel.DiameterList.Count, Is.EqualTo(2));
            Assert.That(viewModel.DiameterList[0].Value, Is.EqualTo("1,75"));
            Assert.That(viewModel.DiameterList[0].Text, Is.EqualTo("1.75"));
            Assert.That(viewModel.DiameterList[1].Value, Is.EqualTo("2,85"));
            Assert.That(viewModel.DiameterList[1].Text, Is.EqualTo("2.85"));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}