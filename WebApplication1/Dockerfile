# # укажем, на основе какого образа будем 
# # делать наш контейнер. Для сборки приложения используем dotnet-sdk 
# # и назовём этот образ builder
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder 
# # укажем директорию для нашего приложения внутри контейнера
# WORKDIR /Application

# # Скопируем все файлы из проекта в файловую систему контейнера
# COPY . ./
# # Запустим restore для загрузки зависимостей
# RUN dotnet restore
# # Опубликуем собранный dll в папку "output"
# RUN dotnet publish -c Release -o output

# # Теперь соберём образ, в котором наше приложение 
# # будет запущено. Для запуска приложения достаточно
# # среды выполнения aspnet, без sdk
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /Application
# # Скопируем файлы приложения из предыдущего образа 
# COPY --from=builder /Application/output .
# # укажем команду, которая будет запускать приложение
# ENTRYPOINT ["dotnet", "WebApp.dll"]



# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "./WebApplication1/WebApp.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "./WebApp.csproj" -c Release -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.dll"]