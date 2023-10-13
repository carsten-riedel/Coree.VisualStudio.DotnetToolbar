using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{
    /*
    public static class Class1
    {
        public static async Task WindowActivateAsync(this AsyncPackage package , string constants)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            Window window = dte2.Windows.Item(constants);
            window.Activate();
        }

        public static async Task<_Solution> GetSolutionAsync(this AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            return (_Solution)dte2.Solution;
        }

        public static async Task<string> GetSolutionFileNameAsync(this AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            return (await package.GetSolutionAsync()).FullName;
        }

        public static async Task<SolutionConfiguration2> GetSolutionActiveConfigurationAsync(this AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var solution = await GetSolutionAsync(package);
            return (SolutionConfiguration2)solution.SolutionBuild.ActiveConfiguration;
        }

        public static async Task OutputWriteLineAsync(this AsyncPackage package,string message)
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
                        pane.Clear();
                    }

                    return;
                }
            }
        }

    }
    */
 
}
