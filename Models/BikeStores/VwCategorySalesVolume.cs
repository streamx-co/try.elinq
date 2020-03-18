using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class VwCategorySalesVolume
    {
        public string CategoryName { get; set; }
        public int? Year { get; set; }
        public int? Qty { get; set; }
    }
}
