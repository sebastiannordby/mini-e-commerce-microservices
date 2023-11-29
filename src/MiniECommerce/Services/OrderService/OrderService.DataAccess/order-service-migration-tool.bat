@echo off
echo OrderServiceMigrationTool
echo Create entity framework migrations - order service
set /p migname= MigrationName:
dotnet ef migrations add %migname% --project .\OrderService.DataAccess.csproj --startup-project ..\OrderService.API\OrderService.API.csproj
pause

