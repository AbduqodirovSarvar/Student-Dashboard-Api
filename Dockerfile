#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Student-Dashboard-Api.csproj", "."]
RUN dotnet restore "./././Student-Dashboard-Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Student-Dashboard-Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Student-Dashboard-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Add environment variables
ENV ENVIRONMENT=production
ENV CONNECTION_STRING="Data Source=/app/Student.db"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Student-Dashboard-Api.dll"]