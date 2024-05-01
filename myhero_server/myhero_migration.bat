@echo off

SET MIGRATION_NAME=%1

echo dotnet ef migrations add %MIGRATION_NAME% --context AccountDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj
dotnet ef migrations add %MIGRATION_NAME% --context AccountDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj

echo dotnet ef migrations add %MIGRATION_NAME% --context AuthDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj
dotnet ef migrations add %MIGRATION_NAME% --context AuthDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj

echo dotnet ef migrations add %MIGRATION_NAME% --context HowlDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj
dotnet ef migrations add %MIGRATION_NAME% --context HowlDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj


pause