# SQL Server GROUPING SETS

### Setup a sales summary table

Creating an ordinary table is usually not an option in practice since there might be a collision if ewxecuted in parallel. The common technique is a creating temporal table per session. This is what we do here:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/GroupingSet.cs --region SalesSummary
```

> Run the example above and see that 2 separate queries were executed. The first creates the `#sales_summary` table and the second uses it.

Temporary table is potentially complex. The same result can be achieved with a subquery. Also, if the same subquery is going to be used several times, it can be refactored to a separate function. Run the following and see same results as above:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/GroupingSet.cs --region SetupSalesSummaryTest
```

### `GROUPING` function

All the features in one example:

```cs --project ../../SqlServerTutorial/SqlServerTutorial.csproj --source-file ../../SqlServerTutorial/Basic/GroupingSet.cs --region SalesSummary2
```

---

[< BACK](Basic.md) | [HOME](/)
