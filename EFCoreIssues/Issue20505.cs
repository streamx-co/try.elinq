using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using Streamx.Linq.SQL.Grammar;
using Streamx.Linq.SQL.TransactSQL;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;
using static Streamx.Linq.SQL.ScalarFunctions;
using static Streamx.Linq.SQL.TransactSQL.SQL;


#pragma warning disable 1591

namespace EFCoreIssues {
    public class Issue20505 {
        public void Test() {
            using var dbContext = new MyContext();
            // dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();


            using var t = dbContext.Database.BeginTransaction();
            /*var t1 = dbContext.Floors.Where(f => f.Id == 1)
                .Select(f => new {
                    SpaceGroups = f.Spaces
                        .SelectMany(fs => fs.SpaceGroupFloorSpaces.Select(sgfs => sgfs.SpaceGroup))
                        .Distinct()
                        .OrderBy(s => s.Name)
                        .Select(g => new {
                            g.Id,
                        }),
                });
            
            Console.WriteLine(t1.FirstOrDefault());*/

            Test20505(dbContext);

        }

        #region Test
        public static void Test20505(MyContext dbContext) {
            var floorId = 1;
            var t = dbContext.Set<SpaceGroup>().Query((SpaceGroup spaceGroup) => {
                    var spaceGroupIds = GetSpaceGroupIdsSimple(floorId);

                    var result = SELECT(spaceGroup);
                    FROM(spaceGroup);
                    WHERE(spaceGroupIds.Contains(spaceGroup.Id));
                    return result;
                })
                .OrderBy(sg => sg.Name)
                .Select(sg => new {
                    SpaceGroups = sg.Id
                });

            Console.WriteLine(t.FirstOrDefault());
        }

        private static ICollection<int> GetSpaceGroupIdsSimple(int floorId) {
            return SubQuery((Space space) => {
                var allFloorSpaceGroups = SubQuery((SpaceGroup spaceGroup, SpaceGroupFloorSpace floorSpace, SpaceGroupWithSpace alias) => {
                    var r = SELECT<SpaceGroupWithSpace>(floorSpace.SpaceId.@as(alias.SpaceId), spaceGroup.Id.@as(alias.GroupId));
                    FROM(spaceGroup).JOIN(floorSpace).ON(floorSpace.SpaceGroup == spaceGroup);
                    return r;
                });

                var distinctSpaceGroups = SELECT(DISTINCT(allFloorSpaceGroups.GroupId));
                FROM(space).JOIN(allFloorSpaceGroups).ON(space.Id == allFloorSpaceGroups.SpaceId);
                WHERE(floorId == space.Floor.Id);

                return distinctSpaceGroups.AsCollection();
            });
        }
        #endregion

        #region TestFull
        private static ICollection<int> GetSpaceGroupIds(int floorId) {
            return SubQuery(() => {
                var floor = SubQuery((Floor f) => {
                    var r = SELECT(f);
                    FROM(f);
                    WHERE(f.Id == floorId);

                    return r;
                });

                var spaceGroupsIds = SubQuery((Space space, Scalar alias) => {
                    var allFloorSpaceGroups = SubQuery((SpaceGroup spaceGroup, SpaceGroupFloorSpace floorSpace, SpaceGroupWithSpace alias) => {
                        var r = SELECT<SpaceGroupWithSpace>(floorSpace.SpaceId.@as(alias.SpaceId), spaceGroup.Id.@as(alias.GroupId));
                        FROM(spaceGroup).JOIN(floorSpace).ON(floorSpace.SpaceGroup == spaceGroup);
                        return r;
                    });

                    var distinctSpaceGroups = SELECT<Scalar>(DISTINCT(allFloorSpaceGroups.GroupId.@as(alias.Value)));
                    FROM(space).JOIN(allFloorSpaceGroups).ON(space.Id == allFloorSpaceGroups.SpaceId);
                    WHERE(floor == space.Floor);

                    return distinctSpaceGroups;
                });

                var r = SELECT(spaceGroupsIds.Value);
                FROM(floor).OUTER_APPLY(spaceGroupsIds);

                return r.AsCollection();
            });
        }
        #endregion

        #region Helper types
        [Tuple]
        public class SpaceGroupWithSpace {
            public int GroupId { get; }
            public int SpaceId { get; }
        }

        [Tuple]
        public class Scalar {
            public int Value { get; }
        }
        #endregion

        public class Floor {
            public int Id { get; set; }
            public List<Space> Spaces { get; set; }
        }


        public class Space {
            public int Id { get; set; }
            public Floor Floor { get; set; }
            public List<SpaceGroupFloorSpace> SpaceGroupFloorSpaces { get; set; }
        }


        public class SpaceGroup {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<SpaceGroupFloorSpace> SpaceGroupFloorSpaces { get; set; }
        }

        public class SpaceGroupFloorSpace {
            public int SpaceGroupId { get; set; }
            public int SpaceId { get; set; }
            public SpaceGroup SpaceGroup { get; set; }
            public Space Space { get; set; }
        }


        public class MyContext : DbContext {
            public static readonly ILoggerFactory ConsoleLoggerFactory =
                LoggerFactory.Create(builder => {
                    builder
                        .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name && level >= LogLevel.Information)
                        .AddConsole(c => c.DisableColors = true);
                });

            protected override void OnModelCreating(ModelBuilder modelBuilder) {
                modelBuilder.Entity<SpaceGroupFloorSpace>()
                    .HasKey(x => new {x.SpaceGroupId, x.SpaceId});
                modelBuilder.Entity<SpaceGroupFloorSpace>()
                    .HasOne(x => x.SpaceGroup)
                    .WithMany(x => x.SpaceGroupFloorSpaces)
                    .HasForeignKey(x => x.SpaceGroupId);

                modelBuilder.Entity<SpaceGroupFloorSpace>()
                    .HasOne(x => x.Space)
                    .WithMany(x => x.SpaceGroupFloorSpaces)
                    .HasForeignKey(x => x.SpaceId);

                ELinq.Configuration.RegisterVendorCapabilities();
            }

            public DbSet<Floor> Floors { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
                optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer(@"Server=mssql,1473;Database=Repro20505;User Id=sa;Password=455Password");
            }
        }
    }
}
