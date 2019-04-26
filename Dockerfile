FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out ./SnakeCore.Web/*.csproj

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SnakeCore.Web.dll"]