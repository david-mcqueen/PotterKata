using PointOfSalesystem.Inventory;
using PointOfSalesystem.Inventory.HarryPotter;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PointOfSalesystem.DiscountCalculator
{
    public class HarryPotterDiscountCalculator : IDiscountCalculator
    {
        private bool recalculateTotel = false;
        private Dictionary<string, Stack<IStockItem>> _items { get; set; }

        private double _totalDiscount { get; set; }
        public double TotalDiscount
        {
            get
            {
                // Only recalculate if an item has changed
                if (recalculateTotel)
                {
                    _totalDiscount = CalculateTotalDiscount();
                    recalculateTotel = false;
                }

                return _totalDiscount;
            }
        }

        public HarryPotterDiscountCalculator()
        {
            _items = new Dictionary<string, Stack<IStockItem>>();
        }

        /// <summary>
        /// Given a stack, consider it for discounts 
        /// Only if IPotterCollection is implemented
        /// </summary>
        /// <param name="item">KVP: A stack of all items with the same ProductCode</param>
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
        /// Calculates the monetary discount that should be applied for the given stack
        /// </summary>
        /// <returns>Total discount to be applied for the current stack</returns>
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


            // Calculate the *DISCOUNT* which is applied to the stack of books (not the total price after discount)
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

        /// <summary>
        /// For all of the provided stacks, caculate if they are eligible for discount and return it
        /// </summary>
        /// <returns>The total discount which is to be applied</returns>
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
    }
}
