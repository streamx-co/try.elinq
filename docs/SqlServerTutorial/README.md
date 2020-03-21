# SQL Server Tutorial

![Try_.NET Enabled](https://img.shields.io/badge/Try_.NET-Enabled-501078.svg)

XLinq allows you to use C# (or your .NET language of choice) to write strongly typed queries. It is capable to represent _**any**_ practical SQL [DML](https://en.wikipedia.org/wiki/Data_manipulation_language).

To demonstrate XLinq capabilities we took examples from a popular [SQL Server Tutorial](https://www.sqlservertutorial.net/) and implemented **all of them** in C# and XLinq in a strongly typed way. Our goal is to show "pixel-perfect" SQL translation, EF integration, type safety and ease of use (we take the example as is, without trying to improve SQL).

This site is built with a wonderful Try .NET technology. All the examples are interactive, intellisense enabled and runnable with changes you may make. Enjoy!

- [Basic](Basic.md)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(actually most of practical SQL)
- [Advanced](UDF.md) (User-defined Functions)
- [Functions](Functions.md)

> All the examples are executed against a real database, created according to provided [instructions](https://www.sqlservertutorial.net/load-sample-database/). The DbContext is [scaffolded](https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding) with EF tooling in a separate project [hosted](https://github.com/streamx-co/xlinq.live/tree/master/Models) on GitHub.
>
> All queries are executed in a transaction, which is rolled back at the end. Therefore no changes are persisted and each query can be executed multiple times producing same results. In addition we installed a [console logger](https://docs.microsoft.com/en-us/ef/core/miscellaneous/logging) to inspect the executed SQL.
