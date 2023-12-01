using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Linq;

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandDotnetPublish : CommandBase
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4131;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7303216a-a2cb-4519-b645-a34ae1380a78");

        internal readonly MenuCommand MenuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDotnetPublish"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandDotnetPublish(AsyncPackage package, OleMenuCommandService commandService) : base(package, commandService)
        {
            CommandID menuCommandID = new CommandID(CommandSet, CommandId);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable VSTHRD110 // Observe result of async calls
            MenuItem = new MenuCommand((s, e) => ExecuteAsync(s, e), menuCommandID);
            MenuItem.Enabled = false;
#pragma warning restore VSTHRD110 // Observe result of async calls
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            commandService.AddCommand(MenuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandDotnetPublish Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command3's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandDotnetPublish(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        /*
        internal override async System.Threading.Tasks.Task ExecuteAsync(object sender, EventArgs e)
        {
            CommandDotnetBuild.Instance.MenuItem.Enabled = false;
            CommandDotnetPack.Instance.MenuItem.Enabled = false;
            CommandDotnetPublish.Instance.MenuItem.Enabled = false;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = false;
            CommandDotnetClean.Instance.MenuItem.Enabled = false;
            CommandSettings.Instance.MenuItem.Enabled = false;
            CommandDeleteBinObj.Instance.MenuItem.Enabled = false;

            await StartDotNetProcessAsync();

            CommandDotnetBuild.Instance.MenuItem.Enabled = true;
            CommandDotnetPack.Instance.MenuItem.Enabled = true;
            CommandDotnetPublish.Instance.MenuItem.Enabled = true;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = true;
            CommandDotnetClean.Instance.MenuItem.Enabled = true;
            CommandSettings.Instance.MenuItem.Enabled = true;
            CommandDeleteBinObj.Instance.MenuItem.Enabled = true;
        }
        */

        internal override async System.Threading.Tasks.Task StartDotNetProcessAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);
            DTE2 dte2 = (DTE2)await ServiceProvider.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);

            await WindowActivateAsync(EnvDTE.Constants.vsWindowKindOutput);

            var activeConfiguration = await GetActiveSolutionConfigurationAsync();
            if (activeConfiguration == null)
            {
                await PaneWriteLineAsync("Can't not determinate a active solution configuration.");
                return;
            }

            string slnfile = await GetSolutionFileNameAsync();
            string slndir = System.IO.Path.GetDirectoryName(slnfile);

            await PaneClearAsync();

            if (CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.KillAllDotnetProcessBeforeExectue)
            {
                (new System.Diagnostics.Process()).AllDontNetKill("dotnet");
            }

            var projectInfos = (await GetProjectInfosAsync()).Where(e => e.SolutionDirectoryItemNameLocationExists == true).ToList();

            if (CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.BlockNonSdkExecute)
            {
                bool found = false;
                foreach (var item in projectInfos)
                {
                    if (item.IsSdkStyle == false)
                    {
                        await PaneWriteLineAsync("-------------------------------------------------------------------------------");
                        await PaneWriteLineAsync($"Non SDK style project file {item.VSProjectLocation} !");
                        await PaneWriteLineAsync("-------------------------------------------------------------------------------");
                        found = true;
                    }
                }
                if (found)
                {
                    await PaneWriteLineAsync("Done");
                    return;
                }
            }

            bool done = false;

            if (CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject)
            {
                done = true;
                await ExecuteProcessAsync("dotnet.exe", $@"--version", $@"{slndir}");
                await ExecuteProcessAsync("dotnet.exe", $@"publish ""{slnfile}"" --configuration {activeConfiguration.Configuration} {CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.AdditionalCommandlineArguments}", $@"{slndir}");
            }
            else
            {
                foreach (var projectInfo in projectInfos)
                {
                    if (projectInfo.IsSdkStyle == true)
                    {
                        foreach (var targetFramework in projectInfo.VSProjectPropertyTargetFrameworksList)
                        {
                            done = true;
                            await ExecuteProcessAsync("dotnet.exe", $@"--version", $@"{slndir}");
                            await ExecuteProcessAsync("dotnet.exe", $@"publish ""{projectInfo.VSProjectPropertyLocation}"" --configuration {activeConfiguration.Configuration} --framework {targetFramework} {CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.AdditionalCommandlineArguments}", $@"{projectInfo.VSProjectPropertyPath}");
                        }
                    }
                }
            }

            if (!done)
            {
                await PaneWriteLineAsync("Non SDK Style project ?");
            }

            await PaneWriteLineAsync("Done");
        }
    }
}