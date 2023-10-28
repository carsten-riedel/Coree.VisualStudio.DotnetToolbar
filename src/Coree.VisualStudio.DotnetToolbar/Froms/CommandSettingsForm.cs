using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar
{
    public partial class CommandSettingsForm : Form
    {
        public CommandSettingsForm()
        {
            InitializeComponent();
            checkBox1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.KillAllDotnetProcessBeforeExectue;
            checkBox2.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.BlockNonSdkExecute;
            checkBox3.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.NodeReuse;
            checkBox4.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsNugetPush.HideApiKeyInOutput;

            if (CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject)
            {
                radioButton1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject;
                radioButton2.Checked = !CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject;
            }
            else
            {
                radioButton1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject;
                radioButton2.Checked = !CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject;
            }

            textBox1.Text = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsBuild.AdditionalCommandlineArguments;

        }

        private void CommandSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.KillAllDotnetProcessBeforeExectue = checkBox1.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.BlockNonSdkExecute = checkBox2.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.NodeReuse = checkBox3.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsNugetPush.HideApiKeyInOutput = checkBox4.Checked;
            if (radioButton1.Checked)
            {
                CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject = radioButton1.Checked;
            }
            if (radioButton2.Checked)
            {
                CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject = radioButton1.Checked;
            }

            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsBuild.AdditionalCommandlineArguments = textBox1.Text;
            JsonHelper.WriteToFile(CoreeVisualStudioDotnetToolbarPackage.Instance.Settings, CoreeVisualStudioDotnetToolbarPackage.Instance.SettingsFileName);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
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