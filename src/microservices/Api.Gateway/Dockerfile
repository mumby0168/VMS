#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["microservices/Api.Gateway/Api.Gateway.csproj", "microservices/Api.Gateway/"]
COPY ["microservices/Services.RabbitMq/Services.RabbitMq.csproj", "microservices/Services.RabbitMq/"]
COPY ["microservices/Services.Common/Services.Common.csproj", "microservices/Services.Common/"]
RUN dotnet restore "microservices/Api.Gateway/Api.Gateway.csproj"
COPY . .
WORKDIR "/src/microservices/Api.Gateway"
RUN dotnet build "Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Gateway.dll"]