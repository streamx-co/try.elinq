# Setup

To run locally examples from this tutorial follow these [instructions](RunLocally.md).

## Project Setup

- Add references to the following assemblies: <sub>[![nuget](https://img.shields.io/nuget/v/Streamx.Linq.SQL?label=ELINQ%20SQL)](https://www.nuget.org/packages/Streamx.Linq.SQL)
[![nuget](https://img.shields.io/nuget/v/Streamx.Linq.SQL.EFCore?label=ELINQ%20EF%20Core)](https://www.nuget.org/packages/Streamx.Linq.SQL.EFCore)</sub>
- Add using declarations:

  - `using Streamx.Linq.SQL.EFCore;`
  - `using Streamx.Linq.SQL.<vendor>;`
  - `using static Streamx.Linq.SQL.SQL;`
  - `using static Streamx.Linq.SQL.<vendor>.SQL;`
  - `using static Streamx.Linq.SQL.Library;`
  - `using static Streamx.Linq.SQL.Directives;`
  - `using static Streamx.Linq.SQL.AggregateFunctions;`
  - `using static Streamx.Linq.SQL.Operators;`
  - `using static Streamx.Linq.SQL.ScalarFunctions;`

- Register capabilities:

  Registers vendor SQL dialect and language substitutions, like `String.Length => LENGTH(String)`. Should be done only once, for example inside `OnModelCreating()` of your DbContext:

  ```cs
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);

    ELinq.Configuration.RegisterVendorCapabilities();
    ...
  }
  ```
