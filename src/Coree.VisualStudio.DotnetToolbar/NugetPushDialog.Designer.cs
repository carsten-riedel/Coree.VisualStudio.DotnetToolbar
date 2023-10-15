namespace Coree.VisualStudio.DotnetToolbar
{
    partial class NugetPushDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelPackageSource = new System.Windows.Forms.Label();
            this.listBoxPackageSource = new System.Windows.Forms.ListBox();
            this.listBoxPackages = new System.Windows.Forms.ListBox();
            this.labelPackages = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.labelApiKey = new System.Windows.Forms.Label();
            this.buttonPush = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelPackageSource
            // 
            this.labelPackageSource.AutoSize = true;
            this.labelPackageSource.Location = new System.Drawing.Point(12, 9);
            this.labelPackageSource.Name = "labelPackageSource";
            this.labelPackageSource.Size = new System.Drawing.Size(125, 13);
            this.labelPackageSource.TabIndex = 0;
            this.labelPackageSource.Text = "Specifies the server URL";
            // 
            // listBoxSource
            // 
            this.listBoxPackageSource.FormattingEnabled = true;
            this.listBoxPackageSource.Items.AddRange(new object[] {
            "https://api.nuget.org/v3/index.json",
            "https://apiint.nugettest.org/v3/index.json"});
            this.listBoxPackageSource.Location = new System.Drawing.Point(15, 25);
            this.listBoxPackageSource.Name = "listBoxSource";
            this.listBoxPackageSource.ScrollAlwaysVisible = true;
            this.listBoxPackageSource.Size = new System.Drawing.Size(379, 56);
            this.listBoxPackageSource.TabIndex = 1;
            this.listBoxPackageSource.SelectedIndexChanged += new System.EventHandler(this.listBoxSource_SelectedIndexChanged);
            // 
            // listBoxNupkg
            // 
            this.listBoxPackages.FormattingEnabled = true;
            this.listBoxPackages.Location = new System.Drawing.Point(15, 100);
            this.listBoxPackages.Name = "listBoxNupkg";
            this.listBoxPackages.ScrollAlwaysVisible = true;
            this.listBoxPackages.Size = new System.Drawing.Size(379, 56);
            this.listBoxPackages.TabIndex = 2;
            this.listBoxPackages.SelectedIndexChanged += new System.EventHandler(this.listBoxNupkg_SelectedIndexChanged);
            // 
            // labelPackages
            // 
            this.labelPackages.AutoSize = true;
            this.labelPackages.Location = new System.Drawing.Point(12, 84);
            this.labelPackages.Name = "labelPackages";
            this.labelPackages.Size = new System.Drawing.Size(55, 13);
            this.labelPackages.TabIndex = 3;
            this.labelPackages.Text = "Packages";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(15, 175);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(379, 20);
            this.textBoxApiKey.TabIndex = 4;
            // 
            // labelApiKey
            // 
            this.labelApiKey.AutoSize = true;
            this.labelApiKey.Location = new System.Drawing.Point(12, 159);
            this.labelApiKey.Name = "labelApiKey";
            this.labelApiKey.Size = new System.Drawing.Size(235, 13);
            this.labelApiKey.TabIndex = 5;
            this.labelApiKey.Text = "Api key (Stored in Windows Credential Manager)";
            // 
            // buttonPush
            // 
            this.buttonPush.Location = new System.Drawing.Point(198, 201);
            this.buttonPush.Name = "buttonPush";
            this.buttonPush.Size = new System.Drawing.Size(95, 23);
            this.buttonPush.TabIndex = 6;
            this.buttonPush.Text = "Push";
            this.buttonPush.UseVisualStyleBackColor = true;
            this.buttonPush.Click += new System.EventHandler(this.buttonPush_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(299, 201);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(95, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // NugetPushDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 235);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonPush);
            this.Controls.Add(this.labelApiKey);
            this.Controls.Add(this.textBoxApiKey);
            this.Controls.Add(this.labelPackages);
            this.Controls.Add(this.listBoxPackages);
            this.Controls.Add(this.listBoxPackageSource);
            this.Controls.Add(this.labelPackageSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NugetPushDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dotnet nuget push";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNugetPush_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPackageSource;
        private System.Windows.Forms.ListBox listBoxPackageSource;
        private System.Windows.Forms.ListBox listBoxPackages;
        private System.Windows.Forms.Label labelPackages;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label labelApiKey;
        private System.Windows.Forms.Button buttonPush;
        private System.Windows.Forms.Button buttonCancel;
    }
}