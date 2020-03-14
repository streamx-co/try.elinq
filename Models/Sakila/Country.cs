using System;
using System.Collections.Generic;

namespace Models.Sakila
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        public ushort CountryId { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
}
