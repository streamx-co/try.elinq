using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Sakila;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.MySQL;

namespace SakilaHomework {
    public class ActorName {
        public string FullName { get; set; }
    }

    public class ActorNameCount {
        public string LastName { get; set; }
        public int Count { get; set; }
    }

    public class StaffPayment {
        public Staff Staff { get; set; }
        public decimal Amount { get; set; }
    }

    class MyContext : SakilaContext {
        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder => {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level >= LogLevel.Information)
                    .AddConsole(c => c.DisableColors = true);
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging();
            optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=123pass;database=sakila;pooling=true;charset=utf8",
                x => x.ServerVersion("8.0-mysql"));
            
            XLinq.Configuration.RegisterVendorCapabilities();

            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<ActorName> ActorNames { get; set; }
        public virtual DbSet<ActorNameCount> ActorNameCounts { get; set; }
        public virtual DbSet<StaffPayment> StaffPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ActorName>(entity => entity.HasNoKey());
            modelBuilder.Entity<ActorNameCount>(entity => entity.HasNoKey());
            modelBuilder.Entity<StaffPayment>(entity => entity.HasNoKey());
        }
    }

    class Program {
        public static void Main(string region = null,
            string session = null,
            string package = null,
            string project = null,
            string[] args = null) {

            if (session != null)
                Test(session, region);
            else {
                foreach (var method in typeof(SakilaDbQueries).GetMethods()) {
                    if (!method.IsStatic && method.GetParameters().Length == 0)
                        Test(method.Name, "SakilaDbQueries");
                }
            }
        }

        private static void Test(string session, string region) {
            using var context = new MyContext();
            using var t = context.Database.BeginTransaction();
            
            var queries = new SakilaDbQueries(context);
            // ReSharper disable once PossibleNullReferenceException
            queries.GetType().GetMethod(region).Invoke(queries, null);
        }
    }
}
