FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/TodoList/Interface/Microscope.Boilerplate.Services.TodoList.Api/Microscope.Boilerplate.Services.TodoList.Api.csproj", "src/Services/TodoList/Interface/Microscope.Boilerplate.Services.TodoList.Api/"]
RUN dotnet restore "src/Services/TodoList/Interface/Microscope.Boilerplate.Services.TodoList.Api/Microscope.Boilerplate.Services.TodoList.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/TodoList/Interface/Microscope.Boilerplate.Services.TodoList.Api"
RUN dotnet build "Microscope.Boilerplate.Services.TodoList.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microscope.Boilerplate.Services.TodoList.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microscope.Boilerplate.Services.TodoList.Api.dll"]
