FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ChatService/ChatService.csproj", "src/ChatService/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
COPY ["src/DjinniClone.ServiceDefaults/DjinniClone.ServiceDefaults.csproj", "src/DjinniClone.ServiceDefaults/"]
RUN dotnet restore "src/ChatService/ChatService.csproj"
COPY ./src/ChatService ./src/ChatService
COPY ./src/Core ./src/Core
COPY ./src/DjinniClone.ServiceDefaults ./src/DjinniClone.ServiceDefaults
WORKDIR "/src/src/ChatService"
RUN dotnet build "ChatService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatService.dll"]
