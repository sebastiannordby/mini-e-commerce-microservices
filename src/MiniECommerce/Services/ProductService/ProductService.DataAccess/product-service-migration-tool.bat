@echo off
echo ProductServiceMigrationTool
echo Create entity framework migrations - product service
set /p migname= MigrationName:
dotnet ef migrations add %migname% --project .\ProductService.DataAccess.csproj --startup-project ..\ProductService.API\ProductService.API.csproj
pause