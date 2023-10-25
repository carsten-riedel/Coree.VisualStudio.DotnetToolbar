using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TaskStatusCenter;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            return await Package.GetProjectInfosAsync();
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

        public async Task ExecuteProcessAsync(string fileName, string arguments, string workingDirectory,string[] maskOutputs = null)
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

            await PaneWriteLineAsync("-------------------------------------------------------------------------------");
            await PaneWriteLineAsync(commandline);
            await PaneWriteLineAsync("-------------------------------------------------------------------------------");

            process.OutputDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await PaneWriteLineAsync(e.Data,"Build",true,true,maskOutputs); } catch (Exception ex) { Debug.WriteLine(ex.Message); } }); _joinableTasks.Add(joinableTask); };
            process.ErrorDataReceived += (sender, e) => { var joinableTask = ThreadHelper.JoinableTaskFactory.RunAsync(async () => { try { await PaneWriteLineAsync(e.Data, "Build", true, true, maskOutputs); } catch (Exception ex) { Debug.WriteLine(ex.Message); } }); _joinableTasks.Add(joinableTask); };

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            await process.WaitForExitAsync();

            await Task.WhenAll(_joinableTasks.Select(jt => jt.Task));
        }

    }
}