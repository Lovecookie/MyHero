@echo off 

SET MIGRATION_NAME=InitCreate
SET MIGRATION_CMD=migration.bat

echo %MIGRATION_CMD% %MIGRATION_NAME%
%MIGRATION_CMD% %MIGRATION_NAME%

pause