# Run Locally

### Setup Databases

1. Install [docker](https://www.docker.com/)
1. MsSQL:

    Replace `<try.elinq>` with the path where you cloned this repo.

    ```sh
    docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=455Password' -p 1473:1433 -d \
    -v <try.elinq>/DB/BikeStores:/docker-entrypoint-initdb.d \
    -v sqlvolume:/var/opt/mssql mcr.microsoft.com/mssql/server:2019-latest \
    bash docker-entrypoint-initdb.d/entrypoint.sh
    ```

1. MySQL

    Replace `<try.elinq>` with the path where you cloned this repo.

    ```sh
    docker run -e MYSQL_ROOT_PASSWORD=455Password \
    -v <try.elinq>/DB/sakila/mysql:/docker-entrypoint-initdb.d -p 3376:3306 -d mysql:8
    ```

1. Edit /etc/hosts (windows: c:\windows\system32\drivers\etc\hosts)

    Add 2 lines:

    ```sh
    127.0.0.1   mssql
    127.0.0.1   mysql
    ```

### With IDE

Open try.elinq.sln. It contains 2 projects:

- SqlServerTutorial
- SakilaHomework

Both are console apps. Running them will execute all examples in each of them.

### With Try .NET

This will launch the [try.elinq](https://try.entitylinq.com/) site locally.

```sh
> dotnet tool update -g dotnet-try
> dotnet try .
```
