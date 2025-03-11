# Medi - Aplikacja Webowa

Projekt **Medi** to aplikacja internetowa działająca z użyciem backendu w .NET Core oraz bazy danych MySQL. Wszystko to działa w kontenerach Docker, co umożliwia łatwą konfigurację i uruchamianie projektu na różnych środowiskach.

## Wymagania

- **Docker**: Należy mieć zainstalowany Docker, aby uruchomić aplikację w kontenerach.
- **Docker Compose**: Używamy Docker Compose do zarządzania wieloma kontenerami w projekcie. 

## Uruchomienie aplikacji

### 1. Klonowanie repozytorium

Aby rozpocząć, sklonuj repozytorium na swoje lokalne urządzenie:

```
git clone https://github.com/Angelikq/Medi.git
cd Medi
```

### 2. Budowanie i uruchamianie kontenerów Docker
Aby uruchomić aplikację, użyj poniższego polecenia w terminalu:

```
docker-compose up --build
```
To polecenie zbuduje obrazy Docker i uruchomi kontenery, w tym frontend, backend oraz bazę danych MySQL.

### 3. Sprawdzanie statusu kontenerów
Aby sprawdzić, czy kontenery działają prawidłowo, możesz użyć polecenia:

```
docker-compose ps
```
Zobaczysz informacje o kontenerach backendu oraz bazy danych.

### 4. Dostęp do aplikacji
Frontend: Jeśli aplikacja frontendowa jest uruchomiona na porcie 3000, otwórz przeglądarkę i wejdź na:
http://localhost:3000

### 5. Zatrzymywanie kontenerów
Aby zatrzymać uruchomione kontenery, użyj poniższego polecenia:
```
docker-compose down
```
### 6. Inne przydatne komendy
Budowanie obrazu bez uruchamiania kontenerów:
```
docker-compose build
```
Uruchomienie kontenerów w tle (bez blokowania terminala):
```
docker-compose up -d
```
Usuwanie kontenerów, obrazów i wolumenów:
```
docker-compose down --volumes --rmi all
```
Jak wprowadzać zmiany w aplikacji?
Backend i Frontend
Zaktualizuj kod w plikach źródłowych.
Aby ponownie uruchomić aplikację z nowymi zmianami, wykonaj polecenie:
```
docker-compose up --build
```
Aplikacja zostanie odbudowana i uruchomiona na nowo.

Rozwiązywanie problemów
```
docker-compose logs
```
Problemy z bazą danych: Jeśli kontener z bazą danych nie działa prawidłowo, sprawdź logi bazy danych:
```
docker-compose logs db
```
Usuwanie wszystkich kontenerów i obrazów: Jeśli chcesz usunąć wszystkie kontenery, obrazy i wolumeny, użyj poniższego polecenia:
```
docker-compose down --volumes --rmi all
```

