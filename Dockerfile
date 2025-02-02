# Stage 1: Build the .NET app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY casa-dana-api.csproj ./
RUN dotnet restore

# Copy remaining files and build the app
COPY . . 
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run the app using the .NET 9 runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy built app from previous stage
COPY --from=build /app/publish .

# Set the entrypoint to run the application
CMD ["dotnet", "casa-dana-api.dll"]
