@echo off 

rem SET MIGRATION_NAME=AddUserBasicIndex

echo dotnet ef database update --context AccountDbContext
dotnet ef database update --context AccountDbContext

echo dotnet ef database update --context AuthDbContext
dotnet ef database update --context AuthDbContext

pause
