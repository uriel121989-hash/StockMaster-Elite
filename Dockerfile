# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["StockMaster.Web/StockMaster.Web.csproj", "StockMaster.Web/"]
COPY ["StockMaster.Negocio/StockMaster.Negocio.csproj", "StockMaster.Negocio/"]
COPY ["StockMaster.Data/StockMaster.Data.csproj", "StockMaster.Data/"]
COPY ["StockMaster.Entidades/StockMaster.Entidades.csproj", "StockMaster.Entidades/"]

RUN dotnet restore "StockMaster.Web/StockMaster.Web.csproj"

# Copiar todo el código y compilar
COPY . .
WORKDIR "/src/StockMaster.Web"
RUN dotnet build "StockMaster.Web.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "StockMaster.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "StockMaster.Web.dll"]
