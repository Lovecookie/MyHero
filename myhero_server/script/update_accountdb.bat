@echo off 

SET HOST=localhost
SET DB_NAME=accountdb
SET USER_NAME=postgres
SET PW=myhero!@
SET CONNECTION="Host=%HOST%;Database=%DB_NAME%;Username=%USER_NAME%;Password=%PW%"

REM 프로젝트 디렉토리로 이동
cd ..

echo dotnet ef database update --context AccountDBContext --connection %CONNECTION%
dotnet ef database update --context AccountDBContext --connection %CONNECTION%

echo dotnet ef database update --context AuthDBContext --connection %CONNECTION%
dotnet ef database update --context AuthDBContext --connection %CONNECTION%

pause