# Students Management System

This repository contains a 2-tier Students Management System built on .NET Framework 4.8 using ASP.NET MVC 5 and Entity Framework 6.

This commit updates the project to use PostgreSQL via Npgsql (EF6 provider) and adds a docker-compose development setup and environment-variable based configuration (Option 3).

Features included in this commit:
- Switched EF provider to Npgsql for PostgreSQL
- Updated Web.config with PostgreSQL provider registration
- Updated ApplicationDbContext to prefer environment variables (PG_HOST / PG_DB / PG_USER / PG_PASSWORD) and fall back to Web.config DefaultConnection
- Added docker-compose.yml for local PostgreSQL development
- Added .env.example with environment variable names and defaults
- Updated packages.config to include Npgsql and Npgsql.EntityFramework packages

How to run (quick):

1. Start the local PostgreSQL dev server (recommended):
   - From the repo root run:
     docker-compose up -d
   - Defaults used in .env.example and Web.config:
     Host=localhost;Port=5432;Database=studentsdb;Username=postgres;Password=changeme

2. Set environment variables (optional but recommended):
   - Windows (PowerShell):
     $env:PG_HOST = "localhost"
     $env:PG_PORT = "5432"
     $env:PG_DB = "studentsdb"
     $env:PG_USER = "postgres"
     $env:PG_PASSWORD = "changeme"

   - Linux/macOS (bash):
     export PG_HOST=localhost
     export PG_PORT=5432
     export PG_DB=studentsdb
     export PG_USER=postgres
     export PG_PASSWORD=changeme

   - Or copy .env.example to .env if your tooling loads .env files.

   The app prefers environment variables. If none are set, it will fallback to the DefaultConnection in Web.config.

3. Restore NuGet packages
   - Open the project in Visual Studio and restore NuGet packages (Manage NuGet Packages → Restore) or run the appropriate restore command in your environment.

4. Run the app
   - Ensure the PostgreSQL container is up (if using docker-compose).
   - Start the web project (F5 or Ctrl+F5). The EF initializer currently will create the database schema automatically the first time (CreateDatabaseIfNotExists behaviour).

5. Run the scheduler
   - Open the root page and click the "Generate Timetable (Greedy)" button. The greedy scheduler writes to the TimetableSlots table.
   - Inspect results by viewing the TimetableSlots, ClassSubjects, Teachers, and Classes tables in the PostgreSQL database.

Notes and recommendations
- Development: automatic database creation (CreateDatabaseIfNotExists) is enabled for convenience. It is suitable for local development and testing.
- Production: switch to explicit EF Migrations or an SQL migration system and remove automatic create behaviour. I can add EF Migrations files and migration scripts if you want.
- Secrets: avoid committing production credentials. Use environment variables (as implemented) or a secrets manager.
- Migrations with Npgsql: when you want migrations I will add the EF Migrations configuration and an initial migration (so you can use Add-Migration / Update-Database from Package Manager Console).

If you want, I can now:
- Add EF migrations + DataSeeder and push to main
- Create a branch and open a PR with migrations + seeder for review
- Add containerized web service for full docker-compose multi-container setup (web + db)

Which should I do next?