#!/bin/bash

# 변수 설정
MIGRATION_NAME="UpdateHowl"
MIGRATION_CMD="./myhero_migration.sh"

# 명령어 출력 및 실행
echo "$MIGRATION_CMD $MIGRATION_NAME"
$MIGRATION_CMD $MIGRATION_NAME

# 사용자 입력 대기 (일시 중지 기능)
rem read -p "Press any key to continue..." -n1 -s
echo
