# Etapa base: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3000
ENV ASPNETCORE_HTTP_PORTS=3000

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os .csproj primeiro
COPY FIAP.FCG.Transaction.API/FIAP.FCG.Transaction.API.csproj FIAP.FCG.Transaction.API/
COPY FIAP.FCG.Transaction.Domain/FIAP.FCG.Transaction.Domain.csproj FIAP.FCG.Transaction.Domain/
COPY FIAP.FCG.Transaction.Infrastructure/FIAP.FCG.Transaction.Infrastructure.csproj FIAP.FCG.Transaction.Infrastructure/
COPY FIAP.FCG.Transaction.Service/FIAP.FCG.Transaction.Service.csproj FIAP.FCG.Transaction.Service/

# Restaurar dependências
RUN dotnet restore FIAP.FCG.Transaction.API/FIAP.FCG.Transaction.API.csproj
RUN dotnet restore FIAP.FCG.Transaction.Infrastructure/FIAP.FCG.Transaction.Infrastructure.csproj 

# Copiar o restante dos arquivos
COPY FIAP.FCG.Transaction.API/ FIAP.FCG.Transaction.API/
COPY FIAP.FCG.Transaction.Domain/ FIAP.FCG.Transaction.Domain/
COPY FIAP.FCG.Transaction.Infrastructure/ FIAP.FCG.Transaction.Infrastructure/
COPY FIAP.FCG.Transaction.Service/ FIAP.FCG.Transaction.Service/

# Build
WORKDIR /src/FIAP.FCG.Transaction.API
RUN dotnet build FIAP.FCG.Transaction.API.csproj -c Release -o /app/build

# Publish
FROM build AS publish
WORKDIR /src/FIAP.FCG.Transaction.API
RUN dotnet publish FIAP.FCG.Transaction.API.csproj -c Release -o /app/publish
FROM base AS final
WORKDIR /app

# Instalar Datadog .NET Tracer 
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl ca-certificates \
    && curl -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v3.6.0/datadog-dotnet-apm_3.6.0_amd64.deb \
    && dpkg -i ./datadog-dotnet-apm_3.6.0_amd64.deb \
    && rm ./datadog-dotnet-apm_3.6.0_amd64.deb \
    && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

ENV CORECLR_ENABLE_PROFILING=1 \
    CORECLR_PROFILER="{846F5F1C-F9AE-4B07-969E-05C26BC060D8}" \
    CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so \
    DD_DOTNET_TRACER_HOME=/opt/datadog \
    DD_LOGS_INJECTION=true

ENTRYPOINT ["dotnet", "FIAP.FCG.Transaction.API.dll"]
