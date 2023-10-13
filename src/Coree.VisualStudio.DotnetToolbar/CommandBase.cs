using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{
    public abstract class CommandBase
    {
        public AsyncPackage package { get; set; }

        public Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public OleMenuCommandService commandService { get; set; }

        public CommandBase(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            this.package = package;
            this.commandService = commandService;
        }

        public async Task WindowActivateAsync(string constants)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            Window window = dte2.Windows.Item(constants);
            window.Activate();
        }

        public async Task OutputWriteLineAsync(string message, bool clear = false)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);

            EnvDTE.OutputWindowPanes panes = dte2.ToolWindows.OutputWindow.OutputWindowPanes;
            foreach (EnvDTE.OutputWindowPane pane in panes)
            {
                if (pane.Name.Contains("Build"))
                {
                    if (message != null)
                    {
                        pane.OutputString(message + Environment.NewLine);
                        pane.Activate();
                    }
                    else
                    {
                        if (clear == true)
                        {
                            pane.Clear();
                        }
                    }

                    return;
                }
            }
        }

        public async Task<SolutionConfiguration2> GetSolutionActiveConfigurationAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var solution = await GetSolutionAsync();
            return (SolutionConfiguration2)solution.SolutionBuild.ActiveConfiguration;
        }

        public async Task<_Solution> GetSolutionAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            return (_Solution)dte2.Solution;
        }

        public async Task<string> GetSolutionFileNameAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            return (await GetSolutionAsync()).FullName;
        }
    }
}