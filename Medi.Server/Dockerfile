# Etap 1: Budowanie aplikacji
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

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
COPY . .
WORKDIR "/source/Medi.Server"

# Budowanie aplikacji
RUN dotnet build -c Release -o /app/build

# Uruchamiamy `npm install` w katalogu frontendowym (jeśli istnieje)
WORKDIR "/source/medi.client"
RUN if [ -f package.json ]; then npm install && npm run build; fi

WORKDIR "/source/Medi.Server"
RUN dotnet publish "Medi.Server.csproj" -c Release -o /app/publish

# Etap 2: Publikowanie aplikacji
FROM build AS publish
WORKDIR /source/Medi.Server
RUN dotnet publish "Medi.Server.csproj" -c Release -o /app

# Etap 3: Finalny obraz runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app .  
ENTRYPOINT ["dotnet", "Medi.Server.dll"]
