# Bro Garage API

Modern .NET 10 port of the original Garage Management ASP.NET backend used by the Bro Garage React app.

## Structure

- `API/Controllers`: React-compatible HTTP endpoints from the original backend.
- `API/Data`: EF Core `DatabaseContext`, entities, seeds, and migrated baseline migration.
- `API/Shared`: request models, response models, enums, extensions, and utility types.
- `API/Templates` and `API/Images`: export template and fallback static assets.

## Local Setup

1. Update `API/appsettings.json` or user secrets with a real SQL Server connection string.
2. Replace `SecretKey` with a long random secret.
3. Restore and build:

```bash
dotnet restore BroGarage.slnx
dotnet build BroGarage.slnx --no-restore
```

4. Run:

```bash
dotnet run --project API/BroGarage.API.csproj
```

The default local HTTP URL is `http://localhost:5100`.
