using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class Promotions
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public decimal? Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
