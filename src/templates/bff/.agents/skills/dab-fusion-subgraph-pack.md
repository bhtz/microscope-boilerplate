# dab-fusion-subgraph-pack

Create GraphQL Fusion subgraph pack for DAB (Data API Builder).

## Command

Pull schema from GraphQL endpoint:
```bash
gq http://localhost:4700/graphql/ --introspect > schema.graphql
```

Generate the `.fsp` file:
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF/SubGraphs/Dab
gq http://localhost:4700/graphql/ --introspect > schema.graphql
fusion subgraph pack -c subgraph-config.json -p Dab.fsp
```

## Purpose
Generate `.fsp` file for GraphQL Fusion composition

## Output
`Dab.fsp` file created
