using NUnit.Framework;
using PointOfSalesystem.DiscountValidator;

namespace TestPointOfSaleSystem
{
    public class TestHarryPotterDiscountValidator
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ItExists()
        {
            var validator = new HarryPotterDiscountValidator();
        }
    }
}