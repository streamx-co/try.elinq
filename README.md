# ELINQ (Entity LINQ)

<big><sup>Interactive demo &rArr; </sup></big>[![Try_.NET Enabled](https://img.shields.io/badge/Try_.NET-Enabled-501078.svg)](https://try.entitylinq.com/)
[![nuget](https://img.shields.io/nuget/vpre/Streamx.Linq.SQL?label=ELINQ%20SQL)](https://www.nuget.org/packages/Streamx.Linq.SQL)
[![nuget](https://img.shields.io/nuget/vpre/Streamx.Linq.SQL.EFCore?label=ELINQ%20EF%20Core)](https://www.nuget.org/packages/Streamx.Linq.SQL.EFCore)

ELINQ is a Language Integrated Query (LINQ) technology for relational (SQL) databases and EF Core. It  allows you to use C# (or your .NET language of choice) to write strongly typed queries.

## Don't we have LINQ for EF [already](https://docs.microsoft.com/en-us/ef/core/querying/)?

**ELINQ** enhances LINQ with new capabilities:

- Full power of C# for query creation: multiple statements inside lambda, variables, functions, etc.
- No semantic gap with SQL. ELINQ declares SQL statements (like `SELECT`, `FROM`, `WHERE`) as first class C# methods, combining familiar syntax with intellisense, type safety and refactoring.
- No limitations. Any practical DML SQL can be expressed.

With ELINQ SQL becomes just "another" class library exposing its API locally, literally *"Language Integrated SQL"*.

## Demo

We took popular SQL tutorials and implemented **all examples** from them using ELINQ. We want to emphasize that **any** practical DML SQL can be expressed with ELINQ with **no compromises**.

This site is built with a wonderful Try .NET technology. All the examples are interactive, intellisense enabled and runnable with changes you may make. Enjoy!

- [SQL Server Tutorial](docs/SqlServerTutorial/README.md) - 100+ examples covering the entire SQL
- [Sakila Homework](docs/SakilaHomework/README.md) (MySQL database)
- [Batch `INSERT`, bulk `UPDATE` and `UPSERT`](docs/tutorials/Modification.md) for efficient data modification

## EF Core Integration

By integrating with EF, ELINQ maps EF entities to SQL table and column names.

## SQL Support

ELINQ fully supports the modern SQL [DML](https://en.wikipedia.org/wiki/Data_manipulation_language) standard. In addition to conventional relational SQL (SQL-92), ELINQ supports [SQL-99 Common Table Expressions (WITH clause)](https://stackoverflow.com/questions/4740748/when-to-use-common-table-expression-cte), [SQL-2003 Window Functions (OVER clause)](https://www.postgresql.org/docs/current/tutorial-window.html), [SQL-2003 MERGE (UPSERT clause)](https://en.wikipedia.org/wiki/Merge_(SQL)), Dynamic Queries and many, many more.

## Where can I get ELINQ?

- [Product site](https://entitylinq.com)
- Setup [instructions](https://github.com/streamx-co/try.elinq/blob/master/Setup.md)
- [Sample projects](https://github.com/streamx-co/try.elinq/blob/master/RunLocally.md).
