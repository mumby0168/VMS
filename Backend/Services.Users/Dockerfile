#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Backend/Services.Users/Services.Users.csproj", "Backend/Services.Users/"]
COPY ["Backend/Services.RabbitMq/Services.RabbitMq.csproj", "Backend/Services.RabbitMq/"]
COPY ["Backend/Services.Common/Services.Common.csproj", "Backend/Services.Common/"]
RUN dotnet restore "Backend/Services.Users/Services.Users.csproj"
COPY . .
WORKDIR "/src/Backend/Services.Users"
RUN dotnet build "Services.Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Users.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Users.dll"]
