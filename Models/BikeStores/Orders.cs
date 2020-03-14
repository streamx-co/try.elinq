using System;
using System.Collections.Generic;

namespace Models.BikeStores
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Staffs Staff { get; set; }
        public virtual Stores Store { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
