using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.ScalarFunctions;
using static Streamx.Linq.SQL.AggregateFunctions;

namespace SqlServerTutorial.Functions.Window {
    class Rank {
        private MyContext DbContext { get; }

        public Rank(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var query = DbContext.Set<ProductRank>()
                .Query((Products products, ProductRank alias) => {
                    var priceRank = AggregateBy(RANK())
                        .OVER(ORDER(BY(products.ListPrice).DESC));

                    var r = SELECT<ProductRank>(products.ProductId.@as(alias.Product.ProductId),
                        priceRank.@as(alias.Rank));
                    FROM(products);

                    return r;
                })
                .Include(r => r.Product);

            foreach (var rank in query.Take(3))
                Console.WriteLine((rank.Product.ProductId, rank.Product.ProductName, rank.Product.ListPrice, rank.Rank));
            #endregion

        }

        public void B() {

            #region B
            var query = DbContext.Set<ProductRank>()
                .Query((Products products, ProductRank alias) => {
                    var priceRank = AggregateBy(RANK())
                        .OVER(PARTITION(BY(products.BrandId))
                            .ORDER(BY(products.ListPrice).DESC));

                    var r = SELECT<ProductRank>(products.ProductId.@as(alias.Product.ProductId),
                        priceRank.@as(alias.Rank));
                    FROM(products);

                    return r;
                })
                .Where(r => r.Rank <= 3)
                .Include(r => r.Product);

            foreach (var rank in query.Take(3))
                Console.WriteLine((rank.Product.ProductId, rank.Product.ProductName, rank.Product.ListPrice, rank.Rank));
            #endregion

        }
    }
}
