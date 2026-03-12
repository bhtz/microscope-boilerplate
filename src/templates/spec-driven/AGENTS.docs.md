---
apply: always
---

# Code guidelines (Docs template)

Guidance for documentation projects (static site generators, Markdown-first sites) included in the solution.

### Technology Stack

- **Host**: asp net 10
- **Static Site**: vitepress
- **Content**: Markdown with front-matter and shortcodes

### Architecture

- Content source → static generator → site output
- CI validates build and runs link/spell checks before deploy

### Project Structure

```
Project/                                
├── .vitepress/                         # vitepress configuration
├── resources/                          # Markdown source and assets
```

## Available Skills for Coding Agents (Docs)

- `build-docs` to generate the static site locally
- `link-check` and `spell-check` skills for CI

## Adding New Features

1. Add new Markdown files under `content/` with proper front-matter
2. Add shortcodes or templates in `layouts/` as needed
3. Update CI to publish artifacts

## Infrastructure

- Deploy to GitHub Pages, Azure Static Web Apps, or S3 depending on your CI/CD target.
- Provide preview builds on PRs and run `link-check` / `spell-check` in CI to prevent regressions.

````
