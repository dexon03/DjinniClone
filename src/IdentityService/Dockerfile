FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/IdentityService/IdentityService.csproj", "src/IdentityService/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
COPY ["src/DjinniClone.ServiceDefaults/DjinniClone.ServiceDefaults.csproj", "src/DjinniClone.ServiceDefaults/"]
RUN dotnet restore "src/IdentityService/IdentityService.csproj"
COPY ./src/IdentityService ./src/IdentityService
COPY ./src/Core ./src/Core
COPY ./src/DjinniClone.ServiceDefaults ./src/DjinniClone.ServiceDefaults
WORKDIR "/src/src/IdentityService"
RUN dotnet build "IdentityService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "IdentityService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]
