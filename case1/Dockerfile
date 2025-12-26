FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["case1/case1.csproj", "case1/"]
RUN dotnet restore "case1/case1.csproj"

COPY . .
WORKDIR /src/case1
RUN dotnet publish "case1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrations
WORKDIR /src

COPY ["case1/case1.csproj", "case1/"]
RUN dotnet restore "case1/case1.csproj"

COPY . .
WORKDIR /src/case1

RUN dotnet tool install --global dotnet-ef --version 8.*
# ENV PATH="/root/.dotnet/tools:${PATH}" # Bu satırı kaldırın, artık ihtiyacımız yok.

# ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "case1.csproj"] # Bu satırı kaldırın

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "case1.dll"]