# SQL Server SELECT

XLinq extends `DbSet` with a single `Query` method. As any LINQ method, it accepts a Lambda. The Lambda parameters are the entities we want to participate in the query and return value is the result of `SELECT` invocation:

### A) SQL Server `SELECT` – retrieve some columns of a table

Retrieving some columns is not a common case in EF and requires declaration of the dedicated keyless entity (`FullName`).

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Select.cs --region A
```

### B) SQL Server `SELECT` – retrieve all columns from a table

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Select.cs --region B
```

### C) SQL Server `SELECT` – sort the result set

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Select.cs --region C
```

### D) SQL Server `SELECT` – group rows into groups example

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Select.cs --region D
```

### E) SQL Server `SELECT` – filter groups example

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Select.cs --region E
```

---

[< BACK](Basic.md) | [HOME](/)
