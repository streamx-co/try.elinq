using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.Library;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class Merge {
        const String CATEGORY_STAGING = "#category_staging";

        private MyContext DbContext { get; }
        private static IList<Category> stagingCategories;

        public Merge(MyContext context) {
            DbContext = context;

            Category c1 = new Category() {
                CategoryId = 1,
                CategoryName = "Children Bicycles",
                Amount = 15000,
            };
            Category c3 = new Category() {
                CategoryId = 3,
                CategoryName = "Cruisers Bicycles",
                Amount = 13000,
            };
            Category c4 = new Category() {
                CategoryId = 4,
                CategoryName = "Cyclocross Bicycles",
                Amount = 20000,
            };
            Category c5 = new Category() {
                CategoryId = 5,
                CategoryName = "Electric Bikes",
                Amount = 10000,
            };
            Category c6 = new Category() {
                CategoryId = 6,
                CategoryName = "Mountain Bikes",
                Amount = 10000,
            };

            stagingCategories = new List<Category>() {c1, c3, c4, c5, c6};
        }

        public void T1() {

            #region T1
            var query = DbContext.Category.Query((Category category) => {
                PrepareStagingCategories(category);
                var staging = ToTable<Category>(CATEGORY_STAGING);

                MERGE().INTO(category).USING(staging).ON(category.CategoryId == staging.CategoryId);

                WHEN_MATCHED()
                    .THEN(MERGE_UPDATE()
                        .SET(() => {
                            category.CategoryName = staging.CategoryName;
                            category.Amount = staging.Amount;
                        }));

                var set = category.@using((category.CategoryId, category.CategoryName, category.Amount));
                WHEN_NOT_MATCHED().THEN(MERGE_INSERT(set.ColumnNames(), VALUES(set.RowFrom(staging))));

                WHEN_NOT_MATCHED_BY_SOURCE().THEN(DELETE());

                Semicolon();

                return SelectAll(category);
            });

            foreach (var category in query)
                Console.WriteLine((category.CategoryId, category.CategoryName, category.Amount));
            #endregion

        }

        private static void PrepareStagingCategories(Category category) {

            var categoryStaging = ToTable<Category>(CATEGORY_STAGING);

            SELECT(TOP(0).Of(category)).INTO(categoryStaging);
            FROM(category);

            Semicolon();

            var set = categoryStaging.@using((categoryStaging.CategoryId, categoryStaging.CategoryName, categoryStaging.Amount));
            INSERT().INTO(set);
            VALUES(set.RowsFrom(stagingCategories));

            Semicolon();
        }
    }
}
