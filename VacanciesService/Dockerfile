FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VacanciesService/VacanciesService.csproj", "VacanciesService/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "VacanciesService/VacanciesService.csproj"
COPY . .
WORKDIR "/src/VacanciesService"
RUN dotnet build "VacanciesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VacanciesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VacanciesService.dll"]
