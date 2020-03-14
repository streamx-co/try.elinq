using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.Operators;
using static Streamx.Linq.SQL.TransactSQL.SQL;

namespace SqlServerTutorial.Basic {
    class Union {
        private MyContext DbContext { get; }

        public Union(MyContext context) {
            DbContext = context;
        }

        public void T1() {

            #region T1
            var query = DbContext.Set<FullName>()
                .Query((Staffs staffs, Customers customers, FullName alias) => {
                    SELECT<FullName>(staffs.FirstName.@as(alias.FirstName), staffs.LastName.@as(alias.LastName));
                    FROM(staffs);

                    UNION_ALL();

                    var result = SELECT<FullName>(customers.FirstName.@as(alias.FirstName), customers.LastName.@as(alias.LastName));
                    FROM(customers);

                    return result;
                })
                .OrderBy(f => f.FirstName)
                .ThenBy(f => f.LastName);

            foreach (var fullName in query.Take(3))
                Console.WriteLine((fullName.FirstName, fullName.LastName));
            #endregion

        }
    }
}
