@echo off

SET MIGRATION_NAME=%1
SET SHARED_PROJ=../../Shared.Featrues/Shared.Featrues.csproj
SET START_UP_PROJ=../myhero_dotnet.csproj

echo dotnet ef migrations add %MIGRATION_NAME% --context AccountDBContext  --project %SHARED_PROJ% --startup-project %START_UP_PROJ%
dotnet ef migrations add %MIGRATION_NAME% --context AccountDBContext --project %SHARED_PROJ% --startup-project %START_UP_PROJ%

echo dotnet ef migrations add %MIGRATION_NAME% --context AuthDBContext --project %SHARED_PROJ% --startup-project %START_UP_PROJ%
dotnet ef migrations add %MIGRATION_NAME% --context AuthDBContext --project %SHARED_PROJ% --startup-project %START_UP_PROJ%

echo dotnet ef migrations add %MIGRATION_NAME% --context HowlDBContext --project %SHARED_PROJ% --startup-project %START_UP_PROJ%
dotnet ef migrations add %MIGRATION_NAME% --context HowlDBContext --project %SHARED_PROJ% --startup-project %START_UP_PROJ%


pauses