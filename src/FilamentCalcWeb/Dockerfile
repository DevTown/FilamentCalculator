FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy only project files first for better caching
COPY *.csproj .
RUN dotnet restore

# Copy source code
COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Install culture support if needed
RUN apt-get update && apt-get install -y locales && rm -rf /var/lib/apt/lists/*

# Create non-root user
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "FilamentCalculator.dll"]