# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "DSTN.WebAPI/DSTN.WebAPI.csproj"
RUN dotnet publish "DSTN.WebAPI/DSTN.WebAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# Create database directory for SQLite
RUN mkdir -p /app/Database
COPY --from=build /app/publish .
# Expose port 8080 for HTTP traffic
EXPOSE 8080
# Set environment variable for ASP.NET Core to listen on port 8080
ENV ASPNETCORE_URLS="http://+:8080"
# Set environment variable for SQLite connection string
ENV ConnectionStrings__DefaultConnection="Data Source=/app/Database/app.db"
# Start the application
ENTRYPOINT ["dotnet", "DSTN.WebAPI.dll"]