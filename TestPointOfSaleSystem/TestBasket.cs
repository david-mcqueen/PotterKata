using NUnit.Framework;
using PointOfSalesystem;
using PointOfSalesystem.Inventory;
using PointOfSalesystem.Inventory.HarryPotter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPointOfSaleSystem
{
    public class TestBasket
    {
        [SetUp]
        public void Setup()
        {
            // Clear out the basket for each test
            Basket.Instance.Dispose();
        }

        [Test]
        public void ItExists()
        {
            var basket = Basket.Instance;
        }

        [Test]
        public void AddItemToBasket()
        {
            var bk = new Book1();
            Basket.Instance.AddItemToBasket(bk);

            var cart = Basket.Instance.Items;
            Assert.AreEqual(1, cart.Count);
            Assert.AreEqual(bk.ProductCode, cart.First().Key);
        }

        [Test]
        public void AddMultipleItemsToBasket()
        {
            var bk1 = new Book1();

            Basket.Instance.AddItemToBasket(bk1);
            Basket.Instance.AddItemToBasket(bk1);

            var bk2 = new Book2();
            Basket.Instance.AddItemToBasket(bk2);

            var cart = Basket.Instance.Items;
            
            Assert.AreEqual(2, cart.Count); // 2 unique records in the cart

            Assert.IsTrue(cart.TryGetValue(bk1.ProductCode, out Stack<IStockItem> bk1Count));
            Assert.AreEqual(2, bk1Count.Count); // x2 of book1

            Assert.IsTrue(cart.TryGetValue(bk2.ProductCode, out Stack<IStockItem> bk2Count));
            Assert.AreEqual(1, bk2Count.Count); // x1 of book2

        }

        [Test]
        public void GetCostOfBasket()
        {
            var bk1 = new Book1();
            Basket.Instance.AddItemToBasket(bk1);

            Assert.AreEqual(8.0, Basket.Instance.TotalCost);
        }

        [Test]
        public void GetCostOfBasketForMultipleItems()
        {
            var bk1 = new Book1();

            Basket.Instance.AddItemToBasket(bk1);
            Basket.Instance.AddItemToBasket(bk1); // Book not eligible for discount

            var bk2 = new Book2();
            Basket.Instance.AddItemToBasket(bk2);

            // 2 books eligiable for discount
            // 2 * 8 = £16 * 0.95 = £15.2

            // 1 book not eligible for discount
            // 1 * 8 = £8

            // Total Cost: £15.2 + £8 = £23.2
            Assert.AreEqual(23.2, Basket.Instance.TotalCost);
        }

        [Test]
        public void ItDoesntGiveDiscountToNonEligiableBooks()
        {
            var book = new PointOfSalesystem.Inventory.DanBrown.Book1();
            Basket.Instance.AddItemToBasket(book); // !Potter book

            Basket.Instance.AddItemToBasket(new Book1()); // Potter book

            Assert.AreEqual(16, Basket.Instance.TotalCost);
        }

        [Test]
        public void ItDoesntGiveDiscountToNonEligiableBooks_ContainsValidDiscount()
        {
            var book = new PointOfSalesystem.Inventory.DanBrown.Book1();
            Basket.Instance.AddItemToBasket(book); // !Potter book

            Basket.Instance.AddItemToBasket(new Book1()); // Potter book 1
            Basket.Instance.AddItemToBasket(new Book2()); // Potter book 2

            // 2 books eligiable for discount
            // 2 * 8 = £16 * 0.95 = £15.2

            // 1 book not eligible for discount
            // 1 * 8 = £8

            // Total Cost: £15.2 + £8 = £23.2
            Assert.AreEqual(23.2, Basket.Instance.TotalCost);
        }

        [Test]
        public void ItGivesTotalCostForExample()
        {
            // 2 copies of the first book
            Basket.Instance.AddItemToBasket(new Book1());
            Basket.Instance.AddItemToBasket(new Book1());

            // 2 copies of the second book
            Basket.Instance.AddItemToBasket(new Book2());
            Basket.Instance.AddItemToBasket(new Book2());

            // 2 copies of the third book
            Basket.Instance.AddItemToBasket(new Book3());
            Basket.Instance.AddItemToBasket(new Book3());

            // 1 copy of the fourth book
            Basket.Instance.AddItemToBasket(new Book4());

            // 1 copy of the fifth book
            Basket.Instance.AddItemToBasket(new Book5());

            // Total cost before discount is £64
            // Total cost after discount is £51.60
            Assert.AreEqual(51.60, Basket.Instance.TotalCost);
        }
    }
}
