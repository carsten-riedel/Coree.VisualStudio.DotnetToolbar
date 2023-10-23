namespace Coree.VisualStudio.DotnetToolbar
{


    public class SolutionSettingsPublish
    {
        public bool PublishSolutionProject { get; set; } = true;
    }

    public class SolutionSettingsGeneral
    {
        public bool KillAllDotnetProcessBeforeExectue { get; set; } = false;
        public bool BlockNonSdkExecute { get; set; } = true;
        public bool NodeReuse { get; set; } = false;
    }

    public class SolutionSettings
    {
        public SolutionSettingsGeneral solutionSettingsGeneral { get; set; } = new SolutionSettingsGeneral();
        public SolutionSettingsPublish solutionSettingsPublish { get; set; } = new SolutionSettingsPublish();
    }
}