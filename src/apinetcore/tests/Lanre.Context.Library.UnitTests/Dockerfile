#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0c AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["tests/Lanre.Context.Library.UnitTests/Lanre.Context.Library.UnitTests.csproj", "tests/Lanre.Context.Library.UnitTests/"]
COPY ["src/Lanre.Context.Library/Lanre.Context.Library.csproj", "src/Lanre.Context.Library/"]
COPY ["src/Lanre.Infrastructure/Lanre.Infrastructure.csproj", "src/Lanre.Infrastructure/"]
RUN dotnet restore "tests/Lanre.Context.Library.UnitTests/Lanre.Context.Library.UnitTests.csproj"
COPY . .
WORKDIR "/src/tests/Lanre.Context.Library.UnitTests"
RUN dotnet build "Lanre.Context.Library.UnitTests.csproj" -c Release -o /app/build

FROM build AS final
WORKDIR /src/tests/Lanre.Context.Library.UnitTests
ENTRYPOINT ["dotnet","test"]

# ENTRYPOINT ["dotnet","test","--logger","trx;LogFileName=/tests/integration-test-results.trx"]