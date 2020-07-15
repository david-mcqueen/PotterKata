using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PointOfSalesystem.Inventory
{
    public abstract class Book: IStockItem
    {
        public string ProductCode { get; set; }

        public double Price { get; set; }

        // Implement IEqualityComparer<IStockItem>
        // To compare the objects in the basket cart dictionary
        public bool Equals([AllowNull] IStockItem x, [AllowNull] IStockItem y)
        {
            return x.ProductCode.Equals(y.ProductCode);
        }

        public int GetHashCode([DisallowNull] IStockItem obj)
        {
            return obj.GetHashCode();
        }
    }
}
