using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;
using static Streamx.Linq.SQL.Library;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.Operators;

namespace SqlServerTutorial.Basic {
    class SubQuery {
        private MyContext DbContext { get; }

        public SubQuery(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            #region T1
            var city = "New York";

            var query = DbContext.Set<SalesOrder>()
                .Query((Orders orders, SalesOrder alias) => {
                    var customersInCity = SubQuery((Customers customers) => {
                        var id = SELECT((int?) customers.CustomerId);
                        FROM(customers);
                        WHERE(customers.City == city);
                        return id.AsCollection();
                    });

                    var r = SELECT<SalesOrder>(orders.CustomerId.@as(alias.Customer.CustomerId),
                        orders.OrderId.@as(alias.Order.OrderId));
                    FROM(orders);
                    WHERE(customersInCity.Contains(orders.CustomerId));

                    return r;
                })
                .OrderByDescending(so => so.Order.OrderDate)
                .Include(so => so.Customer)
                .Include(so => so.Order);

            foreach (var salesOrder in query.Take(3))
                Console.WriteLine((salesOrder.Order.OrderId, salesOrder.Order.OrderDate, salesOrder.Customer.CustomerId));
            #endregion

        }

        public void T2() {

            #region T2
            var query = DbContext.Products
                .Query((Products product) => {
                    var brands = SubQuery((Brands brand) => {
                        var id = SELECT(brand.BrandId);
                        FROM(brand);
                        WHERE(brand.BrandName == "Strider" || brand.BrandName == "Trek");
                        return id.AsCollection();
                    });

                    var avgPrice = SubQuery((Products product) => {
                        var avg = SELECT(AVG(product.ListPrice));
                        FROM(product);
                        WHERE(brands.Contains(product.BrandId));
                        return avg.AsSingle();
                    });

                    var r = SELECT(product);
                    FROM(product);
                    WHERE(product.ListPrice > avgPrice);

                    return r;
                })
                .OrderBy(p => p.ListPrice);

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductName, product.ListPrice));
            #endregion

        }

        public void T3() {

            #region T3
            var query = DbContext.Set<OrderMaxListPrice>()
                .Query((Orders orders, OrderMaxListPrice alias) => {
                    var maxListPrice = SubQuery((OrderItems items) => {
                        var max = SELECT(MAX(items.ListPrice));
                        FROM(items);
                        WHERE(items.OrderId == orders.OrderId);
                        return max.AsSingle();
                    });

                    var r = SELECT<OrderMaxListPrice>(orders.OrderId.@as(alias.Order.OrderId), maxListPrice.@as(alias.MaxListPrice));
                    FROM(orders);
                    return r;
                })
                .OrderByDescending(o => o.Order.OrderDate)
                .Include(o => o.Order);

            foreach (var order in query.Take(3))
                Console.WriteLine((order.Order.OrderId, order.Order.OrderDate, order.MaxListPrice));
            #endregion

        }

        public void T5() {

            #region T5
            var query = DbContext.Products
                .Query((Products product) => {

                    var avgBrandPrices = SubQuery((Products product) => {
                        var avg = SELECT(AVG(product.ListPrice));
                        FROM(product);
                        GROUP(BY(product.BrandId));
                        return avg.AsCollection();
                    });

                    var r = SELECT(product);
                    FROM(product);
                    WHERE(product.ListPrice >= ANY(avgBrandPrices));

                    return r;
                });

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductName, product.ListPrice));
            #endregion

        }

        public void T6() {

            #region T6
            var query = DbContext.Products
                .Query((Products product) => {

                    var avgBrandPrices = SubQuery((Products product) => {
                        var avg = SELECT(AVG(product.ListPrice));
                        FROM(product);
                        GROUP(BY(product.BrandId));
                        return avg.AsCollection();
                    });

                    var r = SELECT(product);
                    FROM(product);
                    WHERE(product.ListPrice >= ALL(avgBrandPrices));

                    return r;
                });

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductName, product.ListPrice));
            #endregion

        }

        public void T7() {

            #region T7
            var query = DbContext.Customers
                .Query((Customers customer) => {
                    var orders2017 = SubQuery((Orders order) => {
                        var count = SELECT(order.CustomerId);
                        FROM(order);
                        WHERE(order.Customer == customer && YEAR(order.OrderDate) == 2017);
                        return count.AsCollection();
                    });

                    var r = SELECT(customer);
                    FROM(customer);
                    WHERE(EXISTS(orders2017));
                    return r;
                })
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName, customer.City));
            #endregion

        }
        
        public void T8() {

            #region T8
            var query = DbContext.Customers
                .Query((Customers customer) => {
                    var orders2017 = SubQuery((Orders order) => {
                        var count = SELECT(order.CustomerId);
                        FROM(order);
                        WHERE(order.Customer == customer && YEAR(order.OrderDate) == 2017);
                        return count.AsCollection();
                    });

                    var r = SELECT(customer);
                    FROM(customer);
                    WHERE(!orders2017.Any());
                    return r;
                })
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName);

            foreach (var customer in query.Take(3))
                Console.WriteLine((customer.CustomerId, customer.FirstName, customer.LastName, customer.City));
            #endregion

        }

        public void T9() {

            #region T9
            var query = DbContext.Set<Scalar<int>>()
                .Query((Scalar<int> alias) => {
                    var order_count_by_staff = SubQuery((Orders order) => {
                        var count = SELECT<Scalar<int>>(COUNT().@as(alias.Value));
                        FROM(order);
                        GROUP(BY(order.StaffId));
                        return count;
                    });

                    var r = SELECT<Scalar<int>>(AVG(order_count_by_staff.Value).@as(alias.Value));
                    FROM(order_count_by_staff);
                    return r;
                });

            Console.WriteLine(query.Single().Value);
            #endregion

        }
    }
}
