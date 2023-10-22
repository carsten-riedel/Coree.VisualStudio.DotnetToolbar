using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar
{
    public partial class CommandSettingsForm : Form
    {
        public CommandSettingsForm()
        {
            InitializeComponent();
            checkBox1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.KillAllDotnetProcessBeforeExectue;
            checkBox2.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.BlockNonSdkExecute;
        }

        private void CommandSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.KillAllDotnetProcessBeforeExectue = checkBox1.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.BlockNonSdkExecute = checkBox2.Checked;
            JsonHelper.WriteToFile(CoreeVisualStudioDotnetToolbarPackage.Instance.Settings, CoreeVisualStudioDotnetToolbarPackage.Instance.SettingsFileName);
        }

        /*
                     foreach (Project item in dte2.Solution.Projects)
            {
                string fileContents = File.ReadAllText(item.FullName);
                XDocument doc = XDocument.Parse(fileContents);

                ProjectType projectType;
                if (doc.Root.Attribute("Sdk") != null)
                {
                    projectType = ProjectType.SDKStyle;
                }
                else
                {
                    projectType = ProjectType.NonSDKStyle;
                }
         */
    }
}