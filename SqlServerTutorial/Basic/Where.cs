using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Operators;

namespace SqlServerTutorial.Basic {
    class Where {
        private MyContext DbContext { get; }

        public Where(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var query = DbContext.Products.Query((Products products) => {
                var result = SELECT(products);
                FROM(products);
                WHERE(products.CategoryId == 1);
                ORDER(BY(products.ListPrice).DESC);

                return result;
            }).AsEnumerable();

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductId, product.ProductName, product.CategoryId, product.ModelYear, product.ListPrice));
            #endregion

        }

        public void B() {

            #region B
            var query = DbContext.Products.Query((Products products) => {
                var result = SELECT(products);
                FROM(products);
                WHERE(products.CategoryId == 1 && products.ModelYear == 2018);
                ORDER(BY(products.ListPrice).DESC);

                return result;
            }).AsEnumerable();

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductId, product.ProductName, product.CategoryId, product.ModelYear, product.ListPrice));
            #endregion

        }

        public void C() {

            #region C
            var query = DbContext.Products.Query((Products products) => {
                    var result = SELECT(products);
                    FROM(products);
                    WHERE(products.ListPrice > 300 && products.ModelYear == 2018);
                    ORDER(BY(products.ListPrice).DESC);

                    return result;
                })
                .AsEnumerable();

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductId, product.ProductName, product.CategoryId, product.ModelYear, product.ListPrice));
            #endregion

        }

        public void D() {

            #region D
            var query = DbContext.Products.Query((Products products) => {
                    var result = SELECT(products);
                    FROM(products);
                    WHERE(products.ListPrice > 300 || products.ModelYear == 2018);
                    ORDER(BY(products.ListPrice).DESC);

                    return result;
                })
                .AsEnumerable();

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductId, product.ProductName, product.CategoryId, product.ModelYear, product.ListPrice));
            #endregion

        }

        public void E() {

            #region E
            var low = 1899.00M;
            var high = 1999.99M;

            var query = DbContext.Products.Query((Products products) => {
                    var result = SELECT(products);
                    FROM(products);
                    WHERE(BETWEEN(products.ListPrice, low, high));
                    ORDER(BY(products.ListPrice).DESC);

                    return result;
                })
                .AsEnumerable();

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductId, product.ProductName, product.CategoryId, product.ModelYear, product.ListPrice));
            #endregion

        }

        public void F() {

            #region F
            var values = new[] {299.99M, 369.99M, 489.99M};

            var query = DbContext.Products.Query((Products products) => {
                    var result = SELECT(products);
                    FROM(products);
                    WHERE(values.Contains(products.ListPrice));
                    ORDER(BY(products.ListPrice).DESC);

                    return result;
                })
                .AsEnumerable();

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductId, product.ProductName, product.CategoryId, product.ModelYear, product.ListPrice));
            #endregion

        }

        public void G() {

            #region G
            var letters = "Cruiser";

            var query = DbContext.Products.Query((Products products) => {
                    var result = SELECT(products);
                    FROM(products);
                    WHERE(products.ProductName.Contains(letters));
                    ORDER(BY(products.ListPrice));

                    return result;
                })
                .AsEnumerable();

            foreach (var product in query.Take(3))
                Console.WriteLine((product.ProductId, product.ProductName, product.CategoryId, product.ModelYear, product.ListPrice));
            #endregion

        }
    }
}
