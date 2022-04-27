#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tests/Lanre.Context.Poll.UnitTests/Lanre.Context.Poll.UnitTests.csproj", "tests/Lanre.Context.Poll.UnitTests/"]
COPY ["src/Lanre.Context.Poll/Lanre.Context.Poll.csproj", "src/Lanre.Context.Poll/"]
COPY ["src/Lanre.Infrastructure/Lanre.Infrastructure.csproj", "src/Lanre.Infrastructure/"]
RUN dotnet restore "tests/Lanre.Context.Poll.UnitTests/Lanre.Context.Poll.UnitTests.csproj"
COPY . .
WORKDIR "/src/tests/Lanre.Context.Poll.UnitTests"
RUN dotnet build "Lanre.Context.Poll.UnitTests.csproj" -c Release -o /app/build

FROM build AS final
WORKDIR /src/tests/Lanre.Context.Poll.UnitTests
ENTRYPOINT ["dotnet","test"]
# ENTRYPOINT ["dotnet","test","--logger","trx;LogFileName=/tests/integration-test-results.trx"]

