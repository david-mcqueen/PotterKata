using PointOfSalesystem.Inventory;
using PointOfSalesystem.Inventory.HarryPotter;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace PointOfSalesystem.DiscountCalculator
{
    public class DiscountFactory
    {
        // We only want 1 instance of each DiscountCalculator
        private Dictionary<Type, IDiscountCalculator> usedCalculators = new Dictionary<Type, IDiscountCalculator>();

        public bool TryGetCalculator(Type stackType, out IDiscountCalculator calculator)
        {
            if (typeof(IPotterCollection).IsAssignableFrom(stackType))
            {
                // If we already have a calculator for this type, return that
                if (usedCalculators.TryGetValue(typeof(HarryPotterDiscountCalculator), out IDiscountCalculator value)){
                    calculator = value;
                }
                else
                {
                    calculator = new HarryPotterDiscountCalculator();
                    usedCalculators.Add(typeof(HarryPotterDiscountCalculator), calculator);
                }
                return true;
            }

            calculator = null;
            return false;
        }

        public List<IDiscountCalculator> GetCalculators()
        {
            return usedCalculators.Values.ToList();
        }
    }
}
