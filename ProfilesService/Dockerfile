FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfilesService/ProfilesService.csproj", "ProfilesService/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "ProfilesService/ProfilesService.csproj"
COPY . .
WORKDIR "/src/ProfilesService"
RUN dotnet build "ProfilesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfilesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfilesService.dll"]
