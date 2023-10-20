using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar
{
    public partial class CommandSettingsForm : Form
    {
        public bool DialogResultAsync { get; set; }

        public async Task<bool> ShowDialogAsync()
        {
            var cts = new CancellationTokenSource();
            // Attach token cancellation on form closing.
            Closed += (object sender, EventArgs e) =>
            {
                cts.Cancel();
            };
            Show(); // Show message without GUI freezing.
            try
            {
                // await for user button click.
                await Task.Delay(Timeout.Infinite, cts.Token);
            }
            catch (TaskCanceledException)
            { }
            return DialogResultAsync;
        }

        public CommandSettingsForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResultAsync = true;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResultAsync = false;
            Close();
        }
    }
}
