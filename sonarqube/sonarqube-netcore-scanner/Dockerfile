FROM mcr.microsoft.com/dotnet/sdk:5.0

RUN apt-get update && apt-get install -y default-jre

ENV DOTNET_CLI_HOME="/tmp/DOTNET_CLI_HOME"
ENV PATH="${PATH}:${DOTNET_CLI_HOME}/.dotnet/tools"

RUN dotnet tool install --global dotnet-sonarscanner

