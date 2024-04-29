#!/bin/bash

# 첫 번째 인자로부터 마이그레이션 이름 설정
MIGRATION_NAME=$1

# AccountDBContext에 대한 마이그레이션 추가 및 실행
echo "dotnet ef migrations add $MIGRATION_NAME --context AccountDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj"
dotnet ef migrations add $MIGRATION_NAME --context AccountDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj

# AuthDBContext에 대한 마이그레이션 추가 및 실행
echo "dotnet ef migrations add $MIGRATION_NAME --context AuthDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj"
dotnet ef migrations add $MIGRATION_NAME --context AuthDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj

# HowlDBContext에 대한 마이그레이션 추가 및 실행
echo "dotnet ef migrations add $MIGRATION_NAME --context HowlDBContext에 --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj"
dotnet ef migrations add $MIGRATION_NAME --context HowlDBContext --project ../Shared.Featrues/Shared.Featrues.csproj --startup-project myhero_dotnet.csproj

# 사용자 입력 대기 (일시 중지 기능)
read -p "Press any key to continue..." -n1 -s
echo
