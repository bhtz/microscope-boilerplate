---
name: dotnet-build-arm64
description: Publish .NET projects targeting linux-arm64 to produce ARM64-compatible container images (Apple Silicon).
---

# dotnet-build-arm64

Build for ARM64 architecture (e.g., Apple Silicon).

## Command
```bash
dotnet publish -r linux-arm64 -p:PublishProfile=DefaultContainer
```

## Purpose
Create ARM64-compatible containers

## Output
ARM64 Docker images
