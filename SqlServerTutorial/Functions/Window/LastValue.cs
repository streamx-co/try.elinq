using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.ScalarFunctions;
using static Streamx.Linq.SQL.AggregateFunctions;

namespace SqlServerTutorial.Functions.Window {
    class LastValue {
        private MyContext DbContext { get; }

        public LastValue(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var year = 2016;

            var query = DbContext.Set<HighestSalesVolume>()
                .Query((VwCategorySalesVolume salesVolume, HighestSalesVolume alias) => {

                    var highestSalesVolume = AggregateBy(LAST_VALUE(salesVolume.CategoryName))
                        .OVER(ORDER(BY(salesVolume.Qty))
                            .RANGE()
                            .BETWEEN(FrameBounds.UNBOUNDED_PRECEDING)
                            .AND(FrameBounds.UNBOUNDED_FOLLOWING));

                    var r = SELECT<HighestSalesVolume>(salesVolume.CategoryName.@as(alias.CategoryName),
                        salesVolume.Year.@as(alias.Year),
                        salesVolume.Qty.@as(alias.Quantity),
                        highestSalesVolume.@as(alias.HighestCategory));
                    FROM(salesVolume);
                    WHERE(salesVolume.Year == year);

                    return r;
                });

            foreach (var salesVolume in query)
                Console.WriteLine((salesVolume.CategoryName, salesVolume.Year, salesVolume.Quantity, salesVolume.HighestCategory));
            #endregion

        }

        public void B() {

            #region B
            var years = new int?[] {2016, 2017};

            var query = DbContext.Set<HighestSalesVolume>()
                .Query((VwCategorySalesVolume salesVolume, HighestSalesVolume alias) => {

                    var highestSalesVolume = AggregateBy(LAST_VALUE(salesVolume.CategoryName))
                        .OVER(PARTITION(BY(salesVolume.Year))
                            .ORDER(BY(salesVolume.Qty))
                            .RANGE()
                            .BETWEEN(FrameBounds.UNBOUNDED_PRECEDING)
                            .AND(FrameBounds.UNBOUNDED_FOLLOWING));

                    var r = SELECT<HighestSalesVolume>(salesVolume.CategoryName.@as(alias.CategoryName),
                        salesVolume.Year.@as(alias.Year),
                        salesVolume.Qty.@as(alias.Quantity),
                        highestSalesVolume.@as(alias.HighestCategory));
                    FROM(salesVolume);
                    WHERE(years.Contains(salesVolume.Year));

                    return r;
                });

            foreach (var salesVolume in query)
                Console.WriteLine((salesVolume.CategoryName, salesVolume.Year, salesVolume.Quantity, salesVolume.HighestCategory));
            #endregion

        }
    }
}
