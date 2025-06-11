# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "./OnderhoudsbuddyWeb/OnderhoudsbuddyWeb.csproj"
RUN dotnet publish "./OnderhoudsbuddyWeb/OnderhoudsbuddyWeb.csproj" -c Release -o /app/out

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "OnderhoudsbuddyWeb.dll"]
