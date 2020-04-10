using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            optionsBuilder.UseMySql("server=mysql;port=3376;user=root;password=455Password;database=sakila;pooling=true;charset=utf8",
                x => x.ServerVersion("8.0-mysql"));

            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<ActorName> ActorNames { get; set; }
        public virtual DbSet<ActorNameCount> ActorNameCounts { get; set; }
        public virtual DbSet<StaffPayment> StaffPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            ELinq.Configuration.RegisterVendorCapabilities();

            modelBuilder.Entity<ActorName>(entity => entity.HasNoKey());
            modelBuilder.Entity<ActorNameCount>(entity => entity.HasNoKey());
            modelBuilder.Entity<StaffPayment>(entity => entity.HasNoKey());
        }
    }

    class Program {
        private static readonly String RootNamespace = typeof(Program).Namespace;

        public static void Main(string region = null,
            string session = null,
            string package = null,
            string project = null,
            string sourceFile = null) {

            if (sourceFile != null)
                ExecuteSingleExample(region, sourceFile);
            else {

                var target = typeof(SakilaDbQueries);
                ExecuteAllExamples(target);

                Console.WriteLine($"{counter} examples executed");
            }
        }

        static int counter;

        private static void ExecuteAllExamples(Type type) {

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)) {
                if (method.GetParameters().Length == 0) {
                    ExecuteSingleExample(method, type);

                    counter++;
                }
            }
        }

        private static void ExecuteSingleExample(MethodInfo method, Type type) {
            using var context = new MyContext();
            var queries = Activator.CreateInstance(type, context);
            
            using var t = context.Database.BeginTransaction();
            method.Invoke(queries, null);
        }

        private static void ExecuteSingleExample(string methodName, string sourceFile) {
            var start = sourceFile.IndexOf(RootNamespace, StringComparison.Ordinal);
            var type = Type.GetType(sourceFile.Substring(start, sourceFile.Length - start - 3).Replace('/', '.'));
            ExecuteSingleExample(type?.GetMethod(methodName), type);
        }
    }
}
