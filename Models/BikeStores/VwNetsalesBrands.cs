using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class VwNetsalesBrands
    {
        public string BrandName { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public decimal? NetSales { get; set; }
    }
}
