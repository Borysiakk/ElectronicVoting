FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build


WORKDIR /src
COPY ["ElectronicVoting.Main/ElectronicVoting.Main.csproj", "ElectronicVoting.Main/"]
RUN dotnet restore "ElectronicVoting.Main/ElectronicVoting.Main.csproj"
COPY . .

WORKDIR "/src/ElectronicVoting.Persistence"
RUN rm appsettings.json

WORKDIR "/src/ElectronicVoting.Main"
RUN dotnet build "ElectronicVoting.Main.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "ElectronicVoting.Main.csproj" -c Release -o /app/publish


FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronicVoting.Main.dll"]
