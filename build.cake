#tool "nuget:?package=vswhere"

var target = Argument("target", "Default");
var nugetPaths = GetFiles("./**/*.csproj").Except(GetFiles("./**/StackExchange.Windows.Api.csproj"));
var solution = GetFiles("./*.sln").First();
var msbuildPath = VSWhereAll(new VSWhereAllSettings {
     Requires = "'Microsoft.Component.MSBuild'",
     Version = "15",
     ReturnProperty = "installationPath"
});

Task("Clean")
    .Does(() => 
    {
        CleanDirectories(new[]
        {
            "./StackExchange.Windows/bin",
            "./StackExchange.Windows/obj",
            "./StackExchange.Windows.Api/bin",
            "./StackExchange.Windows.Api/obj",
            "./StackExchange.Windows.Tests/bin",
            "./StackExchange.Windows.Tests/obj",
            //"./packages",
        });
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() => {
        // foreach(var path in nugetPaths) {
        //     Information("Restoring {0}", path);
        //     NuGetRestore(path, new NuGetRestoreSettings {
        //         MSBuildVersion = NuGetMSBuildVersion.MSBuild15
        //     });
        // }
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() => {
        DotNetBuild(solution);
    });

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);