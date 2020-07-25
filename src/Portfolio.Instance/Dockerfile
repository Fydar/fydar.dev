FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
EXPOSE 80
EXPOSE 443

COPY /bin/Release/netcoreapp3.1/publish/ /code/
WORKDIR /code
ENTRYPOINT ["dotnet", "Portfolio.Instance.dll"]
