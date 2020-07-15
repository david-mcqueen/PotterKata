using PointOfSalesystem.Inventory;
using System;
using System.Collections.Generic;

namespace PointOfSalesystem
{
    public class Basket: IDisposable
    {
        private static readonly Basket _instance = new Basket();
        public static Basket Instance => _instance;

        public Dictionary<IStockItem, int> Cart { get; private set; }
        private bool recalculateTotel = false;


        private Basket()
        {
            Cart = new Dictionary<IStockItem, int>();
        }

        public void AddItemToBasket(IStockItem item)
        {
            if (!Cart.TryGetValue(item, out int value))
            {
                Cart.Add(item, 1);
            } else
            {
                Cart[item] = value + 1;
            }

            recalculateTotel = true;
        }


        private double CalculateBasketTotal()
        {
            double cost = 0;
            foreach (var item in Cart)
            {
                cost += (item.Key.Price * item.Value);
            }

            return cost;
        }

        private double _totalCost { get; set; }
        public double TotalCost { 
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

        public void Dispose()
        {
            Cart = new Dictionary<IStockItem, int>();
            recalculateTotel = true;
        }

    }
}
