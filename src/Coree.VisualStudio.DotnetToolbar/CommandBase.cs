using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TaskStatusCenter;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Coree.VisualStudio.DotnetToolbar.AsyncPackageExtensions;

namespace Coree.VisualStudio.DotnetToolbar
{
    public abstract class CommandBase
    {
        public AsyncPackage Package { get; set; }

        public Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.Package;
            }
        }

        public OleMenuCommandService CommandService { get; set; }

        public CommandBase(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.Package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            this.Package = package;
            this.CommandService = commandService;
        }

        public async Task WindowActivateAsync(string constants)
        {
            await Package.WindowActivateAsync(constants);
        }

        public async Task<ActiveSolutionConfiguration> GetActiveSolutionConfigurationAsync()
        {
            return await Package.GetActiveSolutionConfigurationAsync();
        }

        public async Task<_Solution> GetSolutionAsync()
        {
            return await Package.GetSolutionAsync();
        }

        public async Task<string> GetSolutionFileNameAsync()
        {
            return await Package.GetSolutionFileNameAsync();
        }

        public async Task<Dictionary<string, string>> GetSolutionPropertiesAsync()
        {
            return await Package.GetSolutionPropertiesAsync();
        }

        public async Task<List<ProjectInfo>> GetProjectInfosAsync()
        {
            return await Package.GetProjectInfosNewAsync();
        }

        public async Task PaneWriteLineAsync(string message, string paneName = "Build", bool addNewLine = true, bool activateAfter = true, string[] maskOutputs = null)
        {
            await Package.PaneWriteLineAsync(message, paneName, addNewLine, activateAfter, maskOutputs);
        }

        public async Task PaneClearAsync(string paneName = "Build", bool activateAfter = true)
        {
            await Package.PaneClearAsync(paneName, activateAfter);
        }

        public async Task WaitForTaskStatusCenterAsync()
        {
            dynamic TaskStatusCenter = (SVsTaskStatusCenterService)await ServiceProvider.GetServiceAsync(typeof(SVsTaskStatusCenterService));

            int InProgressCount;
            do
            {
                InProgressCount = TaskStatusCenter.InProgressCount;
                if (InProgressCount != 0)
                {
                    await PaneWriteLineAsync("Waiting for TaskStatusCenter to finish.");
                    await Task.Delay(3000);
                }
            } while (InProgressCount != 0);
        }

        public async Task ExecuteProcessAsync(string fileName, string arguments, string workingDirectory, string[] maskOutputs = null)
        {
            List<JoinableTask> _joinableTasks = new List<JoinableTask>();

            var process = new System.Diagnostics.Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            var commandline = process.StartInfo.GetProcessStartInfoCommandline();

            if (maskOutputs != null)
            {
                foreach (var item in maskOutputs)
                {
                    commandline = commandline.Replace(item, new string('*', item.Length));
                }
            }

            await PaneWriteLineAsync(commandline);

            process.OutputDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await PaneWriteLineAsync(e.Data, "Build", true, true, maskOutputs); } catch (Exception ex) { Debug.WriteLine(ex.Message); } }); _joinableTasks.Add(joinableTask); };
            process.ErrorDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await PaneWriteLineAsync(e.Data, "Build", true, true, maskOutputs); } catch (Exception ex) { Debug.WriteLine(ex.Message); } }); _joinableTasks.Add(joinableTask); };

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            await process.WaitForExitAsync();

            await Task.WhenAll(_joinableTasks.Select(jt => jt.Task));
        }

        internal virtual async System.Threading.Tasks.Task ExecuteAsync(object sender, EventArgs e)
        {
            CommandDotnetBuild.Instance.MenuItem.Enabled = false;
            CommandDotnetPack.Instance.MenuItem.Enabled = false;
            CommandDotnetPublish.Instance.MenuItem.Enabled = false;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = false;
            CommandDotnetClean.Instance.MenuItem.Enabled = false;
            CommandSettings.Instance.MenuItem.Enabled = false;
            CommandDeleteBinObj.Instance.MenuItem.Enabled = false;
            CommandDotnetTest.Instance.MenuItem.Enabled = false;
            CommandDotnetGlobalJson6.Instance.MenuItem.Enabled = false;
            CommandDotnetGlobalJson7.Instance.MenuItem.Enabled = false;
            CommandDotnetGlobalJson8.Instance.MenuItem.Enabled = false;
            CommandDotnetExperimentalTest.Instance.MenuItem.Enabled = false;

            await StartDotNetProcessAsync();
            
            CommandDotnetBuild.Instance.MenuItem.Enabled = true;
            CommandDotnetPack.Instance.MenuItem.Enabled = true;
            CommandDotnetPublish.Instance.MenuItem.Enabled = true;
            CommandDotnetNugetPush.Instance.MenuItem.Enabled = true;
            CommandDotnetClean.Instance.MenuItem.Enabled = true;
            CommandSettings.Instance.MenuItem.Enabled = true;
            CommandDeleteBinObj.Instance.MenuItem.Enabled = true;
            CommandDotnetTest.Instance.MenuItem.Enabled = true;
            CommandDotnetGlobalJson6.Instance.MenuItem.Enabled = true;
            CommandDotnetGlobalJson7.Instance.MenuItem.Enabled = true;
            CommandDotnetGlobalJson8.Instance.MenuItem.Enabled = true;
            CommandDotnetExperimentalTest.Instance.MenuItem.Enabled = true;
        }

        internal virtual async Task StartDotNetProcessAsync()
        {
            await Task.CompletedTask;
        }
    }
}