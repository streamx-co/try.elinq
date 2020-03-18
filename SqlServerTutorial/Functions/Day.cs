using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using Streamx.Linq.SQL.TransactSQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.ScalarFunctions;

namespace SqlServerTutorial.Functions {
    class Day {
        private MyContext DbContext { get; }

        public Day(MyContext context) {
            DbContext = context;
        }

        public void CalcGrossSalesByDay() {

            #region CalcGrossSalesByDay
            var year = 2017;
            var month = 2;

            var query = DbContext.Set<GrossSalesByDay>()
                .Query((Orders orders, OrderItems orderItems, GrossSalesByDay alias) => {
                    var grossSales = SUM(orderItems.ListPrice * orderItems.Quantity);
                    var shippedDay = orders.ShippedDate?.Day;

                    var r = SELECT<GrossSalesByDay>(shippedDay.@as(alias.Day),
                        grossSales.@as(alias.GrossSales));

                    FROM(orders).JOIN(orderItems).ON(orderItems.Order == orders);
                    WHERE(orders.ShippedDate != null &&
                          orders.ShippedDate?.Year == year &&
                          MONTH(orders.ShippedDate) == month);
                    GROUP(BY(shippedDay));

                    return r;
                })
                .OrderBy(gs => gs.Day);

            foreach (var grossSalesByDay in query.Take(3))
                Console.WriteLine((grossSalesByDay.Day, grossSalesByDay.GrossSales));
            #endregion

        }
    }
}
