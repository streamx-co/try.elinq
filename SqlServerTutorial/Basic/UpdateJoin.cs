using System;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.ScalarFunctions;
using static Streamx.Linq.SQL.Library;

namespace SqlServerTutorial.Basic {
    class UpdateJoin {
        private MyContext DbContext { get; }

        public UpdateJoin(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var rows = DbContext.Database.Query((Commissions commissions, Commissions c, Targets t) => {
                UPDATE(commissions)
                    .SET(() => commissions.Commission = c.BaseAmount * t.Percentage);
                FROM(c).JOIN(t).ON(c.Target == t);
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        #region Declarations
        readonly Commissions c1 = new Commissions() {
            StaffId = 6,
            BaseAmount = 100000M,
        };

        readonly Commissions c2 = new Commissions() {
            StaffId = 7,
            BaseAmount = 120000M,
        };
        #endregion

        public void B() {

            #region B
            var coalesce = 0.1M;
            var query = DbContext.Commissions.Query((Commissions commissions, Commissions c, Targets t) => {
                var set = commissions.@using((commissions.StaffId, commissions.BaseAmount, commissions.TargetId));
                INSERT().INTO(set);
                VALUES(set.RowFrom(c1), set.RowFrom(c2));

                Semicolon();

                UPDATE(commissions)
                    .SET(() => commissions.Commission = c.BaseAmount * COALESCE(t.Percentage, coalesce));
                FROM(c).LEFT_JOIN(t).ON(c.Target == t);

                Semicolon();

                return SelectAll(commissions);
            });

            foreach (var commission in query)
                Console.WriteLine((commission.StaffId, commission.TargetId?.ToString() ?? "NULL", commission.BaseAmount, commission.Commission));
            #endregion

        }
    }
}
