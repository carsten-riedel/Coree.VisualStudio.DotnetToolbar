using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using VSLangProj;

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
            public string VSProjectLocation { get; set; } = String.Empty;
            public string VSProjectPath { get; set; } = String.Empty;
            public string VSProjectPropertyLocation { get; set; } = String.Empty;
            public string VSProjectPropertyPath { get; set; } = String.Empty;
            public string VSProjectPropertyTargetFrameworks { get; set; } = String.Empty;
            public List<string> VSProjectPropertyTargetFrameworksList { get; set; } = new List<string>();
            public string VSProjectPropertyFriendlyTargetFramework { get; set; } = String.Empty;
            public bool SolutionDirectoryItemNameLocationExists { get; set; } = false;
            public string SolutionDirectoryItemNameLocation { get; set; } = String.Empty;
            public string SolutionDirectoryItemNameDirectory { get; set; } = String.Empty;
            public string SolutionDirectory { get; set; } = String.Empty;
            public bool IsVSProjectType { get; set; } = false;
            public bool IsSdkStyle { get; set; } = false;
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

        public static async Task<List<ProjectInfo>> GetProjectInfosNewAsync(this AsyncPackage asyncPackage)
        {
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(asyncPackage.DisposalToken);
            var dte2 = await asyncPackage.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) as DTE2;

            if (dte2 != null)
            {
                _Solution test = (_Solution)dte2.Solution;
                var solutionDirectory = System.IO.Path.GetDirectoryName(test.FullName);
                return await asyncPackage.GenerateProjectInfo(solutionDirectory, dte2.Solution.Projects);
            }

            return projectInfos;
        }

        public static async Task<List<ProjectInfo>> GenerateProjectInfo(this AsyncPackage asyncPackage, string solutionDirectory, Projects solProjects)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(asyncPackage.DisposalToken);
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();

            foreach (Project item in solProjects)
            {
                var projectInfoItem = new ProjectInfo();
                projectInfoItem.SolutionDirectory = solutionDirectory;
                var SolutionDirectoryItemNameLocation = $@"{solutionDirectory}\{item.UniqueName}";

                if (System.IO.File.Exists(SolutionDirectoryItemNameLocation))
                {
                    projectInfoItem.SolutionDirectoryItemNameLocationExists = true;
                    projectInfoItem.SolutionDirectoryItemNameLocation = SolutionDirectoryItemNameLocation;
                    projectInfoItem.SolutionDirectoryItemNameDirectory = System.IO.Path.GetDirectoryName(SolutionDirectoryItemNameLocation);
                }

                var IsVSProjectType = (item.Object is VSProject);

                projectInfoItem.IsVSProjectType = IsVSProjectType;
                projectInfoItem.Name = item.Name;
                projectInfoItem.UniqueName = item.UniqueName;

                if (IsVSProjectType == false)
                {
                    if (projectInfoItem.SolutionDirectoryItemNameLocationExists)
                    {
                        string fileContents = File.ReadAllText(projectInfoItem.SolutionDirectoryItemNameLocation);
                        XDocument doc = XDocument.Parse(fileContents);
                        if (doc.Root.Attribute("Sdk") != null)
                        {
                            projectInfoItem.IsSdkStyle = true;
                            projectInfoItem.SolutionDirectoryItemNameLocation = SolutionDirectoryItemNameLocation;
                        }
                        else
                        {
                            projectInfoItem.IsSdkStyle = false;
                        }
                    }

                    projectInfos.Add(projectInfoItem);
                }
                else
                {
                    projectInfoItem.VSProjectLocation = item.FullName;
                    projectInfoItem.VSProjectPath = System.IO.Path.GetDirectoryName(projectInfoItem.VSProjectLocation);
                    projectInfoItem.VSProjectPropertyLocation = await GetProjectItemAsync(item, "FullProjectFileName", asyncPackage);
                    projectInfoItem.VSProjectPropertyTargetFrameworks = await GetProjectItemAsync(item, "TargetFrameworks", asyncPackage);
                    projectInfoItem.VSProjectPropertyFriendlyTargetFramework = await GetProjectItemAsync(item, "FriendlyTargetFramework", asyncPackage);
                    projectInfoItem.VSProjectPropertyPath = await GetProjectItemAsync(item, "FullPath", asyncPackage);

                    if (projectInfoItem.VSProjectLocation != String.Empty)
                    {
                        string fileContents = File.ReadAllText(projectInfoItem.VSProjectLocation);
                        XDocument doc = XDocument.Parse(fileContents);
                        if (doc.Root.Attribute("Sdk") != null)
                        {
                            projectInfoItem.IsSdkStyle = true;
                        }
                        else
                        {
                            projectInfoItem.IsSdkStyle = false;
                        }
                    }

                    if (projectInfoItem.VSProjectPropertyTargetFrameworks != String.Empty)
                    {
                        projectInfoItem.VSProjectPropertyTargetFrameworksList.AddRange(projectInfoItem.VSProjectPropertyTargetFrameworks.Split(';'));
                    }

                    if (projectInfoItem.VSProjectPropertyTargetFrameworksList.Count == 0)
                    {
                        projectInfoItem.VSProjectPropertyTargetFrameworksList.Add(projectInfoItem.VSProjectPropertyFriendlyTargetFramework.Trim(';'));
                    }

                    projectInfos.Add(projectInfoItem);

                    for (int i = 0; i < projectInfos.Count; i++)
                    {
                        projectInfos[i].VSProjectPropertyTargetFrameworksList = projectInfos[i].VSProjectPropertyTargetFrameworksList.Where(e => e != "").ToList();
                    }
                }
            }

            return projectInfos;
        }
    }
}