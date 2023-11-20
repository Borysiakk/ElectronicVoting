#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Validator.API/Validator.API.csproj", "Validator.API/"]
RUN dotnet restore "Validator.API/Validator.API.csproj"
COPY . .
WORKDIR "/src/Validator.API"
RUN dotnet build "Validator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Validator.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT ["dotnet", "Validator.API.dll"]