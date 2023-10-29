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

            textBoxAdditionalBuildCommandLineArguments.Text = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsBuild.AdditionalCommandlineArguments;
            textBoxAdditionalPackCommandLineArguments.Text = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPack.AdditionalCommandlineArguments;
            textBoxAdditionalPublishCommandLineArguments.Text = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.AdditionalCommandlineArguments;
            textBoxAdditionalCleanCommandLineArguments.Text = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsClean.AdditionalCommandlineArguments;
        }

        private void CommandSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.KillAllDotnetProcessBeforeExectue = checkBox1.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.BlockNonSdkExecute = checkBox2.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsNugetPush.HideApiKeyInOutput = checkBox4.Checked;
            if (radioButton1.Checked)
            {
                CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject = radioButton1.Checked;
            }
            if (radioButton2.Checked)
            {
                CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.PublishSolutionProject = radioButton1.Checked;
            }

            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsBuild.AdditionalCommandlineArguments = textBoxAdditionalBuildCommandLineArguments.Text;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPack.AdditionalCommandlineArguments = textBoxAdditionalPackCommandLineArguments.Text;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsPublish.AdditionalCommandlineArguments = textBoxAdditionalPublishCommandLineArguments.Text;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsClean.AdditionalCommandlineArguments = textBoxAdditionalCleanCommandLineArguments.Text;

            JsonHelper.WriteToFile(CoreeVisualStudioDotnetToolbarPackage.Instance.Settings, CoreeVisualStudioDotnetToolbarPackage.Instance.SettingsFileName);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel2.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel3.Text);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel4.Text);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel5.Text);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel6.Text);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel7.Text);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel8.Text);
        }
    }
}