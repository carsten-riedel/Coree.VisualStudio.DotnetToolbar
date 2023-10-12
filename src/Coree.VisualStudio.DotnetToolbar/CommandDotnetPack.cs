﻿using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandDotnetPack
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4130;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7303216a-a2cb-4519-b645-a34ae1380a78");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        internal readonly MenuCommand menuItem;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDotnetPack"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandDotnetPack(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            menuItem = new MenuCommand((s, e) => ExecuteAsync(s, e), menuCommandID);
            commandService.AddCommand(menuItem);
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
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
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

        private class ProjectInfo
        {
            public string FullProjectFileName { get; set; } = String.Empty;
            public string FullPath { get; set; } = String.Empty;
            public string TargetFrameworks { get; set; } = String.Empty;
            public List<string> TargetFrameworksList { get; set; } = new List<string>();
            public string FriendlyTargetFramework { get; set; } = String.Empty;
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async Task ExecuteAsync(object sender, EventArgs e)
        {
            CommandDotnetBuild.Instance.menuItem.Enabled = false;
            CommandDotnetPack.Instance.menuItem.Enabled = false;
            CommandDotnetPublish.Instance.menuItem.Enabled = false;

            Task myTask = Task.Run(() => StartDotNetProcessAsync());
            await myTask;

            CommandDotnetBuild.Instance.menuItem.Enabled = true;
            CommandDotnetPack.Instance.menuItem.Enabled = true;
            CommandDotnetPublish.Instance.menuItem.Enabled = true;
        }

        private async Task StartDotNetProcessAsync()
        {
            //await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await ServiceProvider.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            Window window = dte2.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            window.Activate();

            SolutionConfiguration2 configuration = (SolutionConfiguration2)dte2.Solution.SolutionBuild.ActiveConfiguration;

            var projectInfos = await Helper.GetProjectInfosAsync(this.package);

            await OutputClearAsync();

            List<JoinableTask> _joinableTasks = new List<JoinableTask>();

            foreach (var projectInfo in projectInfos)
            {
                var process = new System.Diagnostics.Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = "dotnet.exe";
                process.StartInfo.Arguments = $@"pack ""{projectInfo.FullProjectFileName}"" --configuration {configuration.Name}";
                process.StartInfo.WorkingDirectory = $@"{projectInfo.FullPath}";
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                await OutputTaskItemStringExExampleAsync("-------------------------------------------------------------------------------");
                await OutputTaskItemStringExExampleAsync(process.StartInfo.GetProcessStartInfoCommandline());
                await OutputTaskItemStringExExampleAsync("-------------------------------------------------------------------------------");
                process.Start();
                process.OutputDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await OutputTaskItemStringExExampleAsync(e.Data); } catch (Exception ex) { /* Handle the exception */ } }); _joinableTasks.Add(joinableTask); };
                process.ErrorDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await OutputTaskItemStringExExampleAsync(e.Data); } catch (Exception ex) { /* Handle the exception */ } }); _joinableTasks.Add(joinableTask); };
                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();
            }
            
            await Task.WhenAll(_joinableTasks.Select(jt => jt.Task));

            await OutputTaskItemStringExExampleAsync("Done");
        }

        private async Task OutputTaskItemStringExExampleAsync(string buildMessage)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var dte2 = await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) as DTE2;

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

        private async Task OutputClearAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var dte2 = await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) as DTE2;

            EnvDTE.OutputWindowPanes panes = dte2.ToolWindows.OutputWindow.OutputWindowPanes;
            foreach (EnvDTE.OutputWindowPane pane in panes)
            {
                if (pane.Name.Contains("Build"))
                {
                    pane.Clear();
                    return;
                }
            }
        }
    }
}