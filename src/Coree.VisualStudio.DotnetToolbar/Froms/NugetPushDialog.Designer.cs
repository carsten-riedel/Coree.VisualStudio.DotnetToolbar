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
            this.labelPackages = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.labelApiKey = new System.Windows.Forms.Label();
            this.buttonPush = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewPackageSources = new System.Windows.Forms.ListView();
            this.listViewNugetPackages = new System.Windows.Forms.ListView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddLocalPackageSource = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPackageSource
            // 
            this.labelPackageSource.AutoSize = true;
            this.labelPackageSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPackageSource.Location = new System.Drawing.Point(4, 0);
            this.labelPackageSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPackageSource.Name = "labelPackageSource";
            this.labelPackageSource.Size = new System.Drawing.Size(165, 15);
            this.labelPackageSource.TabIndex = 0;
            this.labelPackageSource.Text = "Specifies the server URL target";
            // 
            // labelPackages
            // 
            this.labelPackages.AutoSize = true;
            this.labelPackages.Location = new System.Drawing.Point(4, 158);
            this.labelPackages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPackages.Name = "labelPackages";
            this.labelPackages.Size = new System.Drawing.Size(240, 15);
            this.labelPackages.TabIndex = 3;
            this.labelPackages.Text = "Packages (Below the solution root directory)";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxApiKey.Location = new System.Drawing.Point(4, 304);
            this.textBoxApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(635, 23);
            this.textBoxApiKey.TabIndex = 4;
            this.textBoxApiKey.UseSystemPasswordChar = true;
            this.textBoxApiKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxApiKey_KeyDown);
            // 
            // labelApiKey
            // 
            this.labelApiKey.AutoSize = true;
            this.labelApiKey.Location = new System.Drawing.Point(4, 285);
            this.labelApiKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelApiKey.Name = "labelApiKey";
            this.labelApiKey.Size = new System.Drawing.Size(395, 15);
            this.labelApiKey.TabIndex = 5;
            this.labelApiKey.Text = "API Key (Per-Solution and target storage in Windows Credential Manager)";
            // 
            // buttonPush
            // 
            this.buttonPush.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPush.Location = new System.Drawing.Point(307, 4);
            this.buttonPush.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPush.Name = "buttonPush";
            this.buttonPush.Size = new System.Drawing.Size(162, 49);
            this.buttonPush.TabIndex = 6;
            this.buttonPush.Text = "Push";
            this.buttonPush.UseVisualStyleBackColor = true;
            this.buttonPush.Click += new System.EventHandler(this.buttonPush_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(477, 4);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(162, 49);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listViewPackageSources, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewNugetPackages, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelPackageSource, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPackages, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelApiKey, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxApiKey, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.buttonAddLocalPackageSource, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(643, 388);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // listViewPackageSources
            // 
            this.listViewPackageSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPackageSources.HideSelection = false;
            this.listViewPackageSources.Location = new System.Drawing.Point(3, 18);
            this.listViewPackageSources.Name = "listViewPackageSources";
            this.listViewPackageSources.Size = new System.Drawing.Size(637, 106);
            this.listViewPackageSources.TabIndex = 2;
            this.listViewPackageSources.UseCompatibleStateImageBehavior = false;
            // 
            // listViewNugetPackages
            // 
            this.listViewNugetPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewNugetPackages.HideSelection = false;
            this.listViewNugetPackages.Location = new System.Drawing.Point(3, 176);
            this.listViewNugetPackages.Name = "listViewNugetPackages";
            this.listViewNugetPackages.Size = new System.Drawing.Size(637, 106);
            this.listViewNugetPackages.TabIndex = 3;
            this.listViewNugetPackages.UseCompatibleStateImageBehavior = false;
            this.listViewNugetPackages.View = System.Windows.Forms.View.Details;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel2.Controls.Add(this.buttonPush, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonCancel, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 331);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(643, 57);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // buttonAddLocalPackageSource
            // 
            this.buttonAddLocalPackageSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddLocalPackageSource.Location = new System.Drawing.Point(468, 130);
            this.buttonAddLocalPackageSource.Name = "buttonAddLocalPackageSource";
            this.buttonAddLocalPackageSource.Size = new System.Drawing.Size(172, 25);
            this.buttonAddLocalPackageSource.TabIndex = 7;
            this.buttonAddLocalPackageSource.Text = "Add local package source";
            this.buttonAddLocalPackageSource.UseVisualStyleBackColor = true;
            this.buttonAddLocalPackageSource.Click += new System.EventHandler(this.buttonAddLocalPackageSource_Click);
            // 
            // NugetPushDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 388);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NugetPushDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotnetToolbar";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNugetPush_FormClosing);
            this.Shown += new System.EventHandler(this.NugetPushDialog_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelPackageSource;
        private System.Windows.Forms.Label labelPackages;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label labelApiKey;
        private System.Windows.Forms.Button buttonPush;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListView listViewNugetPackages;
        private System.Windows.Forms.ListView listViewPackageSources;
        private System.Windows.Forms.Button buttonAddLocalPackageSource;
    }
}