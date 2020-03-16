using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal? Amount { get; set; }
    }
}
