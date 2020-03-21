# SQL Server String Functions

All string functions are fully supported and relevant are mappend to `String` methods and properties (`Length`, `Concat`, `ToLower/Invariant`, `ToUpper/Invariant`, `Replace`, `Trim/Start|End`, `Contains`, `Starts|Ends/With`, `Substring`). Of course all the SQL methods can be called directly as well.

> **Note**, `Contains` and `Starts|Ends/With` are implemented using `LIKE` operator.

In addition strings can be freely concatenated:

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test1B
```

and interpolated using C# syntax:

```cs --project ../../SakilaHomework/SakilaHomework.csproj --source-file ../../SakilaHomework/SakilaDbQueries.cs --region Test1B_1
```

---

[< BACK](Functions.md) | [HOME](/)
