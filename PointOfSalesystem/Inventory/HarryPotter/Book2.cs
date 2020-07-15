using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.Inventory.HarryPotter
{
    public class Book2: Book, IPotterCollection
    {
        public Book2()
        {
            Price = 8.0;
            ProductCode = "00002";
        }
    }
}
