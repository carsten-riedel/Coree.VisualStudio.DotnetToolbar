namespace Coree.VisualStudio.DotnetToolbar
{
    public class SolutionSettings
    {
        public SolutionSettingsGeneral SolutionSettingsGeneral { get; set; } = new SolutionSettingsGeneral();
        public SolutionSettingsBuild SolutionSettingsBuild { get; set; } = new SolutionSettingsBuild();
        public SolutionSettingsPack SolutionSettingsPack { get; set; } = new SolutionSettingsPack();
        public SolutionSettingsNugetPush SolutionSettingsNugetPush { get; set; } = new SolutionSettingsNugetPush();
        public SolutionSettingsPublish SolutionSettingsPublish { get; set; } = new SolutionSettingsPublish();
        public SolutionSettingsClean SolutionSettingsClean { get; set; } = new SolutionSettingsClean();
    }

    public class SolutionSettingsGeneral
    {
        public bool KillAllDotnetProcessBeforeExectue { get; set; } = false;
        public bool BlockNonSdkExecute { get; set; } = true;
    }
    /*
         <UseWindowsForms>true</UseWindowsForms>
    <DebugType>embedded</DebugType>
    <ApplicationManifest>Properties\app.manifest</ApplicationManifest>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  </PropertyGroup>
  
  <PropertyGroup>
    <PublishDir>bin\Publish\$(Configuration)\$(TargetFramework)\$(RuntimeIdentifier)</PublishDir>
    <SelfContained>false</SelfContained>
	  <PublishSingleFile>true</PublishSingleFile>
    <IsPackable>false</IsPackable>
  </PropertyGroup>
     */
    public class SolutionSettingsBuild
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo --property:Sample=Value -nodeReuse:true -maxcpucount:1";
    }

    public class SolutionSettingsPack
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo --force";
    }


    public class SolutionSettingsNugetPush
    {
        public bool HideApiKeyInOutput { get; set; } = true;
    }

    public class SolutionSettingsPublish
    {
        public bool PublishSolutionProject { get; set; } = true;

        public string AdditionalCommandlineArguments { get; set; } = "--nologo --property:SelfContained=false --property:PublishSingleFile=true --property:RuntimeIdentifier=win-x64 --property:OutputType=Exe --property:DebugType=embedded";
    }

    public class SolutionSettingsClean
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo --verbosity:n";
    }
}