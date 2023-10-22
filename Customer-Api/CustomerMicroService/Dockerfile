#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CustomerMicroService/CustomerMicroService.API.csproj", "CustomerMicroService/"]
COPY ["CustomerMicroService.Application/CustomerMicroService.Application.csproj", "CustomerMicroService.Application/"]
COPY ["CustomerMicroService.Core/CustomerMicroService.Framework.csproj", "CustomerMicroService.Core/"]
COPY ["CustomerMicroService.Data/CustomerMicroService.domain.csproj", "CustomerMicroService.domain/"]
COPY ["CustomerMicroService.Data/CustomerMicroService.Data.csproj", "CustomerMicroService.Data/"]
RUN dotnet restore "CustomerMicroService/CustomerMicroService.API.csproj"
COPY . .
WORKDIR "/src/CustomerMicroService"
RUN dotnet build "CustomerMicroService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerMicroService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerMicroService.API.dll"]