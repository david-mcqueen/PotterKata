using NUnit.Framework;
using PointOfSalesystem.DiscountCalculator;

namespace TestPointOfSaleSystem
{
    public class TestDiscountFactory
    {
        [Test]
        public void ItReturnsACalculatorForAValidType()
        {
            var factory = new DiscountFactory();
            Assert.True(factory.TryGetCalculator(typeof(PointOfSalesystem.Inventory.HarryPotter.Book1), out IDiscountCalculator calculator));
            Assert.AreEqual(typeof(HarryPotterDiscountCalculator), calculator.GetType());
        }

        [Test]
        public void ItDoesntReturnsACalculatorForAnInValidType()
        {
            var factory = new DiscountFactory();
            Assert.False(factory.TryGetCalculator(typeof(PointOfSalesystem.Inventory.DanBrown.Book1), out IDiscountCalculator calculator));
            Assert.Null(calculator);
        }
    }
}
