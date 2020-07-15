using PointOfSalesystem.Inventory;
using PointOfSalesystem.Inventory.HarryPotter;
using System;
using System.Collections.Generic;

namespace PointOfSalesystem
{
    public class Basket: IDisposable
    {
        public static Basket Instance { get; } = new Basket();

        public Dictionary<IStockItem, int> Items { get; private set; }
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
                }

                return _totalCost;
            }
        }

        private Basket()
        {
            Items = new Dictionary<IStockItem, int>();
        }

        public void AddItemToBasket(IStockItem item)
        {
            if (!Items.TryGetValue(item, out int value))
            {
                Items.Add(item, 1);
            } else
            {
                Items[item] = value + 1;
            }

            recalculateTotel = true;
        }


        private double CalculateBasketTotal()
        {
            double cost = 0;
            foreach (var basketItem in Items)
            {
                if (typeof(IPotterCollection).IsAssignableFrom(basketItem.GetType()))
                {
                    //TODO:-  Is a Potter book, so would be eligible for a discount

                }
                cost += (basketItem.Key.Price * basketItem.Value);
            }

            return cost;
        }

        public void Dispose()
        {
            Items.Clear();
            recalculateTotel = true;
        }
    }
}
