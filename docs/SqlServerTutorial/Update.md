# SQL Server UPDATE

EF does a thorough work to track entity state. In cases where the *fact* of change is not clear, it's usually better to let EF to manage the update.

ELINQ (pure SQL) is preferred when we don't want to retrieve the entity or a bulk update is needed.

### 1) Update a single column in all rows example

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Update.cs --region T1
```

### 2) Update multiple columns example

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Update.cs --region T2
```

---

[< BACK](Basic.md) | [HOME](/)
