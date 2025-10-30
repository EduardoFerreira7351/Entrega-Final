FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/FastPoint.API/FastPoint.API.csproj", "src/FastPoint.API/"]
COPY ["src/FastPoint.Application/FastPoint.Application.csproj", "src/FastPoint.Application/"]
COPY ["src/FastPoint.Domain/FastPoint.Domain.csproj", "src/FastPoint.Domain/"]
COPY ["src/FastPoint.Infrastructure/FastPoint.Infrastructure.csproj", "src/FastPoint.Infrastructure/"]
RUN dotnet restore "src/FastPoint.API/FastPoint.API.csproj"
COPY . .
WORKDIR "/src/src/FastPoint.API"
RUN dotnet build "FastPoint.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastPoint.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastPoint.API.dll"]
