using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.Inventory
{
    public abstract class Book: IStockItem
    {
        public string ProductCode { get; set; }

        public double Price { get; set; }   
    }
}
