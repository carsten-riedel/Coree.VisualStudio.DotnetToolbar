using CredentialManagement;
using EnvDTE;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar
{
    public partial class NugetPushDialog : Form
    {
        public enum NugetPushDialogResult
        {
            Close,
            Push,
            Cancel
        }

        public NugetPushDialogResult nugetPushDialogResult { get; set; } = NugetPushDialogResult.Close;

        public string Source { get; set; }
        public string PackageLocation { get; set; }
        public string ApiKey { get; set; }

        private string UserDataPath { get; set; }
        private string SolutionLocation { get; set; }
        public string SolutionDir { get; set; }
        private string SolutionName { get; set; }
        private string SolutionGuid { get; set; }

        

        public NugetPushDialog(string UserDataPath,string SolutionLocation,string SolutionName,string SolutionGuid)
        {
            this.UserDataPath = UserDataPath;
            this.SolutionLocation = SolutionLocation; 
            this.SolutionName = SolutionName;
            this.SolutionGuid = SolutionGuid;   
            this.SolutionDir = System.IO.Path.GetDirectoryName(SolutionLocation);

            var nugets = System.IO.Directory.GetFiles(this.SolutionDir, "*.nupkg", System.IO.SearchOption.AllDirectories).ToList();
            var shortnuget = new List<string>();
            nugets.ForEach(e => shortnuget.Add(e.Substring(this.SolutionDir.Length+1)));

            InitializeComponent();

            listBoxNupkg.Items.AddRange(shortnuget.ToArray());

            if (listBoxNupkg.Items.Count > 0)
            {
                listBoxNupkg.SelectedIndex = 0;
                PackageLocation = listBoxNupkg.Items[listBoxNupkg.SelectedIndex].ToString();
            }

            if (listBoxSource.Items.Count > 0)
            {
                listBoxSource.SelectedIndex = 0;
                Source = listBoxSource.Items[listBoxSource.SelectedIndex].ToString();
            }

            LoadDotnetToolbarCredential();

        }

        private void LoadDotnetToolbarCredential()
        {
            Credential credential = new Credential
            {
                Target = $"DotnetToolbar/{SolutionGuid}/{Source}",
                Type = CredentialType.Generic
            };
            credential.Load();
            if (credential.Username == null)
            {
                textBoxApiKey.Text = string.Empty;
                ApiKey = string.Empty;
            }
            else
            {
                textBoxApiKey.Text = credential.Password;
                ApiKey = credential.Password;
                
            }
        }

        private void SaveUpdateCredential(string password)
        {
            if (password == string.Empty)
            {
                return;
            }
            Credential credential = new Credential
            {
                Username = $"{SolutionGuid}",
                Password = password,
                Target = $"DotnetToolbar/{SolutionGuid}/{Source}",
                Type = CredentialType.Generic,
                Description = "",
                PersistanceType = PersistanceType.LocalComputer,
            };
            credential.Save();
        }

        private void FormNugetPush_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (nugetPushDialogResult == NugetPushDialogResult.Push)
            {
                SaveUpdateCredential(textBoxApiKey.Text);
                ApiKey = textBoxApiKey.Text;
            }
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            nugetPushDialogResult = NugetPushDialogResult.Cancel;
            this.Close();
        }

        private void buttonPush_Click(object sender, System.EventArgs e)
        {
            nugetPushDialogResult = NugetPushDialogResult.Push;
            this.Close();
        }

        private void listBoxNupkg_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            PackageLocation = listBoxNupkg.Items[listBoxNupkg.SelectedIndex].ToString();
        }

        private void listBoxSource_SelectedIndexChanged(object sender, System.EventArgs e)
        {
       
            SaveUpdateCredential(textBoxApiKey.Text);
            Source = listBoxSource.Items[listBoxSource.SelectedIndex].ToString();
            LoadDotnetToolbarCredential();
        }
    }
}