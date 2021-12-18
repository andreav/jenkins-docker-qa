# Manifest

This project is an how-to for setting up a Jenkins docker installation on Windows.

Main features:
* Docekrfile for build a custom Docker Image (built on top of the official one)
* Docker command for speeding up try/fail/retry process
* Configuration by Jenkins [Configuration as Code](https://https://plugins.jenkins.io/configuration-as-code/)
* Prepopulated with test jobs (remmber to [approve](https://stackoverflow.com/a/45771737/1966366) scripts before runnig them otherwise they'll fail)
* Test job for spinning a docker agent

# Shell

Do not use bash on windows!  
Paths starting with / are misinterpreted and expand to C:/Program Files  
You will get strange path errors.  
Use Powershell.

# Customize immage
    
    docker build -t jenkins-plug .

    docker network create jenkins

    docker run --name jenkins-docker --rm --detach --privileged --network jenkins --network-alias docker --env DOCKER_TLS_CERTDIR=/certs --volume jenkins-docker-certs:/certs/client --volume jenkins-data:/var/jenkins_home docker:dind

    docker run --name jenkins-plug --rm --detach --network jenkins --env DOCKER_HOST=tcp://docker:2376 --env DOCKER_CERT_PATH=/certs/client --env DOCKER_TLS_VERIFY=1 --volume jenkins-data:/var/jenkins_home --volume jenkins-docker-certs:/certs/client:ro --publish 8080:8080 --publish 50000:50000 jenkins-plug

    # Add these lines to customize admin user (default is admin/admin)
    --env JENKINS_ADMIN_ID=admin --env JENKINS_ADMIN_PASSWORD=admin_paswd

# Backup volume

    docker cp jenkins-plug:/var/jenkins_home dst_folder

# Clean

    Attention: Read every command before runnig, they are seriously destructive!

    docker stop $(docker ps -aq)
    docker rm $(docker ps -aq)
    docker network prune -f
    docker rmi -f $(docker images --filter dangling=true -qa)
    docker volume rm $(docker volume ls --filter dangling=true -q)
    
    docker rmi -f $(docker images -qa)

# Extract plugin list from running jenkins to populate plugins.txt

    $ JENKINS_HOST=admin:admin@localhost:8080
    $ curl -sSL "http://$JENKINS_HOST/pluginManager/api/xml?depth=1&xpath=/*/*/shortName|/*/*/version&wrapper=plugins" | perl -pe 's/.*?<shortName>([\w-]+).*?<version>([^<]+)()(<\/\w+>)+/\1 \2\n/g'|sed 's/ /:/'
   

# Login

    # From powershell, not working from bash (must use 2 leading slashes from bash)
    docker exec -it jenkins-lts cat /var/jenkins_home/secrets/initialAdminPassword

    login User: admin
    login passowrd: output from previous command

# refs

[Jenkins Official documentation](https://www.jenkins.io/doc/book/installing/docker/)  
[Digital Ocean Casc tutorial](https://www.digitalocean.com/community/tutorials/how-to-automate-jenkins-setup-with-docker-and-jenkins-configuration-as-code)  
[Job DSL plugin API reference](https://jenkinsci.github.io/job-dsl-plugin/#path/job)  
[Digital Ocean Casc Tutorial seed jobs](https://www.digitalocean.com/community/tutorials/how-to-automate-jenkins-job-configuration-using-job-dsl)  
[JCasc examples](https://github.com/jenkinsci/configuration-as-code-plugin/tree/master/demos)  
