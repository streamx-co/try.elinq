using System;
using System.Linq;
using SqlServerTutorial.Basic;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using static Streamx.Linq.SQL.Library;

namespace SqlServerTutorial.Advanced {
    class TableUDF {
        private MyContext DbContext { get; }

        public TableUDF(MyContext context) {
            DbContext = context;
        }

        [Function("udfProductInYear", RequiresAlias = true), Tuple]
        public static ProductInYear GetProductInYear(int year) {
            // the method is not supported on .NET platform.
            // It's supported in SQL Server only
            throw new NotSupportedException();
        }

        public void T1() {

            #region T1
            var query = DbContext.Set<ProductInYear>()
                .Query(() => SelectAll(GetProductInYear(2017)));

            foreach (var orderNetValue in query.Take(3))
                Console.WriteLine((orderNetValue.ProductName, orderNetValue.ModelYear, orderNetValue.ListPrice));
            #endregion

        }

        public void T2() {

            #region T2
            var query = DbContext.Set<ProductInYear>()
                .Query(() => SelectAll(GetProductInYear(2017)))
                .OrderBy(p => p.ProductName);

            foreach (var orderNetValue in query.Take(3))
                Console.WriteLine((orderNetValue.ProductName, orderNetValue.ModelYear, orderNetValue.ListPrice));
            #endregion

        }
    }
}
