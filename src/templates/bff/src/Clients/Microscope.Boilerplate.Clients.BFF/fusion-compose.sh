#!/bin/bash

# Compose Countries remote schema to gateway
fusion compose -p gateway.fgp -s ./SubGraphs/Countries/Countries.fsp
fusion compose -p gateway.Development.fgp -s ./SubGraphs/Countries/Countries.Development.fsp

# Compose other subgraphs schema to gateway
# fusion compose -p gateway.fgp -s ./SubGraphs/<MySubGraph>/<MySubGraph>.fsp
# fusion compose -p gateway.Development.fgp -s ./SubGraphs/<MySubGraph>/<MySubGraph>.Development.fsp
