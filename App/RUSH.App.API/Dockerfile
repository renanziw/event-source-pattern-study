FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["App/RUSH.App.API/RUSH.App.API.csproj", "App/RUSH.App.API/"]
COPY ["App/RUSH.App.Domain/RUSH.App.Domain.csproj", "App/RUSH.App.Domain/"]
COPY ["App/RUSH.App.Infrastructure/RUSH.App.Infrastructure.csproj", "App/RUSH.App.Infrastructure/"]
RUN dotnet restore "App/RUSH.App.API/RUSH.App.API.csproj"
COPY . .
WORKDIR "/src/App/RUSH.App.API"
RUN dotnet build "RUSH.App.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RUSH.App.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RUSH.App.API.dll"]
