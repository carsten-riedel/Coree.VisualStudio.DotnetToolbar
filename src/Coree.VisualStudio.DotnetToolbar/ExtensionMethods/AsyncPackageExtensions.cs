using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using VSLangProj;
using static Coree.VisualStudio.DotnetToolbar.AsyncPackageExtensions;

namespace Coree.VisualStudio.DotnetToolbar
{
    public static partial class AsyncPackageExtensions
    {
        [Obsolete]
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
            public string Name { get; set; } = String.Empty;
            public string UniqueName { get; set; } = String.Empty;
            public string FullProjectFileName { get; set; } = String.Empty;
            public string FullPath { get; set; } = String.Empty;
            public string TargetFrameworks { get; set; } = String.Empty;
            public List<string> TargetFrameworksList { get; set; } = new List<string>();
            public string FriendlyTargetFramework { get; set; } = String.Empty;
            public bool IsSdkStyle { get; set; } = false;
            public string File { get; set; } = String.Empty;
            public bool UnknownUnloaded { get; set; } = true;
        }

        [Obsolete]
        public static async Task<string> GetProjectItemAsync(this Project item, string name, AsyncPackage Package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);

            if (item.Properties == null)
            {
                return string.Empty;
            }
            try
            {
                return (string)item.Properties.Item(name).Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [Obsolete]
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
                    try
                    {
                        var fullname = item.FullName;

                        var ProjectInfoItem = new ProjectInfo()
                        {
                            File = item.FullName,
                            FullProjectFileName = await GetProjectItemAsync(item, "FullProjectFileName", asyncPackage),
                            TargetFrameworks = await GetProjectItemAsync(item, "TargetFrameworks", asyncPackage),
                            FriendlyTargetFramework = await GetProjectItemAsync(item, "FriendlyTargetFramework", asyncPackage),
                            FullPath = await GetProjectItemAsync(item, "FullPath", asyncPackage)
                        };

                        if (ProjectInfoItem.File != String.Empty)
                        {
                            string fileContents = File.ReadAllText(ProjectInfoItem.File);
                            XDocument doc = XDocument.Parse(fileContents);
                            if (doc.Root.Attribute("Sdk") != null)
                            {
                                ProjectInfoItem.IsSdkStyle = true;
                            }
                            else
                            {
                                ProjectInfoItem.IsSdkStyle = false;
                            }
                        }

                        if (ProjectInfoItem.TargetFrameworks != String.Empty)
                        {
                            ProjectInfoItem.TargetFrameworksList.AddRange(ProjectInfoItem.TargetFrameworks.Split(';'));
                        }

                        if (ProjectInfoItem.TargetFrameworksList.Count == 0)
                        {
                            ProjectInfoItem.TargetFrameworksList.Add(ProjectInfoItem.FriendlyTargetFramework.Trim(';'));
                        }

                        projectInfos.Add(ProjectInfoItem);

                        for (int i = 0; i < projectInfos.Count; i++)
                        {
                            projectInfos[i].TargetFrameworksList = projectInfos[i].TargetFrameworksList.Where(e => e != "").ToList();
                        }

                        projectInfos = projectInfos.Where(e => e.File != String.Empty).ToList();

                        if (item.Properties != null)
                        {
                            foreach (Property items in item.Properties)
                            {
                                try
                                {
                                    Debug.WriteLine($@"{items.Name},{items.Value}");
                                }
                                catch (Exception)
                                {
                                    Debug.WriteLine($@"{items.Name}");
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                return projectInfos;
            }

            return projectInfos;
        }

        public static async Task<List<ProjectInfo>> GetProjectInfosNewAsync(this AsyncPackage asyncPackage)
        {
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(asyncPackage.DisposalToken);
            var dte2 = await asyncPackage.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) as DTE2;

            if (dte2 != null)
            {
                return await asyncPackage.GetxAsync(dte2.Solution.Projects);
            }

            return projectInfos;
        }

        public static async Task<List<ProjectInfo>> GetxAsync(this AsyncPackage asyncPackage, Projects solProjects)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(asyncPackage.DisposalToken);
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();
            foreach (Project item in solProjects)
            {
                var isProject = (item.Object is VSProject);
                if (isProject == false)
                {
                    projectInfos.Add(new ProjectInfo() { Name = item.Name, UniqueName = item.UniqueName, UnknownUnloaded = true });
                }
                else
                {
                    var ProjectInfoItem = new ProjectInfo()
                    {
                        Name = item.Name,
                        UniqueName = item.UniqueName,
                        File = item.FullName,
                        FullProjectFileName = await GetProjectItemAsync(item, "FullProjectFileName", asyncPackage),
                        TargetFrameworks = await GetProjectItemAsync(item, "TargetFrameworks", asyncPackage),
                        FriendlyTargetFramework = await GetProjectItemAsync(item, "FriendlyTargetFramework", asyncPackage),
                        FullPath = await GetProjectItemAsync(item, "FullPath", asyncPackage),
                        UnknownUnloaded = false
                    };

                    if (ProjectInfoItem.File != String.Empty)
                    {
                        string fileContents = File.ReadAllText(ProjectInfoItem.File);
                        XDocument doc = XDocument.Parse(fileContents);
                        if (doc.Root.Attribute("Sdk") != null)
                        {
                            ProjectInfoItem.IsSdkStyle = true;
                        }
                        else
                        {
                            ProjectInfoItem.IsSdkStyle = false;
                        }
                    }

                    if (ProjectInfoItem.TargetFrameworks != String.Empty)
                    {
                        ProjectInfoItem.TargetFrameworksList.AddRange(ProjectInfoItem.TargetFrameworks.Split(';'));
                    }

                    if (ProjectInfoItem.TargetFrameworksList.Count == 0)
                    {
                        ProjectInfoItem.TargetFrameworksList.Add(ProjectInfoItem.FriendlyTargetFramework.Trim(';'));
                    }

                    projectInfos.Add(ProjectInfoItem);

                    for (int i = 0; i < projectInfos.Count; i++)
                    {
                        projectInfos[i].TargetFrameworksList = projectInfos[i].TargetFrameworksList.Where(e => e != "").ToList();
                    }
                }
            }

            return projectInfos;
        }
    }
}