using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.CodeDom;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

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
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetPublish.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDropDown.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetNugetPush.InitializeAsync(this);
            Instance = this;
        }


        public CoreeVisualStudioDotnetToolbarPackage()
        {
            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterOpenSolution += (sender, e) => { _ = Task.Run(() => SolutionEvents_OnAfterOpenSolutionAsync(sender, e)); };
            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (sender, e) => { _ = Task.Run(() => SolutionEvents_OnBeforeCloseSolutionAsync(sender, e)); };
        }

        private async Task SolutionEvents_OnBeforeCloseSolutionAsync(object sender, EventArgs e)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE2 dte2 = (DTE2)await this.GetServiceAsync(typeof(DTE));
            dte2.WindowActivateEx();
            dte2.AddText("Build", "DotnetToolbar OnBeforeCloseSolution");
        }

        private async Task SolutionEvents_OnAfterOpenSolutionAsync(object sender, OpenSolutionEventArgs e)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE2 dte2 = (DTE2)await this.GetServiceAsync(typeof(DTE));
            dte2.WindowActivateEx();
            dte2.AddText("Build", "DotnetToolbar OnAfterOpenSolution");
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}