#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["microservices/Services.Business/Services.Business.csproj", "microservices/Services.Business/"]
COPY ["microservices/Services.RabbitMq/Services.RabbitMq.csproj", "microservices/Services.RabbitMq/"]
COPY ["microservices/Services.Common/Services.Common.csproj", "microservices/Services.Common/"]
RUN dotnet restore "microservices/Services.Business/Services.Business.csproj"
COPY . .
WORKDIR "/src/microservices/Services.Business"
RUN dotnet build "Services.Business.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Business.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Business.dll"]