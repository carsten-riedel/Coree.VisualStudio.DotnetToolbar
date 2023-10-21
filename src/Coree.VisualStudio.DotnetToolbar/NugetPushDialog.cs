using CredentialManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

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
        private string CredManagerTarget { get; set; }

        public NugetPushDialog(string UserDataPath, string SolutionLocation, string SolutionName, string SolutionGuid)
        {
            this.UserDataPath = UserDataPath;
            this.SolutionLocation = SolutionLocation;
            this.SolutionName = SolutionName;
            this.SolutionGuid = SolutionGuid;
            this.SolutionDir = System.IO.Path.GetDirectoryName(SolutionLocation);
            this.CredManagerTarget = $"DotnetToolbar/{SolutionName}/{SolutionGuid}";

            var nugets = System.IO.Directory.GetFiles(this.SolutionDir, "*.nupkg", System.IO.SearchOption.AllDirectories).ToList();
            var shortnuget = new List<string>();
            nugets.ForEach(e => shortnuget.Add(e.Substring(this.SolutionDir.Length + 1)));

            var config = ReadNugetConfig();

            InitializeComponent();

            foreach (var nu in config)
            {
                if (!listBoxPackageSource.Items.Contains(nu.Value))
                {
                    listBoxPackageSource.Items.Add(nu.Value);
                }
            }

            listBoxPackages.Items.AddRange(shortnuget.ToArray());

            if (listBoxPackages.Items.Count > 0)
            {
                listBoxPackages.SelectedIndex = 0;
                PackageLocation = listBoxPackages.Items[listBoxPackages.SelectedIndex].ToString();
            }

            if (listBoxPackageSource.Items.Count > 0)
            {
                listBoxPackageSource.SelectedIndex = 0;
                Source = listBoxPackageSource.Items[listBoxPackageSource.SelectedIndex].ToString();
            }

            LoadDotnetToolbarCredential();
        }

        private Dictionary<string, string> ReadNugetConfig()
        {
            string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(roamingPath, @"NuGet\nuget.config");

            string fileContents = File.ReadAllText(filePath);
            XDocument doc = XDocument.Parse(fileContents);

            Dictionary<string, string> packageSources = new Dictionary<string, string>();

            foreach (var source in doc.Descendants("packageSources").Elements("add"))
            {
                string key = source.Attribute("key").Value;
                string value = source.Attribute("value").Value;
                packageSources.Add(key, value);
            }

            return packageSources;
        }

        private void LoadDotnetToolbarCredential()
        {
            Credential credential = new Credential
            {
                Target = CredManagerTarget,
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
            Credential credential = new Credential
            {
                Username = $"{SolutionGuid}",
                Password = password,
                Target = CredManagerTarget,
                Type = CredentialType.Generic,
                Description = "",
                PersistanceType = PersistanceType.LocalComputer,
            };
            credential.Save();
        }

        private void FormNugetPush_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (nugetPushDialogResult == NugetPushDialogResult.Push || nugetPushDialogResult == NugetPushDialogResult.Cancel)
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
            if (listBoxPackages.SelectedIndex >= 0)
            {
                PackageLocation = listBoxPackages.Items[listBoxPackages.SelectedIndex].ToString();
            }
        }

        private void listBoxSource_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SaveUpdateCredential(textBoxApiKey.Text);
            if (listBoxPackageSource.SelectedIndex >= 0)
            {
                Source = listBoxPackageSource.Items[listBoxPackageSource.SelectedIndex].ToString();
            }
            LoadDotnetToolbarCredential();
        }
    }
}