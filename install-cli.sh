#!/bin/bash

cd src/cli
dotnet pack
dotnet tool uninstall --global  Microscope.Boilerplate.CLI
dotnet tool install --global --add-source ./nupkg Microscope.Boilerplate.CLI