# Shell

Sadly, can't not use bash on windows (at leat my installation)   
Paths starting with / are misinterpreted and expand to C:/Program Files  
You will get strange path errors.  
Use Powershell.

# Build and run customized immage

Directly exposing docker daemon on tcp://localhost:2375 without TLS

    .\build-run-jenkins.ps1

Using docker in docker.  
First replace build-run-jenkins.ps1 with build-run-jenkins-with-dind.ps1, then

    .\build-run-jenkins-with-dind.ps1

# Backup volume

    docker cp jenkins-plug:/var/jenkins_home dst_folder

# Clean

Similarmly to previous step (with or withoud docker in docker)

    .\script\stop-destroy-jenkins.ps1

Using docker in docker

     .\script\stop-destroy-jenkins-with-dind.ps1
# Extract plugin list from running jenkins to populate plugins.txt

    $ JENKINS_HOST=admin:admin@localhost:8080
    $ curl -sSL "http://$JENKINS_HOST/pluginManager/api/xml?depth=1&xpath=/*/*/shortName|/*/*/version&wrapper=plugins" | perl -pe 's/.*?<shortName>([\w-]+).*?<version>([^<]+)()(<\/\w+>)+/\1 \2\n/g'|sed 's/ /:/'
   

# Login

    # From powershell, not working from bash (must use 2 leading slashes from bash)
    docker exec -it jenkins-plug cat /var/jenkins_home/secrets/initialAdminPassword

    login User: admin
    login passowrd: output from previous command

# refs

[Jenkins Official documentation](https://www.jenkins.io/doc/book/installing/docker/)  
[Digital Ocean Casc tutorial](https://www.digitalocean.com/community/tutorials/how-to-automate-jenkins-setup-with-docker-and-jenkins-configuration-as-code)  
[Job DSL plugin API reference](https://jenkinsci.github.io/job-dsl-plugin/#path/job)  
[Digital Ocean Casc Tutorial seed jobs](https://www.digitalocean.com/community/tutorials/how-to-automate-jenkins-job-configuration-using-job-dsl)  
[JCasc examples](https://github.com/jenkinsci/configuration-as-code-plugin/tree/master/demos)  
