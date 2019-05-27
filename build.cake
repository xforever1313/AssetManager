string target = Argument( "target", "debug" );

const string version = "0.1.0"; // This is the version.  Update before releasing.

DirectoryPath rootDirectory = new DirectoryPath( "." );
DirectoryPath outputPackages = rootDirectory.Combine( "dist" );

DotNetCoreMSBuildSettings msBuildSettings = new DotNetCoreMSBuildSettings();
msBuildSettings.WithProperty( "Version", version )
    .WithProperty( "AssemblyVersion", version )
    .SetMaxCpuCount( System.Environment.ProcessorCount )
    .WithProperty( "FileVersion", version );

Task( "debug" )
.Does(
    () => 
    {
        DotNetCoreBuildSettings settings = new DotNetCoreBuildSettings
        {
            Configuration = "Debug",
            MSBuildSettings = msBuildSettings
        };
        DotNetCoreBuild( "./AssetManager.sln", settings );
    }
)
.Description( "Compiles AssetManager for Debug." );

Task( "release" )
.Does(
    () =>
    {
        DotNetCoreBuildSettings settings = new DotNetCoreBuildSettings
        {
            Configuration = "Release",
            MSBuildSettings = msBuildSettings
        };
        DotNetCoreBuild( "./AssetManager.sln", settings );
    }
)
.Description( "Compiles AssetManager for Release." );

Task( "unit_test" )
.Does(
    () =>
    {
        DotNetCoreTestSettings settings = new DotNetCoreTestSettings
        {
            Configuration = "Debug",
            NoBuild = true,
            NoRestore = true
        };

        DotNetCoreTest( "./AssetManager.sln", settings );
    }
)
.IsDependentOn( "debug" )
.Description( "Runs the Unit Tests.");

Task( "publish_windows" )
.Does(
    () =>
    {
        const string runTime = "win-x64";
        DoPublish( runTime );
    }
)
.IsDependentOn( "release" )
.Description( "Publishes the app for Windows.");

Task( "publish_linux" )
.Does(
    () =>
    {
        const string runTime = "linux-x64";
        DoPublish( runTime );
    }
)
.IsDependentOn( "release" )
.Description( "Publishes the app for Linux.");

void DoPublish( string runtime )
{
    PublishProject( runtime, "AssetManager.Sqlite", "./AssetManager.Sqlite/AssetManager.Sqlite.csproj" );
    PublishProject( runtime, "AssetManager.Web", "./AssetManager.Web/AssetManager.Web.csproj" );
}

void PublishProject( string runtime, string projectName, string csproj )
{
    DirectoryPath distFolder = outputPackages.Combine( runtime );
    distFolder = distFolder.Combine( projectName );
    EnsureDirectoryExists( distFolder );
    CleanDirectory( distFolder );

    DotNetCorePublishSettings settings = new DotNetCorePublishSettings
    {
        Configuration = "Release",
        OutputDirectory = distFolder,
        Runtime = runtime
    };

    DotNetCorePublish( csproj, settings );
}

Task( "publish_all" )
.Does(
    () =>
    {

    }
)
.IsDependentOn( "publish_windows" )
.IsDependentOn( "publish_linux" )
.Description( "Packages the app for all platforms.");

RunTarget( target );