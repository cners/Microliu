FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Microliu.SMS/Microliu.SMS.csproj", "Microliu.SMS/"]
RUN dotnet restore "Microliu.SMS/Microliu.SMS.csproj"
COPY . .
WORKDIR "/src/Microliu.SMS"
RUN dotnet build "Microliu.SMS.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Microliu.SMS.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Microliu.SMS.dll"]