#!/bin/bash

# pack commands for Countries remote schema as subgraphs
gq https://countries.trevorblades.com/ --introspect > ./SubGraphs/Countries/schema.graphql
fusion subgraph pack -c ./SubGraphs/Countries/subgraph-config.json -p ./SubGraphs/Countries/Countries.fsp -s ./SubGraphs/Countries/schema.graphql
fusion subgraph pack -c ./SubGraphs/Countries/subgraph-config.json -p ./SubGraphs/Countries/Countries.Development.fsp -s ./SubGraphs/Countries/schema.graphql

# export schema & pack commands for DAB as subgraphs
# gq http://<host>/graphql --introspect > ./SubGraphs/<MySubGraph>/schema.graphql
# fusion subgraph pack -c ./SubGraphs/<MySubGraph>/subgraph-config.json -p ./SubGraphs/<MySubGraph>/<MySubGraph>.fsp -s ./SubGraphs/<MySubGraph>/schema.graphql 
# fusion subgraph pack -c ./SubGraphs/<MySubGraph>/subgraph-config.Development.json -p ./SubGraphs/<MySubGraph>/<MySubGraph>.Development.fsp -s ./SubGraphs/<MySubGraph>/schema.graphql

