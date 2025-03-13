# Etap 1: Budowanie aplikacji
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopiujemy pliki projektu i przywracamy zależności (cache Dockera)
COPY ["Medi.Server/Medi.Server.csproj", "Medi.Server/"]
RUN dotnet restore "Medi.Server/Medi.Server.csproj"

# Instalacja Node.js i TypeScript (potrzebne do budowania frontendu)
RUN apt-get update && apt-get install -y curl gnupg lsb-release \
    && curl -sL https://deb.nodesource.com/setup_22.x | bash - \
    && apt-get install -y nodejs \
    && npm install -g typescript \
    && apt-get clean

# Kopiujemy resztę kodu
COPY . /app/src

# Budowanie aplikacji
WORKDIR "/app/src/Medi.Server"
RUN dotnet build -c Release -o /app/build

# Uruchamiamy `npm install` w katalogu frontendowym (jeśli istnieje)
WORKDIR "/app/src/medi.client"
RUN if [ -f package.json ]; then npm install && npm run build; fi

# Publikowanie aplikacji
WORKDIR "/app/src/Medi.Server"
RUN dotnet publish "Medi.Server.csproj" -c Release -o /app/publish

# Etap 2: Finalny obraz runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .  

ENTRYPOINT ["dotnet", "Medi.Server.dll"]
