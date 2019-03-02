string target = Argument( "target", "build" );

const string version = "0.1.0"; // This is the version.  Update before releasing.

DotNetCoreMSBuildSettings msBuildSettings = new DotNetCoreMSBuildSettings();
msBuildSettings.WithProperty( "Version", version )
    .WithProperty( "AssemblyVersion", version )
    .SetMaxCpuCount( System.Environment.ProcessorCount )
    .WithProperty( "FileVersion", version );

Task( "build" )
.Does(
    () => 
    {
        DotNetCoreBuildSettings settings = new DotNetCoreBuildSettings
        {
            MSBuildSettings = msBuildSettings
        };
        DotNetCoreBuild( "./AssetManager.sln", settings );
    }
)
.Description( "Compiles AssetManager." );

RunTarget( target );