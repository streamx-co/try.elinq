FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine

RUN apk add --no-cache nginx \
    && dotnet tool update -g dotnet-try

COPY ./ /xlinq

RUN mkdir -p /run/nginx/    \
    && cp /xlinq/docker/default.conf /etc/nginx/conf.d/    \
    && cd /xlinq/Models/    \
    && dotnet build -p:Configuration=Release Models.csproj   \
    && cd /xlinq/SqlServerTutorial/    \
    && dotnet build SqlServerTutorial.csproj /bl:package_fullBuild.binlog   \
    && cd /xlinq/SakilaHomework/    \
    && dotnet build SakilaHomework.csproj /bl:package_fullBuild.binlog

EXPOSE 80

ENTRYPOINT [ "/xlinq/docker/entrypoint.sh" ]
