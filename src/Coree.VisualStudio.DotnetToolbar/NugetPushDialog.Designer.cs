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
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxSource = new System.Windows.Forms.ListBox();
            this.listBoxNupkg = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonPush = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Specifies the server URL";
            // 
            // listBoxSource
            // 
            this.listBoxSource.FormattingEnabled = true;
            this.listBoxSource.Items.AddRange(new object[] {
            "https://api.nuget.org/v3/index.json",
            "https://apiint.nugettest.org/v3/index.json"});
            this.listBoxSource.Location = new System.Drawing.Point(15, 25);
            this.listBoxSource.Name = "listBoxSource";
            this.listBoxSource.ScrollAlwaysVisible = true;
            this.listBoxSource.Size = new System.Drawing.Size(379, 56);
            this.listBoxSource.TabIndex = 1;
            this.listBoxSource.SelectedIndexChanged += new System.EventHandler(this.listBoxSource_SelectedIndexChanged);
            // 
            // listBoxNupkg
            // 
            this.listBoxNupkg.FormattingEnabled = true;
            this.listBoxNupkg.Location = new System.Drawing.Point(15, 100);
            this.listBoxNupkg.Name = "listBoxNupkg";
            this.listBoxNupkg.ScrollAlwaysVisible = true;
            this.listBoxNupkg.Size = new System.Drawing.Size(379, 56);
            this.listBoxNupkg.TabIndex = 2;
            this.listBoxNupkg.SelectedIndexChanged += new System.EventHandler(this.listBoxNupkg_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Packages";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(15, 175);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(379, 20);
            this.textBoxApiKey.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Api key (Stored in Windows Credential Manager)";
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
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxApiKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxNupkg);
            this.Controls.Add(this.listBoxSource);
            this.Controls.Add(this.label1);
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxSource;
        private System.Windows.Forms.ListBox listBoxNupkg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonPush;
        private System.Windows.Forms.Button buttonCancel;
    }
}