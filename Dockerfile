# Etapa de build con SDK (compila y publica)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos csproj y resto (optimiza cache)
COPY *.sln ./
COPY ApiRestDespliegue/*.csproj ApiRestDespliegue/
RUN dotnet restore

# Copiamos todo y publicamos
COPY . .
RUN dotnet publish ApiRestDespliegue/ApiRestDespliegue.csproj -c Release -o /app --no-restore

# Etapa runtime con imagen ligera
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiamos los artefactos publicados
COPY --from=build /app ./

# Permitir que Kestrel escuche en el puerto interno 10000
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

# Comando para arrancar tu API
ENTRYPOINT ["dotnet", "ApiRestDespliegue.dll"]
