using NUnit.Framework;
using PointOfSalesystem.DiscountCalculator;
using PointOfSalesystem.Inventory.HarryPotter;

namespace TestPointOfSaleSystem
{
    public class TestHarryPotterDiscountCalculator
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ItExists()
        {
            var validator = new HarryPotterDiscountCalculator();
        }

        [Test]
        public void ItDoesntGiveDiscountForSingleItem()
        {
            var validator = new HarryPotterDiscountCalculator();
            var bk = new Book1();
            validator.ConsiderItemForDiscount(bk);

            Assert.AreEqual(0.0, validator.TotalDiscount);
        }

        [Test]
        public void ItGivesDiscountFor2DifferentBooks()
        {
            var validator = new HarryPotterDiscountCalculator();

            validator.ConsiderItemForDiscount(new Book1());
            validator.ConsiderItemForDiscount(new Book2());

            // 2 * £8 = £16 * 0.05 = £ 0.8 discount

            Assert.AreEqual(0.8, validator.TotalDiscount);
        }

        [Test]
        public void ItDoesntGivesDiscountFor2SameBooks()
        {
            var validator = new HarryPotterDiscountCalculator();

            validator.ConsiderItemForDiscount(new Book1());
            validator.ConsiderItemForDiscount(new Book1());

            // 2 * £8 = £16 * 0 = £ 0 discount

            Assert.AreEqual(0, validator.TotalDiscount);
        }

        [Test]
        public void ItGivesDiscountForExample()
        {
            var validator = new HarryPotterDiscountCalculator();

            // 2 copies of the first book
            validator.ConsiderItemForDiscount(new Book1());
            validator.ConsiderItemForDiscount(new Book1());

            // 2 copies of the second book
            validator.ConsiderItemForDiscount(new Book2());
            validator.ConsiderItemForDiscount(new Book2());

            // 2 copies of the third book
            validator.ConsiderItemForDiscount(new Book3());
            validator.ConsiderItemForDiscount(new Book3());

            // 1 copy of the fourth book
            validator.ConsiderItemForDiscount(new Book4());

            // 1 copy of the fifth book
            validator.ConsiderItemForDiscount(new Book5());


            // Total cost before discount is £64
            // Total discount is £12.40
            Assert.AreEqual(12.40, validator.TotalDiscount);
        }
    }
}