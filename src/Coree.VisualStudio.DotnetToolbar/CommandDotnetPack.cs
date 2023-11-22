using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;
using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using System.Linq;

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandDotnetPack : CommandBase
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4130;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7303216a-a2cb-4519-b645-a34ae1380a78");

        internal readonly MenuCommand MenuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDotnetPack"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandDotnetPack(AsyncPackage package, OleMenuCommandService commandService) : base(package, commandService)
        {
            var menuCommandID = new CommandID(CommandSet, CommandId);
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
        public static CommandDotnetPack Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command2's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandDotnetPack(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        /*
        internal override async Task ExecuteAsync(object sender, EventArgs e)
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
        }*/

        internal override async Task StartDotNetProcessAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);
            DTE2 dte2 = (DTE2)await ServiceProvider.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);

            await WindowActivateAsync(EnvDTE.Constants.vsWindowKindOutput);

            var activeConfiguration = await this.GetActiveSolutionConfigurationAsync();
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

            if (CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.BlockNonSdkExecute)
            {
                var projectInfos = (await GetProjectInfosAsync()).Where(e => e.HasProjectFile == true).ToList();

                bool found = false;
                foreach (var item in projectInfos)
                {
                    if (item.IsSdkStyle == false)
                    {
                        await PaneWriteLineAsync("-------------------------------------------------------------------------------");
                        await PaneWriteLineAsync($"Non SDK style project file {item.File} !");
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


            await ExecuteProcessAsync("dotnet.exe", $@"--version", $@"{slndir}");
            await ExecuteProcessAsync("dotnet.exe", $@"pack ""{slnfile}"" --configuration {activeConfiguration.Configuration} {CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPack.AdditionalCommandlineArguments}", $@"{slndir}");

            await PaneWriteLineAsync("Done");
        }
    }
}