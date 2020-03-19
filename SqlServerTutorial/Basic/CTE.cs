using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.Library;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class CTE {
        private MyContext DbContext { get; }

        public CTE(MyContext context) {
            DbContext = context;
        }

        [Tuple]
        class SalesAmount : StaffSales {
            public int Year { get; }
        }

        public void A() {

            #region A
            var query = DbContext.Set<StaffSales>()
                .Query((Products products) => {

                    var salesAmounts = SubQuery((Orders orders, OrderItems items, Staffs staffs, SalesAmount alias) => {

                        var sum = SUM(items.Quantity * items.ListPrice * (1 - items.Discount));
                        var fullName = $"{staffs.FirstName} {staffs.LastName}";
                        var year = orders.OrderDate.Year;

                        var r = SELECT<SalesAmount>(fullName.@as(alias.Staff),
                            sum.@as(alias.Sales),
                            year.@as(alias.Year));
                        FROM(orders).JOIN(items).ON(items.Order == orders).JOIN(staffs).ON(staffs == orders.Staff);
                        GROUP(BY(fullName), BY(year));
                        return r;
                    });

                    WITH(salesAmounts);

                    var result = SELECT<StaffSales>(salesAmounts.Staff.@as(), salesAmounts.Sales.@as());
                    FROM(salesAmounts);
                    WHERE(salesAmounts.Year == 2018);

                    return result;
                });

            foreach (var staffSales in query)
                Console.WriteLine((staffSales.Staff, staffSales.Sales));
            #endregion

        }

        public void B() {

            #region B
            var year = 2018;

            var query = DbContext.Set<Scalar<int>>()
                .Query((Scalar<int> alias) => {

                    var cteSales = SubQuery((Orders order) => {
                        var count = SELECT<Scalar<int>>(COUNT().@as(alias.Value));
                        FROM(order);
                        WHERE(order.OrderDate.Year == year);
                        GROUP(BY(order.StaffId));
                        return count;
                    });

                    WITH(cteSales);

                    var r = SELECT<Scalar<int>>(AVG(cteSales.Value).@as(alias.Value));
                    FROM(cteSales);
                    return r;
                })
                .AsEnumerable();

            Console.WriteLine(query.Single().Value);
            #endregion

        }

        public void C() {

            #region C
            var query = DbContext.Set<CTECategoryCounts>()
                .Query(() => {

                    var categoryCounts = SubQuery((Products p,
                        Categories cat,
                        CTECategoryCounts alias) => {

                        var r = SELECT<CTECategoryCounts>(cat.CategoryId.@as(alias.CategoryId),
                            cat.CategoryName.@as(alias.CategoryName),
                            COUNT().@as(alias.ProductCount));

                        FROM(p).JOIN(cat).ON(p.Category == cat);

                        GROUP(BY(cat.CategoryId), BY(cat.CategoryName));

                        return r;
                    });

                    var categorySales = SubQuery((OrderItems oi,
                        Products p,
                        Orders o,
                        CTECategorySales alias) => {

                        var catId = p.Category.CategoryId;
                        var sales = SUM(oi.Quantity * oi.ListPrice * (1 - oi.Discount));

                        var r = SELECT<CTECategorySales>(catId.@as(alias.CategoryId), sales.@as(alias.Sales));

                        FROM(oi).JOIN(p).ON(p == oi.Product).JOIN(o).ON(o == oi.Order);
                        WHERE(o.OrderStatus == 4); // completed
                        GROUP(BY(catId));

                        return r;
                    });

                    WITH(categoryCounts, categorySales);

                    var r1 = SELECT(categoryCounts, categorySales.Sales.@as());

                    FROM(categoryCounts)
                        .JOIN(categorySales)
                        .ON(categorySales.CategoryId == categoryCounts.CategoryId);

                    ORDER(BY(categoryCounts.CategoryName));

                    return r1;
                });

            foreach (var categoryCounts in query)
                Console.WriteLine((categoryCounts.CategoryId, categoryCounts.CategoryName, categoryCounts.ProductCount, categoryCounts.Sales));
            #endregion

        }

        public void C_1() {

            #region C_1
            var query = DbContext.Set<CategoryCounts>()
                .Query(() => {

                    var categoryCounts = SubQuery((Products p,
                        Categories cat,
                        CategoryCounts alias) => {

                        var r = SELECT<CategoryCounts>(cat.CategoryId.@as(alias.Category.CategoryId),
                            COUNT().@as(alias.ProductCount));

                        FROM(p).JOIN(cat).ON(p.Category == cat);

                        GROUP(BY(cat.CategoryId));

                        return r;
                    });

                    var categorySales = SubQuery((OrderItems oi,
                        Products p,
                        Orders o,
                        CTECategorySales alias) => {

                        var catId = p.Category.CategoryId;
                        var sales = SUM(oi.Quantity * oi.ListPrice * (1 - oi.Discount));

                        var r = SELECT<CTECategorySales>(catId.@as(alias.CategoryId), sales.@as(alias.Sales));

                        FROM(oi).JOIN(p).ON(p == oi.Product).JOIN(o).ON(o == oi.Order);
                        WHERE(o.OrderStatus == 4); // completed
                        GROUP(BY(catId));

                        return r;
                    });

                    // WITH(categoryCounts, categorySales);

                    var r1 = SELECT(categoryCounts, categorySales.Sales.@as());

                    FROM(categoryCounts)
                        .JOIN(categorySales)
                        .ON(categorySales.CategoryId == categoryCounts.Category.CategoryId);

                    return r1;
                })
                .Include(cc => cc.Category)
                .OrderBy(cc => cc.Category.CategoryName);

            foreach (var categoryCounts in query.Take(3))
                Console.WriteLine((categoryCounts.Category.CategoryId, categoryCounts.Category.CategoryName, categoryCounts.ProductCount,
                    categoryCounts.Sales));
            #endregion

        }
    }
}
