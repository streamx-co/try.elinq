# SQL Server Scalar Functions

### Calling a scalar function

To connect an SQL UDF, declare a C# function with the correct signature and add `[Function]` attribute. In fact **everything** is connected this way. ELINQ does not have any special SQL knowledge for `SELECT` or `FROM`. It reads the attributes and generates the query based on metadata.

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Advanced/ScalarUDF.cs --region CallScalarUDF
```

##### inside `SELECT`:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Advanced/ScalarUDF.cs --region T1
```

---

[< BACK](UDF.md) | [HOME](/)
