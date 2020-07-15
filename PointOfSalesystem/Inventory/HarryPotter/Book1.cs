using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.Inventory.HarryPotter
{
    public class Book1: Book, IPotterCollection
    {
        public Book1()
        {
            Price = 8.0;
            ProductCode = "00001";
        }
    }
}
