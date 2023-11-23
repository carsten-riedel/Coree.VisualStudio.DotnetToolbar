using Coree.VisualStudio.DotnetToolbar.ExtensionMethods;
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

        public PackageSources SelectedPackageSource { get; set; }
        public SemVerFileInfo SelectedPackageLocation { get; set; }
        public string ApiKey { get; set; }

        private string UserDataPath { get; set; }
        private string SolutionLocation { get; set; }
        public string SolutionDir { get; set; }
        private string SolutionName { get; set; }
        private string SolutionGuid { get; set; }

        public NugetPushDialog(string UserDataPath, string SolutionLocation, string SolutionName, string SolutionGuid)
        {
            this.UserDataPath = UserDataPath;
            this.SolutionLocation = SolutionLocation;
            this.SolutionName = SolutionName;
            this.SolutionGuid = SolutionGuid;
            this.SolutionDir = System.IO.Path.GetDirectoryName(SolutionLocation);

            var nugetFiles = System.IO.Directory.GetFiles(this.SolutionDir, "*.nupkg", System.IO.SearchOption.AllDirectories).ToList();

            SemVerPharser semVerPharser = new SemVerPharser(nugetFiles);
            semVerPharser.OrderMajorMinorPatchRevisionLastWriteTime();

            var config = ReadNugetConfig();
            List<PackageSources> packages = new List<PackageSources>();
            foreach (var item in config)
            {
                try
                {
                    Uri outUri;

                    if (Uri.TryCreate(item.Value, UriKind.Absolute, out outUri) && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps))
                    {
                        packages.Add(new PackageSources() { Key = item.Key, Value = item.Value, Source = PackageSource.nugetConfig, Type = PackageTypes.remote });
                    }
                    else if (System.IO.Directory.Exists(item.Value))
                    {
                        packages.Add(new PackageSources() { Key = item.Key, Value = item.Value, Source = PackageSource.nugetConfig, Type = PackageTypes.local });
                    }
                    else
                    {
                        packages.Add(new PackageSources() { Key = item.Key, Value = item.Value, Source = PackageSource.nugetConfig, Type = PackageTypes.none });
                    }
                }
                catch (Exception)
                {
                }
            }

            if (!packages.Any(e => e.Value == @"https://api.nuget.org/v3/index.json"))
            {
                packages.Add(new PackageSources() { Key = "Undefined", Value = @"https://api.nuget.org/v3/index.json", Source = PackageSource.@virtual, Type = PackageTypes.remote });
            }

            if (!packages.Any(e => e.Value == @"https://apiint.nugettest.org/v3/index.json"))
            {
                packages.Add(new PackageSources() { Key = "Undefined", Value = @"https://apiint.nugettest.org/v3/index.json", Source = PackageSource.@virtual, Type = PackageTypes.remote });
            }

            InitializeComponent();
            listViewPackageSources.AddClass(packages);
            listViewNugetPackages.AddClass<SemVerFileInfo>(semVerPharser.semVerFileInfos);

            if (listViewPackageSources.Items.Count > 0)
            {
                listViewPackageSources.Items[0].Selected = true;
                listViewPackageSources.Items[0].Focused = true;
                SelectedPackageSource = ((PackageSources)listViewPackageSources.Items[listViewPackageSources.GetSelectedIndex()].Tag);
            }

            var SettingsIndex = CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsNugetPush.LastPackageSourceIndex;
            if (SettingsIndex != -1)
            {
                if (SettingsIndex < listViewPackageSources.Items.Count)
                {
                    listViewPackageSources.Items[SettingsIndex].Selected = true;
                    listViewPackageSources.Items[SettingsIndex].Focused = true;
                    SelectedPackageSource = ((PackageSources)listViewPackageSources.Items[SettingsIndex].Tag);
                }
            }

            if (nugetFiles.Count == 0)
            {
                buttonPush.Enabled = false;
                buttonPush.Text = "No *.nupkg found";
            }

            if (listViewNugetPackages.Items.Count > 0)
            {
                listViewNugetPackages.Items[0].Selected = true;
                listViewNugetPackages.Items[0].Focused = true;
                SelectedPackageLocation = ((SemVerFileInfo)listViewNugetPackages.Items[listViewNugetPackages.GetSelectedIndex()].Tag);
            }

            this.listViewPackageSources.SelectedIndexChanged += new System.EventHandler(this.listViewPackageSources_SelectedIndexChanged);
            this.listViewNugetPackages.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
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
            var CredManagerTarget = $"DotnetToolbar/{SolutionName}/{SolutionGuid}/{SelectedPackageSource.Value}";
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
            if (password != string.Empty)
            {
                var CredManagerTarget = $"DotnetToolbar/{SolutionName}/{SolutionGuid}/{SelectedPackageSource.Value}";
                Credential credential = new Credential
                {
                    Username = $"{SolutionName}",
                    Password = password,
                    Target = CredManagerTarget,
                    Type = CredentialType.Generic,
                    Description = "",
                    PersistanceType = PersistanceType.LocalComputer,
                };
                credential.Save();
            }
        }

        private void FormNugetPush_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (nugetPushDialogResult == NugetPushDialogResult.Push || nugetPushDialogResult == NugetPushDialogResult.Cancel)
            {
                SaveUpdateCredential(textBoxApiKey.Text);
                ApiKey = textBoxApiKey.Text;
            }
            CoreeVisualStudioDotnetToolbarPackage.Instance.Settings.SolutionSettingsNugetPush.LastPackageSourceIndex = listViewPackageSources.GetSelectedIndex();
            JsonHelper.WriteToFile(CoreeVisualStudioDotnetToolbarPackage.Instance.Settings, CoreeVisualStudioDotnetToolbarPackage.Instance.SettingsFileName);
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewNugetPackages.GetSelectedIndex() != -1)
            {
                SelectedPackageLocation = ((SemVerFileInfo)listViewNugetPackages.Items[listViewNugetPackages.GetSelectedIndex()].Tag);
            }
        }

        private void listViewPackageSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveUpdateCredential(textBoxApiKey.Text);
            if (listViewPackageSources.GetSelectedIndex() >= 0)
            {
                SelectedPackageSource = ((PackageSources)listViewPackageSources.Items[listViewPackageSources.GetSelectedIndex()].Tag);
            }

            LoadDotnetToolbarCredential();
        }

        private void NugetPushDialog_Shown(object sender, EventArgs e)
        {
            listViewNugetPackages.Focus();
        }

        private void textBoxApiKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(textBoxApiKey.SelectedText);
                e.SuppressKeyPress = true;
            }
        }
    }
}