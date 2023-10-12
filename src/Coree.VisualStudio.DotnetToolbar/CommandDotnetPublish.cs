using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandDotnetPublish
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4131;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7303216a-a2cb-4519-b645-a34ae1380a78");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDotnetPublish"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandDotnetPublish(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
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
            // Switch to the main thread - the call to AddCommand in Command3's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandDotnetPublish(package, commandService);
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
        private async void Execute(object sender, EventArgs e)
        {
            var dte2 = await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) as DTE2;
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();

            Projects solProjects = dte2.Solution.Projects;
            string solutionDir = System.IO.Path.GetDirectoryName(dte2.Solution.FullName);

            foreach (Project item in solProjects)
            {

                var ProjectInfoItem = new ProjectInfo()
                {
                    FullProjectFileName = (string)item.Properties.Item("FullProjectFileName").Value,
                    TargetFrameworks = (string)item.Properties.Item("TargetFrameworks").Value,
                    FriendlyTargetFramework = (string)item.Properties.Item("FriendlyTargetFramework").Value,
                    FullPath = (string)item.Properties.Item("FullPath").Value
                };


                if (ProjectInfoItem.TargetFrameworks != String.Empty)
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
            Debug.WriteLine($@"ActiveSolution: Configuration: {configuration.Name} PlatformName: {configuration.PlatformName}");

            foreach (var item in projectInfos)
            {
                foreach (var iteml in item.TargetFrameworksList)
                {
                    dte2.ExecuteCommand("Tools.Shell", $@"/o /c /dir:{item.FullPath} dotnet.exe publish {item.FullProjectFileName} --configuration {configuration.Name} --framework {iteml}");
                }
                    
            }


        }
    }
}
