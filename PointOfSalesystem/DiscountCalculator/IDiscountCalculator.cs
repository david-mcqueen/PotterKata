using PointOfSalesystem.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.DiscountCalculator
{
    interface IDiscountCalculator
    {
        void ConsiderItemForDiscount(IStockItem item);
        double TotalDiscount { get; }
    }
}
