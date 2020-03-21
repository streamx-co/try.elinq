# SQL Server Basics

**We suggest to open the corresponding [tutorial page](https://www.sqlservertutorial.net/sql-server-basics/) and go over the examples "side by side".**

> **Disclaimer**: Linq handles most of functionality up to tutorial's section 7 very elegantly. There is no much value in replacing it for these cases. The reason we demonstrate the basic SQL capabilites of XLinq is that the advanced cases are built on patterns of simple cases.
>
> So before jumping to the advanced cases, we suggest to go over and run few basic ones to understand how XLinq works.

- [Section 1. Querying data](#section-1.querying-data)
- [Section 2. Sorting data](#section-2.sorting-data)
- [Section 3. Limiting rows](#section-3.limiting-rows)
- [Section 4. Filtering data](#section-4.filtering-data)
- [Section 5. Joining tables](#section-5.joining-tables)
- [Section 6. Grouping data](#section-6.grouping-data)
- [Section 7. Subquery](#section-7.subquery)
- [Section 8. Set Operators](#section-8.set-operators)
- [Section 9. Common Table Expression (CTE)](#section-9.common-table-expression-cte)
- [Section 11. Modifying data](#section-11.modifying-data)

### Section 1. Querying data

- [SELECT](Select.md)

### Section 2. Sorting data

- [ORDER BY](OrderBy.md)

### Section 3. Limiting rows

- [OFFSET FETCH](OffsetFetch.md)
- [SELECT TOP](Top.md)

### Section 4. Filtering data

- [DISTINCT](Distinct.md)
- [WHERE](Where.md)

### Section 5. Joining tables

- [INNER JOIN](InnerJoin.md)
- Since joins in XLinq are functions, using a required join type is merely a call to the correct function.

### Section 6. Grouping data

- [GROUP BY](GroupBy.md)
- [HAVING](Having.md)
- [GROUPING SETS](GroupingSet.md)
- [CUBE and ROLLUP](CubeRollup.md)

### Section 7. Subquery

- [Subquery](SubQuery.md)
- [Correlated subquery](CoSubQuery.md)
- EXISTS, ANY and ALL are covered in [Subquery](SubQuery.md)

### Section 8. Set Operators

- [UNION, INTERSECT, EXCEPT](Union.md)

### Section 9. Common Table Expression (CTE)

- [CTE](CTE.md)
- [Recursive CTE](RecursiveCTE.md)

### Section 10. Pivot

- Not Supported <big>&#128533;</big>

### Section 11. Modifying data

- [INSERT](Insert.md)
- [INSERT multiple rows](InsertMulti.md) (with _Batch Insert_ example)
- [INSERT INTO SELECT](InsertSelect.md) (_Bulk Insert_)
- [UPDATE](Update.md) (_Bulk Update_)
- [UPDATE JOIN](UpdateJoin.md) (_Bulk Update_)
- [DELETE](Delete.md) (_Bulk Delete_)
- [MERGE](Merge.md) (_Upsert_)

---

[< BACK](README.md) | [HOME](/README.md)
