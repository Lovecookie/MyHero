@echo off

SET MIGRATION_NAME=%1

echo dotnet ef migrations add %MIGRATION_NAME% --context AccountDbContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj
dotnet ef migrations add %MIGRATION_NAME% --context AccountDbContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj

echo dotnet ef migrations add %MIGRATION_NAME% --context AuthDbContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj
dotnet ef migrations add %MIGRATION_NAME% --context AuthDbContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj

echo dotnet ef database update --context AccountDbContext
dotnet ef database update --context AccountDbContext

echo dotnet ef database update --context AuthDbContext
dotnet ef database update --context AuthDbContext

