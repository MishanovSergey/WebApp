FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT Development

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApplication1/WebApp.csproj", "WebApplication1/"]
COPY ["DBConnect/DBConnect.csproj", "DBConnect/"]
# COPY ["./src/app.csproj", "/src"]
# COPY ["./nuget.config", "./"]
RUN dotnet restore "./WebApplication1/WebApp.csproj"
COPY . .

WORKDIR "/src/WebApplication1"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.dll"]

# # См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# # Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER $APP_UID
# WORKDIR /app
# EXPOSE 8080
# EXPOSE 8081


# # Этот этап используется для сборки проекта службы
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
# WORKDIR /src
# COPY ["WebApplication1/WebApp.csproj", "WebApplication1/"]
# COPY ["DBConnect/DBConnect.csproj", "DBConnect/"]
# RUN dotnet restore "./WebApplication1/WebApp.csproj"
# COPY . .
# WORKDIR "/src/WebApplication1"
# RUN dotnet build "./WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# # Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
# FROM build AS publish
# ARG BUILD_CONFIGURATION=Release
# RUN dotnet publish "./WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# # Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "WebApp.dll"]