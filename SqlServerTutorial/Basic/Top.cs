using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class Top {
        private MyContext DbContext { get; }

        public Top(MyContext context) {
            DbContext = context;
        }

        public void T2() {

            #region T2
            var query = DbContext.Products.Query((Products products) => {
                var result = SELECT(TOP(1).PERCENT().Of(products));
                FROM(products);
                ORDER(BY(products.ListPrice).DESC);

                return result;
            });

            foreach (var product in query)
                Console.WriteLine((product.ProductName, product.ListPrice));
            #endregion

        }
        
        public void T3() {

            #region T3
            var query = DbContext.Products.Query((Products products) => {
                var result = SELECT(TOP(3).WITH_TIES().Of(products));
                FROM(products);
                ORDER(BY(products.ListPrice).DESC);

                return result;
            });

            foreach (var product in query.Take(6))
                Console.WriteLine((product.ProductName, product.ListPrice));
            #endregion

        }
    }
}
