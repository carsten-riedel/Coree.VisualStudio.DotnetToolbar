﻿using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Coree.VisualStudio.DotnetToolbar
{
    public static partial class AsyncPackageExtensions
    {
        public class ActiveSolutionConfiguration
        {
            public string Configuration { get; set; }
            public string Platform { get; set; }
        }

        public static async Task<ActiveSolutionConfiguration> GetActiveSolutionConfigurationAsync(this AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE));
            Solution dte2Solution = null;
            if (dte2 != null)
            {
                dte2Solution = (Solution)dte2.Solution;
            }
            if (dte2Solution == null)
            {
                return null;
            }
            else
            {
                var configuration = ((SolutionConfiguration2)dte2Solution.SolutionBuild.ActiveConfiguration).Name;
                var platform = ((SolutionConfiguration2)dte2Solution.SolutionBuild.ActiveConfiguration).PlatformName;
                return new ActiveSolutionConfiguration() { Configuration = configuration, Platform = platform };
            }
        }

        public static async Task PaneWriteLineAsync(this AsyncPackage package, string message, string paneName = "Build", bool addNewLine = true, bool activateAfter = true, string[] maskOutputs = null)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE));

            if (dte2 != null)
            {
                foreach (EnvDTE.OutputWindowPane pane in dte2.ToolWindows.OutputWindow.OutputWindowPanes)
                {
                    if (pane.Name.Equals(paneName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (message != null)
                        {
                            if (maskOutputs != null)
                            {
                                foreach (var item in maskOutputs)
                                {
                                    message = message.Replace(item, new string('*', item.Length));
                                }
                            }

                            if (addNewLine)
                            {
                                message = $"{message}{Environment.NewLine}";
                            }
                            pane.OutputString(message);
                            if (activateAfter)
                            {
                                pane.Activate();
                            }
                        }
                        return;
                    }
                }

                dte2.ToolWindows.OutputWindow.OutputWindowPanes.Add(paneName);

                foreach (EnvDTE.OutputWindowPane pane in dte2.ToolWindows.OutputWindow.OutputWindowPanes)
                {
                    if (pane.Name.Equals(paneName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (message != null)
                        {
                            if (addNewLine)
                            {
                                message = $"{message}{Environment.NewLine}";
                            }
                            pane.OutputString(message);
                            if (activateAfter)
                            {
                                pane.Activate();
                            }
                        }
                        return;
                    }
                }
            }
            return;
        }

        public static async Task PaneClearAsync(this AsyncPackage package, string paneName = "Build", bool activateAfter = true)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE));

            if (dte2 != null)
            {
                foreach (EnvDTE.OutputWindowPane pane in dte2.ToolWindows.OutputWindow.OutputWindowPanes)
                {
                    if (pane.Name.Equals(paneName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        pane.Clear();
                        if (activateAfter)
                        {
                            pane.Activate();
                        }
                        return;
                    }
                }
            }
            return;
        }
    }
}