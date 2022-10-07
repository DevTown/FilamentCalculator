using System.Collections.Generic;
using FilamentCalculator.Models;
using NUnit.Framework;

namespace FilamentCalcTest.Views
{
    [TestFixture] 
    public class CalculatorViewModelTest
    {

        [Test]
        public void TestCalc()
        {
            var testitem = GenerateTestViewModel();
            testitem.weight = 100;
            
            Assert.That(testitem.costs, Is.EqualTo(0));
            
            testitem.Calculate();
            
            Assert.That(testitem.costs, Is.EqualTo(2.626M));
        }

        [Test]
        public void TestCalcEnergy()
        {
             var testitem = GenerateTestViewModel();
            testitem.weight = 100;
            
            Assert.That(testitem.energyCosts, Is.EqualTo(0));
            
            testitem.Calculate();
            
            Assert.That(testitem.energyCosts, Is.EqualTo(0.126M));
        }

        [Test]
        public void TestGetWeight()
        {
            var testitem = GenerateTestViewModel();


            Assert.That(testitem.weight, Is.EqualTo(0));
            Assert.That(testitem.lengthmm, Is.EqualTo(100));

            testitem.GetWeight();

            
            Assert.That(testitem.weight, Is.EqualTo(0.301));
            Assert.That(testitem.lengthmm, Is.EqualTo(100));

        }

        private CalculatorViewModel GenerateTestViewModel()
        {
            var testitem = new CalculatorViewModel {weight = 0, lengthmm = 100, SelectedFilament = 1, printtimeh=120 };

            var filaments = new List<Filament>
            {
                new() {FilamentId = 1, Diameter = 1.75f, Price = 20, SpoolWeight = 800}
            };
            testitem.Filaments = filaments;

            var settings = new Settings {PrinterEnergyUsageW = 300, Energiekosts = (decimal)0.21};

            testitem.Settings = settings;
            
            return testitem;
        }
    }
}