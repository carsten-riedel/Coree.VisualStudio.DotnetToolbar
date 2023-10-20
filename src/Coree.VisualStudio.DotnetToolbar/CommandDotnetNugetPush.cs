using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandDotnetNugetPush : CommandBase
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4134;

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
        private CommandDotnetNugetPush(AsyncPackage package, OleMenuCommandService commandService) : base(package, commandService)
        {
            CommandID menuCommandID = new CommandID(CommandSet, CommandId);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable VSTHRD110 // Observe result of async calls
            MenuItem = new MenuCommand((s, e) => ExecuteAsync(s, e), menuCommandID);
#pragma warning restore VSTHRD110 // Observe result of async calls
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            commandService.AddCommand(MenuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandDotnetNugetPush Instance
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
            Instance = new CommandDotnetNugetPush(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async System.Threading.Tasks.Task ExecuteAsync(object sender, EventArgs e)
        {
            CommandDotnetBuild.Instance.MenuItem.Enabled = false;
            CommandDotnetPack.Instance.MenuItem.Enabled = false;
            CommandDotnetPublish.Instance.MenuItem.Enabled = false;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = false;
            CommandDotnetClean.Instance.MenuItem.Enabled = false;

            await StartDotNetProcessAsync();

            CommandDotnetBuild.Instance.MenuItem.Enabled = true;
            CommandDotnetPack.Instance.MenuItem.Enabled = true;
            CommandDotnetPublish.Instance.MenuItem.Enabled = true;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = true;
            CommandDotnetClean.Instance.MenuItem.Enabled = true;
        }

        private async System.Threading.Tasks.Task StartDotNetProcessAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);
            DTE2 dte2 = (DTE2)await ServiceProvider.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);

            await WindowActivateAsync(Constants.vsWindowKindOutput);

            var configuration = await GetSolutionActiveConfigurationAsync();
            var solinfo = await GetSolutionAsync();
            var solinfox = await GetSolutionPropertiesAsync();

            await OutputWriteLineAsync(null, true);

            List<JoinableTask> _joinableTasks = new List<JoinableTask>();

            NugetPushDialog nugetPushDialog = new NugetPushDialog(Package.UserLocalDataPath, solinfo.FullName, solinfox["Name"], (string)solinfo.Globals["SolutionGuid"]);
            nugetPushDialog.ShowDialog();

            if (nugetPushDialog.nugetPushDialogResult == NugetPushDialog.NugetPushDialogResult.Cancel)
            {
                await OutputWriteLineAsync("dotnet nuget push closed.");
                return;
            }

            if (nugetPushDialog.nugetPushDialogResult == NugetPushDialog.NugetPushDialogResult.Close)
            {
                await OutputWriteLineAsync("dotnet nuget push canceled.");
                return;
            }

            if (String.IsNullOrEmpty(nugetPushDialog.PackageLocation))
            {
                await OutputWriteLineAsync("dotnet nuget push canceled. No package specified.");
                return;
            }

            var process = new System.Diagnostics.Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "dotnet.exe";
            process.StartInfo.Arguments = $@"nuget push ""{nugetPushDialog.SolutionDir}{Path.DirectorySeparatorChar}{nugetPushDialog.PackageLocation}"" --api-key {nugetPushDialog.ApiKey} --source {nugetPushDialog.Source} --skip-duplicate";
            process.StartInfo.WorkingDirectory = $@"{String.Empty}";
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            await OutputWriteLineAsync("-------------------------------------------------------------------------------");
            await OutputWriteLineAsync(process.StartInfo.GetProcessStartInfoCommandline());
            await OutputWriteLineAsync("-------------------------------------------------------------------------------");
            process.Start();
            process.OutputDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await OutputWriteLineAsync(e.Data); } catch (Exception ex) { Debug.WriteLine(ex.Message); } }); _joinableTasks.Add(joinableTask); };
            process.ErrorDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await OutputWriteLineAsync(e.Data); } catch (Exception ex) { Debug.WriteLine(ex.Message); } }); _joinableTasks.Add(joinableTask); };
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            await process.WaitForExitAsync();

            await System.Threading.Tasks.Task.WhenAll(_joinableTasks.Select(jt => jt.Task));

            await OutputWriteLineAsync("Done");
        }
    }
}