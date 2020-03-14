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

        public void T2() {

            #region T2
            var name = "2018 Summer Promotion";
            var discount = 0.15M;
            var startDate = "20180601";
            var expiredDate = "20180901";

            var rows = DbContext.Database.Query((Promotions promo) => {
                var set = promo.@using((promo.PromotionName, promo.Discount, promo.StartDate, promo.ExpiredDate));
                INSERT().INTO(set);
                VALUES(set.Row((name, discount, DataTypes.DATE_T.Raw(startDate), DataTypes.DATE_T.Raw(expiredDate))));
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }
    }
}
