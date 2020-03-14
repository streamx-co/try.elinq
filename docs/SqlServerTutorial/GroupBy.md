# SQL Server GROUP BY

In practice, the `GROUP BY` clause is often used with aggregate functions. Our basic entities do not have properties for them. A dedicated type is needed.

The simplest way is to declare a class that references the entities and adds missing properties:

```cs
public class CustomerYearOrders {
    public Customers Customer { get; set; }
    public int Year { get; set; }
    public int OrdersPlaced { get; set; }
}
```

In EF such types are called [Keyless Entity Types](https://docs.microsoft.com/en-us/ef/core/modeling/keyless-entity-types) and they must be mapped as keyless:

```cs
modelBuilder.Entity<CustomerYearOrders>(entity => entity.HasNoKey());
```

Then we can introduce a dedicated `DbSet<>` property in the context or use ad-hoc `context.Set<>()` method. Now we are ready for prime time:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/GroupBy.cs --region T1
```

> **Note** that in order the referenced entity to get populated, we need to `Include` it.

With `DISTINCT`:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/GroupBy.cs --region T2
```

With aggregate:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/GroupBy.cs --region T3
```

The next few examples do permutations with already covered features. Except the last which has a complex `SUM` expression:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/GroupBy.cs --region T4
```
