using PointOfSalesystem.Inventory;
using System;
using System.Collections.Generic;

namespace PointOfSalesystem
{
    public class Basket: IDisposable
    {
        private static readonly Basket _instance = new Basket();
        public static Basket Instance => _instance;

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
            foreach (var item in Items)
            {
                cost += (item.Key.Price * item.Value);
            }

            return cost;
        }

        public void Dispose()
        {
            Items = new Dictionary<IStockItem, int>();
            recalculateTotel = true;
        }

    }
}
