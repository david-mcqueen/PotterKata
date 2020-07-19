using NUnit.Framework;
using PointOfSalesystem.DiscountCalculator;
using PointOfSalesystem.Inventory;
using PointOfSalesystem.Inventory.HarryPotter;
using System.Collections.Generic;

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

            var newStack = new Stack<IStockItem>();
            newStack.Push(new Book1());

            var kvp = new KeyValuePair<string, Stack<IStockItem>>(new Book1().ProductCode, newStack);
            validator.ConsiderItemForDiscount(kvp);

            Assert.AreEqual(0.0, validator.TotalDiscount);
        }

        [Test]
        public void ItGivesDiscountFor2DifferentBooks()
        {
            var validator = new HarryPotterDiscountCalculator();

            var bk1Stack = new Stack<IStockItem>();
            bk1Stack.Push(new Book1());
            var kvp = new KeyValuePair<string, Stack<IStockItem>>(new Book1().ProductCode, bk1Stack);
            validator.ConsiderItemForDiscount(kvp);

            var bk2Stack = new Stack<IStockItem>();
            bk2Stack.Push(new Book2());
            var kvp2 = new KeyValuePair<string, Stack<IStockItem>>(new Book2().ProductCode, bk2Stack);
            validator.ConsiderItemForDiscount(kvp2);

            // 2 * £8 = £16 * 0.05 = £ 0.8 discount

            Assert.AreEqual(0.8, validator.TotalDiscount);
        }

        [Test]
        public void ItGivesDiscountForNonSequentialBooksBooks()
        {
            var validator = new HarryPotterDiscountCalculator();

            var bk1Stack = new Stack<IStockItem>();
            bk1Stack.Push(new Book1());
            var kvp = new KeyValuePair<string, Stack<IStockItem>>(new Book1().ProductCode, bk1Stack);
            validator.ConsiderItemForDiscount(kvp);

            var bk3Stack = new Stack<IStockItem>();
            bk3Stack.Push(new Book3());
            var kvp3 = new KeyValuePair<string, Stack<IStockItem>>(new Book3().ProductCode, bk3Stack);
            validator.ConsiderItemForDiscount(kvp3);

            // 2 * £8 = £16 * 0.05 = £ 0.8 discount

            Assert.AreEqual(0.8, validator.TotalDiscount);
        }

        [Test]
        public void ItDoesntGivesDiscountFor2SameBooks()
        {
            var validator = new HarryPotterDiscountCalculator();

            var bk1Stack = new Stack<IStockItem>();
            bk1Stack.Push(new Book1());
            bk1Stack.Push(new Book1());

            var kvp = new KeyValuePair<string, Stack<IStockItem>>(new Book1().ProductCode, bk1Stack);
            validator.ConsiderItemForDiscount(kvp);

            // 2 * £8 = £16 * 0 = £ 0 discount

            Assert.AreEqual(0, validator.TotalDiscount);
        }


        [Test]
        public void ItGivesDiscountMultipleTimes()
        {
            var validator = new HarryPotterDiscountCalculator();

            var bk1Stack = new Stack<IStockItem>();
            bk1Stack.Push(new Book1());
            bk1Stack.Push(new Book1());
            var kvp = new KeyValuePair<string, Stack<IStockItem>>(new Book1().ProductCode, bk1Stack);
            validator.ConsiderItemForDiscount(kvp);

            var bk2Stack = new Stack<IStockItem>();
            bk2Stack.Push(new Book2());
            bk2Stack.Push(new Book2());
            var kvp2 = new KeyValuePair<string, Stack<IStockItem>>(new Book2().ProductCode, bk2Stack);
            validator.ConsiderItemForDiscount(kvp2);

            // 2 * £8 = £16 * 0.05 = £ 0.8 discount [Twice] = £1.60

            Assert.AreEqual(1.6, validator.TotalDiscount);
        }

        [Test]
        public void ItGivesDiscountForExample()
        {
            var validator = new HarryPotterDiscountCalculator();

            // 2 copies of the first book
            var bk1Stack = new Stack<IStockItem>();
            bk1Stack.Push(new Book1());
            bk1Stack.Push(new Book1());
            var kvp = new KeyValuePair<string, Stack<IStockItem>>(new Book1().ProductCode, bk1Stack);
            validator.ConsiderItemForDiscount(kvp);

            // 2 copies of the second book
            var bk2Stack = new Stack<IStockItem>();
            bk2Stack.Push(new Book2());
            bk2Stack.Push(new Book2());
            var kvp2 = new KeyValuePair<string, Stack<IStockItem>>(new Book2().ProductCode, bk2Stack);
            validator.ConsiderItemForDiscount(kvp2);

            // 2 copies of the third book
            var bk3Stack = new Stack<IStockItem>();
            bk3Stack.Push(new Book3());
            bk3Stack.Push(new Book3());
            var kvp3 = new KeyValuePair<string, Stack<IStockItem>>(new Book3().ProductCode, bk3Stack);
            validator.ConsiderItemForDiscount(kvp3);

            // 1 copy of the fourth book
            var bk4Stack = new Stack<IStockItem>();
            bk4Stack.Push(new Book4());
            var kvp4 = new KeyValuePair<string, Stack<IStockItem>>(new Book4().ProductCode, bk4Stack);
            validator.ConsiderItemForDiscount(kvp4);

            // 1 copy of the fifth book
            var bk5Stack = new Stack<IStockItem>();
            bk5Stack.Push(new Book5());
            var kvp5 = new KeyValuePair<string, Stack<IStockItem>>(new Book5().ProductCode, bk5Stack);
            validator.ConsiderItemForDiscount(kvp5);


            // Total cost before discount is £64
            // Total discount is £12.40
            Assert.AreEqual(12.40, validator.TotalDiscount);
        }
    }
}