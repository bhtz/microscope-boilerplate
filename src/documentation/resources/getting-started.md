# Microscope.Boilerplate

> An opiniated started kit for product engineering teams

## Requirements

* dotnet 8 SDK
* nodejs 16+
* docker engine

### Get source code
```console
git clone https://github.com/bhtz/microscope-boilerplate.git
```

### Install microscope dotnet templates
```console
cd microscope-boilerplate/templates
dotnet pack
dotnet new install ./bin/Release/Microscope.Boilerplate.1.0.0.nupkg
```

### Generate some project
```console
dotnet new mcsp_cli -n Acme
```

### Uninstall
```console
dotnet new uninstall Microscope.Boilerplate
```