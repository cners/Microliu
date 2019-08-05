FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app


COPY . .
EXPOSE 80

ENTRYPOINT ["dotnet", "Microliu.BizLogger.dll"]