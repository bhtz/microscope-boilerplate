FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BuildingBlocks/ReverseProxy/Mylight.BuildingBlocks.ReverseProxy/Mylight.BuildingBlocks.ReverseProxy.csproj", "src/BuildingBlocks/ReverseProxy/Mylight.BuildingBlocks.ReverseProxy/"]
RUN dotnet restore "src/BuildingBlocks/ReverseProxy/Mylight.BuildingBlocks.ReverseProxy/Mylight.BuildingBlocks.ReverseProxy.csproj"
COPY . .
WORKDIR "/src/src/BuildingBlocks/ReverseProxy/Mylight.BuildingBlocks.ReverseProxy"
RUN dotnet build "Mylight.BuildingBlocks.ReverseProxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mylight.BuildingBlocks.ReverseProxy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mylight.BuildingBlocks.ReverseProxy.dll"]
