FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-stretch AS build
WORKDIR /src
COPY ["Superdigital/Superdigital.csproj", "Superdigital/"]
RUN dotnet restore "Superdigital/Superdigital.csproj"
COPY . .
WORKDIR "/src/Superdigital"
RUN dotnet build "Superdigital.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Superdigital.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Superdigital.dll"]