using EnvDTE;
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
            MenuItem = new MenuCommand((s, e) => ExecuteAsync(s, e), menuCommandID);
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
            CommandDotnetBuild.Instance.MenuItem.Enabled = false;
            CommandDotnetPack.Instance.MenuItem.Enabled = false;
            CommandDotnetPublish.Instance.MenuItem.Enabled = false;

            Task myTask = Task.Run(() => StartDotNetProcessAsync());
            await myTask;

            CommandDotnetBuild.Instance.MenuItem.Enabled = true;
            CommandDotnetPack.Instance.MenuItem.Enabled = true;
            CommandDotnetPublish.Instance.MenuItem.Enabled = true;
        }

        private async Task StartDotNetProcessAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE dte = (DTE)await ServiceProvider.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            DTE2 dte2 = (DTE2)await ServiceProvider.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            Window window = dte2.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            window.Activate();

            SolutionConfiguration2 configuration = (SolutionConfiguration2)dte2.Solution.SolutionBuild.ActiveConfiguration;

            var configurationx = (SolutionBuild2)dte2.Solution.SolutionBuild;

            var solutionFullName = ((Solution2)dte2.Solution).FullName;
            string slnfile = solutionFullName;
            string slndir = System.IO.Path.GetDirectoryName(slnfile);

            var projectInfos = await Helper.GetProjectInfosAsync(this.package);

            await OutputWriteLineAsync(null, true);

            List<JoinableTask> _joinableTasks = new List<JoinableTask>();

            var process = new System.Diagnostics.Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "dotnet.exe";
            process.StartInfo.Arguments = $@"pack ""{slnfile}"" --configuration {configuration.Name} --force";
            process.StartInfo.WorkingDirectory = $@"{slndir}";
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            await OutputWriteLineAsync("-------------------------------------------------------------------------------");
            await OutputWriteLineAsync(process.StartInfo.GetProcessStartInfoCommandline());
            await OutputWriteLineAsync("-------------------------------------------------------------------------------");
            process.Start();
            process.OutputDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await OutputWriteLineAsync(e.Data); } catch (Exception ex) { /* Handle the exception */ } }); _joinableTasks.Add(joinableTask); };
            process.ErrorDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await OutputWriteLineAsync(e.Data); } catch (Exception ex) { /* Handle the exception */ } }); _joinableTasks.Add(joinableTask); };
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();

            await Task.WhenAll(_joinableTasks.Select(jt => jt.Task));

            await OutputWriteLineAsync("Done");
        }
    }
}