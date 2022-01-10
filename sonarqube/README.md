# Start Server

refs - https://docs.sonarqube.org/latest/setup/get-started-2-minutes/

    ./run-sonarqube.ps1

    http://localhost:9000

    user: admin
    pasw: admin

# Build and push netcore sonarqube scanner

You can build your own image, push it to the local registry and use it as agent as in the job: docker-agent-from-local-registry

From the root of this repository:

    docker build -t localhost:5000/netcore-sonarscanner:5.0 .\sonarqube\sonarqube-netcore-scanner
    docker push localhost:5000/netcore-sonarscanner:5.0
    curl http://localhost:5000/v2/_catalog

# Test it manually

    docker run --rm -it mcr.microsoft.com/dotnet/sdk:5.0 bash

    export DOTNET_CLI_HOME="/tmp/DOTNET_CLI_HOME"
    export PATH="$PATH:/tmp/DOTNET_CLI_HOME/.dotnet/tools"

    dotnet tool install --global dotnet-sonarscanner
    dotnet sonarscanner begin /k:"tesproject" /d:sonar.host.url="http://host.docker.internal:9000"  /d:sonar.login="7aa774b870de5d12f3677dc28a154ffedfc2925b"

    dotnet sonarscanner end /d:sonar.login="7aa774b870de5d12f3677dc28a154ffedfc2925b"