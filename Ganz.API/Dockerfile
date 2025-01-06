# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ganz.API/Ganz.API.csproj", "Ganz.API/"]
COPY ["Ganz.Application/Ganz.Application.csproj", "Ganz.Application/"]
COPY ["Ganz.Domain/Ganz.Domain.csproj", "Ganz.Domain/"]
COPY ["Ganz.Infrastructure/Ganz.Infrastructure.csproj", "Ganz.Infrastructure/"]
RUN dotnet restore "./Ganz.API/Ganz.API.csproj"
COPY . .
WORKDIR "/src/Ganz.API"
RUN dotnet build "./Ganz.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ganz.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ganz.API.dll"]