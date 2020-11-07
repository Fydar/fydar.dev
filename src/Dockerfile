FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.7-alpine3.11-arm64v8 AS base
EXPOSE 80
EXPOSE 443

RUN ls

COPY /Portfolio.Instance/bin/Release/netcoreapp3.1/publish/ /code/
COPY /Portfolio.Pipeline.BuildStep/bin/Debug/netcoreapp3.1/output/content/ /code/content

RUN ls ./code

WORKDIR /code
ENTRYPOINT ["dotnet", "Portfolio.Instance.dll"]
