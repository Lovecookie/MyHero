@echo off 

SET MIGRATION_NAME=AddEncryptUID
SET MIGRATION_CMD=myhero_migration.bat

echo %MIGRATION_CMD% %MIGRATION_NAME%
%MIGRATION_CMD% %MIGRATION_NAME%

pause
