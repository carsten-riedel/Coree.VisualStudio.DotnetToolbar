using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar.Froms
{
    public partial class ConfirmDelete : Form
    {
        public enum DialogResultEnum
        {
            Close,
            Delete,
            Abort
        }

        public DialogResultEnum FormDialogResult { get; set; } = DialogResultEnum.Close;

        public ConfirmDelete(List<string> folderPaths)
        {
            InitializeComponent();
            folderPaths.ForEach(path => { listBox1.Items.Add(path); });
            checkBoxDisableConfirmDialog.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsConfirmDialog.DisableConfirmDialog;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            FormDialogResult = DialogResultEnum.Delete;
            this.Close();
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            FormDialogResult = DialogResultEnum.Abort;
            this.Close();
        }

        private void ConfirmDelete_FormClosing(object sender, FormClosingEventArgs e)
        {
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsConfirmDialog.DisableConfirmDialog = checkBoxDisableConfirmDialog.Checked;
            JsonHelper.WriteToFile(CoreeVisualStudioDotnetToolbarPackage.Instance.Settings, CoreeVisualStudioDotnetToolbarPackage.Instance.SettingsFileName);
        }

        private void ConfirmDelete_Shown(object sender, EventArgs e)
        {
            buttonAbort.Focus();
        }
    }
}