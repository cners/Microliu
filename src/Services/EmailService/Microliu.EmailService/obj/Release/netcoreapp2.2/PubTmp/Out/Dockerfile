FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Services/EmailService/Microliu.EmailService/Microliu.EmailService.API.csproj", "Services/EmailService/Microliu.EmailService/"]
COPY ["Microliu.Core/Microliu.Core.Consul/Microliu.Core.Consul.csproj", "Microliu.Core/Microliu.Core.Consul/"]
COPY ["Services/EmailService/Microliu.EmailService.Application/Microliu.EmailService.Application.csproj", "Services/EmailService/Microliu.EmailService.Application/"]
COPY ["Services/EmailService/Microliu.EmailService.Infrastructure/Microliu.EmailService.Infrastructure.csproj", "Services/EmailService/Microliu.EmailService.Infrastructure/"]
COPY ["Microliu.Core/Microliu.Core.Logger/Microliu.Core.Logger.csproj", "Microliu.Core/Microliu.Core.Logger/"]
COPY ["Microliu.Utils/Microliu.Utils/Microliu.Utils.csproj", "Microliu.Utils/Microliu.Utils/"]
COPY ["Microliu.Core/Microliu.Core.Redis/Microliu.Core.Redis.csproj", "Microliu.Core/Microliu.Core.Redis/"]
COPY ["Services/EmailService/Microliu.EmailService.Data/Microliu.EmailService.Data.csproj", "Services/EmailService/Microliu.EmailService.Data/"]
COPY ["Services/EmailService/Microliu.EmailService.Domain/Microliu.EmailService.Domain.csproj", "Services/EmailService/Microliu.EmailService.Domain/"]
RUN dotnet restore "Services/EmailService/Microliu.EmailService/Microliu.EmailService.API.csproj"
COPY . .
WORKDIR "/src/Services/EmailService/Microliu.EmailService"
RUN dotnet build "Microliu.EmailService.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Microliu.EmailService.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Microliu.EmailService.API.dll"]