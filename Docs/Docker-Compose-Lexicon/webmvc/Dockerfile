#docs.docker.com/engine/examples/dotnetcore/

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./*.sln ./
COPY ./Api/*.csproj ./Api/
COPY ./Business/*.csproj ./Business/
COPY ./Import/*.csproj ./Import/
COPY ./IntegrationTest/*.csproj ./IntegrationTest/
COPY ./Repository/*.csproj ./Repository/
COPY ./UnitTest/*.csproj ./UnitTest/
COPY ./Web/*.csproj ./Web/
RUN dotnet restore

# Copy everything else and build
COPY ./Api/. ./Api/
COPY ./Business/. ./Business/
COPY ./Import/. ./Import/
COPY ./IntegrationTest/. ./IntegrationTest/
COPY ./Repository/. ./Repository/
COPY ./UnitTest/. ./UnitTest/
COPY ./Web/. ./Web/
RUN dotnet publish -c Release -o out

# Unit tests
RUN dotnet test "./UnitTest/UnitTest.csproj" -c Release --no-build --no-restore

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Web/out .
ENTRYPOINT ["dotnet", "Web.dll"]