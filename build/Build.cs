using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Docker;
using static Nuke.Common.Tools.Docker.DockerTasks;

[DotNetVerbosityMapping]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    [Solution]
    readonly Solution Solution;

    [GitRepository] readonly GitRepository GitRepository;
    [CI] readonly GitHubActions GitHubActions;

    [Parameter("GitHub Token for authentication with the GitHub registry")] readonly string GitHubToken;
    
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";

    public static int Main () => Execute<Build>(x => x.Test);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            DotNetClean();
            SourceDirectory.GlobDirectories("**/bin", "**/obj").DeleteDirectories();
            TestsDirectory.GlobDirectories("**/bin", "**/obj").DeleteDirectories();
            OutputDirectory.CreateOrCleanDirectory();

        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));

        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());

        });


    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target PushToGitHubRegistry => _ => _
        .DependsOn(Test) // Stellt sicher, dass der Code kompiliert ist
        .Requires(() => GitHubToken) // Das Target kann nur mit einem Token ausgeführt werden
        .OnlyWhenStatic(() => IsServerBuild) // Wird nur auf einem CI-Server ausgeführt
        .Executes(() =>
        {
            // Der Image-Name wird aus der Repository-ID zusammengesetzt (z.B. user/repo)
            var imageName = $"ghcr.io/{GitRepository.Identifier.ToLowerInvariant()}:latest";

            // Bei der GitHub Container Registry anmelden
            DockerLogin(s => s
                .SetServer("ghcr.io")
                .SetUsername(GitHubActions.Actor)
                .SetPassword(GitHubToken)
                .DisableProcessOutputLogging()); // Verhindert, dass das Token im Log erscheint

            // Das Docker-Image bauen
            DockerBuild(s => s
                .SetFile(RootDirectory / "Dockerfile") // Pfad zum Dockerfile
                .SetTag(imageName)
                .SetPath("."));

            // Das Image pushen
            DockerPush(s => s
                .SetName(imageName));
        });
}