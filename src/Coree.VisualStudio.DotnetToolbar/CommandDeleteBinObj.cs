using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

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
           _ = this.Package.JoinableTaskFactory.RunAsync(async delegate  { await ExecuteAsync(sender,e); });
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
            await StartDotNetProcessAsync();
        }

        private async System.Threading.Tasks.Task StartDotNetProcessAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);
        }
    }
}