using PointOfSalesystem.Inventory;
using PointOfSalesystem.Inventory.HarryPotter;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PointOfSalesystem.DiscountCalculator
{
    public class HarryPotterDiscountCalculator : IDiscountCalculator, IDisposable
    {
        private bool recalculateTotel = false;
        private Dictionary<string, Stack<IStockItem>> _items { get; set; }

        public HarryPotterDiscountCalculator()
        {
            _items = new Dictionary<string, Stack<IStockItem>>();
        }

        // TODO:- Make Generic?

        public void ConsiderItemForDiscount(KeyValuePair<string, Stack<IStockItem>> item)
        {
            // Only concerned with IPotterCollection items
            if (typeof(IPotterCollection).IsAssignableFrom(item.Value.First().GetType()))
            {

                _items.Add(item.Key, item.Value);

                recalculateTotel = true;
            }
        }

        /// <summary>
        /// Calculates the monetary discount that should be applied
        /// </summary>
        /// <returns></returns>
        private double getDiscountForStack(Dictionary<string, Stack<IStockItem>> discountItems)
        {
            var stack = new List<IStockItem>();
            // Take 1 of each collection of items
            foreach (var item in discountItems)
            {
                if (item.Value.TryPop(out IStockItem result))
                {
                    stack.Add(result);
                }
            }

            // Total cost of the items in this stack
            var stackCost = stack.Sum(i => i.Price);
            var stackDiscount = 0.0;


            // Calculate the *DISCOUNT* which is applied to the stack of books (not the price after discount)
            switch (stack.Count)
            {
                case 5:
                    stackDiscount = stackCost * 0.25;
                    break;
                case 4:
                    stackDiscount = stackCost * 0.2;
                    break;
                case 3:
                    stackDiscount = stackCost * 0.1;
                    break;
                case 2:
                    stackDiscount = stackCost * 0.05;
                    break;
                default:
                    stackDiscount = 0.0;
                    break;
            }

            return Math.Round((double)stackDiscount, 2);
        }

        private double CalculateTotalDiscount()
        {
            double discount = 0.0;

            var discountItems = _items.ToDictionary(i => i.Key, i => i.Value);

            while (discountItems.Count > 0)
            {
                discount += getDiscountForStack(discountItems);
                discountItems = discountItems.Where(i => i.Value.Count > 0).ToDictionary(i => i.Key, i => i.Value);
            }

            return discount;
        }

        private double _totalDiscount { get; set; }
        public double TotalDiscount
        {
            get 
            {
                // Only recalculate if an item has changed
                if (recalculateTotel)
                {
                    _totalDiscount = CalculateTotalDiscount();
                }

                return _totalDiscount;
            }
        }
        public void Dispose()
        {
            _items.Clear();
            recalculateTotel = true;
        }
    }
}
