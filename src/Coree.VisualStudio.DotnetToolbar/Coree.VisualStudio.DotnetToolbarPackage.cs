using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using EnvDTE;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids.EmptySolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionOpening_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class CoreeVisualStudioDotnetToolbarPackage : AsyncPackage
    {
        /// <summary>
        /// Coree.VisualStudio.DotnetToolbarPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "863aef23-089f-44d7-ba5c-e509e35cd199";

        public static CoreeVisualStudioDotnetToolbarPackage Instance
        {
            get;
            private set;
        }

        public SolutionSettings Settings { get; set; }

        public string SettingsFileName { get; set; }

        public string ExtensionDirectory { get; set; }

        public string ExtensionsRootDirectory { get; set; }

        public string SolutionGuid { get; set; }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            //this.cancellationToken = cancellationToken;
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await CommandDotnetBuild.InitializeAsync(this);
            await CommandDotnetPack.InitializeAsync(this);
            await CommandDotnetNugetPush.InitializeAsync(this);
            await CommandDotnetTest.InitializeAsync(this);
            await CommandDotnetPublish.InitializeAsync(this);
            await CommandDotnetClean.InitializeAsync(this);
            await CommandDeleteBinObj.InitializeAsync(this);
            await CommandSettings.InitializeAsync(this);
            //await CommandDropDown.InitializeAsync(this);

            bool isSolutionLoaded = await IsSolutionLoadedAsync();

            if (isSolutionLoaded)
            {
                await SolutionEvents_OnAfterOpenSolutionAsync(null, new OpenSolutionEventArgs(false));
            }

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterOpenSolution += (sender, e) => { _ = Task.Run(() => SolutionEvents_OnAfterOpenSolutionAsync(sender, e)); };

            Instance = this;
        }

        public CoreeVisualStudioDotnetToolbarPackage()
        {
            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (sender, e) => { _ = Task.Run(() => SolutionEvents_OnBeforeCloseSolutionAsync(sender, e)); };
        }

        private async Task<bool> IsSolutionLoadedAsync()
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync();
            var solService = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;

            ErrorHandler.ThrowOnFailure(solService.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object value));

            return value is bool isSolOpen && isSolOpen;
        }

        private async Task SolutionEvents_OnBeforeCloseSolutionAsync(object sender, EventArgs e)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync();
            await this.PaneClearAsync("DotnetToolbar");
            await this.PaneWriteLineAsync("Solution is now closed.", "DotnetToolbar");

            CommandSettings.Instance.MenuItem.Enabled = false;
            CommandDotnetBuild.Instance.MenuItem.Enabled = false;
            CommandDotnetPack.Instance.MenuItem.Enabled = false;
            CommandDotnetPublish.Instance.MenuItem.Enabled = false;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = false;
            CommandDotnetClean.Instance.MenuItem.Enabled = false;
            CommandDeleteBinObj.Instance.MenuItem.Enabled = false;
            CommandDotnetTest.Instance.MenuItem.Enabled = false;
        }

        private async Task SolutionEvents_OnAfterOpenSolutionAsync(object sender = null, OpenSolutionEventArgs e = null)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync();

            await this.PaneClearAsync($@"DotnetToolbar");
            await this.PaneWriteLineAsync($@"DotnetToolbar {await this.GetVsixVersionAsync()}", "DotnetToolbar");
            await this.PaneWriteLineAsync("Solution is now open.", "DotnetToolbar");
            await this.PaneWriteLineAsync("Notice! Setting files in 0.x.x are work-in-progress and subject of changes and may reset to defaults.", "DotnetToolbar");

            Solution solinfo = (Solution)await this.GetSolutionAsync();
            Dictionary<string, string> solutionProperties = await this.GetSolutionPropertiesAsync();

            if (e.IsNewSolution)
            {
                SolutionGuid = GetSolutionGuid($"{solutionProperties["Path"]}");
            }
            else
            {
                SolutionGuid = $"{(string)solinfo.Globals["SolutionGuid"]}";
            }

            var obsoleteAssemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var obsoleteExtensionDirectory = System.IO.Path.GetDirectoryName(obsoleteAssemblyLocation);
            var obsoleteExtensionsRootDirectory = System.IO.Path.GetDirectoryName(obsoleteExtensionDirectory);

            var thisName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var ExtensionDirectory = System.IO.Path.Combine(this.UserLocalDataPath, "VSIXSettings", thisName);
            if (!System.IO.Directory.Exists(ExtensionDirectory))
            {
                System.IO.Directory.CreateDirectory(ExtensionDirectory);
            }

            SettingsFileName = $@"{ExtensionDirectory}\{solutionProperties["Name"]}_{SolutionGuid}.json";

            await this.PaneWriteLineAsync($@"", "DotnetToolbar");
            await this.PaneWriteLineAsync($@"Migration Notice: Your previous settings are backed up in the *.json and *.json.backup files located in the ""{obsoleteExtensionsRootDirectory}"" directory. Please review them if necessary.", "DotnetToolbar");
            await this.PaneWriteLineAsync($@"All current settings are now stored in the *.json and *.json.backup files within the ""{ExtensionDirectory}"" directory. Refer to these for your latest configurations.", "DotnetToolbar");

            bool Created = JsonHelper.CreateDefault<SolutionSettings>(SettingsFileName);
            /*
            if (Created == true)
            {
                await this.PaneWriteLineAsync($"DotnetToolbar: Default settings file created {SettingsFileName}", "DotnetToolbar");
            }
            */

            Settings = JsonHelper.TryReadFromFile<SolutionSettings>(SettingsFileName);

            /*
            await this.PaneWriteLineAsync($"DotnetToolbar: Settings file loaded {SettingsFileName}", "DotnetToolbar");
            */

            EnableMenuItemIfInstanceNotNull(CommandSettings.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetBuild.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetPack.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetPublish.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetNugetPush.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetClean.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDeleteBinObj.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetTest.Instance);
        }

        private static string GetSolutionGuid(string filePath)
        {
            string line;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("SolutionGuid"))
                    {
                        int indexOfEquals = line.IndexOf("=");
                        string guidString = line.Substring(indexOfEquals + 1).Trim();
                        return guidString;
                    }
                }
            }

            return null;
        }

        private void EnableMenuItemIfInstanceNotNull(dynamic commandInstance)
        {
            try
            {
                if (commandInstance != null && commandInstance.MenuItem != null)
                {
                    commandInstance.MenuItem.Enabled = true;
                }
            }
            catch (RuntimeBinderException)
            {
                // Handle the case where MenuItem or Enabled does not exist.
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}