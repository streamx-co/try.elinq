FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine

RUN apk add --no-cache nginx \
    && dotnet tool update -g dotnet-try

COPY ./ /elinq

RUN mkdir -p /run/nginx/    \
    && cp /elinq/docker/default.conf /etc/nginx/conf.d/    \
    && cd /elinq/Models/    \
    && dotnet build -p:Configuration=Release Models.csproj   \
    && cd /elinq/SqlServerTutorial/    \
    && dotnet build SqlServerTutorial.csproj /bl:package_fullBuild.binlog   \
    && cd /elinq/SakilaHomework/    \
    && dotnet build SakilaHomework.csproj /bl:package_fullBuild.binlog

EXPOSE 80

ENTRYPOINT [ "/elinq/docker/entrypoint.sh" ]
