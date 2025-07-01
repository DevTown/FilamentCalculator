using FilamentCalculator.Models;
using NUnit.Framework;

namespace FilamentCalcTest.Models
{
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
            var filType = new FilamentType
            {
                Name = "Testtype"
            };
            
            testItem.Manufacturer = manufactu;
            testItem.FilamentType = filType;
            testItem.Price = 30;
            testItem.SpoolWeight = 1000;
            testItem.Color = "MyColor";

            Assert.That(testItem.Displayname, Is.EqualTo("Testmanu - Testtype - MyColor - 0,03 EUR/G"));

        }
    }
}