FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore "./Gateway.csproj"
RUN dotnet publish "./Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5080
ENV ASPNETCORE_URLS=http://0.0.0.0:5080
ENTRYPOINT ["dotnet", "Gateway.dll"]

