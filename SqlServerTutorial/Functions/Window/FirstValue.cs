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
    class FirstValue {
        private MyContext DbContext { get; }

        public FirstValue(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var year = 2016;

            var query = DbContext.Set<SalesVolume>()
                .Query((VwCategorySalesVolume salesVolume, SalesVolume alias) => {
                    var lowestSalesVolume = AggregateBy(FIRST_VALUE(salesVolume.CategoryName))
                        .OVER(ORDER(BY(salesVolume.Qty))
                            .RANGE()
                            .BETWEEN(FrameBounds.UNBOUNDED_PRECEDING)
                            .AND(FrameBounds.UNBOUNDED_FOLLOWING));

                    var r = SELECT<SalesVolume>(salesVolume.CategoryName.@as(alias.CategoryName),
                        salesVolume.Year.@as(alias.Year),
                        salesVolume.Qty.@as(alias.Quantity),
                        lowestSalesVolume.@as(alias.VolumeCategory));
                    FROM(salesVolume);
                    WHERE(salesVolume.Year == year);

                    return r;
                });

            foreach (var salesVolume in query)
                Console.WriteLine((salesVolume.Year, salesVolume.CategoryName, salesVolume.Quantity, salesVolume.VolumeCategory));
            #endregion

        }

        public void B() {

            #region B
            var years = new int?[] {2016, 2017};

            var query = DbContext.Set<SalesVolume>()
                .Query((VwCategorySalesVolume salesVolume, SalesVolume alias) => {
                    var lowestSalesVolume = AggregateBy(FIRST_VALUE(salesVolume.CategoryName))
                        .OVER(PARTITION(BY(salesVolume.Year))
                            .ORDER(BY(salesVolume.Qty))
                            .RANGE()
                            .BETWEEN(FrameBounds.UNBOUNDED_PRECEDING)
                            .AND(FrameBounds.UNBOUNDED_FOLLOWING));

                    var r = SELECT<SalesVolume>(salesVolume.CategoryName.@as(alias.CategoryName),
                        salesVolume.Year.@as(alias.Year),
                        salesVolume.Qty.@as(alias.Quantity),
                        lowestSalesVolume.@as(alias.VolumeCategory));
                    FROM(salesVolume);
                    WHERE(years.Contains(salesVolume.Year));

                    return r;
                });

            foreach (var salesVolume in query)
                Console.WriteLine((salesVolume.Year, salesVolume.CategoryName, salesVolume.Quantity, salesVolume.VolumeCategory));
            #endregion

        }
    }
}
