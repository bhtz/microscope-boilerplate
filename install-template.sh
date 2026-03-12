#!/bin/bash

cd src/templates
dotnet pack
dotnet new uninstall Microscope.Boilerplate
dotnet new install ./bin/Release/Microscope.Boilerplate.1.0.0.nupkg