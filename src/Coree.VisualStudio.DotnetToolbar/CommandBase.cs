using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public OleMenuCommandService commandService { get; set; }

        public CommandBase(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.Package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            this.Package = package;
            this.commandService = commandService;
        }

        public async Task WindowActivateAsync(string constants)
        {
            await Package.WindowActivateAsync(constants);
        }

        public async Task OutputWriteLineAsync(string message, bool clear = false)
        {
            await Package.OutputWriteLineAsync(message,clear);
        }

        public async Task<SolutionConfiguration2> GetSolutionActiveConfigurationAsync()
        {
            return await Package.GetSolutionActiveConfigurationAsync(); ;
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
    }
}