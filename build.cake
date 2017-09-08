#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool "nuget:?package=xunit.runner.console"
#addin nuget:?package=Cake.Git
#addin "Cake.Docker"
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

void RunTargetInContainer(string target, string arguments, params string[] includeEnvironmentVariables) {
    var cwd = MakeAbsolute(Directory("./"));
    var env = includeEnvironmentVariables.ToDictionary(key => key, key => EnvironmentVariable(key));

    var missingEnv = env.Where(x => string.IsNullOrEmpty(x.Value)).ToList();
    if (missingEnv.Any()) {
        throw new Exception($"The following environment variables are required to be set: {string.Join(", ", missingEnv.Select(x => x.Key))}");
    }

    var settings = new DockerRunSettings
    {
        Volume = new string[] { $"{cwd}:/artifacts"},
        Workdir = "/artifacts",
        Rm = true,
        Env = env
            .OrderBy(x => x.Key)
            .Select((x) => $"{x.Key}=\"{x.Value}\"")
            .ToArray(),
    };

    Information(string.Join(Environment.NewLine, settings.Env));

    var command = $"{settings.Workdir}/build.sh -t {target} {arguments}";
    Information(command);
    var buildBoxImage = "syncromatics/build-box";
    DockerPull(buildBoxImage);
    DockerRun(settings, buildBoxImage, command);
}

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
    .Does(() => RunTargetInContainer("InnerTest", "--verbosity Diagnostic", "TEST_URL"));

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
        var buildSettings = new ProcessSettings
        {
            Arguments = $"/property:Configuration={configuration}"
        };

        using(var process = StartAndReturnProcess("msbuild", buildSettings))
        {
            process.WaitForExit();
            var exitCode = process.GetExitCode();
            if(exitCode != 0)
                throw new Exception("Build Failed.");
        }
    });

Task("InnerTest")
    .IsDependentOn("InnerBuild")
    .Does(() =>
    {
        XUnit2($"/artifacts/tests/Syncromatics.Clients.Metro.Api.Tests/bin/{configuration}/net46/Syncromatics.Clients.Metro.Api.Tests.dll");
    });

Task("Package")
    .Does(() => RunTargetInContainer("PackageNuget", "--verbosity Diagnostic"));

Task("PackageNuget")
    .IsDependentOn("GetVersion")
    .IsDependentOn("InnerBuild")
    .Does(() =>
    {
        var packageSettings = new NuGetPackSettings
        {
            Version = version,
            Properties = new Dictionary<string, string> 
            {
                { "configuration", configuration }
            }
        };

        NuGetPack("./src/Syncromatics.Clients.Metro.Api/Syncromatics.Clients.Metro.Api.nuspec", packageSettings);
    });

Task("Publish")
    .IsDependentOn("GetVersion")
    .IsDependentOn("Package")
    .Does(() =>
    {
        var package = $"./Syncromatics.Clients.Metro.Api.{version}.nupkg";

        NuGetPush(package, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = EnvironmentVariable("NUGET_API_KEY")
        });
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
