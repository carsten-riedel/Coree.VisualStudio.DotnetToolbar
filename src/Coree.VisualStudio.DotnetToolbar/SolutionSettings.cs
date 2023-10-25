namespace Coree.VisualStudio.DotnetToolbar
{
    public class SolutionSettings
    {
        public SolutionSettingsGeneral SolutionSettingsGeneral { get; set; } = new SolutionSettingsGeneral();
        public SolutionSettingsNugetPush SolutionSettingsNugetPush { get; set; } = new SolutionSettingsNugetPush();
        public SolutionSettingsPublish SolutionSettingsPublish { get; set; } = new SolutionSettingsPublish();
    }

    public class SolutionSettingsGeneral
    {
        public bool KillAllDotnetProcessBeforeExectue { get; set; } = false;
        public bool BlockNonSdkExecute { get; set; } = true;
        public bool NodeReuse { get; set; } = false;
    }

    public class SolutionSettingsPublish
    {
        public bool PublishSolutionProject { get; set; } = true;
    }

    public class SolutionSettingsNugetPush
    {
        public bool HideApiKeyInOutput { get; set; } = true;
    }
}