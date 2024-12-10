# 1. Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# 2. Set the working directory for the build process
WORKDIR /src

# 3. Copy the .csproj file and restore any dependencies (via dotnet restore)
COPY Mds.WebApi.csproj ./
RUN dotnet restore "Mds.WebApi.csproj"

# 4. Copy the rest of the application and build it
COPY . ./
RUN dotnet publish "Mds.WebApi.csproj" -c Release -o /app/publish

# 5. Use the .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# 6. Define entry point for the container to start the app
ENTRYPOINT ["dotnet", "Mds.WebApi.dll"]
