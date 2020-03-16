using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.ScalarFunctions;

namespace SqlServerTutorial.Basic {
    class GroupingSet {
        private MyContext DbContext { get; }

        public GroupingSet(MyContext context) {
            DbContext = context;
        }

        public void SalesSummary() {

            #region SalesSummary
            var sales_summary = "#sales_summary";

            DbContext.Database.Query((OrderItems i, Products p, Brands b, Categories c) => {
                var salesSummary = ToTable<SalesSummaryByYear>(sales_summary);

                SELECT<SalesSummaryByYear>(b.BrandName.@as(salesSummary.Brand),
                        c.CategoryName.@as(salesSummary.Category),
                        p.ModelYear.@as(salesSummary.ModelYear),
                        ROUND(SUM(i.Quantity * i.ListPrice * (1 - i.Discount)), 0).@as(salesSummary.Sales))
                    .INTO(salesSummary);
                FROM(i).JOIN(p).ON(i.Product == p).JOIN(b).ON(p.Brand == b).JOIN(c).ON(p.Category == c);
                GROUP(BY(b.BrandName), BY(c.CategoryName), BY(p.ModelYear));
            });

            var query = DbContext.Set<SalesSummaryByYear>()
                .Query(() => {
                    var salesSummary = ToTable<SalesSummaryByYear>(sales_summary);

                    var result = SELECT(salesSummary);
                    FROM(salesSummary);

                    return result;
                })
                .OrderBy(c => c.Brand)
                .ThenBy(c => c.Category)
                .ThenBy(c => c.ModelYear);

            foreach (var salesSummary in query.Take(3))
                Console.WriteLine((salesSummary.Brand, salesSummary.Category, salesSummary.ModelYear, salesSummary.Sales));
            #endregion

        }

        private static SalesSummaryByYear GetSalesSummaryTable() {
            return SubQuery((OrderItems i, Products p, Brands b, Categories c, SalesSummaryByYear alias) => {
                var r = SELECT<SalesSummaryByYear>(b.BrandName.@as(alias.Brand),
                    c.CategoryName.@as(alias.Category),
                    p.ModelYear.@as(alias.ModelYear),
                    ROUND(SUM(i.Quantity * i.ListPrice * (1 - i.Discount)), 0).@as(alias.Sales));
                FROM(i).JOIN(p).ON(i.Product == p).JOIN(b).ON(p.Brand == b).JOIN(c).ON(p.Category == c);
                GROUP(BY(b.BrandName), BY(c.CategoryName), BY(p.ModelYear));

                return r;
            });
        }

        public void SalesSummary1() {

            #region SalesSummary1
            var query = DbContext.Set<SalesSummaryByYear>()
                .Query(() => {
                    var salesSummary = GetSalesSummaryTable();

                    var result = SELECT(salesSummary);
                    FROM(salesSummary);

                    return result;
                })
                .OrderBy(ss => ss.Brand)
                .ThenBy(ss => ss.Category)
                .ThenBy(ss => ss.ModelYear);

            foreach (var salesSummary in query.Take(3))
                Console.WriteLine((salesSummary.Brand, salesSummary.Category, salesSummary.ModelYear, salesSummary.Sales));
            #endregion

        }

        public void SalesSummary2() {

            #region SalesSummary2
            var query = DbContext.Set<SalesGrouping>()
                .Query((SalesGrouping alias) => {
                    var salesSummary = GetSalesSummaryTable();

                    var result = SELECT<SalesGrouping>(GROUPING(salesSummary.Brand).@as(alias.GroupingBrand),
                        GROUPING(salesSummary.Category).@as(alias.GroupingCategory),
                        salesSummary.Brand.@as(alias.Brand),
                        salesSummary.Category.@as(alias.Category),
                        SUM(salesSummary.Sales).@as(alias.Sales));

                    FROM(salesSummary);
                    GROUP(BY(GROUPING_SETS(SET(salesSummary.Brand, salesSummary.Category),
                        SET(salesSummary.Brand),
                        SET(salesSummary.Category),
                        SET())));

                    return result;
                })
                .OrderBy(ss => ss.Brand)
                .ThenBy(ss => ss.Category);

            foreach (var salesSummary in query.Take(3))
                Console.WriteLine((salesSummary.GroupingBrand, salesSummary.GroupingCategory, salesSummary.Brand ?? "NULL", salesSummary.Category ?? "NULL",
                    salesSummary.Sales));
            #endregion

        }

        public void SalesSummary3() {

            #region SalesSummary3
            var sales_summary = "#sales_summary";

            DbContext.Database.Query(() => {
                var salesSummary = ToTable<SalesSummaryByYear>(sales_summary);

                var salesSummarySource = GetSalesSummaryTable();

                SELECT(salesSummarySource).INTO(salesSummary);
                FROM(salesSummarySource);
            });
            #endregion

        }

        public void SalesCube() {

            #region SalesCube
            var query = DbContext.Set<SalesSummary>()
                .Query((SalesSummary alias) => {
                    var salesSummary = GetSalesSummaryTable();

                    var result = SELECT<SalesSummary>(salesSummary.Brand.@as(alias.Brand),
                        salesSummary.Category.@as(alias.Category),
                        SUM(salesSummary.Sales).@as(alias.Sales));

                    FROM(salesSummary);
                    GROUP(BY(CUBE(salesSummary.Brand, salesSummary.Category)));

                    return result;
                });

            foreach (var salesSummary in query.Take(3))
                Console.WriteLine((salesSummary.Brand, salesSummary.Category, salesSummary.Sales));
            #endregion

        }
        
        public void SalesRollup() {

            #region SalesRollup
            var query = DbContext.Set<SalesSummary>()
                .Query((SalesSummary alias) => {
                    var salesSummary = GetSalesSummaryTable();

                    var result = SELECT<SalesSummary>(salesSummary.Brand.@as(alias.Brand),
                        salesSummary.Category.@as(alias.Category),
                        SUM(salesSummary.Sales).@as(alias.Sales));

                    FROM(salesSummary);
                    GROUP(BY(ROLLUP(salesSummary.Brand, salesSummary.Category)));

                    return result;
                });

            foreach (var salesSummary in query.Take(3))
                Console.WriteLine((salesSummary.Brand, salesSummary.Category, salesSummary.Sales));
            #endregion

        }
    }
}
