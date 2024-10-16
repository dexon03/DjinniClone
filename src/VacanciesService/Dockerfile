FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/VacanciesService/VacanciesService.csproj", "src/VacanciesService/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
COPY ["src/DjinniClone.ServiceDefaults/DjinniClone.ServiceDefaults.csproj", "src/DjinniClone.ServiceDefaults/"]
RUN dotnet restore "src/VacanciesService/VacanciesService.csproj"
COPY ./src/VacanciesService ./src/VacanciesService
COPY ./src/Core ./src/Core
COPY ./src/DjinniClone.ServiceDefaults ./src/DjinniClone.ServiceDefaults
WORKDIR "/src/src/VacanciesService"
RUN dotnet build "VacanciesService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VacanciesService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VacanciesService.dll"]
