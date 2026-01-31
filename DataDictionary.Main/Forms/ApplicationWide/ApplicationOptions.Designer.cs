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
            GroupBox optionsDefaultModeGroup;
            TableLayoutPanel defaultModeLayut;
            GroupBox onlineOptionsGroup;
            TableLayoutPanel onlineOptionsLayout;
            TableLayoutPanel onlineButtonLayout;
            TableLayoutPanel applicationDataLayout;
            GroupBox optionsOfflineGroup;
            TableLayoutPanel offlineOptionsLayout;
            TableLayoutPanel offLineButtonLayout;
            GroupBox userFolderGroup;
            TableLayoutPanel userFolderLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationOptions));
            defaultModeOnLine = new RadioButton();
            defaultModeOffLine = new RadioButton();
            optionHelpDocumentation = new CheckBox();
            propertiesOption = new CheckBox();
            descriptionOption = new CheckBox();
            applicationOptionTab = new TabControl();
            databaseTab = new TabPage();
            serverNameData = new DataDictionary.Main.Controls.TextBoxData();
            databaseNameData = new DataDictionary.Main.Controls.TextBoxData();
            applicationRoleData = new DataDictionary.Main.Controls.TextBoxData();
            commandLoadFromDatabase = new Button();
            commandSaveToDatabase = new Button();
            applicationTab = new TabPage();
            applicationFileData = new DataDictionary.Main.Controls.TextBoxData();
            commandSaveToFile = new Button();
            commandLoadFromFile = new Button();
            projectFolderData = new DataDictionary.Main.Controls.SelectTextBoxData();
            applicationDataFolder = new DataDictionary.Main.Controls.SelectTextBoxData();
            folderBrowserDialog = new FolderBrowserDialog();
            optionsLayout = new TableLayoutPanel();
            optionsDefaultModeGroup = new GroupBox();
            defaultModeLayut = new TableLayoutPanel();
            onlineOptionsGroup = new GroupBox();
            onlineOptionsLayout = new TableLayoutPanel();
            onlineButtonLayout = new TableLayoutPanel();
            applicationDataLayout = new TableLayoutPanel();
            optionsOfflineGroup = new GroupBox();
            offlineOptionsLayout = new TableLayoutPanel();
            offLineButtonLayout = new TableLayoutPanel();
            userFolderGroup = new GroupBox();
            userFolderLayout = new TableLayoutPanel();
            optionsLayout.SuspendLayout();
            optionsDefaultModeGroup.SuspendLayout();
            defaultModeLayut.SuspendLayout();
            applicationOptionTab.SuspendLayout();
            databaseTab.SuspendLayout();
            onlineOptionsGroup.SuspendLayout();
            onlineOptionsLayout.SuspendLayout();
            onlineButtonLayout.SuspendLayout();
            applicationTab.SuspendLayout();
            applicationDataLayout.SuspendLayout();
            optionsOfflineGroup.SuspendLayout();
            offlineOptionsLayout.SuspendLayout();
            offLineButtonLayout.SuspendLayout();
            userFolderGroup.SuspendLayout();
            userFolderLayout.SuspendLayout();
            SuspendLayout();
            // 
            // optionsLayout
            // 
            optionsLayout.ColumnCount = 1;
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            optionsLayout.Controls.Add(optionsDefaultModeGroup, 0, 0);
            optionsLayout.Controls.Add(applicationOptionTab, 0, 1);
            optionsLayout.Dock = DockStyle.Fill;
            optionsLayout.Location = new Point(0, 25);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 2;
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            optionsLayout.Size = new Size(594, 555);
            optionsLayout.TabIndex = 0;
            // 
            // optionsDefaultModeGroup
            // 
            optionsDefaultModeGroup.Controls.Add(defaultModeLayut);
            optionsDefaultModeGroup.Dock = DockStyle.Fill;
            optionsDefaultModeGroup.Location = new Point(3, 3);
            optionsDefaultModeGroup.Name = "optionsDefaultModeGroup";
            optionsDefaultModeGroup.Size = new Size(588, 114);
            optionsDefaultModeGroup.TabIndex = 2;
            optionsDefaultModeGroup.TabStop = false;
            optionsDefaultModeGroup.Text = "Default Mode";
            // 
            // defaultModeLayut
            // 
            defaultModeLayut.ColumnCount = 2;
            defaultModeLayut.ColumnStyles.Add(new ColumnStyle());
            defaultModeLayut.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            defaultModeLayut.Controls.Add(defaultModeOnLine, 0, 0);
            defaultModeLayut.Controls.Add(defaultModeOffLine, 0, 1);
            defaultModeLayut.Controls.Add(optionHelpDocumentation, 1, 0);
            defaultModeLayut.Controls.Add(propertiesOption, 1, 1);
            defaultModeLayut.Controls.Add(descriptionOption, 1, 2);
            defaultModeLayut.Dock = DockStyle.Fill;
            defaultModeLayut.Location = new Point(3, 19);
            defaultModeLayut.Name = "defaultModeLayut";
            defaultModeLayut.RowCount = 3;
            defaultModeLayut.RowStyles.Add(new RowStyle());
            defaultModeLayut.RowStyles.Add(new RowStyle());
            defaultModeLayut.RowStyles.Add(new RowStyle());
            defaultModeLayut.Size = new Size(582, 92);
            defaultModeLayut.TabIndex = 0;
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
            // applicationOptionTab
            // 
            applicationOptionTab.Controls.Add(databaseTab);
            applicationOptionTab.Controls.Add(applicationTab);
            applicationOptionTab.Dock = DockStyle.Fill;
            applicationOptionTab.Location = new Point(3, 123);
            applicationOptionTab.Name = "applicationOptionTab";
            applicationOptionTab.SelectedIndex = 0;
            applicationOptionTab.Size = new Size(588, 429);
            applicationOptionTab.TabIndex = 4;
            // 
            // databaseTab
            // 
            databaseTab.BackColor = SystemColors.Control;
            databaseTab.Controls.Add(onlineOptionsGroup);
            databaseTab.Location = new Point(4, 24);
            databaseTab.Name = "databaseTab";
            databaseTab.Padding = new Padding(3);
            databaseTab.Size = new Size(580, 401);
            databaseTab.TabIndex = 0;
            databaseTab.Text = "Database";
            // 
            // onlineOptionsGroup
            // 
            onlineOptionsGroup.AutoSize = true;
            onlineOptionsGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            onlineOptionsGroup.Controls.Add(onlineOptionsLayout);
            onlineOptionsGroup.Dock = DockStyle.Fill;
            onlineOptionsGroup.Location = new Point(3, 3);
            onlineOptionsGroup.Name = "onlineOptionsGroup";
            onlineOptionsGroup.Size = new Size(574, 395);
            onlineOptionsGroup.TabIndex = 0;
            onlineOptionsGroup.TabStop = false;
            onlineOptionsGroup.Text = "On-line (shared Database)";
            // 
            // onlineOptionsLayout
            // 
            onlineOptionsLayout.ColumnCount = 1;
            onlineOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            onlineOptionsLayout.Controls.Add(serverNameData, 0, 0);
            onlineOptionsLayout.Controls.Add(databaseNameData, 0, 1);
            onlineOptionsLayout.Controls.Add(applicationRoleData, 0, 2);
            onlineOptionsLayout.Controls.Add(onlineButtonLayout, 0, 3);
            onlineOptionsLayout.Dock = DockStyle.Fill;
            onlineOptionsLayout.Location = new Point(3, 19);
            onlineOptionsLayout.Name = "onlineOptionsLayout";
            onlineOptionsLayout.RowCount = 4;
            onlineOptionsLayout.RowStyles.Add(new RowStyle());
            onlineOptionsLayout.RowStyles.Add(new RowStyle());
            onlineOptionsLayout.RowStyles.Add(new RowStyle());
            onlineOptionsLayout.RowStyles.Add(new RowStyle());
            onlineOptionsLayout.Size = new Size(568, 373);
            onlineOptionsLayout.TabIndex = 0;
            // 
            // serverNameData
            // 
            serverNameData.AutoSize = true;
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.HeaderText = "Server Name";
            serverNameData.Location = new Point(3, 3);
            serverNameData.Multiline = false;
            serverNameData.Name = "serverNameData";
            serverNameData.ReadOnly = true;
            serverNameData.Size = new Size(562, 44);
            serverNameData.TabIndex = 0;
            serverNameData.WordWrap = true;
            // 
            // databaseNameData
            // 
            databaseNameData.AutoSize = true;
            databaseNameData.Dock = DockStyle.Fill;
            databaseNameData.HeaderText = "Database Name";
            databaseNameData.Location = new Point(3, 53);
            databaseNameData.Multiline = false;
            databaseNameData.Name = "databaseNameData";
            databaseNameData.ReadOnly = true;
            databaseNameData.Size = new Size(562, 44);
            databaseNameData.TabIndex = 1;
            databaseNameData.WordWrap = true;
            // 
            // applicationRoleData
            // 
            applicationRoleData.AutoSize = true;
            applicationRoleData.Dock = DockStyle.Fill;
            applicationRoleData.HeaderText = "Application Role";
            applicationRoleData.Location = new Point(3, 103);
            applicationRoleData.Multiline = false;
            applicationRoleData.Name = "applicationRoleData";
            applicationRoleData.ReadOnly = true;
            applicationRoleData.Size = new Size(562, 44);
            applicationRoleData.TabIndex = 2;
            applicationRoleData.WordWrap = true;
            // 
            // onlineButtonLayout
            // 
            onlineButtonLayout.AutoSize = true;
            onlineButtonLayout.ColumnCount = 3;
            onlineButtonLayout.ColumnStyles.Add(new ColumnStyle());
            onlineButtonLayout.ColumnStyles.Add(new ColumnStyle());
            onlineButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            onlineButtonLayout.Controls.Add(commandLoadFromDatabase, 1, 0);
            onlineButtonLayout.Controls.Add(commandSaveToDatabase, 0, 0);
            onlineButtonLayout.Dock = DockStyle.Fill;
            onlineButtonLayout.Location = new Point(3, 153);
            onlineButtonLayout.Name = "onlineButtonLayout";
            onlineButtonLayout.RowCount = 1;
            onlineButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            onlineButtonLayout.Size = new Size(562, 217);
            onlineButtonLayout.TabIndex = 3;
            // 
            // commandLoadFromDatabase
            // 
            commandLoadFromDatabase.Location = new Point(84, 3);
            commandLoadFromDatabase.Name = "commandLoadFromDatabase";
            commandLoadFromDatabase.Size = new Size(75, 23);
            commandLoadFromDatabase.TabIndex = 3;
            commandLoadFromDatabase.Text = "Load";
            commandLoadFromDatabase.UseVisualStyleBackColor = true;
            commandLoadFromDatabase.Click += commandLoadFromDatabase_Click;
            // 
            // commandSaveToDatabase
            // 
            commandSaveToDatabase.Location = new Point(3, 3);
            commandSaveToDatabase.Name = "commandSaveToDatabase";
            commandSaveToDatabase.Size = new Size(75, 23);
            commandSaveToDatabase.TabIndex = 4;
            commandSaveToDatabase.Text = "Save";
            commandSaveToDatabase.UseVisualStyleBackColor = true;
            commandSaveToDatabase.Click += commandSaveToDatabase_Click;
            // 
            // applicationTab
            // 
            applicationTab.BackColor = SystemColors.Control;
            applicationTab.Controls.Add(applicationDataLayout);
            applicationTab.Location = new Point(4, 24);
            applicationTab.Name = "applicationTab";
            applicationTab.Padding = new Padding(3);
            applicationTab.Size = new Size(580, 401);
            applicationTab.TabIndex = 1;
            applicationTab.Text = "Application";
            // 
            // applicationDataLayout
            // 
            applicationDataLayout.ColumnCount = 1;
            applicationDataLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            applicationDataLayout.Controls.Add(optionsOfflineGroup, 0, 0);
            applicationDataLayout.Controls.Add(userFolderGroup, 0, 1);
            applicationDataLayout.Dock = DockStyle.Fill;
            applicationDataLayout.Location = new Point(3, 3);
            applicationDataLayout.Name = "applicationDataLayout";
            applicationDataLayout.RowCount = 3;
            applicationDataLayout.RowStyles.Add(new RowStyle());
            applicationDataLayout.RowStyles.Add(new RowStyle());
            applicationDataLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            applicationDataLayout.Size = new Size(574, 395);
            applicationDataLayout.TabIndex = 0;
            // 
            // optionsOfflineGroup
            // 
            optionsOfflineGroup.AutoSize = true;
            optionsOfflineGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsOfflineGroup.Controls.Add(offlineOptionsLayout);
            optionsOfflineGroup.Dock = DockStyle.Fill;
            optionsOfflineGroup.Location = new Point(3, 3);
            optionsOfflineGroup.Name = "optionsOfflineGroup";
            optionsOfflineGroup.Size = new Size(568, 107);
            optionsOfflineGroup.TabIndex = 1;
            optionsOfflineGroup.TabStop = false;
            optionsOfflineGroup.Text = "Off-Line (local Application File)";
            // 
            // offlineOptionsLayout
            // 
            offlineOptionsLayout.AutoSize = true;
            offlineOptionsLayout.ColumnCount = 1;
            offlineOptionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            offlineOptionsLayout.Controls.Add(applicationFileData, 0, 0);
            offlineOptionsLayout.Controls.Add(offLineButtonLayout, 0, 1);
            offlineOptionsLayout.Dock = DockStyle.Fill;
            offlineOptionsLayout.Location = new Point(3, 19);
            offlineOptionsLayout.Name = "offlineOptionsLayout";
            offlineOptionsLayout.RowCount = 2;
            offlineOptionsLayout.RowStyles.Add(new RowStyle());
            offlineOptionsLayout.RowStyles.Add(new RowStyle());
            offlineOptionsLayout.Size = new Size(562, 85);
            offlineOptionsLayout.TabIndex = 0;
            // 
            // applicationFileData
            // 
            applicationFileData.AutoSize = true;
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
            // offLineButtonLayout
            // 
            offLineButtonLayout.AutoSize = true;
            offLineButtonLayout.ColumnCount = 3;
            offLineButtonLayout.ColumnStyles.Add(new ColumnStyle());
            offLineButtonLayout.ColumnStyles.Add(new ColumnStyle());
            offLineButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            offLineButtonLayout.Controls.Add(commandSaveToFile, 0, 0);
            offLineButtonLayout.Controls.Add(commandLoadFromFile, 1, 0);
            offLineButtonLayout.Dock = DockStyle.Fill;
            offLineButtonLayout.Location = new Point(3, 53);
            offLineButtonLayout.Name = "offLineButtonLayout";
            offLineButtonLayout.RowCount = 1;
            offLineButtonLayout.RowStyles.Add(new RowStyle());
            offLineButtonLayout.Size = new Size(556, 29);
            offLineButtonLayout.TabIndex = 1;
            // 
            // commandSaveToFile
            // 
            commandSaveToFile.Location = new Point(3, 3);
            commandSaveToFile.Name = "commandSaveToFile";
            commandSaveToFile.Size = new Size(75, 23);
            commandSaveToFile.TabIndex = 1;
            commandSaveToFile.Text = "Save";
            commandSaveToFile.UseVisualStyleBackColor = true;
            commandSaveToFile.Click += commandSaveToFile_Click;
            // 
            // commandLoadFromFile
            // 
            commandLoadFromFile.Location = new Point(84, 3);
            commandLoadFromFile.Name = "commandLoadFromFile";
            commandLoadFromFile.Size = new Size(75, 23);
            commandLoadFromFile.TabIndex = 2;
            commandLoadFromFile.Text = "Load";
            commandLoadFromFile.UseVisualStyleBackColor = true;
            commandLoadFromFile.Click += CommandLoadFromFile_Click;
            // 
            // userFolderGroup
            // 
            userFolderGroup.AutoSize = true;
            userFolderGroup.Controls.Add(userFolderLayout);
            userFolderGroup.Dock = DockStyle.Fill;
            userFolderGroup.Location = new Point(3, 116);
            userFolderGroup.Name = "userFolderGroup";
            userFolderGroup.Size = new Size(568, 122);
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
            userFolderLayout.Size = new Size(562, 100);
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
            projectFolderData.Size = new Size(556, 44);
            projectFolderData.TabIndex = 0;
            projectFolderData.Validated += ProjectFolderData_Validated;
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
            applicationDataFolder.Size = new Size(556, 44);
            applicationDataFolder.TabIndex = 1;
            applicationDataFolder.Validated += ApplicationDataFolder_Validated;
            applicationDataFolder.SelectCommand += ApplicationDataFolder_SelectCommand;
            // 
            // ApplicationOptions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(594, 580);
            Controls.Add(optionsLayout);
            Name = "ApplicationOptions";
            Text = "Application Options";
            Load += ApplicationOptions_Load;
            Controls.SetChildIndex(optionsLayout, 0);
            optionsLayout.ResumeLayout(false);
            optionsDefaultModeGroup.ResumeLayout(false);
            defaultModeLayut.ResumeLayout(false);
            defaultModeLayut.PerformLayout();
            applicationOptionTab.ResumeLayout(false);
            databaseTab.ResumeLayout(false);
            databaseTab.PerformLayout();
            onlineOptionsGroup.ResumeLayout(false);
            onlineOptionsLayout.ResumeLayout(false);
            onlineOptionsLayout.PerformLayout();
            onlineButtonLayout.ResumeLayout(false);
            applicationTab.ResumeLayout(false);
            applicationDataLayout.ResumeLayout(false);
            applicationDataLayout.PerformLayout();
            optionsOfflineGroup.ResumeLayout(false);
            optionsOfflineGroup.PerformLayout();
            offlineOptionsLayout.ResumeLayout(false);
            offlineOptionsLayout.PerformLayout();
            offLineButtonLayout.ResumeLayout(false);
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
    }
}