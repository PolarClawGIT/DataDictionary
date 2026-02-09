namespace DataDictionary.Main.ApplicationWide
{
    partial class ApplicationOptions
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
            TableLayoutPanel optionsLayout;
            GroupBox applicationDataGroup;
            TableLayoutPanel onlineOptionsLayout;
            TableLayoutPanel offlineOptionsLayout;
            GroupBox userFolderGroup;
            TableLayoutPanel userFolderLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationOptions));
            defaultModeLayout = new TableLayoutPanel();
            defaultModeOnLine = new RadioButton();
            applicationOptionTab = new TabControl();
            databaseTab = new TabPage();
            serverNameData = new DataDictionary.Main.Controls.TextBoxData();
            databaseNameData = new DataDictionary.Main.Controls.TextBoxData();
            applicationRoleData = new DataDictionary.Main.Controls.TextBoxData();
            commandSaveToDatabase = new Button();
            commandLoadFromDatabase = new Button();
            applicationTab = new TabPage();
            applicationFileData = new DataDictionary.Main.Controls.TextBoxData();
            commandSaveToFile = new Button();
            commandLoadFromFile = new Button();
            defaultModeOffLine = new RadioButton();
            optionHelpDocumentation = new CheckBox();
            propertiesOption = new CheckBox();
            descriptionOption = new CheckBox();
            projectFolderData = new DataDictionary.Main.Controls.SelectTextBoxData();
            applicationDataFolder = new DataDictionary.Main.Controls.SelectTextBoxData();
            folderBrowserDialog = new FolderBrowserDialog();
            optionsLayout = new TableLayoutPanel();
            applicationDataGroup = new GroupBox();
            onlineOptionsLayout = new TableLayoutPanel();
            offlineOptionsLayout = new TableLayoutPanel();
            userFolderGroup = new GroupBox();
            userFolderLayout = new TableLayoutPanel();
            optionsLayout.SuspendLayout();
            applicationDataGroup.SuspendLayout();
            defaultModeLayout.SuspendLayout();
            applicationOptionTab.SuspendLayout();
            databaseTab.SuspendLayout();
            onlineOptionsLayout.SuspendLayout();
            applicationTab.SuspendLayout();
            offlineOptionsLayout.SuspendLayout();
            userFolderGroup.SuspendLayout();
            userFolderLayout.SuspendLayout();
            SuspendLayout();
            // 
            // optionsLayout
            // 
            optionsLayout.AutoSize = true;
            optionsLayout.ColumnCount = 1;
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            optionsLayout.Controls.Add(applicationDataGroup, 0, 0);
            optionsLayout.Controls.Add(userFolderGroup, 0, 1);
            optionsLayout.Dock = DockStyle.Fill;
            optionsLayout.Location = new Point(0, 25);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 3;
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            optionsLayout.Size = new Size(594, 467);
            optionsLayout.TabIndex = 0;
            // 
            // applicationDataGroup
            // 
            applicationDataGroup.AutoSize = true;
            applicationDataGroup.Controls.Add(defaultModeLayout);
            applicationDataGroup.Dock = DockStyle.Fill;
            applicationDataGroup.Location = new Point(3, 3);
            applicationDataGroup.Name = "applicationDataGroup";
            applicationDataGroup.Size = new Size(588, 320);
            applicationDataGroup.TabIndex = 2;
            applicationDataGroup.TabStop = false;
            applicationDataGroup.Text = "Application Data";
            // 
            // defaultModeLayout
            // 
            defaultModeLayout.AutoSize = true;
            defaultModeLayout.ColumnCount = 2;
            defaultModeLayout.ColumnStyles.Add(new ColumnStyle());
            defaultModeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            defaultModeLayout.Controls.Add(defaultModeOnLine, 0, 0);
            defaultModeLayout.Controls.Add(applicationOptionTab, 0, 3);
            defaultModeLayout.Controls.Add(defaultModeOffLine, 0, 1);
            defaultModeLayout.Controls.Add(optionHelpDocumentation, 1, 0);
            defaultModeLayout.Controls.Add(propertiesOption, 1, 1);
            defaultModeLayout.Controls.Add(descriptionOption, 1, 2);
            defaultModeLayout.Dock = DockStyle.Fill;
            defaultModeLayout.Location = new Point(3, 19);
            defaultModeLayout.Name = "defaultModeLayout";
            defaultModeLayout.RowCount = 4;
            defaultModeLayout.RowStyles.Add(new RowStyle());
            defaultModeLayout.RowStyles.Add(new RowStyle());
            defaultModeLayout.RowStyles.Add(new RowStyle());
            defaultModeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            defaultModeLayout.Size = new Size(582, 298);
            defaultModeLayout.TabIndex = 0;
            // 
            // defaultModeOnLine
            // 
            defaultModeOnLine.AutoSize = true;
            defaultModeOnLine.Location = new Point(3, 3);
            defaultModeOnLine.Name = "defaultModeOnLine";
            defaultModeOnLine.Size = new Size(183, 19);
            defaultModeOnLine.TabIndex = 0;
            defaultModeOnLine.TabStop = true;
            defaultModeOnLine.Text = "On-line (use shared Database)";
            defaultModeOnLine.UseVisualStyleBackColor = true;
            defaultModeOnLine.CheckedChanged += DefaultModeOnLine_CheckedChanged;
            // 
            // applicationOptionTab
            // 
            defaultModeLayout.SetColumnSpan(applicationOptionTab, 2);
            applicationOptionTab.Controls.Add(databaseTab);
            applicationOptionTab.Controls.Add(applicationTab);
            applicationOptionTab.Dock = DockStyle.Fill;
            applicationOptionTab.Location = new Point(3, 78);
            applicationOptionTab.Name = "applicationOptionTab";
            applicationOptionTab.SelectedIndex = 0;
            applicationOptionTab.Size = new Size(576, 217);
            applicationOptionTab.TabIndex = 4;
            // 
            // databaseTab
            // 
            databaseTab.BackColor = SystemColors.Control;
            databaseTab.Controls.Add(onlineOptionsLayout);
            databaseTab.Location = new Point(4, 24);
            databaseTab.Name = "databaseTab";
            databaseTab.Padding = new Padding(3);
            databaseTab.Size = new Size(568, 189);
            databaseTab.TabIndex = 0;
            databaseTab.Text = "On-Line (shared)";
            // 
            // onlineOptionsLayout
            // 
            onlineOptionsLayout.AutoSize = true;
            onlineOptionsLayout.ColumnCount = 3;
            onlineOptionsLayout.ColumnStyles.Add(new ColumnStyle());
            onlineOptionsLayout.ColumnStyles.Add(new ColumnStyle());
            onlineOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            onlineOptionsLayout.Controls.Add(serverNameData, 0, 0);
            onlineOptionsLayout.Controls.Add(databaseNameData, 0, 1);
            onlineOptionsLayout.Controls.Add(applicationRoleData, 0, 2);
            onlineOptionsLayout.Controls.Add(commandSaveToDatabase, 0, 3);
            onlineOptionsLayout.Controls.Add(commandLoadFromDatabase, 1, 3);
            onlineOptionsLayout.Dock = DockStyle.Fill;
            onlineOptionsLayout.Location = new Point(3, 3);
            onlineOptionsLayout.Name = "onlineOptionsLayout";
            onlineOptionsLayout.RowCount = 4;
            onlineOptionsLayout.RowStyles.Add(new RowStyle());
            onlineOptionsLayout.RowStyles.Add(new RowStyle());
            onlineOptionsLayout.RowStyles.Add(new RowStyle());
            onlineOptionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            onlineOptionsLayout.Size = new Size(562, 183);
            onlineOptionsLayout.TabIndex = 0;
            // 
            // serverNameData
            // 
            serverNameData.AutoSize = true;
            onlineOptionsLayout.SetColumnSpan(serverNameData, 3);
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.HeaderText = "Server Name";
            serverNameData.Location = new Point(3, 3);
            serverNameData.Multiline = false;
            serverNameData.Name = "serverNameData";
            serverNameData.ReadOnly = true;
            serverNameData.Size = new Size(556, 44);
            serverNameData.TabIndex = 0;
            serverNameData.WordWrap = true;
            // 
            // databaseNameData
            // 
            databaseNameData.AutoSize = true;
            onlineOptionsLayout.SetColumnSpan(databaseNameData, 3);
            databaseNameData.Dock = DockStyle.Fill;
            databaseNameData.HeaderText = "Database Name";
            databaseNameData.Location = new Point(3, 53);
            databaseNameData.Multiline = false;
            databaseNameData.Name = "databaseNameData";
            databaseNameData.ReadOnly = true;
            databaseNameData.Size = new Size(556, 44);
            databaseNameData.TabIndex = 1;
            databaseNameData.WordWrap = true;
            // 
            // applicationRoleData
            // 
            applicationRoleData.AutoSize = true;
            onlineOptionsLayout.SetColumnSpan(applicationRoleData, 3);
            applicationRoleData.Dock = DockStyle.Fill;
            applicationRoleData.HeaderText = "Application Role";
            applicationRoleData.Location = new Point(3, 103);
            applicationRoleData.Multiline = false;
            applicationRoleData.Name = "applicationRoleData";
            applicationRoleData.ReadOnly = true;
            applicationRoleData.Size = new Size(556, 44);
            applicationRoleData.TabIndex = 2;
            applicationRoleData.WordWrap = true;
            // 
            // commandSaveToDatabase
            // 
            commandSaveToDatabase.Location = new Point(3, 153);
            commandSaveToDatabase.Name = "commandSaveToDatabase";
            commandSaveToDatabase.Size = new Size(75, 23);
            commandSaveToDatabase.TabIndex = 4;
            commandSaveToDatabase.Text = "Save";
            commandSaveToDatabase.UseVisualStyleBackColor = true;
            commandSaveToDatabase.Click += commandSaveToDatabase_Click;
            // 
            // commandLoadFromDatabase
            // 
            commandLoadFromDatabase.Location = new Point(84, 153);
            commandLoadFromDatabase.Name = "commandLoadFromDatabase";
            commandLoadFromDatabase.Size = new Size(75, 23);
            commandLoadFromDatabase.TabIndex = 3;
            commandLoadFromDatabase.Text = "Load";
            commandLoadFromDatabase.UseVisualStyleBackColor = true;
            commandLoadFromDatabase.Click += commandLoadFromDatabase_Click;
            // 
            // applicationTab
            // 
            applicationTab.BackColor = SystemColors.Control;
            applicationTab.Controls.Add(offlineOptionsLayout);
            applicationTab.Location = new Point(4, 24);
            applicationTab.Name = "applicationTab";
            applicationTab.Padding = new Padding(3);
            applicationTab.Size = new Size(568, 189);
            applicationTab.TabIndex = 1;
            applicationTab.Text = "Off-Line (local)";
            // 
            // offlineOptionsLayout
            // 
            offlineOptionsLayout.AutoSize = true;
            offlineOptionsLayout.ColumnCount = 3;
            offlineOptionsLayout.ColumnStyles.Add(new ColumnStyle());
            offlineOptionsLayout.ColumnStyles.Add(new ColumnStyle());
            offlineOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            offlineOptionsLayout.Controls.Add(applicationFileData, 0, 0);
            offlineOptionsLayout.Controls.Add(commandSaveToFile, 0, 1);
            offlineOptionsLayout.Controls.Add(commandLoadFromFile, 1, 1);
            offlineOptionsLayout.Dock = DockStyle.Fill;
            offlineOptionsLayout.Location = new Point(3, 3);
            offlineOptionsLayout.Name = "offlineOptionsLayout";
            offlineOptionsLayout.RowCount = 2;
            offlineOptionsLayout.RowStyles.Add(new RowStyle());
            offlineOptionsLayout.RowStyles.Add(new RowStyle());
            offlineOptionsLayout.Size = new Size(562, 183);
            offlineOptionsLayout.TabIndex = 0;
            // 
            // applicationFileData
            // 
            applicationFileData.AutoSize = true;
            offlineOptionsLayout.SetColumnSpan(applicationFileData, 3);
            applicationFileData.Dock = DockStyle.Fill;
            applicationFileData.HeaderText = "Application File";
            applicationFileData.Location = new Point(3, 3);
            applicationFileData.Multiline = false;
            applicationFileData.Name = "applicationFileData";
            applicationFileData.ReadOnly = true;
            applicationFileData.Size = new Size(556, 44);
            applicationFileData.TabIndex = 0;
            applicationFileData.WordWrap = true;
            // 
            // commandSaveToFile
            // 
            commandSaveToFile.Location = new Point(3, 53);
            commandSaveToFile.Name = "commandSaveToFile";
            commandSaveToFile.Size = new Size(75, 23);
            commandSaveToFile.TabIndex = 1;
            commandSaveToFile.Text = "Save";
            commandSaveToFile.UseVisualStyleBackColor = true;
            commandSaveToFile.Click += commandSaveToFile_Click;
            // 
            // commandLoadFromFile
            // 
            commandLoadFromFile.Location = new Point(84, 53);
            commandLoadFromFile.Name = "commandLoadFromFile";
            commandLoadFromFile.Size = new Size(75, 23);
            commandLoadFromFile.TabIndex = 2;
            commandLoadFromFile.Text = "Load";
            commandLoadFromFile.UseVisualStyleBackColor = true;
            commandLoadFromFile.Click += CommandLoadFromFile_Click;
            // 
            // defaultModeOffLine
            // 
            defaultModeOffLine.AutoSize = true;
            defaultModeOffLine.Location = new Point(3, 28);
            defaultModeOffLine.Name = "defaultModeOffLine";
            defaultModeOffLine.Size = new Size(183, 19);
            defaultModeOffLine.TabIndex = 1;
            defaultModeOffLine.TabStop = true;
            defaultModeOffLine.Text = "Off-Line (use Application File)";
            defaultModeOffLine.UseVisualStyleBackColor = true;
            // 
            // optionHelpDocumentation
            // 
            optionHelpDocumentation.AutoSize = true;
            optionHelpDocumentation.Checked = true;
            optionHelpDocumentation.CheckState = CheckState.Checked;
            optionHelpDocumentation.Enabled = false;
            optionHelpDocumentation.Location = new Point(192, 3);
            optionHelpDocumentation.Name = "optionHelpDocumentation";
            optionHelpDocumentation.Size = new Size(160, 19);
            optionHelpDocumentation.TabIndex = 3;
            optionHelpDocumentation.Text = "Help and Documentation";
            optionHelpDocumentation.UseVisualStyleBackColor = true;
            // 
            // propertiesOption
            // 
            propertiesOption.AutoSize = true;
            propertiesOption.Checked = true;
            propertiesOption.CheckState = CheckState.Checked;
            propertiesOption.Enabled = false;
            propertiesOption.Location = new Point(192, 28);
            propertiesOption.Name = "propertiesOption";
            propertiesOption.Size = new Size(208, 19);
            propertiesOption.TabIndex = 4;
            propertiesOption.Text = "Attribute and Entity Property types";
            propertiesOption.UseVisualStyleBackColor = true;
            // 
            // descriptionOption
            // 
            descriptionOption.AutoSize = true;
            descriptionOption.Checked = true;
            descriptionOption.CheckState = CheckState.Checked;
            descriptionOption.Enabled = false;
            descriptionOption.Location = new Point(192, 53);
            descriptionOption.Name = "descriptionOption";
            descriptionOption.Size = new Size(215, 19);
            descriptionOption.TabIndex = 5;
            descriptionOption.Text = "Attribute and Entity Definition types";
            descriptionOption.UseVisualStyleBackColor = true;
            // 
            // userFolderGroup
            // 
            userFolderGroup.AutoSize = true;
            userFolderGroup.Controls.Add(userFolderLayout);
            userFolderGroup.Dock = DockStyle.Fill;
            userFolderGroup.Location = new Point(3, 329);
            userFolderGroup.Name = "userFolderGroup";
            userFolderGroup.Size = new Size(588, 122);
            userFolderGroup.TabIndex = 3;
            userFolderGroup.TabStop = false;
            userFolderGroup.Text = "User Folders";
            // 
            // userFolderLayout
            // 
            userFolderLayout.AutoSize = true;
            userFolderLayout.ColumnCount = 1;
            userFolderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            userFolderLayout.Controls.Add(projectFolderData, 0, 0);
            userFolderLayout.Controls.Add(applicationDataFolder, 0, 1);
            userFolderLayout.Dock = DockStyle.Fill;
            userFolderLayout.Location = new Point(3, 19);
            userFolderLayout.Name = "userFolderLayout";
            userFolderLayout.RowCount = 2;
            userFolderLayout.RowStyles.Add(new RowStyle());
            userFolderLayout.RowStyles.Add(new RowStyle());
            userFolderLayout.Size = new Size(582, 100);
            userFolderLayout.TabIndex = 0;
            // 
            // projectFolderData
            // 
            projectFolderData.AutoSize = true;
            projectFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            projectFolderData.Dock = DockStyle.Fill;
            projectFolderData.HeaderText = "Visual Studio Project folder";
            projectFolderData.Location = new Point(3, 3);
            projectFolderData.Name = "projectFolderData";
            projectFolderData.ReadOnly = false;
            projectFolderData.SelectIcon = (Image)resources.GetObject("projectFolderData.SelectIcon");
            projectFolderData.Size = new Size(576, 44);
            projectFolderData.TabIndex = 0;
            projectFolderData.SelectCommand += ProjectFolderData_SelectCommand;
            // 
            // applicationDataFolder
            // 
            applicationDataFolder.AutoSize = true;
            applicationDataFolder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            applicationDataFolder.Dock = DockStyle.Fill;
            applicationDataFolder.HeaderText = "Application Data Folder";
            applicationDataFolder.Location = new Point(3, 53);
            applicationDataFolder.Name = "applicationDataFolder";
            applicationDataFolder.ReadOnly = false;
            applicationDataFolder.SelectIcon = (Image)resources.GetObject("applicationDataFolder.SelectIcon");
            applicationDataFolder.Size = new Size(576, 44);
            applicationDataFolder.TabIndex = 1;
            applicationDataFolder.SelectCommand += ApplicationDataFolder_SelectCommand;
            // 
            // ApplicationOptions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(594, 492);
            Controls.Add(optionsLayout);
            Name = "ApplicationOptions";
            Text = "Application Options";
            Load += ApplicationOptions_Load;
            Controls.SetChildIndex(optionsLayout, 0);
            optionsLayout.ResumeLayout(false);
            optionsLayout.PerformLayout();
            applicationDataGroup.ResumeLayout(false);
            applicationDataGroup.PerformLayout();
            defaultModeLayout.ResumeLayout(false);
            defaultModeLayout.PerformLayout();
            applicationOptionTab.ResumeLayout(false);
            databaseTab.ResumeLayout(false);
            databaseTab.PerformLayout();
            onlineOptionsLayout.ResumeLayout(false);
            onlineOptionsLayout.PerformLayout();
            applicationTab.ResumeLayout(false);
            applicationTab.PerformLayout();
            offlineOptionsLayout.ResumeLayout(false);
            offlineOptionsLayout.PerformLayout();
            userFolderGroup.ResumeLayout(false);
            userFolderGroup.PerformLayout();
            userFolderLayout.ResumeLayout(false);
            userFolderLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData applicationFileData;
        private Controls.TextBoxData serverNameData;
        private Controls.TextBoxData databaseNameData;
        private Controls.TextBoxData applicationRoleData;
        private Button commandLoadFromDatabase;
        private Button commandSaveToFile;
        private RadioButton defaultModeOnLine;
        private RadioButton defaultModeOffLine;
        private Button commandLoadFromFile;
        private Button commandSaveToDatabase;
        private CheckBox optionHelpDocumentation;
        private CheckBox propertiesOption;
        private CheckBox descriptionOption;
        private Controls.SelectTextBoxData projectFolderData;
        private Controls.SelectTextBoxData applicationDataFolder;
        private TabControl applicationOptionTab;
        private TabPage databaseTab;
        private TabPage applicationTab;
        private FolderBrowserDialog folderBrowserDialog;
        private TableLayoutPanel defaultModeLayout;
    }
}