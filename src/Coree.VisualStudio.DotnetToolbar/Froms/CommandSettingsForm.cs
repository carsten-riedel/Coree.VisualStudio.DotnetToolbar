using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;

using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar
{
    public partial class CommandSettingsForm : Form
    {
        public CommandSettingsForm()
        {
            InitializeComponent();
            checkBoxKillDotNet.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.KillAllDotnetProcessBeforeExectue;
            checkBoxBlockNonSdk.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.BlockNonSdkExecute;
            checkBox3.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.WriteDotnetGlobalJson;
            checkBoxHideApiKeyInOutput.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsNugetPush.HideApiKeyInOutput;
            checkBoxDisableConfirmDialog.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsConfirmDialog.DisableConfirmDialog;

            var availibleSDKs = new System.Diagnostics.Process().StartDotNetVersionSync("dotnet.exe", "--list-sdks", string.Empty);
            availibleSDKs.Reverse();
            comboBoxAvailibleSDKs.Items.AddRange(availibleSDKs.ToArray());

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
            textBoxAdditionalTestCommandLineArguments.Text = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsTest.AdditionalCommandlineArguments;
            textBoxAdditionalCleanCommandLineArguments.Text = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsClean.AdditionalCommandlineArguments;
        }

        private void CommandSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.KillAllDotnetProcessBeforeExectue = checkBoxKillDotNet.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.BlockNonSdkExecute = checkBoxBlockNonSdk.Checked;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsGeneral.WriteDotnetGlobalJson = checkBox3.Checked;

            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsNugetPush.HideApiKeyInOutput = checkBoxHideApiKeyInOutput.Checked;
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
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsTest.AdditionalCommandlineArguments = textBoxAdditionalTestCommandLineArguments.Text;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsClean.AdditionalCommandlineArguments = textBoxAdditionalCleanCommandLineArguments.Text;
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsConfirmDialog.DisableConfirmDialog = checkBoxDisableConfirmDialog.Checked;

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

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel9.Text);
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel10.Text);
        }
    }
}