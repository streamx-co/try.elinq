using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;

namespace SqlServerTutorial.Basic {
    class InnerJoin {
        private MyContext DbContext { get; }

        public InnerJoin(MyContext context) {
            DbContext = context;
        }

        public void InnerJoin1() {

            #region InnerJoin1
            var query = DbContext.Set<ProductCategoryPrice>()
                .Query((Products products, Categories category, ProductCategoryPrice alias) => {
                    var result = SELECT<ProductCategoryPrice>(products.ProductName.@as(alias.Product),
                        category.CategoryName.@as(alias.Category),
                        products.ListPrice.@as(alias.ListPrice));
                    FROM(products).JOIN(category).ON(products.Category == category);

                    return result;
                })
                .OrderByDescending(p => p.Product);

            foreach (var product in query.Take(3))
                Console.WriteLine((product.Product, product.Category, product.ListPrice));
            #endregion

        }

        public void InnerJoin2() {

            #region InnerJoin2
            var query = DbContext.Set<ProductCategoryPrice>()
                .Query((Products products, Categories category, ProductCategoryPrice alias) => {
                    var result = SELECT<ProductCategoryPrice>(products.ProductName.@as(alias.Product),
                        category.CategoryName.@as(alias.Category),
                        products.ListPrice.@as(alias.ListPrice));
                    FROM(products).JOIN(category).ON(products.Category.CategoryId == category.CategoryId);

                    return result;
                })
                .OrderByDescending(p => p.Product);

            foreach (var product in query.Take(3))
                Console.WriteLine((product.Product, product.Category, product.ListPrice));
            #endregion

        }

        public void InnerJoin3() {

            #region InnerJoin3
            var query = DbContext.Set<ProductCategoryBrandPrice>()
                .Query((Products products,
                    Categories category,
                    Brands brand,
                    ProductCategoryBrandPrice alias) => {
                    var result = SELECT<ProductCategoryBrandPrice>(products.ProductName.@as(alias.Product),
                        category.CategoryName.@as(alias.Category),
                        brand.BrandName.@as(alias.Brand),
                        products.ListPrice.@as(alias.ListPrice));
                    FROM(products).JOIN(category).ON(products.Category == category).JOIN(brand).ON(products.Brand == brand);

                    return result;
                })
                .OrderByDescending(p => p.Product);

            foreach (var product in query.Take(3))
                Console.WriteLine((product.Product, product.Category, product.Brand, product.ListPrice));
            #endregion

        }
    }
}
