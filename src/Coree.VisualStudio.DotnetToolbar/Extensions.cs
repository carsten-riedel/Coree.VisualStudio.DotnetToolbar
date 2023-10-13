using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{
    internal class ProjectInfo
    {
        public string FullProjectFileName { get; set; } = String.Empty;
        public string FullPath { get; set; } = String.Empty;
        public string TargetFrameworks { get; set; } = String.Empty;
        public List<string> TargetFrameworksList { get; set; } = new List<string>();
        public string FriendlyTargetFramework { get; set; } = String.Empty;
    }

    internal class Helper
    {
        internal async static Task<List<ProjectInfo>> GetProjectInfosAsync(AsyncPackage asyncPackage)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(asyncPackage.DisposalToken);
            var dte2 = await asyncPackage.GetServiceAsync(typeof(DTE2)).ConfigureAwait(false) as DTE2;

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

            return projectInfos;
        }

        internal static Projects Ff(DTE dte2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Projects solProjects = dte2.Solution.Projects;

            return solProjects;
        }
    }

    internal static class DTEExtensions
    {
        internal static Projects GetSolutionProjects(this DTE dte2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Projects solProjects = dte2.Solution.Projects;
            return solProjects;
        }

    }

    internal static class ProcessStartInfoExtensions
    {
        internal static string GetProcessStartInfoCommandline(this ProcessStartInfo processStartInfo)
        {
            return $@"{processStartInfo.FileName} {processStartInfo.Arguments}";
        }
    }
}
