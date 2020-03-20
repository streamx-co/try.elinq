using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.TransactSQL;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class Insert {
        private MyContext DbContext { get; }

        public Insert(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            #region T1
            var name = "2018 Summer Promotion";
            var discount = 0.15M;
            var startDate = "20180601";
            var expiredDate = "20180901";

            var rows = DbContext.Database.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
                INSERT().INTO(set);
                VALUES(set.Row((name, discount, DataTypes.DATE.Raw(startDate), DataTypes.DATE.Raw(expiredDate))));
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        #region newPromo
        Promotions newPromo = new Promotions() {
            PromotionName = "2018 Summer Promotion",
            Discount = 0.15M,
            StartDate = new DateTime(2018, 06, 01),
            ExpiredDate = new DateTime(2018, 09, 01)
        };
        #endregion

        public void T1_1() {

            #region T1_1
            var rows = DbContext.Database.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
                INSERT().INTO(set);
                VALUES(set.RowFrom(newPromo));
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        public void T1_2() {

            #region T1_2
            var rows = DbContext.Database.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.StartDate, promo.ExpiredDate, promo.Discount));
                INSERT().INTO(set);
                VALUES(set.RowFrom(newPromo, DEFAULT()));
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        public void T2() {

            #region T2
            var query = DbContext.Promotions.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
                INSERT().INTO(set);
                var r = OUTPUT(INSERTED<Promotions>());
                VALUES(set.RowFrom(newPromo));

                return r;
            });

            foreach (var promo in query)
                Console.WriteLine((promo.PromotionId, promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
            #endregion

        }
    }
}
