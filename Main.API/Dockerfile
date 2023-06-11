#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Main.API/Main.API.csproj", "Main.API/"]
COPY ["ElectronicVoting.Persistence/ElectronicVoting.Persistence.csproj", "ElectronicVoting.Persistence/"]
COPY ["Main.Domain/Main.Domain.csproj", "Main.Domain/"]
COPY ["ElectronicVoting.Common/ElectronicVoting.Common.csproj", "ElectronicVoting.Common/"]
COPY ["ElectronicVoting.Domain/Validator.Domain.csproj", "ElectronicVoting.Domain/"]
RUN dotnet restore "Main.API/Main.API.csproj"
COPY . .
WORKDIR "/src/Main.API"
RUN dotnet build "Main.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Main.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Main.API.dll"]
