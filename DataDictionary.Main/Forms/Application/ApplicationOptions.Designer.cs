namespace DataDictionary.Main.Dialogs
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
            GroupBox onlineOptionsGroup;
            TableLayoutPanel onlineOptionsLayout;
            TableLayoutPanel onlineButtonLayout;
            GroupBox optionsOfflineGroup;
            TableLayoutPanel offlineOptionsLayout;
            TableLayoutPanel offLineButtonLayout;
            GroupBox optionsDefaultModeGroup;
            TableLayoutPanel defaultModeLayut;
            serverNameData = new Controls.TextBoxData();
            databaseNameData = new Controls.TextBoxData();
            applicationRoleData = new Controls.TextBoxData();
            commandLoadFromDatabase = new Button();
            commandSaveToDatabase = new Button();
            applicationFileData = new Controls.TextBoxData();
            commandSaveToFile = new Button();
            commandLoadFromFile = new Button();
            defaultModeOnLine = new RadioButton();
            defaultModeOffLine = new RadioButton();
            optionHelpDocumentation = new CheckBox();
            propertiesOption = new CheckBox();
            descriptionOption = new CheckBox();
            optionsLayout = new TableLayoutPanel();
            onlineOptionsGroup = new GroupBox();
            onlineOptionsLayout = new TableLayoutPanel();
            onlineButtonLayout = new TableLayoutPanel();
            optionsOfflineGroup = new GroupBox();
            offlineOptionsLayout = new TableLayoutPanel();
            offLineButtonLayout = new TableLayoutPanel();
            optionsDefaultModeGroup = new GroupBox();
            defaultModeLayut = new TableLayoutPanel();
            optionsLayout.SuspendLayout();
            onlineOptionsGroup.SuspendLayout();
            onlineOptionsLayout.SuspendLayout();
            onlineButtonLayout.SuspendLayout();
            optionsOfflineGroup.SuspendLayout();
            offlineOptionsLayout.SuspendLayout();
            offLineButtonLayout.SuspendLayout();
            optionsDefaultModeGroup.SuspendLayout();
            defaultModeLayut.SuspendLayout();
            SuspendLayout();
            // 
            // optionsLayout
            // 
            optionsLayout.ColumnCount = 1;
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            optionsLayout.Controls.Add(onlineOptionsGroup, 0, 1);
            optionsLayout.Controls.Add(optionsOfflineGroup, 0, 2);
            optionsLayout.Controls.Add(optionsDefaultModeGroup, 0, 0);
            optionsLayout.Dock = DockStyle.Fill;
            optionsLayout.Location = new Point(0, 0);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 4;
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            optionsLayout.Size = new Size(594, 538);
            optionsLayout.TabIndex = 0;
            // 
            // onlineOptionsGroup
            // 
            onlineOptionsGroup.AutoSize = true;
            onlineOptionsGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            onlineOptionsGroup.Controls.Add(onlineOptionsLayout);
            onlineOptionsGroup.Dock = DockStyle.Fill;
            onlineOptionsGroup.Location = new Point(3, 123);
            onlineOptionsGroup.Name = "onlineOptionsGroup";
            onlineOptionsGroup.Size = new Size(588, 214);
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
            onlineOptionsLayout.Size = new Size(582, 192);
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
            serverNameData.Size = new Size(576, 44);
            serverNameData.TabIndex = 0;
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
            databaseNameData.Size = new Size(576, 44);
            databaseNameData.TabIndex = 1;
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
            applicationRoleData.Size = new Size(576, 44);
            applicationRoleData.TabIndex = 2;
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
            onlineButtonLayout.Size = new Size(576, 36);
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
            // optionsOfflineGroup
            // 
            optionsOfflineGroup.AutoSize = true;
            optionsOfflineGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsOfflineGroup.Controls.Add(offlineOptionsLayout);
            optionsOfflineGroup.Dock = DockStyle.Fill;
            optionsOfflineGroup.Location = new Point(3, 343);
            optionsOfflineGroup.Name = "optionsOfflineGroup";
            optionsOfflineGroup.Size = new Size(588, 114);
            optionsOfflineGroup.TabIndex = 1;
            optionsOfflineGroup.TabStop = false;
            optionsOfflineGroup.Text = "Off-Line (local Application File)";
            // 
            // offlineOptionsLayout
            // 
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
            offlineOptionsLayout.Size = new Size(582, 92);
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
            applicationFileData.Size = new Size(576, 44);
            applicationFileData.TabIndex = 0;
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
            offLineButtonLayout.Size = new Size(576, 36);
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
            commandLoadFromFile.Click += commandLoadFromFile_Click;
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
            defaultModeOnLine.CheckedChanged += defaultModeOnLine_CheckedChanged;
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
            // ApplicationOptions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(594, 538);
            Controls.Add(optionsLayout);
            Name = "ApplicationOptions";
            Text = "Application Options";
            Load += ApplicationOptions_Load;
            optionsLayout.ResumeLayout(false);
            optionsLayout.PerformLayout();
            onlineOptionsGroup.ResumeLayout(false);
            onlineOptionsLayout.ResumeLayout(false);
            onlineOptionsLayout.PerformLayout();
            onlineButtonLayout.ResumeLayout(false);
            optionsOfflineGroup.ResumeLayout(false);
            offlineOptionsLayout.ResumeLayout(false);
            offlineOptionsLayout.PerformLayout();
            offLineButtonLayout.ResumeLayout(false);
            optionsDefaultModeGroup.ResumeLayout(false);
            defaultModeLayut.ResumeLayout(false);
            defaultModeLayut.PerformLayout();
            ResumeLayout(false);
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
    }
}