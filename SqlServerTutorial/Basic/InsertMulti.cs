using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using Streamx.Linq.SQL.TransactSQL;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class InsertMulti {
        private MyContext DbContext { get; }

        public InsertMulti(MyContext context) {
            DbContext = context;
        }

        public void T2() {

            #region T2
            var newPromoSummer = new Promotions() {
                PromotionName = "2020 Summer Promotion",
                Discount = 0.25M,
                StartDate = new DateTime(2020, 06, 01),
                ExpiredDate = new DateTime(2020, 09, 01)
            };

            var newPromoFall = new Promotions() {
                PromotionName = "2020 Fall Promotion",
                Discount = 0.10M,
                StartDate = new DateTime(2020, 10, 01),
                ExpiredDate = new DateTime(2020, 11, 01)
            };

            var newPromoWinter = new Promotions() {
                PromotionName = "2020 Winter Promotion",
                Discount = 0.25M,
                StartDate = new DateTime(2020, 12, 01),
                ExpiredDate = new DateTime(2021, 01, 01)
            };

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
            var newPromoSummer = new Promotions() {
                PromotionName = "2020 Summer Promotion",
                Discount = 0.25M,
                StartDate = new DateTime(2020, 06, 01),
                ExpiredDate = new DateTime(2020, 09, 01)
            };

            var newPromoFall = new Promotions() {
                PromotionName = "2020 Fall Promotion",
                Discount = 0.10M,
                StartDate = new DateTime(2020, 10, 01),
                ExpiredDate = new DateTime(2020, 11, 01)
            };

            var newPromoWinter = new Promotions() {
                PromotionName = "2020 Winter Promotion",
                Discount = 0.25M,
                StartDate = new DateTime(2020, 12, 01),
                ExpiredDate = new DateTime(2021, 01, 01)
            };

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

        public void T2_3() {

            var newPromoSummer = new Promotions() {
                PromotionName = "2020 Summer Promotion",
                Discount = 0.25M,
                StartDate = new DateTime(2020, 06, 01),
                ExpiredDate = new DateTime(2020, 09, 01)
            };

            var newPromoFall = new Promotions() {
                PromotionName = "2020 Fall Promotion",
                Discount = 0.10M,
                StartDate = new DateTime(2020, 10, 01),
                ExpiredDate = new DateTime(2020, 11, 01)
            };

            var newPromoWinter = new Promotions() {
                PromotionName = "2020 Winter Promotion",
                Discount = 0.25M,
                StartDate = new DateTime(2020, 12, 01),
                ExpiredDate = new DateTime(2021, 01, 01)
            };

            var promos = new List<Promotions>() {newPromoSummer, newPromoFall, newPromoWinter};

            T2_3(promos);
        }

        #region T2_3
        private void T2_3(IList<Promotions> promos) {
            var query = DbContext.Promotions.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));

                INSERT().INTO(set);
                var r = OUTPUT(INSERTED<Promotions>());
                VALUES(BuildBatch<(String, decimal?, DateTime, DateTime)>(promos)(set));

                return r;
            });

            foreach (var promo in query)
                Console.WriteLine((promo.PromotionId, promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
        }
        #endregion

        [Local]
        public Func<IProjection<Promotions, T>, T[]> BuildBatch<T>(IList<Promotions> promos) where T : struct, ITuple {
            Func<IProjection<Promotions, T>, T[]> result = set => new T[] { };

            foreach (var promo in promos) {
                var current = result;
                result = set => Params(set.RowFrom(promo), current(set));
            }

            return result;
        }
    }
}
