using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;
using static Streamx.Linq.SQL.AggregateFunctions;

namespace SqlServerTutorial.Basic {
    class Having {
        private MyContext DbContext { get; }

        public Having(MyContext context) {
            DbContext = context;
        }

        public void T3() {

            #region T3
            var query = DbContext.Set<CustomerYearOrders>()
                .Query((Orders order, CustomerYearOrders alias) => {
                    var year = YEAR(order.OrderDate);
                    var customerId = order.CustomerId;
                    var count = COUNT(order.OrderId);

                    var result = SELECT<CustomerYearOrders>(customerId.@as(alias.Customer.CustomerId),
                        year.@as(alias.Year),
                        count.@as(alias.OrdersPlaced));
                    FROM(order);
                    GROUP(BY(customerId), BY(year));
                    HAVING(count >= 2);

                    return result;
                })
                .OrderBy(c => c.Customer)
                .Include(c => c.Customer);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.Customer.CustomerId, customer.Year));
            #endregion

        }

        public void T4() {

            #region T4
            var query = DbContext.Set<OrderNetValue>()
                .Query((OrderItems orderItems, OrderNetValue alias) => {
                    var sum = SUM(orderItems.Quantity * orderItems.ListPrice * (1 - orderItems.Discount));

                    var result = SELECT<OrderNetValue>(orderItems.OrderId.@as(alias.Order.OrderId), sum.@as(alias.Value));
                    FROM(orderItems);
                    GROUP(BY(orderItems.OrderId));
                    HAVING(sum > 20000);

                    return result;
                })
                .OrderBy(c => c.Value)
                .Include(c => c.Order);

            foreach (var orderNetValue in query.Take(3))
                Console.WriteLine((orderNetValue.Order.OrderId, orderNetValue.Value));
            #endregion

        }
    }
}
