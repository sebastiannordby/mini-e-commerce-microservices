@echo off
echo UserServiceMigrationTool
echo Create entity framework migrations - order service
set /p migname= MigrationName:
dotnet ef migrations add %migname% --project .\UserService.DataAccess.csproj --startup-project ..\UserService.API\UserService.API.csproj
pause

