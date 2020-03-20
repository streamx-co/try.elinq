using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;

namespace SqlServerTutorial.Basic {
    class CoSubQuery {
        private MyContext DbContext { get; }

        public CoSubQuery(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            #region T1
            var query = DbContext.Products.Query((Products p1) => {
                    var highestPriceByCategory = SubQuery((Products p2) => {
                        var max = SELECT(MAX(p2.ListPrice));
                        FROM(p2);
                        WHERE(p1.Category == p2.Category);
                        GROUP(BY(p2.CategoryId));
                        return max.AsCollection();
                    });

                    var result = SELECT(p1);
                    FROM(p1);
                    WHERE(highestPriceByCategory.Contains(p1.ListPrice));

                    return result;
                })
                .OrderBy(p => p.CategoryId)
                .ThenBy(p => p.ProductName);

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductName, product.ListPrice, product.CategoryId));
            #endregion

        }
    }
}
