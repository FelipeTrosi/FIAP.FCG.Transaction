# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FIAP.FCG.Contracts/FIAP.FCG.Contracts.csproj", "FIAP.FCG.Contracts/"]
COPY ["FIAP.FCG.Transaction.API/FIAP.FCG.Transaction.API.csproj", "FIAP.FCG.Transaction.API/"]
COPY ["FIAP.FCG.Transaction.Infrastructure/FIAP.FCG.Transaction.Infrastructure.csproj", "FIAP.FCG.Transaction.Infrastructure/"]
COPY ["FIAP.FCG.Transaction.Domain/FIAP.FCG.Transaction.Domain.csproj", "FIAP.FCG.Transaction.Domain/"]
COPY ["FIAP.FCG.Transaction.Service/FIAP.FCG.Transaction.Service.csproj", "FIAP.FCG.Transaction.Service/"]

RUN dotnet restore "FIAP.FCG.Transaction.API/FIAP.FCG.Transaction.API.csproj"

COPY . .

WORKDIR "/src/FIAP.FCG.Transaction.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

RUN addgroup -S appgroup && adduser -S appuser -G appgroup

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:3000
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 3000

HEALTHCHECK --interval=30s --timeout=5s --retries=3 \
  CMD wget -qO- http://localhost:3000/health || exit 1

USER appuser

ENTRYPOINT ["dotnet", "FIAP.FCG.Transaction.API.dll"]
