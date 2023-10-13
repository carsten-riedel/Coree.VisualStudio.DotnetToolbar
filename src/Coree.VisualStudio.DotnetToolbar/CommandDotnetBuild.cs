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
    internal sealed class CommandDotnetBuild
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4129;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7303216a-a2cb-4519-b645-a34ae1380a78");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        internal readonly MenuCommand MenuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDotnetBuild"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandDotnetBuild(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            MenuItem = new MenuCommand((s, e) => ExecuteAsync(s, e), menuCommandID);

            commandService.AddCommand(MenuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandDotnetBuild Instance
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
            // Switch to the main thread - the call to AddCommand in 's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandDotnetBuild(package, commandService);
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
            //await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await ServiceProvider.GetServiceAsync(typeof(DTE2)).ConfigureAwait(false);
            Window window = dte2.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            window.Activate();

            SolutionConfiguration2 configuration = (SolutionConfiguration2)dte2.Solution.SolutionBuild.ActiveConfiguration;

            var solutionFullName = ((Solution2)dte2.Solution).FullName;
            string slnfile = solutionFullName;
            string slndir = System.IO.Path.GetDirectoryName(slnfile);

            var projectInfos = await Helper.GetProjectInfosAsync(this.package);

            await OutputClearAsync();

            List<JoinableTask> _joinableTasks = new List<JoinableTask>();

            var process = new System.Diagnostics.Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "dotnet.exe";
            process.StartInfo.Arguments = $@"build ""{slnfile}"" --configuration {configuration.Name}";
            process.StartInfo.WorkingDirectory = $@"{slndir}";
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

            await Task.WhenAll(_joinableTasks.Select(jt => jt.Task));

            await OutputTaskItemStringExExampleAsync("Done");
        }

        /*
        // Create a Task but don't await it immediately
        Task myTask = Task.Run(() => MyNonAsyncFunction());

        // Do other work...

        // Later in the code, you can await the Task when you actually need it to be complete
        await myTask;
        */
        /*
         *            var sen =(System.ComponentModel.Design.MenuCommand)sender;
            sen.Enabled = false;
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var dte2 = await ServiceProvider.GetServiceAsync(typeof(DTE2)).ConfigureAwait(false) as DTE2;

            List<ProjectInfo> projectInfos = new List<ProjectInfo>();

            Projects solProjects = dte2.Solution.Projects;
            //string solutionDir = System.IO.Path.GetDirectoryName(dte2.Solution.FullName);

            foreach (Project item in solProjects)
            {
                var ProjectInfoItem = new ProjectInfo()
                {
                    FullProjectFileName = (string)item.Properties.Item("FullProjectFileName").Value,
                    TargetFrameworks = (string)item.Properties.Item("TargetFrameworks").Value,
                    FriendlyTargetFramework = (string)item.Properties.Item("FriendlyTargetFramework").Value,
                    FullPath = (string)item.Properties.Item("FullPath").Value
                };

                if (ProjectInfoItem.TargetFrameworks.Split(';').Length > 0)
                {
                    ProjectInfoItem.TargetFrameworksList.AddRange(ProjectInfoItem.TargetFrameworks.Split(';'));
                }

                if (ProjectInfoItem.TargetFrameworksList.Count == 0)
                {
                    ProjectInfoItem.TargetFrameworksList.Add(ProjectInfoItem.FriendlyTargetFramework);
                }

                projectInfos.Add(ProjectInfoItem);

                foreach (Property items in item.Properties)
                {  //FriendlyTargetFramework
                    Debug.WriteLine($@"{items.Name},{items.Value}");
                }
            }

        SolutionConfiguration2 configuration = (SolutionConfiguration2)dte2.Solution.SolutionBuild.ActiveConfiguration;
        Debug.WriteLine($@"ActiveConfiguration: {configuration.Name}{configuration.PlatformName}");

            foreach (var item in projectInfos)
            {
                dte2.ExecuteCommand("Tools.Shell", $@"/o /c /dir:{item.FullPath} dotnet.exe build {item.FullProjectFileName} --configuration {configuration.Name}");
                await OutputTaskItemStringExExampleAsync($@"Tools.Shell ""/o /c /dir:{item.FullPath} dotnet.exe build {item.FullProjectFileName} --configuration {configuration.Name}""");
        await OutputTaskItemStringExExampleAsync($@"See Command window.");
    }

    Window window = dte2.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
    window.Activate();

            sen.Enabled = true;
        }
*/

        private async Task OutputTaskItemStringExExampleAsync(string buildMessage)
        {
            //await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var dte2 = await package.GetServiceAsync(typeof(DTE2)).ConfigureAwait(false) as DTE2;

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
            //await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var dte2 = await package.GetServiceAsync(typeof(DTE2)).ConfigureAwait(false) as DTE2;

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