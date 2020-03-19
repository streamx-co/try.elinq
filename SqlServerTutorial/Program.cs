using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.TransactSQL;

namespace SqlServerTutorial {
    class MyContext : BikeStoresContext {
        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder => {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level >= LogLevel.Information)
                    .AddConsole(c => c.DisableColors = true);
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer("Server=localhost;Database=BikeStores;User Id=sa;Password=455Password");

            ExLINQ.Configuration.RegisterVendorCapabilities();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FullName>(entity => entity.HasNoKey());
            modelBuilder.Entity<CityCount>(entity => entity.HasNoKey());
            modelBuilder.Entity<Cities>(entity => entity.HasNoKey());
            modelBuilder.Entity<CityState>(entity => entity.HasNoKey());
            modelBuilder.Entity<CityStateZip>(entity => entity.HasNoKey());
            modelBuilder.Entity<Phones>(entity => entity.HasNoKey());
            modelBuilder.Entity<ProductCategoryPrice>(entity => entity.HasNoKey());
            modelBuilder.Entity<ProductCategoryBrandPrice>(entity => entity.HasNoKey());
            modelBuilder.Entity<CustomerYear>(entity => entity.HasNoKey());
            modelBuilder.Entity<CustomerYearOrders>(entity => entity.HasNoKey());
            modelBuilder.Entity<OrderNetSale>(entity => entity.HasNoKey());
            modelBuilder.Entity<SalesSummaryByYear>(entity => entity.HasNoKey());
            modelBuilder.Entity<SalesSummary>(entity => entity.HasNoKey());
            modelBuilder.Entity<SalesGrouping>(entity => entity.HasNoKey());
            modelBuilder.Entity<Scalar<int>>(entity => entity.HasNoKey());
            modelBuilder.Entity<Scalar<String>>(entity => entity.HasNoKey());
            modelBuilder.Entity<Scalar<decimal>>(entity => entity.HasNoKey());
            modelBuilder.Entity<OrderMaxListPrice>(entity => entity.HasNoKey());
            modelBuilder.Entity<StaffSales>(entity => entity.HasNoKey());
            modelBuilder.Entity<CTECategoryCounts>(entity => entity.HasNoKey());
            modelBuilder.Entity<CategoryCounts>(entity => entity.HasNoKey());
            modelBuilder.Entity<GrossSalesByDay>(entity => entity.HasNoKey());
            modelBuilder.Entity<VwNetSalesBrandsCompare>(entity => entity.HasNoKey());
            modelBuilder.Entity<StaffSalesPercentile>(entity => entity.HasNoKey());
            modelBuilder.Entity<SalesVolume>(entity => entity.HasNoKey());
            modelBuilder.Entity<ProductRank>(entity => entity.HasNoKey());
            modelBuilder.Entity<CustomerOrder>(entity => entity.HasNoKey());
            modelBuilder.Entity<ProductInYear>(entity => {

                entity.HasNoKey();

                entity.Property(e => e.ModelYear)
                    .HasColumnName("model_year");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price");

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name");
            });
        }
    }

    class Program {
        private static readonly String RootNamespace = typeof(Program).Namespace;
        private static readonly String[] Categories = {"Basic"};

        public static void Main(string region = null,
            string session = null,
            string package = null,
            string project = null,
            string[] args = null) {

            if (region != null && session != null)
                ExecuteSingleExample(session, region);
            else {

                if (region != null) {
                    Console.WriteLine("Not runnable snippet");
                    return;
                }

                var type = typeof(Functions.Window.RowNumber);
                ExecuteAllExamples(type);

                /*var toExecute = from t in Assembly.GetExecutingAssembly().GetTypes()
                    from cat in Categories
                    let ns = RootNamespace + "." + cat
                    where t.Namespace == ns && !t.IsDefined(typeof(CompilerGeneratedAttribute)) && !t.IsNested
                    select t;
                
                foreach (var t in toExecute)
                    ExecuteAllExamples(t);
                
                Console.WriteLine($"{counter} examples executed");*/
            }
        }

        static int counter = 0;
        private static void ExecuteAllExamples(Type type) {
            
            foreach (var method in type.GetMethods()) {
                if (!method.IsStatic && method.GetParameters().Length == 0) {
                    // ReSharper disable once PossibleNullReferenceException
                    ExecuteSingleExample(method.Name, type.FullName.Substring(RootNamespace.Length + 1));

                    counter++;
                }
            }
        }

        private static void ExecuteSingleExample(string region, string session) {
            using var context = new MyContext();
            using var t = context.Database.BeginTransaction();

            var type = Type.GetType(RootNamespace + "." + session);
            // ReSharper disable once AssignNullToNotNullAttribute
            var queries = Activator.CreateInstance(type, context);

            // ReSharper disable once PossibleNullReferenceException
            queries.GetType().GetMethod(region).Invoke(queries, null);
        }
    }
}
