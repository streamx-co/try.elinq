using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Library;

namespace SqlServerTutorial.Basic {
    class Select {
        private MyContext DbContext { get; }

        public Select(MyContext context) {
            DbContext = context;
        }

        public void B() {

            #region B
            var query = DbContext.Customers.Query((Customers customer) => {
                var result = SELECT(customer);
                FROM(customer);

                return result;
            });

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName));
            #endregion

        }

        public void C() {

            #region C
            var state = "CA";

            var query = DbContext.Customers.Query((Customers customer) => {
                var result = SELECT(customer);
                FROM(customer);
                WHERE(customer.State == state);
                ORDER(BY(customer.FirstName));

                return result;
            }).AsEnumerable();

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName));
            #endregion

        }
        
        public void E() {

            #region E
            var state = "CA";

            var query = DbContext.Set<CityCount>().Query((Customers customer, CityCount cityCount) => {
                var count = COUNT();
                var result = SELECT<CityCount>(customer.City.@as(cityCount.City), count.@as(cityCount.Count));
                FROM(customer);
                WHERE(customer.State == state);
                GROUP(BY(customer.City));
                HAVING(count > 10);
                ORDER(BY(customer.City));

                return result;
            }).AsEnumerable();

            foreach (var cityCount in query.Take(5))
                Console.WriteLine((cityCount.City, cityCount.Count));
            #endregion

        }
    }
}
