using System.Collections.Generic;
using FilamentCalculator.Models;
using FilamentCalculator.ViewModels;
using NUnit.Framework;
using System;

namespace FilamentCalcTest.ViewModels
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
            
            Assert.That(testitem.costs, Is.EqualTo(27.381m));
        }

        [Test]
        public void TestCalcEnergy()
        {
             var testitem = GenerateTestViewModel();
            testitem.weight = 100;
            
            Assert.That(testitem.energyCosts, Is.EqualTo(0));
            
            testitem.Calculate();
            
            Assert.That(testitem.energyCosts, Is.EqualTo(0.756m));
        }

       

        [Test]
        public void TestFalschSzenarien()
        {
            // Test 1: Negative Gewichtswerte
            var testitem1 = GenerateTestViewModel();
            testitem1.weight = -50;
            testitem1.Calculate();
            
            // Kosten sollten trotz negativem Gewicht berechnet werden (könnte ein Bug sein)
            Assert.That(testitem1.filamentCosts, Is.LessThan(0), "Negative Gewichtswerte sollten negative Filamentkosten ergeben");
            
           
            // Test 3: Sehr große Werte
            var testitem3 = GenerateTestViewModel();
            testitem3.weight = 1000000; // 1 Tonne
            testitem3.printtime = 1000000; // Sehr lange Druckzeit
            testitem3.Calculate();
            
            // Kosten sollten sehr hoch sein aber nicht unendlich
            Assert.That(testitem3.costs, Is.GreaterThan(0), "Sehr große Werte sollten positive Kosten ergeben");
            Assert.That(testitem3.costs, Is.LessThan(decimal.MaxValue), "Kosten sollten nicht unendlich sein");
            
            // Test 4: Ungültige Filament-ID
            var testitem4 = GenerateTestViewModel();
            testitem4.SelectedFilament = 999; // Nicht existierende ID
            
            // Sollte eine Exception werfen
            Assert.Throws<InvalidOperationException>(() => testitem4.Calculate(), 
                "Ungültige Filament-ID sollte eine Exception werfen");
            
            // Test 5: Null Settings
            var testitem5 = GenerateTestViewModel();
            testitem5.Settings = null;
            
            // Sollte eine NullReferenceException werfen
            Assert.Throws<NullReferenceException>(() => testitem5.Calculate(), 
                "Null Settings sollten eine NullReferenceException werfen");
            
            // Test 6: Sehr kleine aber positive Werte
            var testitem6 = GenerateTestViewModel();
            testitem6.weight = 0.001M; // 1 mg
            testitem6.printtime = 0.1M; // 6 Sekunden
            testitem6.Calculate();
            
            // Kosten sollten sehr klein aber größer als 0 sein
            Assert.That(testitem6.costs, Is.GreaterThan(0), "Sehr kleine positive Werte sollten positive Kosten ergeben");
            Assert.That(testitem6.costs, Is.LessThan(1), "Sehr kleine Werte sollten sehr kleine Kosten ergeben");
        }

        
        [Test]
        public void TestCalculateWithManufacturingWork()
        {
            var testitem = GenerateTestViewModel();
            testitem.manufacurworktime = 60; // 1 Stunde Arbeit
            
            testitem.Calculate();
            
            // Manufacturing costs sollten berechnet werden: (25€/h / 60) * 60 min = 25€
            Assert.That(testitem.manufacturingCosts, Is.EqualTo(25.0M), "Manufacturing costs sollten korrekt berechnet werden");
        }

        [Test]
        public void TestCalculateWithExtendedMaterialCosts()
        {
            var testitem = GenerateTestViewModel();
            testitem.extendedmaterialcosts = 15.50M;
            
            testitem.Calculate();
            
            // Extended material costs sollten zu den Gesamtkosten addiert werden
            Assert.That(testitem.costs, Is.GreaterThan(testitem.filamentCosts + testitem.energyCosts + testitem.printerCosts + testitem.manufacturingCosts), 
                "Extended material costs sollten zu den Gesamtkosten addiert werden");
        }

        [Test]
        public void TestCalculateRevenue()
        {
            var testitem = GenerateTestViewModel();
            testitem.Calculate();
            
            // Revenue sollte 10% der Gesamtkosten betragen
            Assert.That(testitem.revenu, Is.EqualTo(testitem.costs * 0.1M), "Revenue sollte 10% der Gesamtkosten betragen");
        }

        [Test]
        public void TestTotalCostsCalculation()
        {
            var testitem = GenerateTestViewModel();
            testitem.Calculate();
            
            // Total costs sollten costs + revenue sein
            Assert.That(testitem.totalCosts, Is.EqualTo(testitem.costs + testitem.revenu), 
                "Total costs sollten costs + revenue sein");
        }

        [Test]
        public void TestCalculateWithDifferentMissprintChance()
        {
            var testitem = GenerateTestViewModel();
            testitem.Settings.MissprintChance = 20; // 20% Fehldruck-Chance
            testitem.weight = 100;
            
            testitem.Calculate();
            
            // Filament costs sollten um 20% erhöht werden: 2.5 * 1.2 = 3.0
            var expectedFilamentCosts = (100M * (20M / 800M)) * 1.2M;
            Assert.That(testitem.filamentCosts, Is.EqualTo(expectedFilamentCosts), 
                "Filament costs sollten mit Missprint-Chance berechnet werden");
        }

        [Test]
        public void TestCalculateWithZeroMissprintChance()
        {
            var testitem = GenerateTestViewModel();
            testitem.Settings.MissprintChance = 0; // 0% Fehldruck-Chance
            testitem.weight = 100;
            
            testitem.Calculate();
            
            // Filament costs sollten ohne Erhöhung berechnet werden
            var expectedFilamentCosts = 100M * (20M / 800M);
            Assert.That(testitem.filamentCosts, Is.EqualTo(expectedFilamentCosts), 
                "Filament costs sollten ohne Missprint-Chance berechnet werden");
        }

        [Test]
        public void TestCalculateWithDifferentFilament()
        {
            var testitem = GenerateTestViewModel();
            
            // Erstelle ein anderes Filament
            var filaments = new List<Filament>
            {
                new() {FilamentId = 2, Diameter = 2.85f, Price = 30, SpoolWeight = 1000}
            };
            testitem.Filaments = filaments;
            testitem.SelectedFilament = 2;
            testitem.weight = 100;
            
            testitem.Calculate();
            
            // Filament costs sollten mit dem neuen Filament berechnet werden
            var expectedFilamentCosts = (100M * (30M / 1000M)) * 1.05M; // 5% Missprint-Chance
            Assert.That(testitem.filamentCosts, Is.EqualTo(expectedFilamentCosts), 
                "Filament costs sollten mit dem neuen Filament berechnet werden");
        }

        [Test]
        public void TestCalculateWithDifferentPrinter()
        {
            var testitem = GenerateTestViewModel();
            
            // Erstelle einen anderen Drucker
            var printers = new List<Printer>
            {
                new() {PrinterId = 2, EnergyConsumptionW = 500, Price = 2000, PeriotOfAmortisation = 1000}
            };
            testitem.Printers = printers;
            testitem.SelectedPrinter = 2;
            testitem.printtime = 12;
            
            testitem.Calculate();
            
            // Energy costs sollten mit dem neuen Drucker berechnet werden
            var expectedEnergyCosts = ((12M * 500M) / 1000M) * 0.21M;
            Assert.That(testitem.energyCosts, Is.EqualTo(expectedEnergyCosts), 
                "Energy costs sollten mit dem neuen Drucker berechnet werden");
        }

        [Test]
        public void TestCalculateWithNullFilaments()
        {
            var testitem = GenerateTestViewModel();
            testitem.Filaments = null;
            
            // Sollte eine NullReferenceException werfen
            Assert.Throws<NullReferenceException>(() => testitem.Calculate(), 
                "Null Filaments sollten eine NullReferenceException werfen");
        }
        

        private CalculatorViewModel GenerateTestViewModel()
        {
            var testitem = new CalculatorViewModel 
            {
                weight = 0, 
                lengthmm = 100, 
                SelectedFilament = 1, 
                SelectedPrinter = 1,
                printtime = 12,
                manufacurworktime = 0,
                extendedmaterialcosts = 0,
                isMinuit = false
            };

            var filaments = new List<Filament>
            {
                new() {FilamentId = 1, Diameter = 1.75f, Price = 20, SpoolWeight = 800}
            };
            testitem.Filaments = filaments;

            var printers = new List<Printer>
            {
                new() {PrinterId = 1, EnergyConsumptionW = 300,Price = 1000, PeriotOfAmortisation = 500}
            };
            testitem.Printers = printers;

            var settings = new Settings 
            {
                Energiekosts = (decimal)0.21,
                MissprintChance = 5,
                Hourlywage = 25.0M,
                Revenuepercentage = 0.1M
            };

            testitem.Settings = settings;
            
            return testitem;
        }
    }
}