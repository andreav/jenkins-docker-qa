# This versione needs exposing daemon on tcp://localhost:2375 without TLS
# Assure firewall does not allows inbound traffic on that port

docker build -t jenkins-plug .

docker network create jenkins

docker run --name jenkins-plug --rm --detach --network jenkins --env DOCKER_HOST=tcp://host.docker.internal:2375 --volume jenkins-data:/var/jenkins_home --volume jenkins-docker-certs:/certs/client:ro --publish 8080:8080 --publish 50000:50000 jenkins-plug

# Add these lines to customize admin user (default is admin/admin)
# --env JENKINS_ADMIN_ID=admin --env JENKINS_ADMIN_PASSWORD=admin_paswd
