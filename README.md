# XLINQ

![Try_.NET Enabled](https://img.shields.io/badge/Try_.NET-Enabled-501078.svg)
<!-- [![GitHub](https://img.shields.io/github/stars/streamx-co/xlinq.live?style=social)](https://github.com/streamx-co/xlinq.live) -->

XLinq is a **L**anguage **IN**tegrated **Q**uery technology for relational (SQL) databases for EF Core. It  allows you to use C# (or your .NET language of choice) to write strongly typed queries.

## Don't we have LINQ for EF [already](https://docs.microsoft.com/en-us/ef/core/querying/)?

**"X"** prefix provides 2 new capabilities:

- full language support: multiple statements inside lambda, variables, functions etc - use all language features for query creation.
- no "special" syntax or [layer translated to SQL](https://docs.microsoft.com/en-us/ef/core/querying/). XLinq declares SQL statements (like `SELECT`, `FROM`, `WHERE`) as first class C# methods, combining familiar syntax with intellisense, type safety and refactoring.

Result? SQL becomes a *boring* class library exposing its API locally, literally *"Language Integrated SQL"*.

## EF Core Integration

By integrating with EF, XLinq maps EF entities to SQL table and column names.

## What's in the box?

We took popular SQL tutorials and implemented **all examples** from them using XLinq. We want to emphasize that **any** practical DML SQL can be expressed with XLinq with **no compromises**.

Skeptic? Fortunately this site is built with a wonderful Try .NET technology. All the examples are interactive, intellisense enabled and runnable with changes you may make. Enjoy!

- [SQL Server Tutorial](SqlServerTutorial.md)

## SQL Support

XLinq covers the modern SQL DML standard. In addition to conventional relational SQL (SQL-92), XLinq supports [SQL-99 Common Table Expressions (WITH clause)](https://stackoverflow.com/questions/4740748/when-to-use-common-table-expression-cte), [SQL-2003 Window Functions (OVER clause)](https://www.postgresql.org/docs/current/tutorial-window.html), [SQL-2003 MERGE (UPSERT clause)](https://en.wikipedia.org/wiki/Merge_(SQL)), Dynamic Queries and many, many more.

## Can I get it?

The source code of this site is hosted in [GitHub](https://github.com/streamx-co/xlinq.live) containing sample projects, instructions to run it [locally](RunLocally.md) and XLinq setup.
