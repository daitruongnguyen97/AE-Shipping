#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/Shipping.Api/Shipping.API.csproj", "Shipping.Api/"]
# RUN dotnet restore "Shipping.Api/Shipping.API.csproj"

COPY ["src/Shipping.Application/Shipping.Application.csproj", "Shipping.Application/"]
# RUN dotnet restore "Shipping.Application/Shipping.Application.csproj"

COPY ["src/Shipping.Domain/Shipping.Domain.csproj", "Shipping.Domain/"]
# RUN dotnet restore "Shipping.Domain/Shipping.Domain.csproj"

COPY ["src/Shipping.Infrastructure/Shipping.Infrastructure.csproj", "Shipping.Infrastructure/"]
# RUN dotnet restore "Shipping.Infrastructure/Shipping.Infrastructure.csproj"

COPY /src .

# RUN dotnet build "Shipping.Api/Shipping.API.csproj" -c Release -o /app/build
# RUN dotnet build "Shipping.Infrastructure/Shipping.Infrastructure.csproj" -c Release -o /app/build
# RUN dotnet build "Shipping.Domain/Shipping.Domain.csproj" -c Release -o /app/build
# RUN dotnet build "Shipping.Application/Shipping.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shipping.Api/Shipping.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shipping.API.dll"]