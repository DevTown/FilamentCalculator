using FilamentCalculator.Models;

namespace FilamentCalcTest.Views
{
    using NUnit.Framework;
    
    [TestFixture]
    public class FilamentTest
    {
        [Test]
        public void TestDisplayname()
        {
            var testItem = new Filament();
            var manufactu = new Manufacturer
            {
                Name = "Testmanu",
                ManufacturerId = -1,
            };

            testItem.Manufacturer = manufactu;
            testItem.Color = "MyColor";

            Assert.That(testItem.Displayname, Is.EqualTo("Testmanu - MyColor - "));

        }
    }
}