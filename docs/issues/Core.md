# EF Core issues

Solutions that ELINQ offers to some unresolved or "under investigation" issues.

| EF Core | ELINQ |
| --- | --- |
| [Allow DbFunction to return scalar value from aggregates](https://github.com/dotnet/efcore/issues/11850) | [fully supported](../SqlServerTutorial/AggregateFunctions.md) |
| [Invalid SQL generated with 3.1](https://github.com/dotnet/efcore/issues/20505) (bug in EF) | [see it working](20505.md) |
| [Support database window functions](https://github.com/dotnet/efcore/issues/12747) | [fully supported](../SqlServerTutorial/WindowFunctions.md) |
| [Ability to map a CLR method returning queryable to TVF](https://github.com/dotnet/efcore/issues/20051) | [Table UDF](https://try.entitylinq.com/docs/SqlServerTutorial/TableUDF.md) |
| [Translate component of datetime/timespan after arithmetic operations](https://github.com/dotnet/efcore/issues/18939) | Passed ON to the database |
| [Query: optimize arithmetic operation on constants](https://github.com/dotnet/efcore/issues/18819) | Thanks to the C# compiler |
| [Add LINQ methods for INTERSECT ALL and EXCEPT ALL](https://github.com/dotnet/efcore/issues/16273) | Builtin operators |

[< BACK](/README.md) | [HOME](/)
