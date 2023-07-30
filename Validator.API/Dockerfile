#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Validator.API/Validator.API.csproj", "Validator.API/"]
COPY ["Validator.Infrastructure/Validator.Infrastructure.csproj", "Validator.Infrastructure/"]
COPY ["ElectronicVoting.Persistence/ElectronicVoting.Persistence.csproj", "ElectronicVoting.Persistence/"]
COPY ["Validator.Domain/Validator.Domain.csproj", "Validator.Domain/"]
RUN dotnet restore "Validator.API/Validator.API.csproj"
COPY . .
WORKDIR "/src/Validator.API"
RUN dotnet build "Validator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Validator.API.csproj" -c Release -o /app/publish
RUN dotnet dev-certs https
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Validator.API.dll"]