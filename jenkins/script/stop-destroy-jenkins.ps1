docker stop jenkins-plug

docker rm jenkins-plug

docker network rm jenkins

docker rmi -f $(docker images --filter dangling=true -qa)

docker volume rm jenkins-data
