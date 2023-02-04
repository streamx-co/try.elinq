FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine
#FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine

RUN apk add --no-cache nginx \
    && dotnet tool update -g Microsoft.dotnet-try

COPY ./ /elinq

#-p:Configuration=Release 
RUN mkdir -p /run/nginx/    \
    && cp /elinq/docker/default.conf /etc/nginx/http.d/    \
    && cd /elinq/Models/    \
    && dotnet build Models.csproj   \
    && cd /elinq/SqlServerTutorial/    \
    && dotnet build SqlServerTutorial.csproj /bl:package_fullBuild.binlog   \
    && cd /elinq/SakilaHomework/    \
    && dotnet build SakilaHomework.csproj /bl:package_fullBuild.binlog   \
    && cd /elinq/EFCoreIssues/    \
    && dotnet build EFCoreIssues.csproj /bl:package_fullBuild.binlog

EXPOSE 80

ENTRYPOINT [ "/elinq/docker/entrypoint.sh" ]
