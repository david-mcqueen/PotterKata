using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.Inventory.HarryPotter
{
    public class Book3: Book, IPotterCollection
    {
        public Book3()
        {
            Price = 8.0;
            ProductCode = "00003";
        }
    }
}
