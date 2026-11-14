# .NET-Based

A collection of .NET learning projects and reference notes covering ASP.NET Core Web APIs, asynchronous programming, dependency injection, object-oriented design, SOLID principles, and design patterns. The projects are independent examples; run them separately.

## Projects

### ASP.NET Core Web API

`.NETWebAPIbasedProject/api` contains a .NET 8 Web API for stocks, comments, and user portfolios. It uses Entity Framework Core with SQL Server, ASP.NET Core Identity, JWT bearer authentication, repository interfaces, and Swagger in development. It can also look up stock information through the Financial Modeling Prep (FMP) API.

Main controller groups:

- `api/account` — account registration and login
- `api/stock` — stock listing and CRUD operations
- `api/comment` — comments associated with stocks
- `api/userstock` — the authenticated user's stock portfolio

The API includes EF Core migrations, DTOs, model mappers, repository implementations, and a sample SQL query file.

### C# concepts and patterns

- `AdvancedC#/AsyncAwait` — small console examples demonstrating `async`/`await` and `Task` usage.
- `AdvancedC#/DependencyInjection` — a dependency-injection console sample and notes explaining registrations, constructor injection, resolution, and service lifetimes.
- `DesignPatternsProject/DesignPatternsApp` — a .NET 8 console project with examples grouped into creational, structural, and behavioral design patterns, plus OOP and SOLID principle examples. The `src` directory also contains pattern and principles notes.

## Repository layout

```text
.NETWebAPIbasedProject/
  api/                         ASP.NET Core API, migrations, and SQL/HTTP samples
AdvancedC#/
  AsyncAwait/                  Async/await and Task console samples
  DependencyInjection/         DI sample and notes
DesignPatternsProject/
  DesignPatternsApp/            OOP, SOLID, and design-pattern examples
  DesignPatternsApp.sln
```

## Requirements

- .NET 8 SDK
- SQL Server for the Web API
- An FMP API key for endpoints that look up stocks from Financial Modeling Prep

## Run the Web API

From the repository root, configure the API's required settings using .NET user secrets (or an equivalent secure configuration provider). Do not commit credentials to `appsettings.json`:

```bash
cd .NETWebAPIbasedProject/api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your SQL Server connection string>"
dotnet user-secrets set "JWT:Issuer" "<your issuer>"
dotnet user-secrets set "JWT:Audience" "<your audience>"
dotnet user-secrets set "JWT:SigningKey" "<a sufficiently long secret key>"
dotnet user-secrets set "FMPKey" "<your Financial Modeling Prep API key>"
```

Restore and run the project:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

If the `dotnet ef` command is unavailable, install or use the .NET EF Core CLI tool before applying the migrations. When the API is running in the Development environment, Swagger UI is available at `/swagger` on the local URL printed by `dotnet run`.

## Run the console examples

Run commands from the repository root:

```bash
dotnet run --project "AdvancedC#/AsyncAwait/SampleAA/SampleAA.csproj"
dotnet run --project "AdvancedC#/AsyncAwait/TaskRelated/SampleTaskAA/SampleTaskAA.csproj"
dotnet run --project "AdvancedC#/DependencyInjection/DISample/DISample.csproj"
dotnet run --project "DesignPatternsProject/DesignPatternsApp/DesignPatternsApp.csproj"
```

The API can be built with `dotnet build .NETWebAPIbasedProject/api/api.sln`; the design-patterns project can be built with `dotnet build DesignPatternsProject/DesignPatternsApp.sln`.

## Notes and examples

- General .NET and Web API notes: `.NETWebAPIbasedProject/api/DotNetRelatedNotes.txt`
- Dependency-injection notes: `AdvancedC#/DependencyInjection/di-notes.md`
- Design-pattern notes: `DesignPatternsProject/DesignPatternsApp/src/DesignPatterns/designpatterns_notes.md`
- SOLID principles notes: `DesignPatternsProject/DesignPatternsApp/src/SOLIDPrinciples/solidprinciples.md`
- A sample HTTP request file is available at `.NETWebAPIbasedProject/api/api.http`.
