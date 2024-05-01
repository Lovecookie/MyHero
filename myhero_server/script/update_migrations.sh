#!/bin/bash

MIGRATION_NAME="AddEncryptUID"
MIGRATION_CMD="myhero_migration.sh"

echo "$MIGRATION_CMD $MIGRATION_NAME"
$MIGRATION_CMD $MIGRATION_NAME

rem read -p "Press any key to continue..." -n1 -s
echo

