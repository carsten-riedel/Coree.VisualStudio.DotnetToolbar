namespace Coree.VisualStudio.DotnetToolbar
{
    partial class CommandSettingsForm
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
            this.checkBoxKillDotNet = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.checkBoxDisableConfirmDialog = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBoxBlockNonSdk = new System.Windows.Forms.CheckBox();
            this.tabPageBuild = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBoxAdditionalBuildCommandLineArguments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.tabPagePack = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.textBoxAdditionalPackCommandLineArguments = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.tabPageNugetPush = new System.Windows.Forms.TabPage();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.tabPagePublish = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAdditionalPublishCommandLineArguments = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.tabPageClean = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            this.textBoxAdditionalCleanCommandLineArguments = new System.Windows.Forms.TextBox();
            this.linkLabel8 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageBuild.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPagePack.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPageNugetPush.SuspendLayout();
            this.tabPagePublish.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPageClean.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxKillDotNet
            // 
            this.checkBoxKillDotNet.AutoSize = true;
            this.checkBoxKillDotNet.Location = new System.Drawing.Point(9, 8);
            this.checkBoxKillDotNet.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxKillDotNet.Name = "checkBoxKillDotNet";
            this.checkBoxKillDotNet.Size = new System.Drawing.Size(276, 21);
            this.checkBoxKillDotNet.TabIndex = 0;
            this.checkBoxKillDotNet.Text = "Kill all Dotnet processes before exectution.";
            this.checkBoxKillDotNet.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 355);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(823, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(823, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Enabled = false;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Enabled = false;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 330);
            this.panel1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageBuild);
            this.tabControl1.Controls.Add(this.tabPagePack);
            this.tabControl1.Controls.Add(this.tabPageNugetPush);
            this.tabControl1.Controls.Add(this.tabPagePublish);
            this.tabControl1.Controls.Add(this.tabPageClean);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(823, 330);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.checkBoxDisableConfirmDialog);
            this.tabPageGeneral.Controls.Add(this.comboBox1);
            this.tabPageGeneral.Controls.Add(this.checkBox3);
            this.tabPageGeneral.Controls.Add(this.checkBoxBlockNonSdk);
            this.tabPageGeneral.Controls.Add(this.checkBoxKillDotNet);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 26);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageGeneral.Size = new System.Drawing.Size(815, 300);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisableConfirmDialog
            // 
            this.checkBoxDisableConfirmDialog.AutoSize = true;
            this.checkBoxDisableConfirmDialog.Location = new System.Drawing.Point(9, 96);
            this.checkBoxDisableConfirmDialog.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxDisableConfirmDialog.Name = "checkBoxDisableConfirmDialog";
            this.checkBoxDisableConfirmDialog.Size = new System.Drawing.Size(388, 21);
            this.checkBoxDisableConfirmDialog.TabIndex = 5;
            this.checkBoxDisableConfirmDialog.Text = "Disable warning and confirmation prompts for folder deletion";
            this.checkBoxDisableConfirmDialog.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(376, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 25);
            this.comboBox1.TabIndex = 4;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(9, 67);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(360, 21);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Write dotnet global.json to solution directory if not exists";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBoxBlockNonSdk
            // 
            this.checkBoxBlockNonSdk.AutoSize = true;
            this.checkBoxBlockNonSdk.Location = new System.Drawing.Point(9, 38);
            this.checkBoxBlockNonSdk.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxBlockNonSdk.Name = "checkBoxBlockNonSdk";
            this.checkBoxBlockNonSdk.Size = new System.Drawing.Size(273, 21);
            this.checkBoxBlockNonSdk.TabIndex = 1;
            this.checkBoxBlockNonSdk.Text = "Block executing on non SDK style projects.";
            this.checkBoxBlockNonSdk.UseVisualStyleBackColor = true;
            // 
            // tabPageBuild
            // 
            this.tabPageBuild.Controls.Add(this.tableLayoutPanel1);
            this.tabPageBuild.Location = new System.Drawing.Point(4, 26);
            this.tabPageBuild.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageBuild.Name = "tabPageBuild";
            this.tabPageBuild.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageBuild.Size = new System.Drawing.Size(815, 300);
            this.tabPageBuild.TabIndex = 3;
            this.tabPageBuild.Text = "build";
            this.tabPageBuild.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAdditionalBuildCommandLineArguments, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(807, 292);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(4, 8);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(387, 17);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBoxAdditionalBuildCommandLineArguments
            // 
            this.textBoxAdditionalBuildCommandLineArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdditionalBuildCommandLineArguments.Location = new System.Drawing.Point(4, 103);
            this.textBoxAdditionalBuildCommandLineArguments.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalBuildCommandLineArguments.Multiline = true;
            this.textBoxAdditionalBuildCommandLineArguments.Name = "textBoxAdditionalBuildCommandLineArguments";
            this.textBoxAdditionalBuildCommandLineArguments.Size = new System.Drawing.Size(799, 185);
            this.textBoxAdditionalBuildCommandLineArguments.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Additional build command line arguments";
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(4, 41);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(610, 17);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-command-line-refer" +
    "ence?view=vs-2022";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // tabPagePack
            // 
            this.tabPagePack.Controls.Add(this.tableLayoutPanel2);
            this.tabPagePack.Location = new System.Drawing.Point(4, 26);
            this.tabPagePack.Margin = new System.Windows.Forms.Padding(4);
            this.tabPagePack.Name = "tabPagePack";
            this.tabPagePack.Padding = new System.Windows.Forms.Padding(4);
            this.tabPagePack.Size = new System.Drawing.Size(815, 300);
            this.tabPagePack.TabIndex = 4;
            this.tabPagePack.Text = "pack";
            this.tabPagePack.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.linkLabel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxAdditionalPackCommandLineArguments, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.linkLabel4, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(807, 292);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // linkLabel3
            // 
            this.linkLabel3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(4, 8);
            this.linkLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(385, 17);
            this.linkLabel3.TabIndex = 0;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-pack";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // textBoxAdditionalPackCommandLineArguments
            // 
            this.textBoxAdditionalPackCommandLineArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdditionalPackCommandLineArguments.Location = new System.Drawing.Point(4, 103);
            this.textBoxAdditionalPackCommandLineArguments.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalPackCommandLineArguments.Multiline = true;
            this.textBoxAdditionalPackCommandLineArguments.Name = "textBoxAdditionalPackCommandLineArguments";
            this.textBoxAdditionalPackCommandLineArguments.Size = new System.Drawing.Size(799, 185);
            this.textBoxAdditionalPackCommandLineArguments.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Additional pack command line arguments";
            // 
            // linkLabel4
            // 
            this.linkLabel4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(4, 41);
            this.linkLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(610, 17);
            this.linkLabel4.TabIndex = 1;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-command-line-refer" +
    "ence?view=vs-2022";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // tabPageNugetPush
            // 
            this.tabPageNugetPush.Controls.Add(this.checkBox4);
            this.tabPageNugetPush.Location = new System.Drawing.Point(4, 26);
            this.tabPageNugetPush.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageNugetPush.Name = "tabPageNugetPush";
            this.tabPageNugetPush.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageNugetPush.Size = new System.Drawing.Size(815, 300);
            this.tabPageNugetPush.TabIndex = 2;
            this.tabPageNugetPush.Text = "nuget push";
            this.tabPageNugetPush.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(9, 8);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(159, 21);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "Hide api-key in output.";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // tabPagePublish
            // 
            this.tabPagePublish.Controls.Add(this.tableLayoutPanel3);
            this.tabPagePublish.Location = new System.Drawing.Point(4, 26);
            this.tabPagePublish.Margin = new System.Windows.Forms.Padding(4);
            this.tabPagePublish.Name = "tabPagePublish";
            this.tabPagePublish.Padding = new System.Windows.Forms.Padding(4);
            this.tabPagePublish.Size = new System.Drawing.Size(815, 300);
            this.tabPagePublish.TabIndex = 1;
            this.tabPagePublish.Text = "publish";
            this.tabPagePublish.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxAdditionalPublishCommandLineArguments, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(807, 292);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Additional publish command line arguments";
            // 
            // textBoxAdditionalPublishCommandLineArguments
            // 
            this.textBoxAdditionalPublishCommandLineArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdditionalPublishCommandLineArguments.Location = new System.Drawing.Point(4, 102);
            this.textBoxAdditionalPublishCommandLineArguments.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalPublishCommandLineArguments.Multiline = true;
            this.textBoxAdditionalPublishCommandLineArguments.Name = "textBoxAdditionalPublishCommandLineArguments";
            this.textBoxAdditionalPublishCommandLineArguments.Size = new System.Drawing.Size(799, 186);
            this.textBoxAdditionalPublishCommandLineArguments.TabIndex = 6;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(807, 65);
            this.tableLayoutPanel5.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(625, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(178, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Publish on";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(88, 25);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(72, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Projects";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 25);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(73, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Solution";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.linkLabel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.linkLabel6, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(621, 65);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // linkLabel5
            // 
            this.linkLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(4, 8);
            this.linkLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(400, 17);
            this.linkLabel5.TabIndex = 3;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel6
            // 
            this.linkLabel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(4, 41);
            this.linkLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(610, 17);
            this.linkLabel6.TabIndex = 4;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-command-line-refer" +
    "ence?view=vs-2022";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // tabPageClean
            // 
            this.tabPageClean.Controls.Add(this.tableLayoutPanel6);
            this.tabPageClean.Location = new System.Drawing.Point(4, 26);
            this.tabPageClean.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageClean.Name = "tabPageClean";
            this.tabPageClean.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageClean.Size = new System.Drawing.Size(815, 300);
            this.tabPageClean.TabIndex = 5;
            this.tabPageClean.Text = "clean";
            this.tabPageClean.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.linkLabel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.textBoxAdditionalCleanCommandLineArguments, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.linkLabel8, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(807, 292);
            this.tableLayoutPanel6.TabIndex = 9;
            // 
            // linkLabel7
            // 
            this.linkLabel7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel7.AutoSize = true;
            this.linkLabel7.Location = new System.Drawing.Point(4, 8);
            this.linkLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(388, 17);
            this.linkLabel7.TabIndex = 0;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-clean";
            this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel7_LinkClicked);
            // 
            // textBoxAdditionalCleanCommandLineArguments
            // 
            this.textBoxAdditionalCleanCommandLineArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdditionalCleanCommandLineArguments.Location = new System.Drawing.Point(4, 103);
            this.textBoxAdditionalCleanCommandLineArguments.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalCleanCommandLineArguments.Multiline = true;
            this.textBoxAdditionalCleanCommandLineArguments.Name = "textBoxAdditionalCleanCommandLineArguments";
            this.textBoxAdditionalCleanCommandLineArguments.Size = new System.Drawing.Size(799, 185);
            this.textBoxAdditionalCleanCommandLineArguments.TabIndex = 8;
            // 
            // linkLabel8
            // 
            this.linkLabel8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabel8.AutoSize = true;
            this.linkLabel8.Location = new System.Drawing.Point(4, 41);
            this.linkLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel8.Name = "linkLabel8";
            this.linkLabel8.Size = new System.Drawing.Size(610, 17);
            this.linkLabel8.TabIndex = 1;
            this.linkLabel8.TabStop = true;
            this.linkLabel8.Text = "https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-command-line-refer" +
    "ence?view=vs-2022";
            this.linkLabel8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel8_LinkClicked);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 82);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Additional clean command line arguments";
            // 
            // CommandSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 377);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommandSettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotnetToolbar Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommandSettingsForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageBuild.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPagePack.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPageNugetPush.ResumeLayout(false);
            this.tabPageNugetPush.PerformLayout();
            this.tabPagePublish.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPageClean.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxKillDotNet;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.CheckBox checkBoxBlockNonSdk;
        private System.Windows.Forms.TabPage tabPagePublish;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageNugetPush;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TabPage tabPageBuild;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox textBoxAdditionalBuildCommandLineArguments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPagePack;
        private System.Windows.Forms.TabPage tabPageClean;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.TextBox textBoxAdditionalPackCommandLineArguments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.TextBox textBoxAdditionalPublishCommandLineArguments;
        private System.Windows.Forms.LinkLabel linkLabel8;
        private System.Windows.Forms.LinkLabel linkLabel7;
        private System.Windows.Forms.TextBox textBoxAdditionalCleanCommandLineArguments;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBoxDisableConfirmDialog;
    }
}