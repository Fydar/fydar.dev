FROM mcr.microsoft.com/dotnet/aspnet:5.0.2-alpine3.12-arm64v8 AS base
EXPOSE 80
EXPOSE 443

RUN ls

COPY /Portfolio.Instance/bin/Release/net5.0/publish/ /code/
COPY /Portfolio.Pipeline.BuildStep/bin/Debug/net5.0/output/content/ /code/content

RUN ls ./code

WORKDIR /code
ENTRYPOINT ["dotnet", "Portfolio.Instance.dll"]
