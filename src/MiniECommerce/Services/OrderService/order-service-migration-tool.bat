@echo off
echo OrderServiceMigrationTool
set /p migname= MigrationName:
dotnet ef migrations add %migname% --project .\OrderService.DataAccess\OrderService.DataAccess.csproj --startup-project .\OrderService.API\OrderService.API.csproj
pause