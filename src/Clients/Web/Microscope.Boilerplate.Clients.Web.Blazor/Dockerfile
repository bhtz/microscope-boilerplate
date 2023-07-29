FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Clients/Web/Microscope.Boilerplate.Clients.Web.Blazor/Microscope.Boilerplate.Clients.Web.Blazor.csproj", "src/Clients/Web/Microscope.Boilerplate.Clients.Web.Blazor/"]
RUN dotnet restore "src/Clients/Web/Microscope.Boilerplate.Clients.Web.Blazor/Microscope.Boilerplate.Clients.Web.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Clients/Web/Microscope.Boilerplate.Clients.Web.Blazor"
RUN dotnet build "Microscope.Boilerplate.Clients.Web.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.Boilerplate.Clients.Web.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microscope.Boilerplate.Clients.Web.Blazor.dll"]
