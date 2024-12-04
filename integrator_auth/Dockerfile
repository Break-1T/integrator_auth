#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Определение версии приложения
ARG VERSION
ENV APP_VERSION=$VERSION

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG VERSION  # Используем VERSION на этапе сборки
WORKDIR /src
COPY ["integrator_auth/integrator_auth.csproj", "integrator_auth/"]
COPY ["integrator_auth.database/integrator_auth.database.csproj", "integrator_auth.database/"]
RUN dotnet restore "./integrator_auth/integrator_auth.csproj"
COPY . .
WORKDIR "/src/integrator_auth"
RUN dotnet build "./integrator_auth.csproj" -c $BUILD_CONFIGURATION -p:Version=$VERSION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG VERSION  # Используем VERSION на этапе публикации
RUN dotnet publish "./integrator_auth.csproj" -c $BUILD_CONFIGURATION -p:Version=$VERSION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV APP_VERSION=$VERSION
ENTRYPOINT ["dotnet", "integrator_auth.dll"]