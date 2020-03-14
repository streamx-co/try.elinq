using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;

namespace SqlServerTutorial.Basic {
    class OrderBy {
        private MyContext DbContext { get; }

        public OrderBy(MyContext context) {
            DbContext = context;
        }

        public void D() {

            #region D
            var query = DbContext.Customers.Query((Customers customer) => {
                var result = SELECT(customer);
                FROM(customer);
                ORDER(BY(customer.City), BY(customer.FirstName));

                return result;
            }).AsEnumerable();

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.City, customer.FirstName, customer.LastName));
            #endregion

        }
        
        public void F() {

            #region F
            var query = DbContext.Customers.Query((Customers customer) => {
                var result = SELECT(customer);
                FROM(customer);
                ORDER(BY(customer.FirstName.Length).DESC);

                return result;
            }).AsEnumerable();

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.FirstName, customer.LastName));
            #endregion

        }
    }
}
