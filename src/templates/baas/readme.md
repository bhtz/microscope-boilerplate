# Microscope.Boilerplate

Microscope.Boilerplate data api builder template

dotnet new mcsp_baas --Hasura --Dab -o . -n Acme.Project

## Hasura

### install CLI

        https://hasura.io/docs/2.0/hasura-cli/install-hasura-cli/

### use CLI

        cd IAC/Docker/Hasura
        hasura console

## Data Api Builder

### install CLI

        dotnet tool install --global Microsoft.DataApiBuilder

### use CLI

        dab add Country --source "dbo.CountryCodes" --permissions "anonymous:*"
        