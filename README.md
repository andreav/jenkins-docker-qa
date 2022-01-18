This project is a starter kit for setting up a Jenkins docker installation on Windows and related CI/CD utilities

Main features:

* jenkins folder

    * Dockerfile for building a custom Docker Image (built on top of the official one)
    * Docker command for speeding up try/fail/retry process
    * Configuration by [Configuration as Code](https://https://plugins.jenkins.io/configuration-as-code/) plugin
    * Prepopulated with test jobs (remember to [approve](https://stackoverflow.com/a/45771737/1966366) scripts before runnig them otherwise they'll fail)
    * Test jobs docker:
        * default-agent
        * docker-agent
        * push_image_to_local_registry
        * docker-agent-from-local-registry (first run a local registry)
    * Test jobs sonarqube:
        * 01_build_netcore_sonarscanner_image_and_push_to_local_registry  
          Builds a netcore image with java & sonarscanner already installed and puh it to local registry
        * 02_sonarscanner-in-docker-agent  
          Example of integrating jenkins - netcore - sonarqube   
          Before running this job:
          * In Sonarqube: create a token for a user
          * In jenkins: change credential "sonarqube-token-admin" with the token just created

          After running this job login to sonarqube http://localhost:9000 and you'll see:
          * Static code analysis
          * Code coverage
          * Test results
          
* registry folder

    * running a local (insecure) registry for hosting custom images (used as agent from jenkins)

* sonarqube folder

    * running a sonarqube server
      Plese be sure to meet [Docker Host Requirements](https://hub.docker.com/_/sonarqube)  
        ```
        sysctl -w vm.max_map_count=524288
        sysctl -w fs.file-max=131072
        ulimit -n 131072
        ulimit -u 8192
        ```

    * Dockerfile for creating a sonarqube netcore scanner for using as docker agent 

* selenium folder

    * a docker-compose.yml file for setting up a basic selenium grid
    * a xUnit project for testing the grid

# docker compose

Project ships with a docker compose for bringing up all toghether (jenkins, registry, sonarqube)

    docker compose up --build
    docker compose down -v

