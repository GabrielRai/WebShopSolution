FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env
WORKDIR /App

# Copy everything
COPY WebShopSolution ./
COPY Repository/Repository.csproj Repository/
COPY WebShopTests/WebShopTests.csproj WebShopTests/
COPY WebShop/WebShop.csproj WebShop/

# Restore as distinct layers
RUN dotnet restore WebShopSolution.sln

COPY . ./

RUN dotnet test WebShopTests/WebShopTests.csproj --no-build --verbosity normal

# Build and publish a release
RUN dotnet publish WebShop/WebShop.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
WORKDIR /App
COPY --from=build-env /App/out .

# Expose port and define entry point
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "WebShop.dll"]