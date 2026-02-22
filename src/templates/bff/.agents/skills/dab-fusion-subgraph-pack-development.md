# dab-fusion-subgraph-pack-development

Create GraphQL Fusion subgraph pack for DAB in development environment

## Command

Pull schema from GraphQL endpoint:
```bash
gq http://localhost:4700/graphql/ --introspect > schema.graphql
```

Generate the `.fsp` file:
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF/SubGraphs/Dab
fusion subgraph pack -c subgraph-config.Development.json -p Dab.Development.fsp
```

## Purpose
Generate development `.fsp` file for local testing

## Output
`Dab.Development.fsp` file created
