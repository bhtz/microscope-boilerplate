#!/bin/bash

echo "🔐 Local certificates generation ..."

# Vérifie que mkcert est installé
if ! command -v mkcert &> /dev/null; then
    echo "❌ mkcert n'est pas installé."
    echo "   macOS  → brew install mkcert"
    echo "   Linux  → apt install mkcert"
    echo "   Windows → choco install mkcert"
    exit 1
fi

mkcert -install
# Inclut les noms utilises par le host et les conteneurs Docker
mkcert -cert-file ./Keycloak/Certs/keycloak.crt -key-file  ./Keycloak/Certs/keycloak.key localhost 127.0.0.1 ::1 host.docker.internal

echo "✅ Certificates generated at ./Keycloak/certs/"