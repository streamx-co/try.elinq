# SQL Server Inner Join

Join's `ON` method accepts a simple Object equality expression and infers the actual relationship from EF metadata:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/InnerJoin.cs --region InnerJoin1
```

There is also an option to specify the relationship manually:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/InnerJoin.cs --region InnerJoin2
```

Any number of entities can be joined:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/InnerJoin.cs --region InnerJoin3
```

[< BACK](Basic.md) | [HOME](/)
