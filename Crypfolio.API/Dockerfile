FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Crypfolio.API/Crypfolio.API.csproj", "Crypfolio.API/"]
COPY ["Crypfolio.Application/Crypfolio.Application.csproj", "Crypfolio.Application/"]
COPY ["Crypfolio.Domain/Crypfolio.Domain.csproj", "Crypfolio.Domain/"]
COPY ["Crypfolio.Infrastructure/Crypfolio.Infrastructure.csproj", "Crypfolio.Infrastructure/"]
RUN dotnet restore "Crypfolio.API/Crypfolio.API.csproj"
COPY . .
WORKDIR "/src/Crypfolio.API"
RUN dotnet build "./Crypfolio.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Crypfolio.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crypfolio.API.dll"]
