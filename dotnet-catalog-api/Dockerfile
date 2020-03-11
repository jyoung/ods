FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
LABEL author="Joe Young"
WORKDIR /app

FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /src
COPY ["OutdoorShop.Catalog.Api/OutdoorShop.Catalog.Api.csproj", "OutdoorShop.Catalog.Api/"]
COPY ["OutdoorShop.Catalog.Api.Models/OutdoorShop.Catalog.Api.Models.csproj", "OutdoorShop.Catalog.Api.Models/"]
COPY ["OutdoorShop.Catalog.Domain/OutdoorShop.Catalog.Domain.csproj", "OutdoorShop.Catalog.Domain/"]
RUN dotnet restore "OutdoorShop.Catalog.Api/OutdoorShop.Catalog.Api.csproj"
COPY . .
WORKDIR /src/OutdoorShop.Catalog.Api
RUN dotnet build "OutdoorShop.Catalog.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OutdoorShop.Catalog.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "OutdoorShop.Catalog.Api.dll"]
