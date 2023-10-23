using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar
{
    public partial class CommandSettingsForm : Form
    {
        public CommandSettingsForm()
        {
            InitializeComponent();
            checkBox1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsGeneral.KillAllDotnetProcessBeforeExectue;
            checkBox2.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsGeneral.BlockNonSdkExecute;
            checkBox3.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsGeneral.NodeReuse;
            if (CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsPublish.PublishSolutionProject)
            {
                radioButton1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsPublish.PublishSolutionProject;
                radioButton2.Checked = !CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsPublish.PublishSolutionProject;
            }
            else
            {
                radioButton1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsPublish.PublishSolutionProject;
                radioButton2.Checked = !CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsPublish.PublishSolutionProject;
            }
            
        }

        private void CommandSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsGeneral.KillAllDotnetProcessBeforeExectue = checkBox1.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsGeneral.BlockNonSdkExecute = checkBox2.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsGeneral.NodeReuse = checkBox3.Checked;
            if (radioButton1.Checked)
            {
                CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsPublish.PublishSolutionProject = radioButton1.Checked;
            }
            if (radioButton2.Checked)
            {
                CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.solutionSettingsPublish.PublishSolutionProject = radioButton1.Checked;
            }
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