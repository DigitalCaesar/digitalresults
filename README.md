DigitalResults
============================

# Introduction 
A library for a union return type that allows for a method to return either a value or error without breaking.

# Projects
This is a single project solution. 

Project | Description
--------|------------
DigitalResults   | Base for all generic models

# Build
The build pipeline is in the room build-pipelines.yml run on Azure DevOps.  Builds are triggered by any develop, release, or main branch update.  

Pipeline | Status
---------|--------
debug    | [![Build Status](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_apis/build/status%2FDigitalResults?repoName=DigitalResults&branchName=debug)](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_build/latest?definitionId=29&repoName=DigitalResults&branchName=debug)
release  | [![Build Status](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_apis/build/status%2FDigitalResults?repoName=DigitalResults&branchName=release)](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_build/latest?definitionId=29&repoName=DigitalResults&branchName=release)
master   | [![Build Status](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_apis/build/status%2FDigitalResults?repoName=DigitalResults&branchName=master)](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_build/latest?definitionId=29&repoName=DigitalResults&branchName=master)

# Test
Testing is provided by Unit Test and Integration Test projects which are required to pass before build and packaging. 

# Sample
A sample project is available to illustrate usage of features available in the package.  

# Release 
Release Pipelines are defined on Azure Devops.  Only main and release branch updates trigger releases.  Pre-Release distribution is triggered by release branch updates.  Production distribution is triggered by main branch updates.
[TBA]

# Contribute
This project is currently private and not accepting contributions.

# Credits
There are currently no third party package dependencies.

This implementation was inspired by tutorials from Milan Jovanovic and existing libraries ErrorOr and OneOf.  Each has an elements that I prefer and do not prefer.  The purpose of this library is to bring together the preferable elements and adapt for specific needs.  
Inspiration derived from: 
* Milan Jovanovic
* ErrorOr: Amichai Mantinband : https://github.com/amantinband/error-or/blob/main/README.md
* OneOf: Harry McIntyre:  https://github.com/mcintyre321/OneOf/blob/master/README.md
