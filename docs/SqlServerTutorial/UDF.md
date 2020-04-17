# SQL Server User-defined Functions

**We suggest to open the corresponding [SQL Server tutorial page](https://www.sqlservertutorial.net/sql-server-user-defined-functions/) and go over the examples "side by side".**

- [User-defined scalar functions](ScalarUDF.md)
- [Table-valued functions](TableUDF.md)

> **Hint:** *Table-valued functions* let collaborate with DB admins easily, yet keep the responsibility boundaries: the developer "sees" a function that returns an entity. From the developer perspective it's absolutely "normal" entity, which can be joined or filtered. But since this is a function it can receive parameters! The DBA may use them to `SELECT` from different tables or load data from file system. There is no limits to what can be done, keeping the integration simple and transparent.

---

[< BACK](README.md) | [HOME](/)
