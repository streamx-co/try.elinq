using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using Streamx.Linq.SQL.TransactSQL;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.ScalarFunctions;

namespace SqlServerTutorial.Functions {
    class Aggregate {
        private MyContext DbContext { get; }

        public Aggregate(MyContext context) {
            DbContext = context;
        }

        #region CalcAveragePrice
        public static readonly DataType<decimal> USD = DataTypes.DECIMAL.Numeric(10, 2);

        public void CalcAveragePrice() {

            var avgProductPrice = DbContext.Set<Scalar<decimal>>()
                .Query((Products products, Scalar<decimal> alias) => {
                    var result = SELECT<Scalar<decimal>>(USD.Cast(ROUND(AVG(products.ListPrice), 2)).@as(alias.Value));
                    FROM(products);

                    return result;
                })
                .Single();

            Console.WriteLine(avgProductPrice.Value);
        }
        #endregion
    }
}
