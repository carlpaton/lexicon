#docs.docker.com/engine/examples/dotnetcore/

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./tmpwebmvc/*.sln ./
COPY ./tmpwebmvc/Api/*.csproj ./Api/
COPY ./tmpwebmvc/Business/*.csproj ./Business/
COPY ./tmpwebmvc/Import/*.csproj ./Import/
COPY ./tmpwebmvc/IntegrationTest/*.csproj ./IntegrationTest/
COPY ./tmpwebmvc/Repository/*.csproj ./Repository/
COPY ./tmpwebmvc/UnitTest/*.csproj ./UnitTest/
COPY ./tmpwebmvc/Web/*.csproj ./Web/
RUN dotnet restore

# Copy everything else and build
COPY ./tmpwebmvc/Api/. ./Api/
COPY ./tmpwebmvc/Business/. ./Business/
COPY ./tmpwebmvc/Import/. ./Import/
COPY ./tmpwebmvc/IntegrationTest/. ./IntegrationTest/
COPY ./tmpwebmvc/Repository/. ./Repository/
COPY ./tmpwebmvc/UnitTest/. ./UnitTest/
COPY ./tmpwebmvc/Web/. ./Web/
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Web/out .
ENTRYPOINT ["dotnet", "Web.dll"]