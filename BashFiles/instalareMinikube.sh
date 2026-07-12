if [ "$EUID" -ne 0 ]; then
  echo "Please run the script with sudo: sudo $0"
  exit 1
fi

if ! command -v minikube &> /dev/null; then
    curl -LO https://storage.googleapis.com/minikube/releases/latest/minikube-linux-amd64
    install minikube-linux-amd64 /usr/local/bin/minikube
    rm minikube-linux-amd64
    echo "Minikube was sucessfully installed in /usr/local/bin/minikube!"
else
    echo "Minikube is already installed."
fi

if ! command -v kubectl &> /dev/null; then
    curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
    install -o root -g root -m 0755 kubectl /usr/local/bin/kubectl
    rm kubectl
    echo "Kubectl was successfully installed in /usr/local/bin/kubectl!"
else
    echo "Kubectl was already installed."
fi

usermod -aG docker jenkins
sudo -u jenkins minikube delete -p minikube || true
sudo -u jenkins minikube start -p minikube --driver=docker
sudo -u jenkins minikube status -p minikube
