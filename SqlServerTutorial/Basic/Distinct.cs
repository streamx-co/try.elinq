using System;
using System.Linq;
using Models.BikeStores;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;

namespace SqlServerTutorial.Basic {
    class Distinct {
        private MyContext DbContext { get; }

        public Distinct(MyContext context) {
            DbContext = context;
        }

        public void A() {

            #region A
            var query = DbContext.Set<Cities>()
                .Query((Customers customer, Cities city) => {
                    var result = SELECT(DISTINCT<Cities>(customer.City.@as(city.City)));
                    FROM(customer);

                    return result;
                })
                .OrderBy(city => city.City);

            foreach (var city in query.Take(3))
                Console.WriteLine(city.City);
            #endregion

        }

        public void B() {

            #region B
            var query = DbContext.Set<CityState>()
                .Query((Customers customer, CityState city) => {
                    var result = SELECT(DISTINCT<CityState>(customer.City.@as(city.City), customer.State.@as(city.State)));
                    FROM(customer);

                    return result;
                });

            foreach (var cityState in query.Take(3))
                Console.WriteLine((cityState.City, cityState.State));
            #endregion

        }

        public void C() {

            #region C
            var query = DbContext.Set<Phones>()
                .Query((Customers customer, Phones phones) => {
                    var result = SELECT(DISTINCT<Phones>(customer.Phone.@as(phones.Phone)));
                    FROM(customer);

                    return result;
                })
                .OrderBy(t => t.Phone);

            foreach (var phone in query.Take(3))
                Console.WriteLine(phone?.Phone ?? "NULL");
            #endregion

        }

        public void C1() {

            #region C1
            var query = DbContext.Set<CityStateZip>()
                .Query((Customers customer, CityStateZip cityStateZip) => {
                    var result = SELECT<CityStateZip>(customer.City.@as(cityStateZip.City),
                        customer.State.@as(cityStateZip.State),
                        customer.ZipCode.@as(cityStateZip.ZipCode));
                    FROM(customer);
                    GROUP(BY(customer.City), BY(customer.State), BY(customer.ZipCode));

                    return result;
                })
                .OrderBy(t => t.City)
                .ThenBy(t => t.State)
                .ThenBy(t => t.ZipCode);

            foreach (var cityStateZip in query.Take(3))
                Console.WriteLine((cityStateZip.City, cityStateZip.State, cityStateZip.ZipCode));
            #endregion

        }

        public void C2() {

            #region C2
            var query = DbContext.Set<CityStateZip>()
                .Query((Customers customer, CityStateZip cityStateZip) => {
                    var result = SELECT(DISTINCT<CityStateZip>(customer.City.@as(cityStateZip.City),
                        customer.State.@as(cityStateZip.State),
                        customer.ZipCode.@as(cityStateZip.ZipCode)));
                    FROM(customer);

                    return result;
                });

            foreach (var cityStateZip in query.Take(3))
                Console.WriteLine((cityStateZip.City, cityStateZip.State, cityStateZip.ZipCode));
            #endregion

        }
    }
}
