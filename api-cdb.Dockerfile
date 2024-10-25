FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./back/ApiCdb ./
RUN dotnet publish "ApiCdb.csproj" -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
RUN apt-get update && apt-get install -y curl
ENTRYPOINT ["dotnet", "ApiCdb.dll"]
EXPOSE 5163