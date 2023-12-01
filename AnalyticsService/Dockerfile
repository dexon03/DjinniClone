FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AnalyticsService/AnalyticsService.csproj", "AnalyticsService/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "AnalyticsService/AnalyticsService.csproj"
COPY . .
WORKDIR "/src/AnalyticsService"
RUN dotnet build "AnalyticsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnalyticsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnalyticsService.dll"]
