# This versione uses Docker in Docker as per docuemntation.
# We can avoid exposing daemon on tcp://localhost:2375 without TLS

docker build -t jenkins-plug ..

docker network create jenkins

docker run --name jenkins-docker --rm --detach --privileged --network jenkins --network-alias docker --env DOCKER_TLS_CERTDIR=/certs --volume jenkins-docker-certs:/certs/client --volume jenkins-data:/var/jenkins_home docker:dind

docker run --name jenkins-plug --rm --detach --network jenkins --env DOCKER_HOST=tcp://docker:2376 --env DOCKER_CERT_PATH=/certs/client --env DOCKER_TLS_VERIFY=1 --volume jenkins-data:/var/jenkins_home --volume jenkins-docker-certs:/certs/client:ro --publish 8080:8080 --publish 50000:50000 jenkins-plug

# Add these lines to customize admin user (default is admin/admin)
# --env JENKINS_ADMIN_ID=admin --env JENKINS_ADMIN_PASSWORD=admin_paswd
