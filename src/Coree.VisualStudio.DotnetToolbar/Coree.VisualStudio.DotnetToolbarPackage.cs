using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using EnvDTE;
using EnvDTE80;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using System;
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
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetBuild.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetPack.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetNugetPush.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetPublish.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetClean.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDeleteBinObj.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandSettings.InitializeAsync(this);


            bool isSolutionLoaded = await IsSolutionLoadedAsync();

            if (isSolutionLoaded)
            {
                await SolutionEvents_OnAfterOpenSolutionAsync();
            }

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterOpenSolution += (sender, e) => { _ = Task.Run(() => SolutionEvents_OnAfterOpenSolutionAsync(sender, e)); };

            //await Coree.VisualStudio.DotnetToolbar.CommandDropDown.InitializeAsync(this);
            Instance = this;
        }

        //Try later on https://github.com/madskristensen/SolutionLoadSample
        public CoreeVisualStudioDotnetToolbarPackage()
        {
            //Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterOpenSolution += (sender, e) => { _ = Task.Run(() => SolutionEvents_OnAfterOpenSolutionAsync(sender, e)); };
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
            DTE2 dte2 = (DTE2)await this.GetServiceAsync(typeof(DTE));
            dte2.WindowActivateEx();
            dte2.AddText("Build", "DotnetToolbar OnBeforeCloseSolution");
            CommandSettings.Instance.MenuItem.Enabled = false;
            CommandDotnetBuild.Instance.MenuItem.Enabled = false;
            CommandDotnetPack.Instance.MenuItem.Enabled = false;
            CommandDotnetPublish.Instance.MenuItem.Enabled = false;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = false;
            CommandDotnetClean.Instance.MenuItem.Enabled = false;
        }

        public SolutionSettings Settings { get; set; }
        public string SettingsFileName { get; set; }

        private async Task SolutionEvents_OnAfterOpenSolutionAsync(object sender =null, OpenSolutionEventArgs e = null)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE2 dte2 = (DTE2)await this.GetServiceAsync(typeof(DTE));
            dte2.WindowActivateEx();
            dte2.AddText("Build", "DotnetToolbar OnAfterOpenSolution");

            var solinfo = await this.GetSolutionAsync();
            var solutionProperties = await this.GetSolutionPropertiesAsync();

            SettingsFileName = $@"{UserLocalDataPath}\{solutionProperties["Name"]}_{(string)solinfo.Globals["SolutionGuid"]}.json";

            bool Created = JsonHelper.CreateDefault<SolutionSettings>(SettingsFileName);
            if (Created == true)
            {
                dte2.AddText("Build", $"DotnetToolbar settings file created {SettingsFileName}");
            }

            Settings = JsonHelper.ReadFromFile<SolutionSettings>(SettingsFileName);

            dte2.AddText("Build", $"DotnetToolbar settings file loaded {SettingsFileName}");

            // Usage
            EnableMenuItemIfInstanceNotNull(CommandSettings.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetBuild.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetPack.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetPublish.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetNugetPush.Instance);
            EnableMenuItemIfInstanceNotNull(CommandDotnetClean.Instance);
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