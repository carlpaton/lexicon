FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./tmpwebmvc/*.sln ./
COPY ./tmpwebmvc/Api/*.csproj ./Api/
COPY ./tmpwebmvc/Import/*.csproj ./Import/
COPY ./tmpwebmvc/IntegrationTest/*.csproj ./IntegrationTest/
COPY ./tmpwebmvc/Web/*.csproj ./Web/
COPY ./tmpwebmvc/UnitTest/*.csproj ./UnitTest/
COPY ./tmpwebmvc/Repository/*.csproj ./Repository/
COPY ./tmpwebmvc/Business/*.csproj ./Business/
RUN dotnet restore

# Copy everything else and build
COPY ./tmpwebmvc/Web/. ./Web/
COPY ./tmpwebmvc/Api/. ./Api/
COPY ./tmpwebmvc/UnitTest/. ./UnitTest/
COPY ./tmpwebmvc/Repository/. ./Repository/
COPY ./tmpwebmvc/Business/. ./Business/
COPY ./tmpwebmvc/IntegrationTest/. ./IntegrationTest/
COPY ./tmpwebmvc/Import/. ./Import/
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Web.dll"]