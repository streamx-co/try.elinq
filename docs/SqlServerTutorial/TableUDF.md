# SQL Server Table-valued Functions

### Executing a table-valued function

To connect an SQL tABLE UDF, declare a c# function with the correct signature and add `[Function(RequiresAlias = true)]` and `[Tuple]` attributes. Note that the function returns an entity, as expected.

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Advanced/TableUDF.cs --region CallTableUDF
```

##### inside a complex statement:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Advanced/TableUDF.cs --region T2
```

---

[< BACK](UDF.md) | [HOME](/)
