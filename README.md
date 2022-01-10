This project is a starter kit for setting up a Jenkins docker installation on Windows and related CI/CD utilities

Main features:

* jenkins folder

    * Dockerfile for building a custom Docker Image (built on top of the official one)
    * Docker command for speeding up try/fail/retry process
    * Configuration by [Configuration as Code](https://https://plugins.jenkins.io/configuration-as-code/) plugin
    * Prepopulated with test jobs (remember to [approve](https://stackoverflow.com/a/45771737/1966366) scripts before runnig them otherwise they'll fail)
    * Test jobs:
        * default-agent
        * docker-agent
        * push_image_to_local_registry
        * docker-agent-from-local-registry (first run a local registry)
        * sonarscanner-in-docker-agent
        Before running this job please follow these steps:
          * Login to sonarqube server and create a project
          * On jenkins, into the job pipeline, change the variables:  SONARQUBE_PROJECT_NAME and SONARQUBE_LOGIN according to values from previuos step
          * After you build the job, you will see sonaqube results appear in sonarqube dashboard 

* registry folder

    * running a local (insecure) registry for hosting custom images (used as agent from jenkins)

* sonarqube folder

    * running a sonarqube server
    * Dockerfile for creating a sonarqube netcore scanner for using as docker agent 

# docker compose

Project ships with a docker compose for bringing up all toghether (jenkins, registry, sonarqube)

    docker compose up --build
    docker compose down -v

