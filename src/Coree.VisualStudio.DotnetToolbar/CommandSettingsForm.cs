using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar
{
    public partial class CommandSettingsForm : Form
    {
        public CommandSettingsForm()
        {
            InitializeComponent();
            checkBox1.Checked = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.KillAllDotnetProcessBeforeExectue;
        }

        private void CommandSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
             CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.KillAllDotnetProcessBeforeExectue = checkBox1.Checked;
            JsonHelper.WriteToFile(CoreeVisualStudioDotnetToolbarPackage.Instance.Settings, CoreeVisualStudioDotnetToolbarPackage.Instance.SettingsFileName);
        }
    }
}
