FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Microliu.EmailService/Microliu.EmailService.csproj", "Microliu.EmailService/"]
RUN dotnet restore "Microliu.EmailService/Microliu.EmailService.csproj"
COPY . .
WORKDIR "/src/Microliu.EmailService"
RUN dotnet build "Microliu.EmailService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Microliu.EmailService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Microliu.EmailService.dll"]