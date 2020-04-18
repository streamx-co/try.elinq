using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.TransactSQL;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class InsertSelect {
        private MyContext DbContext { get; }

        public InsertSelect(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            #region T1
            var rows = DbContext.Database.Execute((Customers customer, Addresses address) => {
                var set = address.@using((address.Street, address.City, address.State, address.ZipCode));

                INSERT().INTO(set);
                SELECT((customer.Street, customer.City, customer.State, customer.ZipCode));
                FROM(customer);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        public void T2() {

            #region T2
            var cities = new[] {"Santa Cruz", "Baldwin"};

            var rows = DbContext.Database.Execute((Stores stores, Addresses address) => {
                var set = address.@using((address.Street, address.City, address.State, address.ZipCode));

                INSERT().INTO(set);
                SELECT((stores.Street, stores.City, stores.State, stores.ZipCode));
                FROM(stores);
                WHERE(cities.Contains(stores.City));
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        public void T3() {

            #region T3
            var rows = DbContext.Database.Execute((Customers customer, Addresses address) => {
                var set = address.@using((address.Street, address.City, address.State, address.ZipCode));

                INSERT(TOP(10)).INTO(set);
                SELECT((customer.Street, customer.City, customer.State, customer.ZipCode));
                FROM(customer);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        public void T4() {

            #region T4
            var rows = DbContext.Database.Execute((Customers customer, Addresses address) => {
                var set = address.@using((address.Street, address.City, address.State, address.ZipCode));

                INSERT(TOP(10).PERCENT()).INTO(set);
                SELECT((customer.Street, customer.City, customer.State, customer.ZipCode));
                FROM(customer);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }
    }
}
