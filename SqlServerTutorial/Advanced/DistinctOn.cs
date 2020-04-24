using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using SqlServerTutorial.Basic;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Advanced {
    class DistinctOn {
        private MyContext DbContext { get; }

        public DistinctOn(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            var ins = new InsertMulti(DbContext);
            ins.T2_2();

            #region T1
            var query = DbContext.Promotions.Query((Promotions promo) => {
                // partition by StartDate-ExpiredDate and order by Discount
                var window = PARTITION(BY(promo.StartDate), BY(promo.ExpiredDate)).ORDER(BY(promo.Discount).DESC);

                var r = SELECT(DISTINCT<Promotions>(promo.StartDate.@as(), promo.ExpiredDate.@as(),
                    // take the FIRST_VALUE which corresponds to the row with the highest discount
                    AggregateBy(FIRST_VALUE(promo.Discount)).OVER(window).@as(promo.Discount),
                    AggregateBy(FIRST_VALUE(promo.PromotionId)).OVER(window).@as(promo.PromotionId),
                    AggregateBy(FIRST_VALUE(promo.PromotionName)).OVER(window).@as(promo.PromotionName)));
                FROM(promo);
                return r;
            });

            foreach (var promo in query)
                Console.WriteLine((promo.PromotionId, promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
            #endregion

        }
    }
}
