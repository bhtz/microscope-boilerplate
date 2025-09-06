# Templates

## Install
```console
cd microscope-boilerplate/templates
dotnet pack
dotnet new install ./bin/Release/Microscope.Boilerplate.1.0.0.nupkg
```

## Uninstall
```console
dotnet new uninstall Microscope.Boilerplate
```

## Service template
```console
dotnet new mcsp_service -n Acme.AwesomeProject
```

## BFF/Frontend template
```console
dotnet new mcsp_bff -n Acme.AwesomeProject -C
```

## CLI template
```console
dotnet new mcsp_cli -n Acme.CLI
```

## Documentation as code template
```console
dotnet new mcsp_doc -n Acme.Doc
```