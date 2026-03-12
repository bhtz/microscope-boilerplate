---
apply: always
---

# Code guidelines (Desktop template)

Guidance for desktop applications (MAUI / WPF / WinForms depending on template variant) that pair UI client code with local services.

### Technology Stack

- **UI**: Avalonia (cross platform)

### Architecture

- MVVM or MVU patterns for UI separation

### Project Structure

```
Project/                              
├── Views/                          # UI pages, XAML files or UI fragments
├── ViewModels/                     # Presentation logic and bindings
├── Models/                         # Domain and persistence models
└── Program.cs                      # Application startup (MAUI/WPF entry)
```

## Available Skills for Coding Agents (Desktop)

- `dotnet-build`, `dotnet-run` for local builds and launch
- `user-secrets-config` for local dev secrets
- packaging skills for creating installable artifacts

## Adding New Features

1. Add View + ViewModel + Tests
2. Register viewmodel services in DI container
3. Add packaging pipeline (MSIX / DMG) under CI

## Infrastructure

- Use local SQLite for dev state and provide sync adapters for remote storage when needed.
- CI should produce platform artifacts and run UI tests (Appium / WinAppDriver / MAUI test tooling).


````
