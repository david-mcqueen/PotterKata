﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSalesystem.Inventory
{
    public interface IStockItem: IEqualityComparer<IStockItem>
    {
        string ProductCode { get; set; }
        double Price { get; set; }
    }
}
