using System;
using System.Collections.Generic;

namespace Models.Sakila
{
    public partial class Rental
    {
        public Rental()
        {
            Payment = new HashSet<Payment>();
        }

        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public uint InventoryId { get; set; }
        public ushort CustomerId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public byte StaffId { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
