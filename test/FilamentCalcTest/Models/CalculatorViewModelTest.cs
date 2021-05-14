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
            
        }

        [Test]
        public void TestGetWeight()
        {
            var testitem = new CalculatorViewModel();
            testitem.weight = 0;
            testitem.lengthmm = 100;
            testitem.SelectedFilament = 1;
            
            
            Assert.That(testitem.weight, Is.EqualTo(0));
            Assert.That(testitem.lengthmm, Is.EqualTo(100));

            testitem.GetWeight();

            
            Assert.That(testitem.weight, Is.EqualTo(0.301));
            Assert.That(testitem.lengthmm, Is.EqualTo(100));

        }
    }
}