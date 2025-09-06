# CLI TOOL

> Microscope dotnet tool is a CLI application to generate project with console GUI

## Images

**Category selector :**
![../images/tool.png](../images/tool.png)

**microscope templates :**
![../images/tool2.png](../images/tool2.png)

**.NET templates :**
![../images/tool3.png](../images/tool3.png)

**Aspire templates :**
![../images/tool4.png](../images/tool4.png)

**Community templates :**
![../images/tool5.png](../images/tool5.png)

## Installation

### Install microscope CLI (as dotnet tool)
```console
cd microscope-boilerplate/tool
dotnet pack
dotnet tool install --global --add-source ./nupkg Microscope.Boilerplate.Tool.CLI
```

### Uninstall microscope CLI
```console
dotnet tool uninstall --global  Microscope.Boilerplate.Tool.CLI
```

### Install templates
> Will install Aspire, Hotchocolate templates & Aspire CLI
```console
microscope install
```
> **WARNING**: Microscope.Boilerplate.Templates are not published on nuget yet ! Make sure to install manually : [installation](/templates/installation)

### Use CLI
```console
microscope
```
