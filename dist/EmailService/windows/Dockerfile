FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

COPY ./ ./



ENTRYPOINT ["dotnet", "Microliu.EmailService.API.dll"]