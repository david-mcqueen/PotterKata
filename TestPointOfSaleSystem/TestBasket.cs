using NUnit.Framework;
using PointOfSalesystem;
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

            var cart = Basket.Instance.Cart;
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

            var cart = Basket.Instance.Cart;
            
            Assert.AreEqual(2, cart.Count); // 2 unique records in the cart

            Assert.IsTrue(cart.TryGetValue(bk1.ProductCode, out int bk1Count)); 
            Assert.AreEqual(2, bk1Count); // x2 of book1

            Assert.IsTrue(cart.TryGetValue(bk2.ProductCode, out int bk2Count));
            Assert.AreEqual(1, bk2Count); // x1 of book2

        }
    }
}
