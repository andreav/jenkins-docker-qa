FROM jenkins/jenkins:lts-jdk11

# ATTENTION DirectoryBrowserSupport.CSP lowers security. It is used for correctly visualizing K6 HTML report. If it is not necessary, remove that line
ENV JAVA_OPTS -Djenkins.install.runSetupWizard=false \
              -Dcasc.jenkins.config=/var/jenkins_home/casc_configs/ \
              -Dhudson.model.DirectoryBrowserSupport.CSP=\"sandbox; default-src \'none\'; img-src \'self\' raw.githubusercontent.com/benc-uk/k6-reporter/main/assets/icon.png; style-src \'self\' use.fontawesome.com unpkg.com \'unsafe-inline\'; font-src use.fontawesome.com;\"

USER root
RUN apt-get update && apt-get install -y lsb-release
RUN curl -fsSLo /usr/share/keyrings/docker-archive-keyring.asc \
  https://download.docker.com/linux/debian/gpg
RUN echo "deb [arch=$(dpkg --print-architecture) \
  signed-by=/usr/share/keyrings/docker-archive-keyring.asc] \
  https://download.docker.com/linux/debian \
  $(lsb_release -cs) stable" > /etc/apt/sources.list.d/docker.list
RUN apt-get update && apt-get install -y docker-ce-cli docker-compose jq curl

USER jenkins

COPY --chown=jenkins:jenkins plugins.txt /usr/share/jenkins/ref/plugins.txt
RUN jenkins-plugin-cli -f /usr/share/jenkins/ref/plugins.txt

RUN mkdir /var/jenkins_home/casc_configs/
COPY --chown=jenkins:jenkins casc_configs/ /var/jenkins_home/casc_configs/
