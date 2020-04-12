using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Sakila;
using Streamx.Linq.SQL;
using Streamx.Linq.SQL.EFCore;
using static Streamx.Linq.SQL.SQL;
using static Streamx.Linq.SQL.MySQL.SQL;
using static Streamx.Linq.SQL.Library;
using static Streamx.Linq.SQL.Directives;
using static Streamx.Linq.SQL.AggregateFunctions;

namespace SakilaHomework {
    class SakilaDbQueries {
        private MyContext DbContext { get; }

        public SakilaDbQueries(MyContext context) {
            DbContext = context;
        }

        public void Test1B() {

            #region Test1B
            var query = DbContext.ActorNames.Query((Actor actor, ActorName alias) => {
                var result = SELECT<ActorName>((actor.FirstName + " " + actor.LastName).ToUpper().@as(alias.FullName));
                FROM(actor);

                return result;
            });

            foreach (var actorName in query.Take(3))
                Console.WriteLine(actorName.FullName);
            #endregion

        }
        
        public void Test1B_1() {

            #region Test1B_1
            var query = DbContext.ActorNames.Query((Actor actor, ActorName alias) => {
                var result = SELECT<ActorName>($"{actor.FirstName} {actor.LastName}".ToUpper().@as(alias.FullName));
                FROM(actor);

                return result;
            });

            foreach (var actorName in query.Take(3))
                Console.WriteLine(actorName.FullName);
            #endregion

        }
        
        public void TestUpsert() {
            
            var newOrExisting = DbContext.Store.First();
            
            #region TestUpsert
            // There is a store which might already exist in the database.
            // Should we add it or update? (PK is not always the only UNIQUE KEY)
            newOrExisting.LastUpdate = DateTime.Now;

            var rows = DbContext.Database.Query((Store store) => {
                var view = store.@using((store.StoreId, store.AddressId, store.ManagerStaffId, store.LastUpdate));
                INSERT().INTO(view);
                VALUES(view.RowFrom(newOrExisting));
                ON_DUPLICATE_KEY_UPDATE(() => store.LastUpdate = INSERTED_VALUES(store.LastUpdate));
            });

            Console.WriteLine($"{rows} rows affected");
            #endregion

        }

        public void Test2A() {

            #region Test2A
            var name = "Joe";

            var query = DbContext.Actor.Query((Actor actor) => {
                var result = SELECT(actor);
                FROM(actor);
                WHERE(actor.FirstName.ToLower() == name.ToLower());

                return result;
            });

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.ActorId, actor.FirstName, actor.LastName, actor.LastUpdate));
            #endregion

        }

        public void Test2B() {

            #region Test2B
            var letters = "GEN";

            var query = DbContext.Actor.Query((Actor actor) => {
                var result = SELECT(actor);
                FROM(actor);
                WHERE(actor.LastName.ToUpper().Contains(letters));

                return result;
            });

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.ActorId, actor.FirstName, actor.LastName, actor.LastUpdate));
            #endregion

        }

        public void Test2C() {

            #region Test2C
            var letters = "LI";

            var query = DbContext.Actor.Query((Actor actor) => {
                var result = SELECT(actor);
                FROM(actor);
                WHERE(actor.LastName.ToUpper().Contains(letters));
                ORDER(BY(actor.LastName), BY(actor.FirstName));

                return result;
            });

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.ActorId, actor.FirstName, actor.LastName, actor.LastUpdate));
            #endregion

        }

        public void Test2D() {

            #region Test2D
            string[] countries = {"China", "Afghanistan", "Bangladesh"};

            var query = DbContext.Country.Query((Country country) => {
                var result = SELECT(country);
                FROM(country);
                WHERE(countries.Contains(country.Name));

                return result;
            });

            foreach (var country in query.Take(3))
                Console.WriteLine((country.CountryId, country.Name, country.City, country.LastUpdate));
            #endregion

        }

        public void Test4A() {

            #region Test4A
            var query = DbContext.ActorNameCounts.Query((Actor actor, ActorNameCount alias) => {
                var result = SELECT<ActorNameCount>(actor.LastName.@as(alias.LastName), COUNT().@as(alias.Count));
                FROM(actor);
                GROUP(BY(actor.LastName));
                ORDER(BY(actor.LastName).DESC);

                return result;
            });

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.LastName, actor.Count));
            #endregion

        }

        public void Test4B() {

            #region Test4B
            var query = DbContext.ActorNameCounts.Query((Actor actor, ActorNameCount alias) => {
                var actorCount = COUNT();

                var result = SELECT<ActorNameCount>(actor.LastName.@as(alias.LastName), actorCount.@as(alias.Count));
                FROM(actor);
                GROUP(BY(actor.LastName));
                HAVING(actorCount > 1);
                ORDER(BY(actorCount).DESC, BY(actor.LastName));

                return result;
            });

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.LastName, actor.Count));
            #endregion

        }

        public void Test4C() {

            #region Test4C
            var newFirstName = "HARPO";
            var oldFirstName = "GROUCHO";
            var lastName = "WILLIAMS";

            var query = DbContext.Actor.Query((Actor actor) => {
                    UPDATE(actor).SET(() => actor.FirstName = newFirstName);
                    WHERE(actor.FirstName == oldFirstName && actor.LastName == lastName);

                    Semicolon();

                    var result = SELECT(actor);
                    FROM(actor);
                    WHERE(actor.LastName == lastName);

                    return result;
                })
                .AsEnumerable();

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.ActorId, actor.FirstName, actor.LastName, actor.LastUpdate));
            #endregion

        }

        public void Test6B() {

            #region Test6B
            var payMonth = 8;
            var payYear = 2005;

            var query = DbContext.StaffPayments.Query((Staff staff, Payment payment, StaffPayment alias) => {
                    var r = SELECT<StaffPayment>(staff.StaffId.@as(alias.Staff.StaffId), SUM(payment.Amount).@as(alias.Amount));
                    FROM(staff).LEFT_JOIN(payment).ON(staff == payment.Staff);
                    WHERE(MONTH(payment.PaymentDate) == payMonth && YEAR(payment.PaymentDate) == payYear);
                    GROUP(BY(staff.StaffId)); //must GROUP BY StaffId since this field is used in SELECT

                    return r;
                })
                .Include(sp => sp.Staff);

            foreach (var staffPayment in query.Take(3))
                Console.WriteLine((staffPayment.Staff.FirstName, staffPayment.Staff.LastName, staffPayment.Amount));
            #endregion

        }

        public void Test6B_1() {

            #region Test6B_1
            var payMonth = 8;
            var payYear = 2005;

            var query = DbContext.StaffPayments.Query((Staff staff, Payment payment, StaffPayment alias) => {
                    var r = SELECT<StaffPayment>(staff.StaffId.@as(alias.Staff.StaffId), SUM(payment.Amount).@as(alias.Amount));
                    FROM(staff).LEFT_JOIN(payment).ON(staff == payment.Staff);
                    WHERE(payment.PaymentDate.Month == payMonth && payment.PaymentDate.Year == payYear);
                    GROUP(BY(staff.StaffId)); //must GROUP BY StaffId since this field is used in SELECT

                    return r;
                })
                .Include(sp => sp.Staff);

            foreach (var staffPayment in query.Take(3))
                Console.WriteLine((staffPayment.Staff.FirstName, staffPayment.Staff.LastName, staffPayment.Amount));
            #endregion

        }

        public void Test7A() {

            #region Test7A
            var language = "English";

            var query = DbContext.Film.Query((Film film) => {

                var languages = SubQuery((Language lang) => {
                    var l = SELECT(lang.LanguageId);
                    FROM(lang);
                    WHERE(lang.Name == language);

                    return l.AsCollection();
                });

                var r = SELECT(film);
                FROM(film);
                WHERE((film.Title.StartsWith("K") || film.Title.StartsWith("Q")) && languages.Contains(film.Language.LanguageId));

                return r;
            });

            foreach (var film in query.Take(3))
                Console.WriteLine(film.Title);
            #endregion

        }

        public void Test7B() {

            #region Test7B
            var filmName = "Alone Trip";

            var query = DbContext.Actor.Query((Actor actor) => {

                var films = SubQuery((Film film) => {
                    var l = SELECT(film.FilmId);
                    FROM(film);
                    WHERE(film.Title.ToLower() == filmName.ToLower());

                    return l.AsCollection();
                });

                var actors = SubQuery((FilmActor filmActor) => {
                    var l = SELECT(filmActor.ActorId);
                    FROM(filmActor);
                    WHERE(films.Contains(filmActor.FilmId));

                    return l.AsCollection();
                });

                var r = SELECT(actor);
                FROM(actor);
                WHERE(actors.Contains(actor.ActorId));

                return r;
            });

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.FirstName, actor.LastName));
            #endregion

        }

        #region Test7B_1
        public void Test7B_1() {
            var filmName = "Alone Trip";

            var query = DbContext.Actor.Query((Actor actor) => {
                var films = SubQuery((Film film) => {
                    var l = SELECT(film.FilmId);
                    FROM(film);
                    WHERE(film.Title.ToLower() == filmName.ToLower());

                    return l.AsCollection();
                });

                var actors = ActorsByFilms(films);

                var r = SELECT(actor);
                FROM(actor);
                WHERE(actors.Contains(actor.ActorId));

                return r;
            });

            foreach (var actor in query.Take(3))
                Console.WriteLine((actor.FirstName, actor.LastName));
        }

        private static ICollection<ushort> ActorsByFilms(ICollection<ushort> films) {
            return SubQuery((FilmActor filmActor) => {
                var l = SELECT(filmActor.ActorId);
                FROM(filmActor);
                WHERE(films.Contains(filmActor.FilmId));

                return l.AsCollection();
            });
        }
        #endregion
    }
}
