# SQL Server UPDATE JOIN

EF does a thorough work to track entity state. In cases where the *fact* of change is not clear, it's usually better to let EF to manage the update.

ELINQ (pure SQL) is preferred when we don't want to retrieve the entity or a bulk update is needed.

### A) SQL Server `UPDATE INNER JOIN` example

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/UpdateJoin.cs --region A
```

### B) SQL Server `UPDATE LEFT JOIN` example

Declarations:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/UpdateJoin.cs --region Declarations --editable false
```

We `INSERT`, `UPDATE` and `SELECT` in a nice single compound statement (Run to see):

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/UpdateJoin.cs --region B
```

---

[< BACK](Basic.md) | [HOME](/)
