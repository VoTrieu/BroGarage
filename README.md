# BroGarage

BroGarage is a full-stack garage management application. The repository contains a React frontend and a .NET 10 Web API backend backed by SQL Server.

The app helps a garage manage customers, vehicles, repair orders, maintenance cycles, spare parts, order templates, and printable/exportable service records.

## Project Structure

- `API/` - .NET 10 Web API.
- `API/Controllers/` - HTTP endpoints used by the React app.
- `API/Data/` - EF Core database context, entities, migrations, and seed data.
- `API/Shared/` - request models, response models, enums, extensions, and utility types.
- `Web/` - React frontend built with PrimeReact, Redux Toolkit, React Router, and i18next.
- `docker-compose.yml` - local SQL Server container and database initialization.

## Main Features

- Customer and vehicle management.
- Spare part inventory management.
- Maintenance cycle templates by vehicle type.
- Repair order creation, status tracking, payment details, and print/export support.
- JWT-style authentication with access and refresh token settings.
- Seed data for local development and testing.
- Vietnamese and English frontend localization.

## Local Requirements

- .NET 10 SDK
- Node.js and npm
- Docker Desktop

## Database

Start SQL Server:

```bash
docker compose up -d
```

The compose file exposes SQL Server on `localhost:1433` and creates the `brogarage` database if it does not already exist.

Default local connection string:

```text
Server=localhost,1433;Database=brogarage;User Id=sa;Password=Password@1;TrustServerCertificate=True
```

Apply EF Core migrations and seed data:

```bash
dotnet ef database update --project API/BroGarage.API.csproj
```

## Run The API

```bash
dotnet restore BroGarage.slnx
dotnet build BroGarage.slnx --no-restore
dotnet run --project API/BroGarage.API.csproj
```

Default API URL:

```text
http://localhost:5100
```

## Run The Web App

Create local frontend environment config:

```bash
cp Web/.env.example Web/.env
```

Install dependencies and start React:

```bash
cd Web
npm install
npm start
```

Default frontend URL:

```text
http://localhost:3000
```

If port `3000` is already in use:

```bash
PORT=3001 npm start
```

## Useful Commands

Build the backend:

```bash
dotnet build API/BroGarage.API.csproj
```

Build the frontend:

```bash
cd Web
npm run build
```

Create a new EF migration:

```bash
dotnet ef migrations add MigrationName --project API/BroGarage.API.csproj
```

Stop local SQL Server:

```bash
docker compose down
```

Remove SQL Server data volume:

```bash
docker compose down -v
```

## Development Notes

- `API/appsettings.json` contains local development settings. Replace `SecretKey` with a strong secret before using this outside local development.
- `Web/.env` is local-only and should not be committed.
- `Web/.env.example` documents the expected frontend API base URL.
- The frontend expects the API base URL in `REACT_APP_BASE_URL`.
- Seed data is managed through EF Core `HasData` seed classes under `API/Data/Seeds`.
