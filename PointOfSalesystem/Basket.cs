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
                }

                return _totalCost;
            }
        }

        private Basket()
        {
            Items = new Dictionary<string, Stack<IStockItem>>();
        }

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


        private double CalculateBasketTotal()
        {
            double cost = 0;
            
            // TODO:- Allow for other IDiscountCalculator
            var discountcalculator = new HarryPotterDiscountCalculator();

            foreach (var basketItemStack in Items)
            {

                if (typeof(IPotterCollection).IsAssignableFrom(basketItemStack.Value.First().GetType()))
                {
                    //TODO:-  Is a Potter book, so would be eligible for a discount
                    discountcalculator.ConsiderItemForDiscount(basketItemStack);

                }
                cost += basketItemStack.Value.Sum(i => i.Price);
            }

            return cost - discountcalculator.TotalDiscount;
        }

        public void Dispose()
        {
            Items.Clear();
            recalculateTotel = true;
        }
    }
}
