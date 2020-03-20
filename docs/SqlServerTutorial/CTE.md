# SQL Server CTE

### A) Simple SQL Server CTE example

For CTE we often need a dedicated Type. Since it's used "inside" XLinq it's not required to register it in DbContext. It's enough to attribute it with `[Tuple]`:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/CTE.cs --region SalesAmount --editable false
```

CTE is a subquery. After creation we need to pass it to the `WITH()` function and then use as usual:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/CTE.cs --region A
```

> **Note**, that C# [string interpolation](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated) works correctly in XLinq. Run the query to see the results.

### B) Using a common table expression to make report averages based on counts

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/CTE.cs --region B
```

### C) Using multiple SQL Server CTE in a single query example

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/CTE.cs --region C
```

SQL Server CTE has a limitation - it cannot be used inside a subquery. As a result we cannot `Include()` related entities with the results or use `Skip()` or `Take()`. Fortunately, unless the CTE is recursive, it can be re-written with a subquery. In XLinq this is as simple as *not* calling the `WITH()` function:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/CTE.cs --region C_1
```

[< BACK](Basic.md) | [HOME](/)
