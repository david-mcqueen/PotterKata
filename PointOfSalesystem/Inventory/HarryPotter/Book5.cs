using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.Inventory.HarryPotter
{
    public class Book5: Book, IPotterCollection
    {
        public Book5()
        {
            Price = 8.0;
            ProductCode = "00005";
        }
    }
}
