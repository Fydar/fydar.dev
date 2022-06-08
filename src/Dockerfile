FROM mcr.microsoft.com/dotnet/aspnet:5.0.2-alpine3.12-arm64v8 AS base
EXPOSE 80
EXPOSE 443

RUN ls

COPY /Portfolio.Application.Instance/bin/Release/net5.0/publish/ /code/
COPY /bin/built-content/content/ /code/content/

RUN ls ./code

WORKDIR /code
ENTRYPOINT ["dotnet", "Portfolio.Application.Instance.dll"]
