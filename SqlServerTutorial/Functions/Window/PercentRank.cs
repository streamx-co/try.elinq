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
    class PercentRank {
        private MyContext DbContext { get; }

        public PercentRank(MyContext context) {
            DbContext = context;
        }
        
        public void A() {

            #region A
            var year = 2017;

            var query = DbContext.Set<StaffSalesPercentile>()
                .Query((VwStaffSales sales, Staffs staffs, StaffSalesPercentile alias) => {

                    var fullName = CONCAT_WS(" ", staffs.FirstName, staffs.LastName);
                    var cumeDist = AggregateBy(PERCENT_RANK())
                        .OVER(ORDER(BY(sales.NetSales).DESC));

                    var r = SELECT<StaffSalesPercentile>(fullName.@as(alias.FullName),
                        sales.NetSales.@as(alias.NetSales),
                        sales.Year.@as(alias.Year),
                        cumeDist.@as(alias.Percentile));
                    FROM(sales).JOIN(staffs).ON(staffs.StaffId == sales.StaffId);
                    WHERE(sales.Year == year);

                    return r;
                });

            foreach (var percentile in query)
                Console.WriteLine((percentile.Year, percentile.FullName, percentile.NetSales, percentile.Percentile));
            #endregion

        }

        public void B() {

            #region B
            var years = new int?[] {2016, 2017};

            var query = DbContext.Set<StaffSalesPercentile>()
                .Query((VwStaffSales sales, Staffs staffs, StaffSalesPercentile alias) => {

                    var fullName = CONCAT_WS(" ", staffs.FirstName, staffs.LastName);
                    var cumeDist = AggregateBy(PERCENT_RANK())
                        .OVER(PARTITION(BY(sales.Year))
                            .ORDER(BY(sales.NetSales).DESC));

                    var r = SELECT<StaffSalesPercentile>(fullName.@as(alias.FullName),
                        sales.NetSales.@as(alias.NetSales),
                        sales.Year.@as(alias.Year),
                        cumeDist.@as(alias.Percentile));
                    FROM(sales).JOIN(staffs).ON(staffs.StaffId == sales.StaffId);
                    WHERE(years.Contains(sales.Year));

                    return r;
                });

            foreach (var percentile in query)
                Console.WriteLine((percentile.Year, percentile.FullName, percentile.NetSales, percentile.Percentile));
            #endregion

        }
    }
}
