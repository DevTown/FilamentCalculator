using FilamentCalculator.Controllers;

namespace FilamentCalcTest.Views
{
    using NUnit.Framework;
    
    [TestFixture]
    public class FilamentTest
    {
        [Test]
        public void TestIndex()
        {
            var testItem = new FilamentController();
            var item = testItem.Index();
            
        }
    }
}