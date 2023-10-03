#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Gateway/MiniECommerce.Gateway/MiniECommerce.Gateway.csproj", "Gateway/MiniECommerce.Gateway/"]
RUN dotnet restore "Gateway/MiniECommerce.Gateway/MiniECommerce.Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway/MiniECommerce.Gateway"
RUN dotnet build "MiniECommerce.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiniECommerce.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniECommerce.Gateway.dll"]