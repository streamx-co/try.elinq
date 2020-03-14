using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;

namespace SqlServerTutorial.Basic {
    class Null {
        private MyContext DbContext { get; }

        public Null(MyContext context) {
            DbContext = context;
        }

        public void IsNull() {

            #region IsNull
            var query = DbContext.Customers.Query((Customers customer) => {
                var result = SELECT(customer);
                FROM(customer);
                WHERE(customer.Phone == null);
                ORDER(BY(customer.FirstName), BY(customer.LastName));

                return result;
            }).AsEnumerable();

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName, customer.Phone ?? "NULL"));
            #endregion

        }
        
        public void IsNotNull() {

            #region IsNotNull
            var query = DbContext.Customers.Query((Customers customer) => {
                var result = SELECT(customer);
                FROM(customer);
                WHERE(customer.Phone != null);
                ORDER(BY(customer.FirstName), BY(customer.LastName));

                return result;
            }).AsEnumerable();

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName, customer.Phone ?? "NULL"));
            #endregion

        }
    }
}
