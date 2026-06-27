# Students Management System

This repository contains a 2-tier Students Management System built on .NET Framework 4.8 using ASP.NET MVC 5 and Entity Framework 6.

This commit updates the project to use PostgreSQL via Npgsql (EF6 provider) and adds a docker-compose development setup.

Features included in this commit:
- Switched EF provider to Npgsql for PostgreSQL
- Updated Web.config with PostgreSQL connection string and provider registrations
- Updated packages.config to include Npgsql and Npgsql.EntityFramework packages
- Added docker-compose.yml for local PostgreSQL development
- Updated README with PostgreSQL setup and migration instructions

How to run (quick):
1. Start a local PostgreSQL instance (recommended: docker-compose up -d)
2. Update Web.config connection string credentials if needed
3. Restore NuGet packages in Visual Studio
4. Run the site (F5) — EF will create the database schema automatically (CreateDatabaseIfNotExists initializer)

If you want, I can also add EF Migrations folder and seed data automatically — say "Add seeder and migrations" and I'll push that next.
