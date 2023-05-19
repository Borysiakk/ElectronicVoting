#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ElectronicVoting.API/ElectronicVoting.API.csproj", "ElectronicVoting.API/"]
COPY ["ElectronicVoting.Persistence/ElectronicVoting.Persistence.csproj", "ElectronicVoting.Persistence/"]
COPY ["ElectronicVoting.Domain/ElectronicVoting.Domain.csproj", "ElectronicVoting.Domain/"]
RUN dotnet restore "ElectronicVoting.API/ElectronicVoting.API.csproj"
COPY . .
WORKDIR "/src/ElectronicVoting.API"
RUN dotnet build "ElectronicVoting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElectronicVoting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronicVoting.API.dll"]
