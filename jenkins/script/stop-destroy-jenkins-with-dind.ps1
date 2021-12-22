docker stop jenkins-plug
docker stop jenkins-docker

docker rm jenkins-plug
docker rm jenkins-docker

docker network rm jenkins

docker rmi -f $(docker images --filter dangling=true -qa)

docker volume rm jenkins-data
docker volume rm jenkins-docker-certs
