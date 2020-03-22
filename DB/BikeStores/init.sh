#!/bin/bash

sleep 10s

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 455Password -d BikeStores -Q "SELECT 1"

if [ $? != 0 ]; then
	/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 455Password -d master -i /docker-entrypoint-initdb.d/create_db.sql
	/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 455Password -d BikeStores -i /docker-entrypoint-initdb.d/create_objects.sql
	/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 455Password -d BikeStores -i /docker-entrypoint-initdb.d/load_data.sql
	/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 455Password -d BikeStores -i /docker-entrypoint-initdb.d/create_objects_2.sql
fi

echo "init completed"
