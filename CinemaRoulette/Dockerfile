FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CinemaRoulette.csproj", "./"]
RUN dotnet restore "CinemaRoulette.csproj"
COPY . .
RUN dotnet build "CinemaRoulette.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CinemaRoulette.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaRoulette.dll"]

# ── Этап для дебага ──────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS debug
WORKDIR /app
COPY ["CinemaRoulette.csproj", "./"]
RUN dotnet restore "CinemaRoulette.csproj"
COPY . .

# Устанавливаем vsdbg для Rider
RUN apt-get update && apt-get install -y unzip curl && \
    curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

ENTRYPOINT ["dotnet", "run", "--project", "CinemaRoulette.csproj"]