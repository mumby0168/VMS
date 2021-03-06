#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["microservices/Services.Logs/Services.Logs.csproj", "microservices/Services.Logs/"]
COPY ["microservices/Services.Common/Services.Common.csproj", "microservices/Services.Common/"]
RUN dotnet restore "microservices/Services.Logs/Services.Logs.csproj"
COPY . .
WORKDIR "/src/microservices/Services.Logs"
RUN dotnet build "Services.Logs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Logs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Logs.dll"]