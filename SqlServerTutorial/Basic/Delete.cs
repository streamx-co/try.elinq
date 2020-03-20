using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class Delete {
        
        const String PRODUCT_HISTORY = "#product_history";
        
        private MyContext DbContext { get; }

        public Delete(MyContext context) {
            DbContext = context;
        }
        
        public void T1() {

            #region T1
            var rows = DbContext.Database.Query((Products products) => {
                PrepareProductHistory(products);
                var productHistory = ToTable<Products>(PRODUCT_HISTORY);

                DELETE(TOP(21)).FROM(productHistory);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion
        }

        public void T2() {

            #region T2
            var rows = DbContext.Database.Query((Products products) => {
                PrepareProductHistory(products);
                var productHistory = ToTable<Products>(PRODUCT_HISTORY);

                DELETE(TOP(5).PERCENT()).FROM(productHistory);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion
        }
        
        public void T3() {

            #region T3
            var rows = DbContext.Database.Query((Products products) => {
                PrepareProductHistory(products);
                var productHistory = ToTable<Products>(PRODUCT_HISTORY);

                DELETE().FROM(productHistory);
                WHERE(productHistory.ModelYear == 2017);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion
        }
        
        public void T4() {

            #region T4
            var rows = DbContext.Database.Query((Products products) => {
                PrepareProductHistory(products);
                var productHistory = ToTable<Products>(PRODUCT_HISTORY);

                DELETE().FROM(productHistory);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion
        }

        #region Declarations
        private static void PrepareProductHistory(Products products) {
            var productHistory = ToTable<Products>(PRODUCT_HISTORY);

            SELECT(products).INTO(productHistory);
            FROM(products);

            Semicolon();
        }
        #endregion
    }
}
