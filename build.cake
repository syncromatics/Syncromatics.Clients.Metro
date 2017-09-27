#addin nuget:?package=Cake.Git
// #addin "Cake.Docker"
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

// void RunTargetInContainer(string target, string arguments, params string[] includeEnvironmentVariables) {
//     var cwd = MakeAbsolute(Directory("./"));
//     var env = includeEnvironmentVariables.ToDictionary(key => key, key => EnvironmentVariable(key));

//     var missingEnv = env.Where(x => string.IsNullOrEmpty(x.Value)).ToList();
//     if (missingEnv.Any()) {
//         throw new Exception($"The following environment variables are required to be set: {string.Join(", ", missingEnv.Select(x => x.Key))}");
//     }

//     var settings = new DockerRunSettings
//     {
//         Volume = new string[] { $"{cwd}:/artifacts"},
//         Workdir = "/artifacts",
//         Rm = true,
//         Env = env
//             .OrderBy(x => x.Key)
//             .Select((x) => $"{x.Key}=\"{x.Value}\"")
//             .ToArray(),
//     };

//     Information(string.Join(Environment.NewLine, settings.Env));

//     var command = $"cake -t {target} {arguments}";
//     Information(command);
//     var buildBoxImage = "syncromatics/build-box";
//     DockerPull(buildBoxImage);
//     DockerRun(settings, buildBoxImage, command);
// }

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
        Information($"GetVersion: Current branch is {branch}");
        var travisBranch = EnvironmentVariable("TRAVIS_BRANCH");
        if (branch == "(no branch)" && !string.IsNullOrEmpty(travisBranch))
        {
            branch = travisBranch;
        }
        Information($"GetVersion: Current branch (after Travis) is {branch}");
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
    // .Does(() => RunTargetInContainer("InnerTest", "", "TEST_URL"));

Task("Package")
    .IsDependentOn("InnerPackage");
    // .Does(() => RunTargetInContainer("InnerPackage", ""));

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
        DotNetCoreTool("./tests/Syncromatics.Clients.Metro.Api.Tests/Syncromatics.Clients.Metro.Api.Tests.csproj", "xunit");
    });

Task("InnerPackage")
    .IsDependentOn("GetVersion")
    .IsDependentOn("InnerTest")
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
