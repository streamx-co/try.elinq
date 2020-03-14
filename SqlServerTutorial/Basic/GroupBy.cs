using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.Operators;

namespace SqlServerTutorial.Basic {
    class GroupBy {
        private MyContext DbContext { get; }

        public GroupBy(MyContext context) {
            DbContext = context;
        }
        
        public void T1() {

            #region T1
            var customerIds = new int?[] {1, 2};

            var query = DbContext.Set<CustomerYear>()
                .Query((Orders order, CustomerYear alias) => {
                    var year = YEAR(order.OrderDate);
                    var customerId = order.CustomerId;

                    var result = SELECT<CustomerYear>(customerId.@as(alias.Customer.CustomerId), year.@as(alias.Year));
                    FROM(order);
                    WHERE(customerIds.Contains(customerId));
                    GROUP(BY(customerId), BY(year));

                    return result;
                })
                .OrderBy(c => c.Customer)
                .Include(c => c.Customer);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.Customer.CustomerId, customer.Year));
            #endregion

        }

        public void T2() {

            #region T2
            var customerIds = new[] {1, 2};

            var query = DbContext.Set<CustomerYear>()
                .Query((Orders order, CustomerYear alias) => {
                    var year = YEAR(order.OrderDate);
                    var customerId = order.CustomerId;

                    var result = SELECT(DISTINCT<CustomerYear>(customerId.@as(alias.Customer.CustomerId), year.@as(alias.Year)));
                    FROM(order);
                    WHERE(IN(customerId, customerIds));
                    GROUP(BY(customerId), BY(year));

                    return result;
                })
                .OrderBy(c => c.Customer)
                .Include(c => c.Customer);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.Customer.CustomerId, customer.Year));
            #endregion

        }

        public void T3() {

            #region T3
            var customerIds = new[] {1, 2};

            var query = DbContext.Set<CustomerYearOrders>()
                .Query((Orders order, CustomerYearOrders alias) => {
                    var year = YEAR(order.OrderDate);
                    var customerId = order.CustomerId;
                    var count = COUNT(order.OrderId);

                    var result = SELECT<CustomerYearOrders>(customerId.@as(alias.Customer.CustomerId),
                        year.@as(alias.Year),
                        count.@as(alias.OrdersPlaced));
                    FROM(order);
                    WHERE(IN(customerId, customerIds));
                    GROUP(BY(customerId), BY(year));

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

                    return result;
                })
                .Include(c => c.Order);

            foreach (var orderNetValue in query.Take(3))
                Console.WriteLine((orderNetValue.Order.OrderId, orderNetValue.Value));
            #endregion

        }
    }
}
