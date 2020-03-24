# XLINQ

![Try_.NET Enabled](https://img.shields.io/badge/Try_.NET-Enabled-501078.svg)
[![nuget](https://img.shields.io/nuget/vpre/Streamx.Linq.SQL?label=XLINQ%20SQL)](https://www.nuget.org/packages/Streamx.Linq.SQL)
[![nuget](https://img.shields.io/nuget/vpre/Streamx.Linq.SQL.EFCore?label=XLINQ%20EF%20Core)](https://www.nuget.org/packages/Streamx.Linq.SQL.EFCore)

XLinq is a Language Integrated Query (LINQ) technology for relational (SQL) databases and EF Core. It  allows you to use C# (or your .NET language of choice) to write strongly typed queries.

## Don't we have LINQ for EF [already](https://docs.microsoft.com/en-us/ef/core/querying/)?

**X** prefix provides 2 new capabilities:

- Full C# support for query creation: multiple statements inside lambda, variables, functions, etc.
- No semantic gap with SQL. XLinq declares SQL statements (like `SELECT`, `FROM`, `WHERE`) as first class C# methods, combining familiar syntax with intellisense, type safety and refactoring.

As a result SQL becomes just "another" class library exposing its API locally, literally *"Language Integrated SQL"*.

## Curious?

We took popular SQL tutorials and implemented **all examples** from them using XLinq. We want to emphasize that **any** practical DML SQL can be expressed with XLinq with **no compromises**.

This site is built with a wonderful Try .NET technology. All the examples are interactive, intellisense enabled and runnable with changes you may make. Enjoy!

- [SQL Server Tutorial](docs/SqlServerTutorial/README.md) - 100+ examples covering the entire SQL
- [Sakila Homework](docs/SakilaHomework/README.md) (MySQL database)

## EF Core Integration

By integrating with EF, XLinq maps EF entities to SQL table and column names.

## SQL Support

XLinq fully supports the modern SQL [DML](https://en.wikipedia.org/wiki/Data_manipulation_language) standard. In addition to conventional relational SQL (SQL-92), XLinq supports [SQL-99 Common Table Expressions (WITH clause)](https://stackoverflow.com/questions/4740748/when-to-use-common-table-expression-cte), [SQL-2003 Window Functions (OVER clause)](https://www.postgresql.org/docs/current/tutorial-window.html), [SQL-2003 MERGE (UPSERT clause)](https://en.wikipedia.org/wiki/Merge_(SQL)), Dynamic Queries and many, many more.

## Where can I get XLinq?

The source code of this site is hosted on [GitHub](https://github.com/streamx-co/xlinq.live) with setup [instructions](Setup.md) and [sample projects](RunLocally.md).
