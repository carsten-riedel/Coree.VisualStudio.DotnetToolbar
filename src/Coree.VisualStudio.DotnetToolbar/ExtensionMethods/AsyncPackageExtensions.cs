using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{
    public static class AsyncPackageExtensions
    {
        public static async Task WindowActivateAsync(this AsyncPackage package, string constants)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            if (dte2 != null)
            {
                Window window = dte2.Windows.Item(constants);
                window.Activate();
            }
        }

        public static async Task<_Solution> GetSolutionAsync(this AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            if (dte2 != null)
            {
                return (_Solution)dte2.Solution;
            }
            return null;
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

        public static async Task OutputWriteLineAsync(this AsyncPackage package, string message, bool clear = false)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            if (dte2 != null)
            {
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
        }

        public static async Task WindowOutputWriteLineAsync(this AsyncPackage package, string message, bool clear = false)
        {
            await package.WindowActivateAsync(Constants.vsWindowKindOutput);
            await package.OutputWriteLineAsync(message, clear);
        }

        public static async Task<Dictionary<string, string>> GetSolutionPropertiesAsync(this AsyncPackage package)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            DTE2 dte2 = (DTE2)await package.GetServiceAsync(typeof(DTE)).ConfigureAwait(false);
            if (dte2 != null)
            {
                Properties properties = dte2.Solution.Properties;
                foreach (Property item in properties)
                {
                    try
                    {
                        dict.Add(item.Name, item.Value.ToString());
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine($@"{item.Name}");
                    }
                }
                return dict;
            }
            return dict;
        }

        public class ProjectInfo
        {
            public string FullProjectFileName { get; set; } = String.Empty;
            public string FullPath { get; set; } = String.Empty;
            public string TargetFrameworks { get; set; } = String.Empty;
            public List<string> TargetFrameworksList { get; set; } = new List<string>();
            public string FriendlyTargetFramework { get; set; } = String.Empty;
        }

        public static async Task<List<ProjectInfo>> GetProjectInfosAsync(this AsyncPackage asyncPackage)
        {
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(asyncPackage.DisposalToken);
            var dte2 = await asyncPackage.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) as DTE2;

            if (dte2 != null)
            {
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

                    if (ProjectInfoItem.TargetFrameworks != String.Empty)
                    {
                        ProjectInfoItem.TargetFrameworksList.AddRange(ProjectInfoItem.TargetFrameworks.Split(';'));
                    }

                    if (ProjectInfoItem.TargetFrameworksList.Count == 0)
                    {
                        ProjectInfoItem.TargetFrameworksList.Add(ProjectInfoItem.FriendlyTargetFramework);
                    }

                    projectInfos.Add(ProjectInfoItem);

                    for (int i = 0; i < projectInfos.Count; i++)
                    {
                        projectInfos[i].TargetFrameworksList = projectInfos[i].TargetFrameworksList.Where(e => e != "").ToList();
                    }

                    foreach (Property items in item.Properties)
                    {
                        Debug.WriteLine($@"{items.Name},{items.Value}");
                    }

                   
                }

                return projectInfos;
            }

            return projectInfos;
        }
    }
}