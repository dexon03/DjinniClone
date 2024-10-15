FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ProfilesService/ProfilesService.csproj", "src/ProfilesService/"]
#COPY ["src/AspireApp1/AspireApp1.ServiceDefaults/AspireApp1.ServiceDefaults.csproj", "src/AspireApp1/AspireApp1.ServiceDefaults/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
RUN dotnet restore "src/ProfilesService/ProfilesService.csproj"
COPY . .
WORKDIR "/src/src/ProfilesService"
RUN dotnet build "ProfilesService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProfilesService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfilesService.dll"]
