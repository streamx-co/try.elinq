using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;

namespace SqlServerTutorial.Functions.Window {
    class Lead {
        private MyContext DbContext { get; }

        public Lead(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var year = 2017;

            var query = DbContext.Set<VwNetSalesWithNext>()
                .Query((VwNetSalesWithNext alias) => {

                    var yearSales = SubQuery((VwNetsalesBrands sales) => {
                        var r = SELECT<VwNetsalesBrands>(sales.Month.@as(), SUM(sales.NetSales).@as(sales.NetSales));
                        FROM(sales);
                        WHERE(sales.Year == year);
                        GROUP(BY(sales.Month));

                        return r;
                    });

                    WITH(yearSales);

                    var nextMonthSales = AggregateBy(LEAD(yearSales.NetSales, 1))
                        .OVER(ORDER(BY(yearSales.Month)));

                    var r = SELECT<VwNetSalesWithNext>(yearSales.Month.@as(alias.Month),
                        yearSales.NetSales.@as(alias.NetSales),
                        nextMonthSales.@as(alias.NextNetSales));
                    FROM(yearSales);

                    return r;
                })
                .AsEnumerable();

            foreach (var salesByMonth in query.Take(5))
                Console.WriteLine((salesByMonth.Month, salesByMonth.NetSales, salesByMonth.NextNetSales));
            #endregion

        }

        public void B() {

            #region B
            var year = 2018;

            var query = DbContext.Set<VwNetSalesBrandsWithNext>()
                .Query((VwNetsalesBrands sales, VwNetSalesBrandsWithNext alias) => {

                    var nextMonthSales = AggregateBy(LEAD(sales.NetSales, 1))
                        .OVER(PARTITION(BY(sales.BrandName)).ORDER(BY(sales.Month)));

                    var r = SELECT<VwNetSalesBrandsWithNext>(sales.Month.@as(alias.Month),
                        sales.BrandName.@as(alias.BrandName),
                        sales.NetSales.@as(alias.NetSales),
                        nextMonthSales.@as(alias.NextNetSales));
                    FROM(sales);
                    WHERE(sales.Year == year);

                    return r;
                });

            foreach (var salesByMonth in query.Take(5))
                Console.WriteLine((salesByMonth.Month, salesByMonth.BrandName, salesByMonth.NetSales, salesByMonth.NextNetSales));
            #endregion

        }
    }
}
