#addin nuget:?package=Cake.Coverlet&version=3.0.4
#addin nuget:?package=Cake.AzureDevOps&version=3.0.0
#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=5.1.19

string version = EnvironmentVariable("VersionNumber", "1.0.0");
string solution = Argument("solution", "./Source/DigitalResults.sln");

var target = Argument("target", "Default");

string branchName = "local";
string buildId = "0";
bool IsRelease = false;
string versionSuffix = "-local";
string configuration = "Debug";
string ARTIFACT_OUTPUT_DIR = "./artifacts";
string TEST_COVERAGE_OUTPUT_DIR = $"{ARTIFACT_OUTPUT_DIR}/coverage";
string TEST_COVERAGE_REPORTS_DIR = $"{TEST_COVERAGE_OUTPUT_DIR}/reports";
string TEST_RESULTS_OUTPUT_DIR = $"{ARTIFACT_OUTPUT_DIR}/testresults";
string PACKAGE_OUTPUT_DIR = $"{ARTIFACT_OUTPUT_DIR}/package";

if(BuildSystem.IsRunningOnAzurePipelines)
{
    buildId = BuildSystem.AzurePipelines.Environment.Build.Id.ToString();
    branchName = BuildSystem.AzurePipelines.Environment.Repository.SourceBranchName;
}

switch(branchName) {
    case "master":
        IsRelease = true;
        configuration = "Release";
        versionSuffix = "";
        break;
    case "release":
        IsRelease = true;
        configuration = "Release";
        versionSuffix = "-rc";
        break;
    case "develop":
        IsRelease = false;
        configuration = "Debug";
        versionSuffix = "-dev";
        break;
    default:
        IsRelease = false;
        configuration = "Debug";
        versionSuffix = "-local";
        break;
}

Task("Clean")
  .Does(() => {
    DotNetClean(solution);
  });

Task("Restore")
    .IsDependentOn("Clean")
    .Description("Restoring the solution dependencies")
    .Does(() => {
        GetFiles("./**/**/*.csproj").ToList().ForEach(project => {
            string projectFileName = project.ToString();
            Information($"Restoring dependencies for: {projectFileName}");
            DotNetRestore(
                projectFileName, 
                new DotNetRestoreSettings
                {
                    Verbosity = DotNetVerbosity.Minimal,
                    Sources = new [] { "https://api.nuget.org/v3/index.json" }
                }
            );
        });
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        GetFiles("./**/**/*.csproj").ToList().ForEach(project => {
            string projectFileName = project.ToString();
            Information($"Building {projectFileName} in {branchName} with version {version}{versionSuffix} and build {buildId}.");
            DotNetBuild(
                projectFileName, 
                new DotNetBuildSettings {
                    Configuration = configuration,
                    NoRestore = true,
                    MSBuildSettings = new DotNetMSBuildSettings {
                        Verbosity = DotNetVerbosity.Minimal, 
                        ContinuousIntegrationBuild = true,
                        Version = version, 
                        VersionSuffix = versionSuffix, 
                        ArgumentCustomization = args=>args.Append("/p:Deterministic=true")
                    }
                }
            );
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        Information($"Running Tests");
        GetFiles("./Tests/**/*.csproj").ToList().ForEach(project => {
            var projectName = $"{project.GetFilenameWithoutExtension()}";
            var projectFileName = project.ToString();
            Information($"Found Test for { projectFileName }");

            var testResultsOutputName = $"{projectName}.trx";
            var codeCoverageOutputName = $"{projectName}.cobertura.xml";
            var vsTestResultsOutputName = $"{projectName}.TestResults.xml";
            
            Information($"Running Test for {projectFileName}");
            DotNetTest(
                project.ToString(), 
                new DotNetTestSettings {
                    Configuration = configuration,
                    NoBuild = true,
                    NoRestore = true, 
                    VSTestReportPath = new FilePath($"{TEST_RESULTS_OUTPUT_DIR}/{vsTestResultsOutputName}"),
                }, 
                new CoverletSettings {
                    CollectCoverage = true,
                    CoverletOutputFormat = CoverletOutputFormat.cobertura,
                    CoverletOutputDirectory = $"{TEST_COVERAGE_OUTPUT_DIR}", 
                    CoverletOutputName = codeCoverageOutputName, 
                    ArgumentCustomization = args => args.Append($"--logger trx;LogFileName={testResultsOutputName}")
                }
            );
        });

        Information($"Looking for code coverage results in {TEST_COVERAGE_OUTPUT_DIR}");
        var glob = new GlobPattern($"**/*.cobertura.xml");
        Information($"Found the following: {glob.ToString()}");
        var outputDirectory = Directory($"{TEST_COVERAGE_REPORTS_DIR}");
        ReportGenerator(
            glob, 
            outputDirectory, 
            new ReportGeneratorSettings
            {
                ArgumentCustomization = args => args.Append($"-reportTypes:HtmlInline_AzurePipelines;Cobertura")
            }
        );
    });

Task("Publish-CodeCoverage")
  .IsDependentOn("Test")
  .WithCriteria(BuildSystem.IsRunningOnAzurePipelines)
  .Does(() => {
    var summaryLocation = $"{TEST_COVERAGE_REPORTS_DIR}/Cobertura.xml";
    Information($"Publishing Code Coverage for {summaryLocation}.");
    BuildSystem.AzurePipelines.Commands.PublishCodeCoverage(
      new AzurePipelinesPublishCodeCoverageData { 
        CodeCoverageTool = AzurePipelinesCodeCoverageToolType.Cobertura,
        SummaryFileLocation = new FilePath($"{summaryLocation}"),
        ReportDirectory = Directory($"{TEST_COVERAGE_REPORTS_DIR}")
      });
  });

Task("Publish-TestResults")
  .IsDependentOn("Test")
  .WithCriteria(BuildSystem.IsRunningOnAzurePipelines)
  .Does(() => {
    var vsTestFiles = GetFiles($"**/*.TestResults.xml").ToList();
    Information($"Found {vsTestFiles.Count()} Test Result files.");
    Information($"Publishing Test Results.");
    BuildSystem.AzurePipelines.Commands.PublishTestResults(
      new AzurePipelinesPublishTestResultsData { 
        TestRunner = AzurePipelinesTestRunnerType.VSTest,
        TestResultsFiles = vsTestFiles
      });
  });

Task("Pack")
  .IsDependentOn("Test")
  .WithCriteria(IsRelease)
  .Does(() => {
    GetFiles("./**/**/*.csproj").ToList().ForEach(project => {
        var projectFileName = project.ToString();
        Information($"Building {projectFileName}");
        DotNetPack(
            projectFileName, 
            new DotNetPackSettings {
                Configuration = configuration, 
                NoBuild = true,
                NoRestore = true,
                OutputDirectory = $"{PACKAGE_OUTPUT_DIR}",
                Verbosity = DotNetVerbosity.Minimal, 
                VersionSuffix = versionSuffix
            }
        );
    });
  });

Task("Publish-Nuget")
  .IsDependentOn("Pack")
  .WithCriteria(IsRelease)
  .WithCriteria(BuildSystem.IsRunningOnAzurePipelines)
  .Does(() => {
      AzurePipelines.Commands.UploadArtifactDirectory($"{PACKAGE_OUTPUT_DIR}");
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Publish-CodeCoverage")
    .IsDependentOn("Publish-TestResults")
    .IsDependentOn("Pack")
    .IsDependentOn("Publish-Nuget");
RunTarget(target);