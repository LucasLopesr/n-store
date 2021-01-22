#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/services/NStore.Pedido.API/NStore.Pedido.API.csproj", "src/services/NStore.Pedido.API/"]
RUN dotnet restore "src/services/NStore.Pedido.API/NStore.Pedido.API.csproj"
COPY . .
WORKDIR "/src/src/services/NStore.Pedido.API"
RUN dotnet build "NStore.Pedido.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NStore.Pedido.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NStore.Pedido.API.dll"]