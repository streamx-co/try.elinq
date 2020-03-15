using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class Taxes
    {
        public int TaxId { get; set; }
        public string State { get; set; }
        public decimal? StateTaxRate { get; set; }
        public decimal? AvgLocalTaxRate { get; set; }
        public decimal? CombinedRate { get; set; }
        public decimal? MaxLocalTaxRate { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
