#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../Rebalancing.Web/Rebalancing.Web.csproj", "Rebalancing.Web/"]
COPY ["../Rebalancing.Core/Rebalancing.Core.csproj", "Rebalancing.Core/"]
COPY ["../Rebalancing.Data/Rebalancing.Data.csproj", "Rebalancing.Data/"]
COPY ["../Rebalancing.Import/Rebalancing.Import.csproj", "Rebalancing.Import/"]
COPY ["../Rebalancing.Integrations.Web/Rebalancing.Integrations.csproj", "Rebalancing.Integrations.Web/"]
RUN dotnet restore "Rebalancing.Web/Rebalancing.Web.csproj"
COPY . .
WORKDIR "/src/Rebalancing.Web"
RUN dotnet build "Rebalancing.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rebalancing.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rebalancing.Web.dll"]
