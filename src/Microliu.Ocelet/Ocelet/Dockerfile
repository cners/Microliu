FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Ocelet/Microliu.Ocelet.csproj", "Ocelet/"]
RUN dotnet restore "Ocelet/Microliu.Ocelet.csproj"
COPY . .
WORKDIR "/src/Ocelet"
RUN dotnet build "Microliu.Ocelet.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Microliu.Ocelet.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Microliu.Ocelet.dll"]