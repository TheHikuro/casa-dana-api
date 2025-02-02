# Use the official .NET 9 SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy everything and restore dependencies
COPY . . 
RUN dotnet restore

# Build and publish the project
RUN dotnet publish -c Release -o /publish

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /publish .
CMD ["dotnet", "CasaDanaAPI.dll"]
