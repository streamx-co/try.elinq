#!/bin/bash

#sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=455Password' -p 1433:1433 -d -v /home/kostat/devel/xlinq.live/SQL-Server-Sample-Database:/docker-entrypoint-initdb.d -v sqlvolume:/var/opt/mssql mcr.microsoft.com/mssql/server:2019-latest bash docker-entrypoint-initdb.d/entrypoint.sh

/docker-entrypoint-initdb.d/init.sh &

exec /opt/mssql/bin/sqlservr
