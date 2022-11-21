#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Recipes.Web/Recipes.Web.csproj", "Recipes.Web/"]
COPY ["Recipes.Infrustructure/Recipes.Infrustructure.csproj", "Recipes.Infrustructure/"]
COPY ["Recipes.Application/Recipes.Application.csproj", "Recipes.Application/"]
COPY ["Recipes.Contracts/Recipes.Contracts.csproj", "Recipes.Contracts/"]
COPY ["Recipes.Domain/Recipes.Domain.csproj", "Recipes.Domain/"]
COPY ["Recipes.Persistance/Recipes.Persistance.csproj", "Recipes.Persistance/"]
COPY ["Recipes.Shared/Recipes.Shared.csproj", "Recipes.Shared/"]
RUN dotnet restore "Recipes.Web/Recipes.Web.csproj"
COPY . .
WORKDIR "/src/Recipes.Web"
RUN dotnet build "Recipes.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recipes.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recipes.Web.dll"]