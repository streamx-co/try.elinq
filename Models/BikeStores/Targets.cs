using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class Targets
    {
        public Targets()
        {
            Commissions = new HashSet<Commissions>();
        }

        public int TargetId { get; set; }
        public decimal Percentage { get; set; }

        public virtual ICollection<Commissions> Commissions { get; set; }
    }
}
