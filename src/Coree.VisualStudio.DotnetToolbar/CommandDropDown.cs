using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Task = System.Threading.Tasks.Task;

//https://github.com/microsoft/VSSDK-Extensibility-Samples/tree/master/Combo_Box/C%23

namespace Coree.VisualStudio.DotnetToolbar
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandDropDown : CommandBase
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int cmdidMyDropDownCombo = 5000;

        public const int cmdidMyDropDownComboGetList = 5001;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7303216a-a2cb-4519-b645-a34ae1380a78");

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDotnetBuild"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandDropDown(AsyncPackage package, OleMenuCommandService commandService) : base(package, commandService)
        {
            CommandID menuMyDropDownComboGetListCommandID = new CommandID(CommandSet, cmdidMyDropDownComboGetList);

            OleMenuCommand menuMyDropDownComboGetListCommand = new OleMenuCommand(async (s, e) => OnMenuMyDropDownComboGetListAsync(s, e), menuMyDropDownComboGetListCommandID);

            commandService.AddCommand(menuMyDropDownComboGetListCommand);

            CommandID menuMyDropDownComboCommandID = new CommandID(CommandSet, cmdidMyDropDownCombo);
            MenuCommand menuMyDropDownComboCommand = new OleMenuCommand(async (s, e) => ExecuteAsync(s, e), menuMyDropDownComboCommandID);
            commandService.AddCommand(menuMyDropDownComboCommand);
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in 's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandDropDown(package, commandService);
        }

        private readonly string[] dropDownComboChoices = { "build", "pack", "publish" };
        private string currentDropDownComboChoice = "build";

        private async Task OnMenuMyDropDownComboGetListAsync(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);

            if (e is OleMenuCmdEventArgs eventArgs)
            {
                object inParam = eventArgs.InValue;
                IntPtr vOut = eventArgs.OutValue;

                if (inParam != null)
                {
                    throw (new ArgumentException("Resources.InParamIllegal")); // force an exception to be thrown
                }
                else if (vOut != IntPtr.Zero)
                {
                    Marshal.GetNativeVariantForObject(dropDownComboChoices, vOut);
                }
                else
                {
                    throw (new ArgumentException("Resources.OutParamRequired")); // force an exception to be thrown
                }
            }
        }

        internal override async Task ExecuteAsync(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);

            if (e is OleMenuCmdEventArgs eventArgs)
            {
                IntPtr vOut = eventArgs.OutValue;

                if (vOut != IntPtr.Zero)
                {
                    // when vOut is non-NULL, the IDE is requesting the current value for the combo
                    Marshal.GetNativeVariantForObject(currentDropDownComboChoice, vOut);
                }
                else if (eventArgs.InValue is string newChoice)
                {
                    // new value was selected or typed in
                    // see if it is one of our items
                    bool validInput = false;
                    int indexInput = -1;
                    for (indexInput = 0; indexInput < dropDownComboChoices.Length; indexInput++)
                    {
                        if (string.Compare(dropDownComboChoices[indexInput], newChoice, StringComparison.CurrentCultureIgnoreCase) == 0)
                        {
                            validInput = true;
                            break;
                        }
                    }

                    if (validInput)
                    {
                        currentDropDownComboChoice = dropDownComboChoices[indexInput];
                        await WindowActivateAsync(Constants.vsWindowKindOutput);
                        await PaneWriteLineAsync($@"Choice: {currentDropDownComboChoice}");
                    }
                    else
                    {
                        throw (new ArgumentException("Resources.ParamNotValidStringInList")); // force an exception to be thrown
                    }
                }
            }
            else
            {
                // We should never get here; EventArgs are required.
                throw (new ArgumentException("Resources.EventArgsRequired")); // force an exception to be thrown
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandDropDown Instance
        {
            get;
            private set;
        }
    }
}