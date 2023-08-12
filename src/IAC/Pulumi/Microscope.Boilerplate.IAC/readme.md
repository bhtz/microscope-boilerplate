# Microscope.Boilerplate.IAC

## Install pulumi & azure cli
```console
brew update
brew install azure-cli
brew install pulumi/tap/pulumi
```

## Azure CLI login
```console
az login
```

## Set azure location (default francecentral)
```console
pulumi config set azure-native:location westus2
```

## Deploy stack on Azure
```console
cd ./src/IAC/Pulumi/Microscope.Boilerplate.IAC
pulumi up
```

