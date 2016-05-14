var target = Argument("target", "Default");

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("../Source/SimpleDataGenerator.Json.sln");
});

Task("Clean")
    .Does(() =>
{
    CleanDirectories("../Source/**/bin");
    CleanDirectories("../Source/**/obj");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    MSBuild("../Source/SimpleDataGenerator.Json.sln", settings => settings.SetConfiguration("Release"));
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit("../Source/SimpleDataGenerator.Json.Tests/bin/Release/SimpleDataGenerator.Json.Tests.dll", new NUnitSettings {
        ToolPath = "../Source/packages/NUnit.ConsoleRunner.3.2.0/tools/nunit3-console.exe"
    });
	
});

Task("Create-NuGet-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    var nuGetPackSettings = new NuGetPackSettings {
        Id                      = "SimpleDataGenerator.Json",
        Version                 = EnvironmentVariable("APPVEYOR_BUILD_VERSION"),
        Title                   = "SimpleDataGenerator.Json",
        Authors                 = new[] {"Filip Paluch"},
        Owners                  = new[] {"Filip Paluch"},
        Description             = "Keyboard listener detecting shortcuts.",
        ProjectUrl              = new Uri("https://github.com/filip-paluch/SimpleDataGenerator.Json"),
        LicenseUrl              = new Uri("https://github.com/filip-paluch/SimpleDataGenerator.Json/blob/master/LICENSE"),
        Copyright               = "Filip Paluch 2016",
        Dependencies            = new []{
            new NuSpecDependency{
                Id              = "System.Collections.Immutable",
                Version         = "1.1.32-beta"
            }
        },        
        RequireLicenseAcceptance= false,
        Symbols                 = false,
        NoPackageAnalysis       = true,
        Files                   = new[] { new NuSpecContent {Source = "bin/Release/SimpleDataGenerator.Json.dll", Target = "lib/net452"} },
        BasePath                = "../Source/SimpleDataGenerator.Json",
        OutputDirectory         = ".."
    };
    
    NuGetPack(nuGetPackSettings);
});

Task("Push-NuGet-Package")
    .IsDependentOn("Create-NuGet-Package")
    .Does(() =>
{
    var package = "../SimpleDataGenerator.Json." + EnvironmentVariable("APPVEYOR_BUILD_VERSION") +".nupkg";
                
    NuGetPush(package, new NuGetPushSettings {
        Source = "https://nuget.org/",
        ApiKey = EnvironmentVariable("NUGET_API_KEY")
    });
});

Task("Default")
	.IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    Information("SimpleDataGenerator.Json building finished.");
});

RunTarget(target);