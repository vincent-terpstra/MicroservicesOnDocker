# Migrations

This directory contains the Entity Framework Core migrations for the `PlatformService`.

## Generating a New Migration

Whenever you make changes to your entity models or the `AppDbContext`, you need to generate a new migration.

From the `PlatformService` project directory, run the following command (replace `<MigrationName>` with a descriptive name for your changes, e.g., `AddPlatformTable`):
```bash
    dotnet ef migrations add <MigrationName>
```

## Updating the Database

To manually apply pending migrations to your database, run:
```bash
 dotnet ef database update
```

## Removing a Migration

If you made a mistake and want to remove the most recently generated migration (only if it hasn't been applied to the database yet), you can run:
```bash
  dotnet ef migrations remove
```