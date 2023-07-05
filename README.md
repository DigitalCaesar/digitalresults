DigitalResults
============================

# Introduction 
A library for a union return type that allows for a method to return either a value or error without breaking.

# Projects
Project | Description
--------|------------
DigitalResults   | Base for all generic models

# Build
The build pipeline is in the room build-pipelines.yml run on Azure DevOps.  Builds are triggered by any develop, release, or main branch update.  

# Test
Testing is provided by Unit Test and Integration Test projects which are required to pass before build and packaging. 

# Release 
Release Pipelines are defined on Azure Devops.  Only main and release branch updates trigger releases.  Pre-Release distribution is triggered by release branch updates.  Production distribution is triggered by main branch updates.

# Contribute
This project is currently private and not accepting contributions.

# Credits
There are currently no third party package dependencies.
Inspiration derived from: 
* Milan Jovanovic
* ErrorOr: Amichai Mantinband : https://github.com/amantinband/error-or/blob/main/README.md
* OneOf:  https://github.com/mcintyre321/OneOf/blob/master/README.md
