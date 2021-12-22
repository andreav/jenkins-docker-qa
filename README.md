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

* registry folder

    * running a local (insecure) registry for hosting custom images (used as agent from jenkins)

