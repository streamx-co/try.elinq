FROM alpine:3.11

RUN apk add --no-cache \
    ca-certificates \
    \
    # .NET Core dependencies
    krb5-libs \
    libgcc \
    libintl \
    libssl1.1 \
    libstdc++ \
    zlib \
    # nginx
    nginx

# Configure web servers to bind to port 80 when present
ENV \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Set the invariant mode since icu_libs isn't included (see https://github.com/dotnet/announcements/issues/20)
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8 \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

# Install .NET Core SDK
RUN dotnet_sdk_version=3.1.102 \
    && wget -O dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$dotnet_sdk_version/dotnet-sdk-$dotnet_sdk_version-linux-musl-x64.tar.gz \
    && dotnet_sha512='21f7fab1eb4244f9ed89d1dccee4c67813fd1499d693a575e55d077049df14468905803ada814c5fc99ddfec748df453ae3d9d4b1206f9b1089044191e99a57d' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -oxzf dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    && rm dotnet.tar.gz \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help

RUN dotnet tool update -g dotnet-try

COPY ./ /xlinq

RUN mkdir -p /run/nginx/    \
    && cp /xlinq/docker/default.conf /etc/nginx/conf.d/    \
    && cd /xlinq/SqlServerTutorial/    \
    && dotnet build SqlServerTutorial.csproj /bl:package_fullBuild.binlog   \
    && cd /xlinq/SakilaHomework/    \
    && dotnet build SakilaHomework.csproj /bl:package_fullBuild.binlog

EXPOSE 80

ENTRYPOINT [ "/xlinq/docker/entrypoint.sh" ]
