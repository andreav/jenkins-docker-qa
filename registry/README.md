# Running & Populating Registry

refs - https://docs.docker.com/registry/

    docker run -d -p 5000:5000 --name registry registry:2

    docker pull ubuntu
    docker image tag ubuntu localhost:5000/myubuntu
    docker push localhost:5000/myubuntu

# Allowing jenkins to pull from this insecure regisry (only for test)

refs - https://docs.docker.com/registry/insecure/

1. Edit `%userprofile%\.docker\daemon.json`

        { 
            "insecure-registries" : [
                "host.docker.internal:5000", 
                "<my-ip>:5000"
            ] 
        }

    Restart docker destop

    Otherwise you'll get:

        Error response from daemon: Get "https://<my-ip>:5000/v2/": http: server gave HTTP response to HTTPS client

1. From inside job set agent pointing to host.docker.internal or \<my-ip>
    
        pipeline {
            agent { 
                docker { 
                    image 'myubuntu'
                    registryUrl 'http://host.docker.internal:5000/'
                } 
            }
            stages {
                ....

    You can replace  `host.docker.internal` with `<my-ip>`
        
            registryUrl 'http://<my-ip>:5000/'


# Browsing repositories (images)

    http://localhost:5000/v2/_catalog
    http://<machine-ip>:5000/v2/_catalog


