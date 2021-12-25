FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY requestapp.csproj .
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR "/src/."
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
COPY --from=build /app ./

RUN apt-get update && apt-get install -y tcpdump && apt-get install -y curl && \
    cd /root && curl -JLO https://aka.ms/dotnet-trace/linux-x64 && chmod +x ./dotnet-trace

ENTRYPOINT ["dotnet", "requestapp.dll"]
