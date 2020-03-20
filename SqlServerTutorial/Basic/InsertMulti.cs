using System;
using System.Collections.Generic;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class InsertMulti {
        private MyContext DbContext { get; }

        public InsertMulti(MyContext context) {
            DbContext = context;
        }

        #region Declarations
        readonly Promotions newPromoSummer = new Promotions() {
            PromotionName = "2020 Summer Promotion",
            Discount = 0.25M,
            StartDate = new DateTime(2020, 06, 01),
            ExpiredDate = new DateTime(2020, 09, 01)
        };

        readonly Promotions newPromoFall = new Promotions() {
            PromotionName = "2020 Fall Promotion",
            Discount = 0.10M,
            StartDate = new DateTime(2020, 10, 01),
            ExpiredDate = new DateTime(2020, 11, 01)
        };

        readonly Promotions newPromoWinter = new Promotions() {
            PromotionName = "2020 Winter Promotion",
            Discount = 0.25M,
            StartDate = new DateTime(2020, 12, 01),
            ExpiredDate = new DateTime(2021, 01, 01)
        };
        #endregion

        public void T2() {

            #region T2
            var query = DbContext.Promotions.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
                INSERT().INTO(set);
                var r = OUTPUT(INSERTED<Promotions>());
                VALUES(set.RowFrom(newPromoSummer), set.RowFrom(newPromoFall), set.RowFrom(newPromoWinter));

                return r;
            });

            foreach (var promo in query)
                Console.WriteLine((promo.PromotionId, promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
            #endregion

        }

        public void T2_1() {

            #region T2_1
            var query = DbContext.Promotions.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
                var rows = new[] {set.RowFrom(newPromoSummer), set.RowFrom(newPromoFall), set.RowFrom(newPromoWinter)};

                INSERT().INTO(set);
                var r = OUTPUT(INSERTED<Promotions>());
                VALUES(rows);

                return r;
            });

            foreach (var promo in query)
                Console.WriteLine((promo.PromotionId, promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
            #endregion

        }

        public void T2_2() {

            var promos = new List<Promotions>() {newPromoSummer, newPromoFall, newPromoWinter};

            T2_Batch(promos);
        }

        #region T2_2
        public void T2_Batch(IEnumerable<Promotions> promos) {
            var query = DbContext.Promotions.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));

                INSERT().INTO(set);
                var r = OUTPUT(INSERTED<Promotions>());
                VALUES(set.RowsFrom(promos));

                return r;
            });

            foreach (var promo in query)
                Console.WriteLine((promo.PromotionId, promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
        }
        #endregion
    }
}
