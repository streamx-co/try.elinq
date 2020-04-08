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

        public void A() {

            #region A
            var query = DbContext.Set<FullName>()
                .Query((Customers customer, FullName alias) => {
                    var result = SELECT<FullName>(customer.FirstName.@as(alias.FirstName), customer.LastName.@as(alias.LastName));
                    FROM(customer);

                    return result;
                });

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }

        public void A_1() {

            #region A_1
            var query = from customers in DbContext.Customers.Query((Customers customer) => {
                    var result = SELECT(customer);
                    FROM(customer);

                    return result;
                })
                select new {customers.FirstName, customers.LastName};

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

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

                    return result;
                })
                .OrderBy(c => c.FirstName);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName));
            #endregion

        }

        public void D() {

            #region D
            var state = "CA";

            var query = DbContext.Set<CityCount>()
                .Query((Customers customer, CityCount cityCount) => {
                    var count = COUNT();
                    var result = SELECT<CityCount>(customer.City.@as(cityCount.City), count.@as(cityCount.Count));
                    FROM(customer);
                    WHERE(customer.State == state);
                    GROUP(BY(customer.City));

                    return result;
                })
                .OrderBy(cc => cc.City);

            foreach (var cityCount in query.Take(3))
                Console.WriteLine((cityCount.City, cityCount.Count));
            #endregion

        }

        public void E() {

            #region E
            var state = "CA";
            var minCount = 10;

            var query = DbContext.Set<CityCount>()
                .Query((Customers customer, CityCount cityCount) => {
                    var count = COUNT();
                    var result = SELECT<CityCount>(customer.City.@as(cityCount.City), count.@as(cityCount.Count));
                    FROM(customer);
                    WHERE(customer.State == state);
                    GROUP(BY(customer.City));
                    HAVING(count > minCount);

                    return result;
                })
                .OrderBy(cc => cc.City);

            foreach (var cityCount in query)
                Console.WriteLine((cityCount.City, cityCount.Count));
            #endregion

        }
    }
}
