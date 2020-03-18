using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Advanced {
    class ScalarUDF {
        private MyContext DbContext { get; }

        public ScalarUDF(MyContext context) {
            DbContext = context;
        }

        #region CallScalarUDF
        [Function("sales.udfNetSale")]
        public static decimal NetSale(int quantity, decimal listPrice, decimal discount) {
            // the method is not supported on .NET platform.
            // It's supported in SQL Server only
            throw new NotSupportedException();
        }

        public void CallScalarUDF() {
            var quantity = 10;
            var listPrice = 100M;
            var discount = 0.1M;

            var scalar = DbContext.Set<Scalar<decimal>>()
                .Query((Scalar<decimal> alias) =>
                    SELECT<Scalar<decimal>>(NetSale(quantity, listPrice, discount).@as(alias.Value)))
                .Single();

            Console.WriteLine(scalar.Value);
        }
        #endregion

        public void T1() {

            #region T1
            var query = DbContext.Set<OrderNetSale>()
                .Query((OrderItems orderItems, OrderNetSale alias) => {
                    var sum = SUM(NetSale(orderItems.Quantity, orderItems.ListPrice, orderItems.Discount));

                    var result = SELECT<OrderNetSale>(orderItems.OrderId.@as(alias.Order.OrderId), sum.@as(alias.NetSale));
                    FROM(orderItems);
                    GROUP(BY(orderItems.OrderId));

                    return result;
                })
                .OrderByDescending(s => s.NetSale)
                .Include(c => c.Order);

            foreach (var orderNetValue in query.Take(3))
                Console.WriteLine((orderNetValue.Order.OrderId, orderNetValue.NetSale));
            #endregion

        }
    }
}
