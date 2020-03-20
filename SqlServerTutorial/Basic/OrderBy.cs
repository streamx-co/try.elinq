using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;

namespace SqlServerTutorial.Basic {
    class OrderBy {
        private MyContext DbContext { get; }

        public OrderBy(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var query = DbContext.Customers.Query((Customers customer) => {
                    var result = SELECT(customer);
                    FROM(customer);

                    return result;
                })
                .OrderBy(c => c.FirstName);

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
                })
                .OrderByDescending(c => c.FirstName);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }

        public void C() {

            #region C
            var query = DbContext.Customers.Query((Customers customer) => {
                    var result = SELECT(customer);
                    FROM(customer);

                    return result;
                })
                .OrderBy(c => c.City)
                .ThenBy(c => c.FirstName);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }

        public void D() {

            #region D
            var query = DbContext.Customers.Query((Customers customer) => {
                    var result = SELECT(customer);
                    FROM(customer);

                    return result;
                })
                .OrderByDescending(c => c.City)
                .ThenBy(c => c.FirstName);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }

        public void E() {

            #region E
            var query = DbContext.Customers.Query((Customers customer) => {
                    var result = SELECT(customer);
                    FROM(customer);

                    return result;
                })
                .OrderBy(c => c.State);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }

        public void F() {

            #region F
            var query = DbContext.Customers.Query((Customers customer) => {
                var result = SELECT(customer);
                FROM(customer);
                ORDER(BY(customer.FirstName.Length).DESC);
                OFFSET(0).ROWS();

                return result;
            });

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }

        public void F_1() {

            #region F_1
            var query = DbContext.Customers.Query((Customers customer) => {
                    var result = SELECT(customer);
                    FROM(customer);

                    return result;
                })
                .OrderByDescending(c => c.FirstName.Length);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }

        public void G() {

            #region G
            var query = DbContext.Set<FullName>()
                .Query((Customers customer, FullName alias) => {
                    var result = SELECT<FullName>(customer.FirstName.@as(alias.FirstName), customer.LastName.@as(alias.LastName));
                    FROM(customer);
                    ORDER(BY(1), BY(2));
                    OFFSET(0).ROWS();

                    return result;
                });

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }
    }
}
