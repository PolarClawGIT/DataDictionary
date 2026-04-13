namespace DataDictionary.Main.Forms.Scripting
{
    partial class Transform
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
            TableLayoutPanel transformLayout;
            TableLayoutPanel detailLayout;
            GroupBox filePatternGroup;
            TableLayoutPanel filePatternLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transform));
            Label fileBaseName;
            TableLayoutPanel scriptFileLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            transformTabs = new TabControl();
            transformTab = new TabPage();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            filePrefixData = new DataDictionary.Main.Controls.TextBoxData();
            fileSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            fileExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            transformScriptTab = new TabPage();
            scriptToolStrip = new ToolStrip();
            scriptOpenCommand = new ToolStripButton();
            scriptSaveCommand = new ToolStripButton();
            scriptFileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            scriptData = new DataDictionary.Main.Controls.TextBoxData();
            documentTab = new TabPage();
            fileLayout = new TableLayoutPanel();
            sourceDocumentData = new DataDictionary.Main.Controls.ComboBoxData();
            fileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            documentToolStrip = new ToolStrip();
            documentNewCommand = new ToolStripButton();
            documentOpenCommand = new ToolStripButton();
            transformTitleData = new DataDictionary.Main.Controls.TextBoxData();
            transformLayout = new TableLayoutPanel();
            detailLayout = new TableLayoutPanel();
            filePatternGroup = new GroupBox();
            filePatternLayout = new TableLayoutPanel();
            fileBaseName = new Label();
            scriptFileLayout = new TableLayoutPanel();
            transformLayout.SuspendLayout();
            transformTabs.SuspendLayout();
            transformTab.SuspendLayout();
            detailLayout.SuspendLayout();
            filePatternGroup.SuspendLayout();
            filePatternLayout.SuspendLayout();
            transformScriptTab.SuspendLayout();
            scriptFileLayout.SuspendLayout();
            scriptToolStrip.SuspendLayout();
            documentTab.SuspendLayout();
            fileLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            documentToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(templateTitleData, 0, 0);
            transformLayout.Controls.Add(transformTabs, 0, 2);
            transformLayout.Controls.Add(transformTitleData, 0, 1);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(0, 25);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 3;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            transformLayout.Size = new Size(517, 578);
            transformLayout.TabIndex = 5;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = true;
            templateTitleData.Size = new Size(511, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // transformTabs
            // 
            transformTabs.Controls.Add(transformTab);
            transformTabs.Controls.Add(transformScriptTab);
            transformTabs.Controls.Add(documentTab);
            transformTabs.Dock = DockStyle.Fill;
            transformTabs.Location = new Point(3, 103);
            transformTabs.Name = "transformTabs";
            transformTabs.SelectedIndex = 0;
            transformTabs.Size = new Size(511, 472);
            transformTabs.TabIndex = 6;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(detailLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(503, 444);
            transformTab.TabIndex = 0;
            transformTab.Text = "Transform";
            // 
            // detailLayout
            // 
            detailLayout.ColumnCount = 1;
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailLayout.Controls.Add(filePatternGroup, 0, 0);
            detailLayout.Dock = DockStyle.Fill;
            detailLayout.Location = new Point(3, 3);
            detailLayout.Name = "detailLayout";
            detailLayout.RowCount = 2;
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            detailLayout.Size = new Size(497, 438);
            detailLayout.TabIndex = 7;
            // 
            // filePatternGroup
            // 
            filePatternGroup.AutoSize = true;
            filePatternGroup.Controls.Add(filePatternLayout);
            filePatternGroup.Dock = DockStyle.Fill;
            filePatternGroup.Location = new Point(3, 3);
            filePatternGroup.Name = "filePatternGroup";
            filePatternGroup.Size = new Size(491, 174);
            filePatternGroup.TabIndex = 5;
            filePatternGroup.TabStop = false;
            filePatternGroup.Text = "File Pattern";
            // 
            // filePatternLayout
            // 
            filePatternLayout.AutoSize = true;
            filePatternLayout.ColumnCount = 4;
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.Controls.Add(rootFolderData, 0, 0);
            filePatternLayout.Controls.Add(relativePathData, 2, 0);
            filePatternLayout.Controls.Add(filePrefixData, 0, 2);
            filePatternLayout.Controls.Add(fileBaseName, 1, 2);
            filePatternLayout.Controls.Add(fileSuffixData, 2, 2);
            filePatternLayout.Controls.Add(fileExtensionData, 3, 2);
            filePatternLayout.Controls.Add(localPathData, 0, 1);
            filePatternLayout.Dock = DockStyle.Fill;
            filePatternLayout.Location = new Point(3, 19);
            filePatternLayout.Name = "filePatternLayout";
            filePatternLayout.RowCount = 3;
            filePatternLayout.RowStyles.Add(new RowStyle());
            filePatternLayout.RowStyles.Add(new RowStyle());
            filePatternLayout.RowStyles.Add(new RowStyle());
            filePatternLayout.Size = new Size(485, 152);
            filePatternLayout.TabIndex = 0;
            // 
            // rootFolderData
            // 
            rootFolderData.AutoSize = true;
            rootFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            filePatternLayout.SetColumnSpan(rootFolderData, 2);
            rootFolderData.Dock = DockStyle.Fill;
            rootFolderData.DropDownStyle = ComboBoxStyle.DropDown;
            rootFolderData.HeaderText = "Root Folder";
            rootFolderData.Location = new Point(3, 3);
            rootFolderData.Name = "rootFolderData";
            rootFolderData.ReadOnly = false;
            rootFolderData.Size = new Size(219, 46);
            rootFolderData.TabIndex = 0;
            // 
            // relativePathData
            // 
            relativePathData.AutoSize = true;
            relativePathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            filePatternLayout.SetColumnSpan(relativePathData, 2);
            relativePathData.Dock = DockStyle.Fill;
            relativePathData.HeaderText = "Relative Path";
            relativePathData.Location = new Point(228, 3);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(254, 46);
            relativePathData.TabIndex = 1;
            // 
            // filePrefixData
            // 
            filePrefixData.AutoSize = true;
            filePrefixData.Dock = DockStyle.Fill;
            filePrefixData.HeaderText = "File Prefix";
            filePrefixData.Location = new Point(3, 105);
            filePrefixData.Multiline = false;
            filePrefixData.Name = "filePrefixData";
            filePrefixData.ReadOnly = false;
            filePrefixData.Size = new Size(120, 44);
            filePrefixData.TabIndex = 2;
            filePrefixData.WordWrap = true;
            // 
            // fileBaseName
            // 
            fileBaseName.Anchor = AnchorStyles.None;
            fileBaseName.AutoSize = true;
            fileBaseName.Location = new Point(129, 119);
            fileBaseName.Name = "fileBaseName";
            fileBaseName.Size = new Size(93, 15);
            fileBaseName.TabIndex = 5;
            fileBaseName.Text = "<objectName/>";
            // 
            // fileSuffixData
            // 
            fileSuffixData.AutoSize = true;
            fileSuffixData.Dock = DockStyle.Fill;
            fileSuffixData.HeaderText = "File Suffix";
            fileSuffixData.Location = new Point(228, 105);
            fileSuffixData.Multiline = false;
            fileSuffixData.Name = "fileSuffixData";
            fileSuffixData.ReadOnly = false;
            fileSuffixData.Size = new Size(120, 44);
            fileSuffixData.TabIndex = 3;
            fileSuffixData.WordWrap = true;
            // 
            // fileExtensionData
            // 
            fileExtensionData.AutoSize = true;
            fileExtensionData.Dock = DockStyle.Fill;
            fileExtensionData.HeaderText = "File Extension";
            fileExtensionData.Location = new Point(354, 105);
            fileExtensionData.Multiline = false;
            fileExtensionData.Name = "fileExtensionData";
            fileExtensionData.ReadOnly = false;
            fileExtensionData.Size = new Size(128, 44);
            fileExtensionData.TabIndex = 6;
            fileExtensionData.WordWrap = true;
            // 
            // localPathData
            // 
            localPathData.AutoSize = true;
            filePatternLayout.SetColumnSpan(localPathData, 4);
            localPathData.Dock = DockStyle.Fill;
            localPathData.HeaderText = "Local Path";
            localPathData.Location = new Point(3, 55);
            localPathData.Multiline = false;
            localPathData.Name = "localPathData";
            localPathData.ReadOnly = true;
            localPathData.Size = new Size(479, 44);
            localPathData.TabIndex = 7;
            localPathData.WordWrap = false;
            // 
            // transformScriptTab
            // 
            transformScriptTab.BackColor = SystemColors.Control;
            transformScriptTab.Controls.Add(scriptFileLayout);
            transformScriptTab.Location = new Point(4, 24);
            transformScriptTab.Name = "transformScriptTab";
            transformScriptTab.Padding = new Padding(3);
            transformScriptTab.Size = new Size(192, 72);
            transformScriptTab.TabIndex = 2;
            transformScriptTab.Text = "Script";
            // 
            // scriptFileLayout
            // 
            scriptFileLayout.ColumnCount = 1;
            scriptFileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            scriptFileLayout.Controls.Add(scriptToolStrip, 0, 0);
            scriptFileLayout.Controls.Add(scriptFileNameData, 0, 1);
            scriptFileLayout.Controls.Add(scriptData, 0, 2);
            scriptFileLayout.Dock = DockStyle.Fill;
            scriptFileLayout.Location = new Point(3, 3);
            scriptFileLayout.Name = "scriptFileLayout";
            scriptFileLayout.RowCount = 3;
            scriptFileLayout.RowStyles.Add(new RowStyle());
            scriptFileLayout.RowStyles.Add(new RowStyle());
            scriptFileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scriptFileLayout.Size = new Size(186, 66);
            scriptFileLayout.TabIndex = 6;
            // 
            // scriptToolStrip
            // 
            scriptToolStrip.Items.AddRange(new ToolStripItem[] { scriptOpenCommand, scriptSaveCommand });
            scriptToolStrip.Location = new Point(0, 0);
            scriptToolStrip.Name = "scriptToolStrip";
            scriptToolStrip.Size = new Size(186, 25);
            scriptToolStrip.TabIndex = 0;
            scriptToolStrip.Text = "toolStrip1";
            // 
            // scriptOpenCommand
            // 
            scriptOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            scriptOpenCommand.Image = (Image)resources.GetObject("scriptOpenCommand.Image");
            scriptOpenCommand.ImageTransparentColor = Color.Magenta;
            scriptOpenCommand.Name = "scriptOpenCommand";
            scriptOpenCommand.Size = new Size(23, 22);
            scriptOpenCommand.Text = "Open";
            scriptOpenCommand.Click += ScriptOpenCommand_Click;
            // 
            // scriptSaveCommand
            // 
            scriptSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            scriptSaveCommand.Image = (Image)resources.GetObject("scriptSaveCommand.Image");
            scriptSaveCommand.ImageTransparentColor = Color.Magenta;
            scriptSaveCommand.Name = "scriptSaveCommand";
            scriptSaveCommand.Size = new Size(23, 22);
            scriptSaveCommand.Text = "Save";
            scriptSaveCommand.Click += ScriptSaveCommand_Click;
            // 
            // scriptFileNameData
            // 
            scriptFileNameData.AutoSize = true;
            scriptFileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scriptFileNameData.Dock = DockStyle.Fill;
            scriptFileNameData.HeaderText = "Script File";
            scriptFileNameData.Location = new Point(3, 28);
            scriptFileNameData.Name = "scriptFileNameData";
            scriptFileNameData.ReadOnly = false;
            scriptFileNameData.SelectIcon = (Image)resources.GetObject("scriptFileNameData.SelectIcon");
            scriptFileNameData.Size = new Size(180, 44);
            scriptFileNameData.TabIndex = 1;
            // 
            // scriptData
            // 
            scriptData.AutoSize = true;
            scriptData.Dock = DockStyle.Fill;
            scriptData.HeaderText = "Script";
            scriptData.Location = new Point(3, 78);
            scriptData.Multiline = true;
            scriptData.Name = "scriptData";
            scriptData.ReadOnly = false;
            scriptData.Size = new Size(180, 1);
            scriptData.TabIndex = 2;
            scriptData.WordWrap = false;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(fileLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(503, 444);
            documentTab.TabIndex = 1;
            documentTab.Text = "Documents";
            // 
            // fileLayout
            // 
            fileLayout.ColumnCount = 1;
            fileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fileLayout.Controls.Add(sourceDocumentData, 0, 3);
            fileLayout.Controls.Add(fileNameData, 0, 2);
            fileLayout.Controls.Add(documentData, 0, 1);
            fileLayout.Controls.Add(documentToolStrip, 0, 0);
            fileLayout.Dock = DockStyle.Fill;
            fileLayout.Location = new Point(3, 3);
            fileLayout.Name = "fileLayout";
            fileLayout.RowCount = 4;
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.Size = new Size(497, 438);
            fileLayout.TabIndex = 5;
            // 
            // sourceDocumentData
            // 
            sourceDocumentData.AutoSize = true;
            sourceDocumentData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            sourceDocumentData.Dock = DockStyle.Fill;
            sourceDocumentData.DropDownStyle = ComboBoxStyle.DropDown;
            sourceDocumentData.HeaderText = "Source Document (schema)";
            sourceDocumentData.Location = new Point(3, 389);
            sourceDocumentData.Name = "sourceDocumentData";
            sourceDocumentData.ReadOnly = false;
            sourceDocumentData.Size = new Size(491, 46);
            sourceDocumentData.TabIndex = 12;
            // 
            // fileNameData
            // 
            fileNameData.AutoSize = true;
            fileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fileNameData.Dock = DockStyle.Fill;
            fileNameData.HeaderText = "File Name";
            fileNameData.Location = new Point(3, 339);
            fileNameData.Name = "fileNameData";
            fileNameData.ReadOnly = false;
            fileNameData.SelectIcon = (Image)resources.GetObject("fileNameData.SelectIcon");
            fileNameData.Size = new Size(491, 44);
            fileNameData.TabIndex = 13;
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { FileNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 28);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(491, 305);
            documentData.TabIndex = 9;
            // 
            // FileNameColumn
            // 
            FileNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileNameColumn.DataPropertyName = "FileName";
            FileNameColumn.HeaderText = "File Name";
            FileNameColumn.Name = "FileNameColumn";
            FileNameColumn.ReadOnly = true;
            // 
            // documentToolStrip
            // 
            documentToolStrip.Items.AddRange(new ToolStripItem[] { documentNewCommand, documentOpenCommand });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(497, 25);
            documentToolStrip.TabIndex = 14;
            documentToolStrip.Text = "Document Tools";
            // 
            // documentNewCommand
            // 
            documentNewCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentNewCommand.Image = (Image)resources.GetObject("documentNewCommand.Image");
            documentNewCommand.ImageTransparentColor = Color.Magenta;
            documentNewCommand.Name = "documentNewCommand";
            documentNewCommand.Size = new Size(23, 22);
            documentNewCommand.Text = "New";
            documentNewCommand.Click += DocumentNewCommand_Click;
            // 
            // documentOpenCommand
            // 
            documentOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentOpenCommand.Image = (Image)resources.GetObject("documentOpenCommand.Image");
            documentOpenCommand.ImageTransparentColor = Color.Magenta;
            documentOpenCommand.Name = "documentOpenCommand";
            documentOpenCommand.Size = new Size(23, 22);
            documentOpenCommand.Text = "Open";
            documentOpenCommand.Click += DocumentOpenCommand_Click;
            // 
            // transformTitleData
            // 
            transformTitleData.AutoSize = true;
            transformTitleData.Dock = DockStyle.Fill;
            transformTitleData.HeaderText = "Transform";
            transformTitleData.Location = new Point(3, 53);
            transformTitleData.Multiline = false;
            transformTitleData.Name = "transformTitleData";
            transformTitleData.ReadOnly = false;
            transformTitleData.Size = new Size(511, 44);
            transformTitleData.TabIndex = 1;
            transformTitleData.WordWrap = true;
            // 
            // Transform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 603);
            Controls.Add(transformLayout);
            Name = "Transform";
            Text = "Transform";
            Load += Transform_Load;
            Controls.SetChildIndex(transformLayout, 0);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformTabs.ResumeLayout(false);
            transformTab.ResumeLayout(false);
            detailLayout.ResumeLayout(false);
            detailLayout.PerformLayout();
            filePatternGroup.ResumeLayout(false);
            filePatternGroup.PerformLayout();
            filePatternLayout.ResumeLayout(false);
            filePatternLayout.PerformLayout();
            transformScriptTab.ResumeLayout(false);
            scriptFileLayout.ResumeLayout(false);
            scriptFileLayout.PerformLayout();
            scriptToolStrip.ResumeLayout(false);
            scriptToolStrip.PerformLayout();
            documentTab.ResumeLayout(false);
            fileLayout.ResumeLayout(false);
            fileLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            documentToolStrip.ResumeLayout(false);
            documentToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private TabControl transformTabs;
        private TabPage transformTab;
        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData filePrefixData;
        private Controls.TextBoxData fileSuffixData;
        private Controls.TextBoxData fileExtensionData;
        private Controls.TextBoxData localPathData;
        private TabPage documentTab;
        private TableLayoutPanel fileLayout;
        private Controls.ComboBoxData sourceDocumentData;
        private Controls.SelectTextBoxData fileNameData;
        private DataGridView documentData;
        private DataGridViewTextBoxColumn FileNameColumn;
        private ToolStrip documentToolStrip;
        private Controls.TextBoxData transformTitleData;
        private TabPage transformScriptTab;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStrip scriptToolStrip;
        private Controls.SelectTextBoxData scriptFileNameData;
        private Controls.TextBoxData scriptData;
        private ToolStripButton documentNewCommand;
        private ToolStripButton scriptOpenCommand;
        private ToolStripButton scriptSaveCommand;
        private ToolStripButton documentOpenCommand;
    }
}