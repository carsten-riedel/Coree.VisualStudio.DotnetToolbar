using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;
using EnvDTE80;
using System.IO.Packaging;
using Microsoft.VisualStudio.Shell.Events;

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
    public sealed class CoreeVisualStudioDotnetToolbarPackage : AsyncPackage, IVsSolutionEvents
    {
        /// <summary>
        /// Coree.VisualStudio.DotnetToolbarPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "863aef23-089f-44d7-ba5c-e509e35cd199";
        
        private DTE2 dte2;
        private CancellationToken cancellationToken;
        private IVsSolution _solution;
        private uint _hSolutionEvents = uint.MaxValue;

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            this.cancellationToken = cancellationToken;
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetBuild.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetPack.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetPublish.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDropDown.InitializeAsync(this);
            await Coree.VisualStudio.DotnetToolbar.CommandDotnetNugetPush.InitializeAsync(this);
            dte2 = (DTE2)await GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            await AdviseSolutionEventsAsync(cancellationToken);
        }

        protected override async void Dispose(bool disposing)
        {
            await UnadviseSolutionEventsAsync(this.cancellationToken);
            base.Dispose(disposing);
        }

        private async Task AdviseSolutionEventsAsync(CancellationToken cancellationToken)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await UnadviseSolutionEventsAsync(cancellationToken);

            _solution = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;

            _solution?.AdviseSolutionEvents(this, out _hSolutionEvents);
        }

        private async Task UnadviseSolutionEventsAsync(CancellationToken cancellationToken)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            if (_solution == null) return;
            if (_hSolutionEvents != uint.MaxValue)
            {
                _solution.UnadviseSolutionEvents(_hSolutionEvents);
                _hSolutionEvents = uint.MaxValue;
            }

            _solution = null;
        }

        #region Implementation of IVsSolutionEvents

        int IVsSolutionEvents.OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            CommandDotnetBuild.Instance.MenuItem.Enabled = true;
            CommandDotnetPack.Instance.MenuItem.Enabled = true;
            CommandDotnetPublish.Instance.MenuItem.Enabled = true;

            Task.Run(async () => await OutputAsync("DotnetToolbar enabled."));

            Trace.WriteLine("DotnetToolbar OnAfterOpenSolution", "VSTestPackage1");
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnBeforeCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        int IVsSolutionEvents.OnAfterCloseSolution(object pUnkReserved)
        {
            CommandDotnetBuild.Instance.MenuItem.Enabled = false;
            CommandDotnetPack.Instance.MenuItem.Enabled = false;
            CommandDotnetPublish.Instance.MenuItem.Enabled = false;
            Task.Run(async () => await OutputAsync("DotnetToolbar disabled."));
            return VSConstants.S_OK;
        }

        #endregion

        private async Task OutputAsync(string buildMessage)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
       

            Window window = dte2.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            window.Activate();

            EnvDTE.OutputWindowPanes panes = dte2.ToolWindows.OutputWindow.OutputWindowPanes;
            foreach (EnvDTE.OutputWindowPane pane in panes)
            {
                if (pane.Name.Contains("Build"))
                {
                    pane.OutputString(buildMessage + "\n");
                    pane.Activate();
                    return;
                }
            }
        }
    }
    #endregion Package Members
}

