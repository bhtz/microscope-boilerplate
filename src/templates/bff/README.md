# Microscope.Boilerplate

> Microscope.Boilerplate BFF template.

## Requirements

- .NET 10
- NPM
- Docker
    
## Install required tools
    
    npm install -g graphqurl
    dotnet tool install StrawberryShake.Tools --local

## Generate local certificate for keycloak https

    cd src/IAC/Aspire/
    chmod +x ./generate-certs.sh
    ./generate-certs.sh

## Add spec driven development specification

dotnet new mcsp_codingrules --Template bff

> 🎉 Enjoy !