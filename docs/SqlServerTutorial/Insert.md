# SQL Server INSERT

### 1) Basic `INSERT` example

In SQL, `INSERT` requires an ad-hoc specification of columns to insert to. There is a similar concept in C# - [value tuple](https://docs.microsoft.com/en-us/dotnet/csharp/tuples).

ELINQ defines an extension method - `@using()` that accepts a tuple. The returned object has several `RowXXX()` overloads to create `VALUES` parameters in a type-safe way.

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Insert.cs --region T1
```

Insert a row from an object:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Insert.cs --region newPromo --editable false
```

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Insert.cs --region T1_1
```

We can also override properties from the passed object by passing additional parameters to the `RowFrom()` method. In the next example `discount` will be inserted with `DEFAULT`:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Insert.cs --region T1_2
```

### 2) Insert and return inserted values

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/Insert.cs --region T2
```

---

[< BACK](Basic.md) | [HOME](/)
