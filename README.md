# MilleniumRecruitmentTask

Rozwiązanie przygotowane na potrzeby rekrutacji w Banku Millennium – mikroserwis zwracający dozwolone akcje dla kart użytkownika.

---

## Spis treści

- [Opis projektu](#opis-projektu)  
- [Struktura rozwiązania](#struktura-rozwiązania)  
- [Wymagania](#wymagania)  
- [Uruchomienie](#uruchomienie)  
- [Testy](#testy)  
- [Rozszerzalność](#rozszerzalnosc)  

---

## Opis projektu

Mikroserwis w .NET 8 wystawiający REST‑owe API, które dla zadanego `userId` i `cardNumber` zwraca listę dozwolonych akcji (`ACTION1`–`ACTION13`) na podstawie typu karty, jej statusu oraz tego, czy PIN jest ustawiony.

Reguły akcji zdefiniowane są w klasie `AllowedActionsRule`, a dane testowe symulowane są w `CardService` przy pomocy statycznej metody `CreateSampleUserCards()`.

---

## Struktura rozwiązania
```text
MilleniumRecruitmentTask.sln
├── Api
│   ├── Program.cs
│   ├── Controllers
│   │   └── CardsController.cs
│   └── Middlewares
│       ├── ApiKeyMiddleware.cs
│       └── ExceptionHandlingMiddleware.cs
├── Model
│   ├── Enums
│   │   ├── CardType.cs
│   │   └── CardStatus.cs
│   ├── Model
│   │   ├── CardDetails.cs
│   │   └── ActionRule.cs
│   ├── Exceptions
│   │   └── CardNotFoundException.cs
│   └── Rules
│       └── AllowedActionsRule.cs
├── Services
│   ├── Abstract
│   │   └── ICardService.cs
│   └── Concrete
│       └── CardService.cs
└── Tests
    ├── Services
    │   └── CardServiceTests.cs
    └── Rules
        └── AllowedActionsRuleTests.cs
```

## Wymagania
.NET 8 SDK
Visual Studio 2022

Narzędzia: dotnet cli, Swagger UI
## Uruchomienie
1. Sklonuj to repozytorium
2. Uruchom w środowiku Visual Studio i (lub) zbuduj poprzez komendę dotnet build
3. Uruchom projekt MilleniumRecruitmentTask.Api w profilu https
4. Sprawdź działanie pod adresem https://localhost:7067/swagger/index.html

## Testy
Repo zawiera testy:

Services/CardServiceTests – weryfikacja scenariuszy GetCardDetailsAsync oraz GetAllowedActions.
Testy można uruchomić poprzez komendę dotnet test lub poprzez IDE Visual Studio: Test-> Uruchom wszystkie testy

## Rozszerzalność
Reguły można przenieść do pliku JSON lub bazy danych z np. wykorzystaniem ORM Entity Framework– wystarczy zamienić implementację AllowedActionsRule na odczyt z zewnętrznego źródła.

Autoryzacja – użyto prostego klucza API (X-API-KEY) zdefiniowanego w appsettings, a wymaganego w nagłówku GetAllowedActions, można by użyć np JWT

Nowe endpointy – można dodać kolejne akcje w CardsController i łatwo je testować w Swagger UI. Do aplikacji w łatwy sposób można dodawać kolejne serwisy 

Dependency Injection – wszystkie zależności (serwisy, logger, repozytoria) rejestrowane są w Program.cs, co pozwala na:
testowanie z użyciem mocków (NullLogger, testowy ICardService),
łatwe podmienianie implementacji interfejsów,
centralne zarządzanie konfiguracją i cyklem życia obiektów.


Interfejsy i separacja odpowiedzialności – projekt podzielony jest na warstwy:

Model – definicje CardDetails, enumy, wyjątki, reguły,
Services – logika biznesowa w ICardService/CardService,
Api – kontrolery, middleware, punkt wejścia,
Tests – testy jednostkowe i reguł.