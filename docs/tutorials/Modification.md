# Efficient Data Modification

The rule of dumb tells that for optimal performance we need to make as few database requests as possible. This is especially relevant for insert and update scenarios, where we sometimes need to work with thousands of objects. Sending those objects to the database one by one is usually significantly slower than in a batch.

ELINQ lets write optimized `INSERT` queries using C#:

- [`INSERT` Multiple Rows](../SqlServerTutorial/InsertMulti.md) in a single statement. (Refer to your vendor documentation for an optimal batch size)
- [`INSERT INTO SELECT`](../SqlServerTutorial/InsertSelect.md) - so called *Bulk Insert* - in case the data is already in the database, it is much cheaper to avoid data pulling altogether.

In case of **data update** there are 3 important scenarios:

- [Bulk Update](../SqlServerTutorial/Update.md), where there is a need to update multiple rows in the same table. There is a special SQL construct for this case - `UPDATE ... WHERE`, which performs the update in a single query. Some databases, like SQL server, also support a more powerfull [UPDATE with JOIN](../SqlServerTutorial/UpdateJoin.md) construct.
- [Bulk Delete](../SqlServerTutorial/Delete.md), same idea for **delete** case.

And the last scenario - `UPSERT`. `INSERT` the new rows and `UPDATE` existing. Most vendors support it, but with different syntax and capabilities:

- [MERGE](../SqlServerTutorial/Merge.md) - SQL Server and Oracle.
- `INSERT ... ON DUPLICATE ...` - MySQL and Postgres. Sakila MySQL example:

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region TestUpsert
```

The queries can be freely combined as demonstrated in the [MERGE](../SqlServerTutorial/Merge.md) above.

[< BACK](/README.md) | [HOME](/)
