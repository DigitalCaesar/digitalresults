#DigitalResults

[![Build Status](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_apis/build/status%2FCakeBuildFromGitHub?repoName=DigitalCaesar%2Fdigitalresults&branchName=main)](https://dev.azure.com/DigitalCaesarLLC/DigitalResults/_build/latest?definitionId=33&repoName=DigitalCaesar%2Fdigitalresults&branchName=main)
[![NuGet Version (DigitalResults)](https://img.shields.io/nuget/dt/DigitalResults)](https://www.nuget.com/packages/DigitalResults/)


## Introduction 
A library for a union return type that allows for a method to return either a value or error without breaking.

## Usage

### Creating a Result

Creating a result is as simple as referencing the DigitalCaesar.Results namespace, changing the return type of you method to Result<T> and returning a value and at least one exception from your method.  

```csharp
using DigitalCaesar.Results;

public Result<string> GetResult()
{
    if(Good)
      return "This is a successful result";
    else
      return new Exception("This is a failure result");
}
```

### Using a Result to return a value

To use the Result, use the Match method to retrieve the appropriate value or exception.  Match uses two functions, one for success and one for failure.  The function that matches the state of the Result will be executed and the value returned.

```csharp
var result = GetResult();
string resultMessage = result.Match(
  success => success,
  failure => failure.Message
);
Console.WriteLine(resultMessage);
```

### Using a Result to run an Action

You can also use the Switch method to run different Actions based on the state of the Result.  Switch uses two Actions, one for success and one for failure.  The Action that matches the state of the Result will be executed.

```csharp
var result = GetResult();
result.Switch(
  success => Console.WriteLine(success),
  failure => Console.WriteLine(failure.Message)
);
```

## Contribute
Do you like what you see and do you want to help contribute to the community?  Send feedback, submit issues, and submit pull requests!  Want to go further?  Consider sponsoring this project or check out the website to see how you can donate or buy me a coffee.  Caffiene is always welcome.  

### Projects
This is a single project solution. 

Project | Description
--------|------------
DigitalResults   | Main project for the package
Samples | Sample project to illustrate usage of the package

### Build
Builds are run on Azure DevOps.  The build pipeline is cake-build-pipelines.yml run on Azure DevOps.  Builds are triggered by any develop, release, or main branch updates.  

Cake is used builds with the single script, build.cake.  Builds can be run locally with cake as normal 'dotnet cake' from the root project folder.  The build script will run the build, test, and pack tasks.  If you are not familiar with cake, then check the documentation on their website, https://cakebuild.net/. 

### Test
Testing is provided by Unit Test and Integration Test projects which are required to pass before build and packaging. 

### Release 
Release Pipelines are defined on Azure Devops.  Only main and release branch updates trigger releases.  Pre-Release distribution is triggered by release branch updates.  Production distribution is triggered by main branch updates.  Releases are first pushed to an internal store pending approval for public release.

Versions from a release branch will have a '-preview' suffix added.  Versions from main will have no suffix.  Versioning is handled by Nerdbank.GitVersioning.  See their documentation for more information.

## Credits
There are currently no third party package dependencies.

This implementation was inspired by tutorials from Milan Jovanovic and existing libraries ErrorOr and OneOf.  Each has an elements that I prefer and do not prefer.  The purpose of this library is to bring together the preferable elements and adapt for specific needs.  
Inspiration derived from: 
* Milan Jovanovic - [YouTube series](https://www.youtube.com/playlist?list=PLYpjLpq5ZDGstQ5afRz-34o_0dexr1RGa)
* [ErrorOr](https://github.com/amantinband/error-or/blob/main/README.md): Amichai Mantinband
* [OneOf](https://github.com/mcintyre321/OneOf/blob/master/README.md): Harry McIntyre
