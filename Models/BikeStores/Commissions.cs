using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class Commissions
    {
        public int StaffId { get; set; }
        public int? TargetId { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal Commission { get; set; }

        public virtual Staffs Staff { get; set; }
        public virtual Targets Target { get; set; }
    }
}
