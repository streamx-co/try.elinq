# SQL Server ORDER BY

### A) Sort a result set by one column in ascending order

There is a full integration between XLinq and EF, so we can sort using Linq. This is usually preferrable since we usually want to sort the *outer* result set managed by EF (run and see the SQL to understand better).

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region A
```

### B) Sort a result set by one column in descending order

It's convenient to return the "full" entities in ORM because otherwise there will be an "object hell". We will behave this way throughput this tutorial whenever it makes sense.

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region B
```

### C) Sort a result set by multiple columns

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region D
```

### D) Sort a result set by multiple columns and different orders

<!-- We return all columns as usually done in EF, though can retrieve some - as done in [SELECT - (A)](Select.md). -->

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region D
```

### E) Sort a result set by a column that is not in the select list

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region E
```

### F) Sort a result set by an expression

XLinq maps `String`'s `Length()` function to TSQL's `LEN`.

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region F
```

But we can do it in Linq as previously:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region F_1
```

### G) Sort by ordinal positions of columns

XLinq can do this, but it's definitely not a recommended way to sort:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/OrderBy.cs --region G
```

[< BACK](Basic.md) | [HOME](/)
