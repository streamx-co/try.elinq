# SQL Server Recursive CTE

### A) Simple SQL Server recursive CTE example

Weekday declaration:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/RecursiveCTE.cs --region Weekday --editable false
```

Recursive CTE behaves like an iterator. ELINQ provides `Current()` method to access the "recursive member".

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/RecursiveCTE.cs --region A
```

### B) Using a SQL Server recursive CTE to query hierarchical data

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/RecursiveCTE.cs --region B
```

---

[< BACK](Basic.md) | [HOME](/)
