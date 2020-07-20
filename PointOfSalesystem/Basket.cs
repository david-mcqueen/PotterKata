using PointOfSalesystem.DiscountCalculator;
using PointOfSalesystem.Inventory;
using PointOfSalesystem.Inventory.HarryPotter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSalesystem
{
    public class Basket: IDisposable
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static Basket Instance { get; } = new Basket();

        public Dictionary<string, Stack<IStockItem>> Items { get; private set; }
        private bool recalculateTotel = false;

        private double _totalCost { get; set; }
        public double TotalCost
        {
            get
            {
                // Only recalculate if an item has changed
                if (recalculateTotel)
                {
                    _totalCost = CalculateBasketTotal();
                    recalculateTotel = false;
                }

                return _totalCost;
            }
        }

        private Basket()
        {
            Items = new Dictionary<string, Stack<IStockItem>>();
        }

        /// <summary>
        /// Adds an item to the basket
        /// </summary>
        /// <param name="item">An item to add to the basket</param>
        public void AddItemToBasket(IStockItem item)
        {
            if (!Items.TryGetValue(item.ProductCode, out Stack<IStockItem> value))
            {
                var newStack = new Stack<IStockItem>();
                newStack.Push(item);
                Items.Add(item.ProductCode, newStack);
            } else
            {
                value.Push(item);
            }

            recalculateTotel = true;
        }

        /// <summary>
        /// Gives a total cost for all items in the basket, after eligible discounts have been applied
        /// </summary>
        /// <returns>Total cost</returns>
        private double CalculateBasketTotal()
        {
            double cost = 0;

            // Allow for other IDiscountCalculator
            var discountFactory = new DiscountFactory();

            foreach (var basketItemStack in Items)
            {
                if (discountFactory.TryGetCalculator(basketItemStack.Value.First().GetType(), out IDiscountCalculator discountCalculator))
                {
                    discountCalculator.ConsiderItemForDiscount(basketItemStack);
                }
                cost += basketItemStack.Value.Sum(i => i.Price);
            }

            var totalDiscount = discountFactory.GetCalculators().Sum(c => c.TotalDiscount);

            return cost - totalDiscount;
            
        }

        public void Dispose()
        {
            Items.Clear();
            recalculateTotel = true;
        }
    }
}
