# SQL Server CUME_DIST Function

### Using SQL Server `CUME_DIST()` function over a result set example

To create a window we need to call `AggregateBy()` function, which let's specify the `OVER` clause:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Functions/Window/CumeDist.cs --region A
```

### Using SQL Server `CUME_DIST()` function over a partition example

The `OVER` clause accepts an `ORDER` clause as above or `PARTITION` clause as below:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Functions/Window/CumeDist.cs --region B
```

---

[< BACK](WindowFunctions.md) | [HOME](/)
