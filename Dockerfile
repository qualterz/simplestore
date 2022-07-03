FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

EXPOSE 80

WORKDIR /app

# Copy everything
COPY . ./

# Build and publish a release
RUN dotnet publish ./SimpleStore.Web -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/SimpleStore.Web

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SimpleStore.Web.dll"]