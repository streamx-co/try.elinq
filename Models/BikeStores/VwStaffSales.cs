using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class VwStaffSales
    {
        public int StaffId { get; set; }
        public int? Year { get; set; }
        public decimal? NetSales { get; set; }
    }
}
