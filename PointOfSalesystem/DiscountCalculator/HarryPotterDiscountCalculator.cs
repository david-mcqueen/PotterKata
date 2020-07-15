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

        public void ConsiderItemForDiscount(IStockItem item)
        {
            // Only concerned with IPotterCollection items
            if (typeof(IPotterCollection).IsAssignableFrom(item.GetType()))
            {
                if (!_items.TryGetValue(item.ProductCode, out Stack<IStockItem> value))
                {
                    var newStack = new Stack<IStockItem>();
                    newStack.Push(item);
                    _items.Add(item.ProductCode, newStack);
                }
                else
                {
                    value.Push(item);
                }

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

            switch (stack.Count)
            {
                case 5:
                    stackCost *= 0.25;
                    break;
                case 4:
                    stackCost *= 0.2;
                    break;
                case 3:
                    stackCost *= 0.1;
                    break;
                case 2:
                    stackCost *= 0.05;
                    break;
                default:
                    stackCost = 0;
                    break;
            }

            return Math.Round((double)stackCost, 2);
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
