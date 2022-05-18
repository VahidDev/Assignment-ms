FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Assignment/Assignment.csproj", "Assignment/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["DomainModels/DomainModels.csproj", "DomainModels/"]
RUN dotnet restore "Assignment/Assignment.csproj"
COPY . .
WORKDIR "/src/Assignment"
RUN dotnet build "Assignment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assignment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Assignment.dll