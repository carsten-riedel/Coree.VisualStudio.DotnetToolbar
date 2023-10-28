namespace Coree.VisualStudio.DotnetToolbar
{
    public class SolutionSettings
    {
        public SolutionSettingsGeneral SolutionSettingsGeneral { get; set; } = new SolutionSettingsGeneral();
        public SolutionSettingsBuild SolutionSettingsBuild { get; set; } = new SolutionSettingsBuild();
        public SolutionSettingsNugetPush SolutionSettingsNugetPush { get; set; } = new SolutionSettingsNugetPush();
        public SolutionSettingsPublish SolutionSettingsPublish { get; set; } = new SolutionSettingsPublish();
    }

    public class SolutionSettingsGeneral
    {
        public bool KillAllDotnetProcessBeforeExectue { get; set; } = false;
        public bool BlockNonSdkExecute { get; set; } = true;
        public bool NodeReuse { get; set; } = false;
    }

    public class SolutionSettingsBuild
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo";
    }


    public class SolutionSettingsNugetPush
    {
        public bool HideApiKeyInOutput { get; set; } = true;
    }

    public class SolutionSettingsPublish
    {
        public bool PublishSolutionProject { get; set; } = true;
    }
}