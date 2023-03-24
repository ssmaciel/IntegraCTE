#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IntegraCTE.API/IntegraCTE.API.csproj", "IntegraCTE.API/"]
COPY ["IntegraCTE.Core/IntegraCTE.Core.csproj", "IntegraCTE.Core/"]
COPY ["IntegraCTE.Infra/IntegraCTE.Infra.csproj", "IntegraCTE.Infra/"]
RUN dotnet restore "IntegraCTE.API/IntegraCTE.API.csproj"
COPY . .
WORKDIR "/src/IntegraCTE.API"
RUN dotnet build "IntegraCTE.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntegraCTE.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntegraCTE.API.dll"]