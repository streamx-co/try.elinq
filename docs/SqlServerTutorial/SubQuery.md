# SQL Server Subquery

#### sales orders of the customers who locate in `New York`:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T1
```

> When subquery `SELECTs` a single column (or a tuple), we may return it `AsCollection()` or `AsSingle()`. The correct type propagates out. Thus the return type of `SubQuery()` method is `ICollection<int?>`.

For clarity let's assign the result of the subquery to a local variable (of course SQL is same in either case):

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T1_1
```

#### Nesting subquery

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T2
```

#### SQL Server subquery is used in place of an expression

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T3
```

#### SQL Server subquery is used with `IN` operator

In fact we already covered `IN` operator in the very first example above.

#### SQL Server subquery is used with `ANY` operator

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T5
```

#### SQL Server subquery is used with `ALL` operator

Just replace `ANY` with `ALL` above and run!

##### customers who bought products in 2017:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T7
```

##### customers who did not buy any products in 2017:

XLinq maps SQL's `EXISTS` operator to `Any()`:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T8
```

#### SQL Server subquery in the `FROM` clause

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/SubQuery.cs --region T9
```

> Since XLinq supports variables, this very powerfull feature does not lead to complex nesting.

---

[< BACK](Basic.md) | [HOME](/)
