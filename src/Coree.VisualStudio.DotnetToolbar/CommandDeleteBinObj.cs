using Coree.VisualStudio.DotnetToolbar.Froms;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandDeleteBinObj : CommandBase
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4137;

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
        private CommandDeleteBinObj(AsyncPackage package, OleMenuCommandService commandService) : base(package, commandService)
        {
            CommandID menuCommandID = new CommandID(CommandSet, CommandId);

            MenuItem = new MenuCommand(Execute, menuCommandID);
            MenuItem.Enabled = false;
            commandService.AddCommand(MenuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandDeleteBinObj Instance
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
            Instance = new CommandDeleteBinObj(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            _ = this.Package.JoinableTaskFactory.RunAsync(async delegate { await ExecuteAsync(sender, e); });
        }

        internal override async System.Threading.Tasks.Task StartDotNetProcessAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);

            await PaneClearAsync();

            var VSProjects = (await GetProjectInfosAsync()).Where(e => e.IsVSProjectType == true).ToList();

            if (!CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsConfirmDialog.DisableConfirmDialog)
            {
                List<string> folderPaths = new List<string>();

                foreach (var item in VSProjects)
                {
                    string binFolderPath = System.IO.Path.Combine(item.VSProjectPath.Replace(item.SolutionDirectory, "").TrimStart('\\'), "bin");
                    string objFolderPath = System.IO.Path.Combine(item.VSProjectPath.Replace(item.SolutionDirectory, "").TrimStart('\\'), "obj");
                    folderPaths.Add(binFolderPath);
                    folderPaths.Add(objFolderPath);
                }

                ConfirmDelete confirmDeleteDialog = new ConfirmDelete(folderPaths);
                confirmDeleteDialog.ShowDialog();

                if (confirmDeleteDialog.FormDialogResult == ConfirmDelete.DialogResultEnum.Close || confirmDeleteDialog.FormDialogResult == ConfirmDelete.DialogResultEnum.Abort)
                {
                    await PaneWriteLineAsync($@"Delete confirmation aborted.");
                    return;
                }
            }

            foreach (var item in VSProjects)
            {
                var binfolder = new System.IO.DirectoryInfo($@"{System.IO.Path.Combine(item.VSProjectPath, "bin")}");
                var objfolder = new System.IO.DirectoryInfo($@"{System.IO.Path.Combine(item.VSProjectPath, "obj")}");

                if (binfolder.Exists)
                {
                    var files = System.IO.Directory.GetFiles(binfolder.FullName, "*", System.IO.SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        try
                        {
                            System.IO.File.Delete(file);
                            await PaneWriteLineAsync($@"Deleted file: {file}.");
                        }
                        catch (Exception)
                        {
                            await PaneWriteLineAsync($@"Could not delete file: {file}.");
                        }
                    }

                    try
                    {
                        System.IO.Directory.Delete(binfolder.FullName, true);
                        await PaneWriteLineAsync($@"Deleted directory: {binfolder.FullName}.");
                    }
                    catch (Exception)
                    {
                        await PaneWriteLineAsync($@"Could not delete directory: {binfolder.FullName}.");
                    }
                }

                if (objfolder.Exists)
                {
                    var files = System.IO.Directory.GetFiles(objfolder.FullName, "*", System.IO.SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        try
                        {
                            System.IO.File.Delete(file);
                            await PaneWriteLineAsync($@"Deleted file: {file}.");
                        }
                        catch (Exception)
                        {
                            await PaneWriteLineAsync($@"Could not delete file: {file}.");
                        }
                    }

                    try
                    {
                        System.IO.Directory.Delete(objfolder.FullName, true);
                        await PaneWriteLineAsync($@"Deleted directory: {objfolder.FullName}.");
                    }
                    catch (Exception)
                    {
                        await PaneWriteLineAsync($@"Could not delete directory: {objfolder.FullName}.");
                    }
                }
            }

            string slnfile = await GetSolutionFileNameAsync();
            string slndir = System.IO.Path.GetDirectoryName(slnfile);
            
            await ExecuteProcessAsync("dotnet.exe", $@"--version", $@"{slndir}");
            await ExecuteProcessAsync("dotnet.exe", $@"restore ""{slnfile}""", $@"{slndir}");
            await PaneWriteLineAsync("Done");
        }
    }
}