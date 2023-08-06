FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Clients/BFF/Microscope.Boilerplate.Clients.BFF/Microscope.Boilerplate.Clients.BFF.csproj", "src/Clients/BFF/Microscope.Boilerplate.Clients.BFF/"]
RUN dotnet restore "src/Clients/BFF/Microscope.Boilerplate.Clients.BFF/Microscope.Boilerplate.Clients.BFF.csproj"
COPY . .
WORKDIR "/src/src/Clients/BFF/Microscope.Boilerplate.Clients.BFF"
RUN dotnet build "Microscope.Boilerplate.Clients.BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.Boilerplate.Clients.BFF.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microscope.Boilerplate.Clients.BFF.dll"]
