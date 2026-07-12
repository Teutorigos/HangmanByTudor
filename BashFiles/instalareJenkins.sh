sudo apt update && sudo apt upgrade -y
if ! command -v docker &> /dev/null; then
    echo "Docker nu este instalat. Instalare Docker..."
    sudo apt install -y docker.io
    sudo systemctl start docker
    sudo systemctl enable docker
fi

sudo docker run -d \
  --name jenkins \
  -p 8080:8080 \
  -p 50000:50000 \
  -v jenkins_home:/var/jenkins_home \
  jenkins/jenkins:lts-jdk17


sleep 30

sudo docker exec jenkins cat /var/jenkins_home/secrets/initialAdminPassword
