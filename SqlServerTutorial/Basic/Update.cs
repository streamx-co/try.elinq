using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.TransactSQL;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class Update {
        private MyContext DbContext { get; }

        public Update(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            #region T1
            var rows = DbContext.Database.Query((Taxes taxes) =>
                UPDATE(taxes).SET(() => taxes.UpdatedAt = GETDATE()));

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        public void T2() {

            #region T2
            var one = 0.01M;
            var two = 0.02M;
            var rows = DbContext.Database.Query((Taxes taxes) => {
                UPDATE(taxes)
                    .SET(() => {
                        taxes.MaxLocalTaxRate += two;
                        taxes.AvgLocalTaxRate += one;
                    });
                WHERE(taxes.MaxLocalTaxRate == one);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }
    }
}
