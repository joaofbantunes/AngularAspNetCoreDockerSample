#TODO: switch to alpine images when available

FROM microsoft/aspnetcore-build:2.0 AS builder
WORKDIR /app

# Copy solution and restore as distinct layers to cache dependencies
# TODO: Check if this can be done in a better way than copying every csproj like this
COPY ./src/CodingMilitia.AngularAspNetCoreDockerSample.Data/*.csproj ./src/CodingMilitia.AngularAspNetCoreDockerSample.Data/
COPY ./src/CodingMilitia.AngularAspNetCoreDockerSample.WebApi/*.csproj ./src/CodingMilitia.AngularAspNetCoreDockerSample.WebApi/
COPY *.sln ./
RUN dotnet restore

# Publish the WebApi
COPY . ./
WORKDIR /app/src/CodingMilitia.AngularAspNetCoreDockerSample.WebApi
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=builder /app/src/CodingMilitia.AngularAspNetCoreDockerSample.WebApi/out .
ENTRYPOINT ["dotnet", "CodingMilitia.AngularAspNetCoreDockerSample.WebApi.dll"]