#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["test/DotNet.Factory.Generic.DataLayer.WebApi/DotNet.Factory.Generic.DataLayer.WebApi.csproj", "test/DotNet.Factory.Generic.DataLayer.WebApi/"]
COPY ["src/DotNet.Factory.Generic.DataLayer/DotNet.Factory.Generic.DataLayer.csproj", "src/DotNet.Factory.Generic.DataLayer/"]
RUN dotnet restore "test/DotNet.Factory.Generic.DataLayer.WebApi/DotNet.Factory.Generic.DataLayer.WebApi.csproj"
COPY . .
WORKDIR "/src/test/DotNet.Factory.Generic.DataLayer.WebApi"
RUN dotnet build "DotNet.Factory.Generic.DataLayer.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNet.Factory.Generic.DataLayer.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNet.Factory.Generic.DataLayer.WebApi.dll"]