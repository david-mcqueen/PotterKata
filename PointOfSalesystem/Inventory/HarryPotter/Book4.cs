using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.Inventory.HarryPotter
{
    public class Book4: Book, IPotterCollection
    {
        public Book4()
        {
            Price = 8.0;
            ProductCode = "00004";
        }
    }
}
