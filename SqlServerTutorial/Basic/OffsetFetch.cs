using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;

namespace SqlServerTutorial.Basic {
    class OffsetFetch {
        private MyContext DbContext { get; }

        public OffsetFetch(MyContext context) {
            DbContext = context;
        }

        public void Offset() {

            #region Offset
            var query = DbContext.Products.Query((Products products) => {
                var result = SELECT(products);
                FROM(products);
                ORDER(BY(products.ListPrice), BY(products.ProductName));
                OFFSET(10).ROWS().FETCH_NEXT(10).ROWS_ONLY();

                return result;
            });

            foreach (var product in query)
                Console.WriteLine((product.ProductName, product.ListPrice));
            #endregion

        }
    }
}
