---
apply: always
---

# Code guidelines (CLI template)

Guidance for command-line tools and CLI-based templates included in the solution.

### Technology Stack

- **Framework**: .NET CLI tooling (System.CommandLine / Spectre.Console)
- **Packaging**: single-file publish, native installers as needed
- **Testing**: unit tests and integration tests invoking CLI entrypoints

### Architecture

- Small, composable commands with clear verbs and options
- Use exit codes and structured output (JSON) for automation

### Project Structure

```
src/CLI/                             # CLI project
├── Commands/                        # Command implementations
└── Program.cs                        # Entrypoint wiring commands
```

## Available Skills for Coding Agents (CLI)

- `dotnet-build`, `dotnet-publish` for creating distributables
- `package-cli` for creating installers or archives

## Adding New Features

1. Add a new command under `Commands/` with clear options and help
2. Add tests that invoke the command and assert exit codes and output
3. Update CI to publish artifacts

## Infrastructure

- CI builds and publishes CLI artifacts; provide packaging for target OSs

````
