using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.ScalarFunctions;
using static Streamx.Linq.SQL.AggregateFunctions;
using SubQuery = SqlServerTutorial.Basic.SubQuery;

namespace SqlServerTutorial.Functions.Window {
    class RowNumber {
        private MyContext DbContext { get; }

        public RowNumber(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var query = DbContext.Set<CustomerOrder>()
                .Query((Customers customer, CustomerOrder alias) => {

                    var rowNum = AggregateBy(ROW_NUMBER())
                        .OVER(ORDER(BY(customer.FirstName)));

                    var r = SELECT<CustomerOrder>(customer.CustomerId.@as(alias.Customer.CustomerId),
                        rowNum.@as(alias.RowNumber));
                    FROM(customer);

                    return r;
                })
                .Include(r => r.Customer);

            foreach (var order in query.Take(3))
                Console.WriteLine((order.RowNumber, order.Customer.FirstName, order.Customer.LastName, order.Customer.City));
            #endregion

        }

        public void B() {

            #region B
            var query = DbContext.Set<CustomerOrder>()
                .Query((Customers customer, CustomerOrder alias) => {

                    var rowNum = AggregateBy(ROW_NUMBER())
                        .OVER(PARTITION(BY(customer.City))
                            .ORDER(BY(customer.FirstName)));

                    var r = SELECT<CustomerOrder>(customer.CustomerId.@as(alias.Customer.CustomerId),
                        rowNum.@as(alias.RowNumber));
                    FROM(customer);

                    return r;
                })
                .OrderBy(r => r.Customer.City)
                .Include(r => r.Customer);

            foreach (var order in query.Take(5))
                Console.WriteLine((order.RowNumber, order.Customer.FirstName, order.Customer.LastName, order.Customer.City));
            #endregion

        }

        public void C() {

            #region C
            var query = DbContext.Customers
                .Query((Customers customer) => {
                    var order = SubQuery((CustomerOrder alias) => {

                        var rowNum = AggregateBy(ROW_NUMBER())
                            .OVER(ORDER(BY(customer.FirstName), BY(customer.LastName)));

                        var r = SELECT<CustomerOrder>(customer.CustomerId.@as(alias.Customer.CustomerId),
                            rowNum.@as(alias.RowNumber));
                        FROM(customer);

                        return r;
                    });

                    var r = SELECT(customer);
                    FROM(customer).JOIN(order).ON(customer == order.Customer);
                    WHERE(order.RowNumber > 20 && order.RowNumber <= 30);
                    return r;
                });

            foreach (var customer in query)
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName));
            #endregion

        }
    }
}
