# SQL Server SELECT

XLinq extends `DbSet` with a single `Query` method. As any LINQ method, it accepts a Lambda. The Lambda parameters are the entities we want to participate in the query and return value is the result of `SELECT` invocation:

### A) SQL Server `SELECT` – retrieve some columns of a table

Retrieving some columns is probably not a common case in EF and requires declaration of the dedicated keyless entity.

### B) SQL Server `SELECT` – retrieve all columns from a table
### C) SQL Server `SELECT` – sort the result set
### D) SQL Server `SELECT` – group rows into groups example
### E) SQL Server `SELECT` – filter groups example
