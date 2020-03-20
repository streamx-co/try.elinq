# SQL Server WHERE

### A) Finding rows by using a simple equality

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Where.cs --region A
```

### B) Finding rows that meet two conditions

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Where.cs --region B
```

### C) Finding rows by using a comparison operator

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Where.cs --region C
```

### D) Finding rows that meet any of two conditions

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Where.cs --region D
```

### E) Finding rows with the value between two values

Let's start using parameters:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Where.cs --region E
```

### F) Finding rows that have a value in a list of values

XLinq maps `IList.Contains()` method to SQL `IN` operator:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Where.cs --region F
```

### G) Finding rows whose values contain a string

And `String.Contains()` method to SQL `LIKE` operator:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Where.cs --region G
```

---

[< BACK](Basic.md) | [HOME](/)
