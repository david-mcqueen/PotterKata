using PointOfSalesystem.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.DiscountCalculator
{
    public interface IDiscountCalculator
    {
        void ConsiderItemForDiscount(KeyValuePair<string, Stack<IStockItem>> item);
        double TotalDiscount { get; }
    }
}
