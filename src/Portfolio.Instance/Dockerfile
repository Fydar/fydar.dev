#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Portfolio.Instance/Portfolio.Instance.csproj", "Portfolio.Instance/"]
RUN dotnet restore "Portfolio.Instance/Portfolio.Instance.csproj"
COPY . .
WORKDIR "/src/Portfolio.Instance"
RUN dotnet build "Portfolio.Instance.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portfolio.Instance.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Instance.dll"]
