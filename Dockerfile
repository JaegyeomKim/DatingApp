# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . ./
RUN dotnet restore "./DatingAppAPI/DatingAppAPI/DatingAppAPI.csproj" --disable-parallel
RUN dotnet build "./DatingAppAPI/DatingAppAPI/DatingAppAPI.csproj" -c release -o /app --no-restore
RUN dotnet publish "./DatingAppAPI/DatingAppAPI/DatingAppAPI.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "DatingAppAPI.dll"]
