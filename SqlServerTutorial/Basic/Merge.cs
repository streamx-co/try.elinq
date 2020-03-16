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

        public Merge(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            var cat1 = new Category() {
                CategoryId = 1,
                CategoryName = "Children Bicycles",
                Amount = 15000,
            };
            var cat3 = new Category() {
                CategoryId = 3,
                CategoryName = "Cruisers Bicycles",
                Amount = 13000,
            };
            var cat4 = new Category() {
                CategoryId = 4,
                CategoryName = "Cyclocross Bicycles",
                Amount = 20000,
            };
            var cat5 = new Category() {
                CategoryId = 5,
                CategoryName = "Electric Bikes",
                Amount = 10000,
            };
            var cat6 = new Category() {
                CategoryId = 6,
                CategoryName = "Mountain Bikes",
                Amount = 10000,
            };

            #region T1
            var stagingCategories = new List<Category> {cat1, cat3, cat4, cat5, cat6};

            var query = DbContext.Category.Query((Category category) => {
                PrepareStagingCategories(category, stagingCategories);
                var staging = ToTable<Category>(CATEGORY_STAGING);

                MERGE().INTO(category).USING(staging).ON(category == staging);

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

        private static void PrepareStagingCategories(Category category, IEnumerable<Category> stagingCategories) {

            var staging = ToTable<Category>(CATEGORY_STAGING);

            SELECT(TOP(0).Of(category)).INTO(staging);
            FROM(category);

            Semicolon();

            var set = staging.@using((staging.CategoryId, staging.CategoryName, staging.Amount));
            INSERT().INTO(set);
            VALUES(set.RowsFrom(stagingCategories));

            Semicolon();
        }
    }
}
