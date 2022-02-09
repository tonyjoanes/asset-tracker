#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/WorkItems/WorkItems.API/WorkItems.API.csproj", "Services/WorkItems/WorkItems.API/"]
RUN dotnet restore "Services/WorkItems/WorkItems.API/WorkItems.API.csproj"
COPY . .
WORKDIR "/src/Services/WorkItems/WorkItems.API"
RUN dotnet build "WorkItems.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkItems.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkItems.API.dll"]