FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8090

# Use the official ASP.NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/services/auth/WebApi/WebApi.csproj", "src/services/auth/WebApi/"]
RUN dotnet restore "src/services/auth/WebApi/WebApi.csproj"
#COPY ["WebApi/WebApi.csproj", "WebApi/"]
#COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
#COPY ["Core/Core.csproj", "Core/"]
#RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/src/services/auth/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

# Copy the build app to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
