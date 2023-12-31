﻿namespace Coree.VisualStudio.DotnetToolbar
{
    public class SolutionSettings
    {
        public SolutionSettingsGeneral SolutionSettingsGeneral { get; set; } = new SolutionSettingsGeneral();
        public SolutionSettingsBuild SolutionSettingsBuild { get; set; } = new SolutionSettingsBuild();
        public SolutionSettingsPack SolutionSettingsPack { get; set; } = new SolutionSettingsPack();
        public SolutionSettingsNugetPush SolutionSettingsNugetPush { get; set; } = new SolutionSettingsNugetPush();
        public SolutionSettingsTest SolutionSettingsTest { get; set; } = new SolutionSettingsTest();
        public SolutionSettingsPublish SolutionSettingsPublish { get; set; } = new SolutionSettingsPublish();
        public SolutionSettingsClean SolutionSettingsClean { get; set; } = new SolutionSettingsClean();
        public SolutionSettingsConfirmDialog SolutionSettingsConfirmDialog { get; set; } = new SolutionSettingsConfirmDialog();
    }

    public class SolutionSettingsGeneral
    {
        public bool KillAllDotnetProcessBeforeExectue { get; set; } = false;
        public bool BlockNonSdkExecute { get; set; } = true;
        public bool WriteDotnetGlobalJson { get; set; } = false;
    }

    public class SolutionSettingsBuild
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo -nodeReuse:true --property:Sample=Value";
    }

    public class SolutionSettingsPack
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo --verbosity:n";
    }

    public class SolutionSettingsNugetPush
    {
        public bool HideApiKeyInOutput { get; set; } = true;

        public int LastPackageSourceIndex { get; set; } = -1;
    }

    public class SolutionSettingsTest
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo --verbosity:n";
    }

    public class SolutionSettingsPublish
    {
        public bool PublishSolutionProject { get; set; } = true;

        public string AdditionalCommandlineArguments { get; set; } = "--nologo --verbosity:n";
    }

    public class SolutionSettingsClean
    {
        public string AdditionalCommandlineArguments { get; set; } = "--nologo --verbosity:n";
    }

    public class SolutionSettingsConfirmDialog
    {
        public bool DisableConfirmDialog { get; set; } = false;
    }
}