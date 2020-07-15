using PointOfSalesystem.Inventory;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace PointOfSalesystem
{
    public class Basket
    {
        private static readonly Basket _instance = new Basket();
        public static Basket Instance => _instance;

        public Dictionary<string, int> Cart { get; }

        private Basket()
        {
            Cart = new Dictionary<string, int>();
        }

        public void AddItemToBasket(IStockItem item)
        {
            if (!Cart.TryGetValue(item.ProductCode, out int value))
            {
                Cart.Add(item.ProductCode, 1);
            } else
            {
                Cart[item.ProductCode] = value + 1;
            }
        }

    }
}
