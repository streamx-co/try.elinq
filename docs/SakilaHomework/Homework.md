# Sakila Database Queries

- [1b. Display the first and last name of each actor in a single column in upper case letters. Name the column `Actor Name`](#b.display-the-first-and-last-name-of-each-actor-in-a-single-column-in-upper-case-letters.name-the-column-actor-name)
- [2a. You need to find the ID number, first name, and last name of an actor, of whom you know only the first name, "Joe."](#a.you-need-to-find-the-id-number-first-name-and-last-name-of-an-actor-of-whom-you-know-only-the-first-name-joe)
- [2b. Find all actors whose last name contain the letters `GEN`](#b.find-all-actors-whose-last-name-contain-the-letters-gen)
- [2c. Find all actors whose last names contain the letters `LI`. This time, order the rows by last name and first name, in that order](#c.find-all-actors-whose-last-names-contain-the-letters-li.this-time-order-the-rows-by-last-name-and-first-name-in-that-order)
- [2d. Using `IN`, display the `country_id` and `country` columns of the following countries: Afghanistan, Bangladesh, and China](#d.using-in-display-the-country_id-and-country-columns-of-the-following-countries-afghanistan-bangladesh-and-china)
- [4a. List the last names of actors, as well as how many actors have that last name.](#a.list-the-last-names-of-actors-as-well-as-how-many-actors-have-that-last-name)
- [4b. List last names of actors and the number of actors who have that last name, but only for names that are shared by at least two actors](#b.list-last-names-of-actors-and-the-number-of-actors-who-have-that-last-name-but-only-for-names-that-are-shared-by-at-least-two-actors)
- [4c. The actor `HARPO WILLIAMS` was accidentally entered in the actor table as GROUCHO WILLIAMS. Write a query to fix the record](#c.the-actor-harpo-williams-was-accidentally-entered-in-the-actor-table-as-groucho-williams.write-a-query-to-fix-the-record)
- [6b. Use `JOIN` to display the total amount rung up by each staff member in August of 2005. Use tables `staff` and `payment`](#b.use-join-to-display-the-total-amount-rung-up-by-each-staff-member-in-august-of-2005.use-tables-staff-and-payment)
- [7a. The music of Queen and Kris Kristofferson have seen an unlikely resurgence. As an unintended consequence, films starting with the letters `K` and `Q` have also soared in popularity. Use subqueries to display the titles of movies starting with the letters `K` and `Q` whose language is English](#a.the-music-of-queen-and-kris-kristofferson-have-seen-an-unlikely-resurgence.as-an-unintended-consequence-films-starting-with-the-letters-k-and-q-have-also-soared-in-popularity.use-subqueries-to-display-the-titles-of-movies-starting-with-the-letters-k-and-q-whose-language-is-english)
- [7b. Use subqueries to display all actors who appear in the film `Alone Trip`.](#b.use-subqueries-to-display-all-actors-who-appear-in-the-film-alone-trip)

> We omitted the most trivial questions and DDL.

#### 1b. Display the first and last name of each actor in a single column in upper case letters. Name the column `Actor Name`

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test1B
```

... using C# interpolation syntax:

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test1B_1
```

#### 2a. You need to find the ID number, first name, and last name of an actor, of whom you know only the first name, "Joe."

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test2A
```

#### 2b. Find all actors whose last name contain the letters `GEN`

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test2B
```

#### 2c. Find all actors whose last names contain the letters `LI`. This time, order the rows by last name and first name, in that order

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test2C
```

#### 2d. Using `IN`, display the `country_id` and `country` columns of the following countries: Afghanistan, Bangladesh, and China

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test2D
```

#### 4a. List the last names of actors, as well as how many actors have that last name

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test4A
```

#### 4b. List last names of actors and the number of actors who have that last name, but only for names that are shared by at least two actors

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test4B
```

#### 4c. The actor `HARPO WILLIAMS` was accidentally entered in the actor table as GROUCHO WILLIAMS. Write a query to fix the record

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test4C
```

#### 6b. Use `JOIN` to display the total amount rung up by each staff member in August of 2005. Use tables `staff` and `payment`

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test6B
```

using `DateTime` properties mappings:

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test6B_1
```

#### 7a. The music of Queen and Kris Kristofferson have seen an unlikely resurgence. As an unintended consequence, films starting with the letters `K` and `Q` have also soared in popularity. Use subqueries to display the titles of movies starting with the letters `K` and `Q` whose language is English

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test7A
```

#### 7b. Use subqueries to display all actors who appear in the film `Alone Trip`

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test7B
```

same with `ActorsByFilms` refactored out:

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test7B_1
```

---

[< BACK](README.md) | [HOME](/)
