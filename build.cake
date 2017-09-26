#tool "nuget:?package=xunit.runner.console"
#addin nuget:?package=Cake.Git
using System.Text.RegularExpressions;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./src/Syncromatics.Clients.Metro.Api/bin") + Directory(configuration);
var testDir = Directory("./tests/Syncromatics.Clients.Metro.Api.Tests/bin") + Directory(configuration);
var currentDirectory = MakeAbsolute(Directory("./"));

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
var version = "";
var semVersion = "";
Task("GetVersion")
    .Does(() =>
    {
        var repositoryPath = Directory(".");
        var branch = GitBranchCurrent(repositoryPath).FriendlyName;
        var prereleaseTag = Regex.Replace(branch, @"\W+", "-");
        var describe = GitDescribe(repositoryPath, GitDescribeStrategy.Tags);

        var isMaster = prereleaseTag == "master" || prereleaseTag == "-no-branch-";
        version = string.Join(".", describe.Split(new[] { '-' }, 3).Take(2));
        semVersion = version + (isMaster ? "" : $"-{prereleaseTag}");
    });

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(buildDir);
        CleanDirectory(testDir);
    });

Task("Build")
    .IsDependentOn("InnerTest");

Task("Package")
    .IsDependentOn("InnerPackage");

Task("Publish")
    .IsDependentOn("GetVersion")
    .IsDependentOn("Package")
    .Does(() =>
    {
        var package = $"./Syncromatics.Clients.Metro.Api.{semVersion}.nupkg";

        NuGetPush(package, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = EnvironmentVariable("NUGET_API_KEY")
        });
    });

Task("InnerRestore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

Task("InnerBuild")
    .IsDependentOn("InnerRestore")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings
        {
            Configuration = configuration,
        };
        DotNetCoreBuild("./", settings);
    });

Task("InnerTest")
    .IsDependentOn("InnerBuild")
    .Does(() =>
    {
        XUnit2($"./tests/Syncromatics.Clients.Metro.Api.Tests/bin/{configuration}/net46/Syncromatics.Clients.Metro.Api.Tests.dll");
    });

Task("InnerPackage")
    .IsDependentOn("GetVersion")
    .IsDependentOn("InnerBuild")
    .Does(() =>
    {
        var packageSettings = new DotNetCorePackSettings
        {
            Configuration =  configuration,
            OutputDirectory = "./",
            ArgumentCustomization = args => args.Append($"/p:Version={semVersion}")
        };

        DotNetCorePack(File("./src/Syncromatics.Clients.Metro.Api/Syncromatics.Clients.Metro.Api.csproj"), packageSettings);
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
