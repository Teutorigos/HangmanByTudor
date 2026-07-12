#!/bin/bash

# Stop the script at first error
set -e

echo "Adding Helm"
curl https://raw.githubusercontent.com/helm/helm/main/scripts/get-helm-3 | bash
sudo -u jenkins helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
sudo -u jenkins helm repo add grafana https://grafana.github.io/helm-charts
sudo -u jenkins helm repo update

# Creating the namespace if it doesn't exist
sudo -u jenkins minikube kubectl -- create namespace monitoring --dry-run=client -o yaml | sudo -u jenkins minikube kubectl -- apply -f -

# Installing Prometheus and Grafana
sudo -u jenkins helm upgrade --install prometheus prometheus-community/kube-prometheus-stack \
  --namespace monitoring

# Installing Loki (optimized, as I don't have enough resources)
sudo -u jenkins helm upgrade --install loki grafana/loki-stack \
  --namespace monitoring \
  --set loki.persistence.enabled=false \
  --set promtail.enabled=false \
  --set fluent-bit.enabled=false \
  --set grafana.enabled=false \
  --set prometheus.enabled=false

sudo -u jenkins minikube kubectl -- get pods -n monitoring
sudo -u jenkins minikube kubectl -- get svc -n monitoring
