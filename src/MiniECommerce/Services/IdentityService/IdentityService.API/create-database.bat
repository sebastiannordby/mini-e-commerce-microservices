docker run -it \
    -e "ACCEPT_EULA=Y" \
    -e "SA_PASSWORD=A&VeryComplex123Password" \
    -p 1433:1433 \
    --name sql-server-2022 \
    mcr.microsoft.com/mssql/server:2022-latest
pause