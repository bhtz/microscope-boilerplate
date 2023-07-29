FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Clients/CLI/Microscope.Boilerplate.Clients.CLI/Microscope.Boilerplate.Clients.CLI.csproj", "src/Clients/CLI/Microscope.Boilerplate.Clients.CLI/"]
RUN dotnet restore "src/Clients/CLI/Microscope.Boilerplate.Clients.CLI/Microscope.Boilerplate.Clients.CLI.csproj"
COPY . .
WORKDIR "/src/src/Clients/CLI/Microscope.Boilerplate.Clients.CLI"
RUN dotnet build "Microscope.Boilerplate.Clients.CLI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.Boilerplate.Clients.CLI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microscope.Boilerplate.Clients.CLI.dll"]
