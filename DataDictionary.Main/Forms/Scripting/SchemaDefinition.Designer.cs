namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
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
            TableLayoutPanel schemaLayout;
            TableLayoutPanel detailLayout;
            GroupBox filePatternGroup;
            TableLayoutPanel filePatternLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaDefinition));
            Label fileBaseName;
            GroupBox schemaRootNodeGroup;
            TableLayoutPanel rootNodeLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaTabs = new TabControl();
            schemaTab = new TabPage();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            filePrefixData = new DataDictionary.Main.Controls.TextBoxData();
            fileSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            fileExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            rootNodeData = new DataDictionary.Main.Controls.TextBoxData();
            forEachScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            documentTab = new TabPage();
            fileLayout = new TableLayoutPanel();
            objectData = new DataDictionary.Main.Controls.ComboBoxData();
            fileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            documentToolStrip = new ToolStrip();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaLayout = new TableLayoutPanel();
            detailLayout = new TableLayoutPanel();
            filePatternGroup = new GroupBox();
            filePatternLayout = new TableLayoutPanel();
            fileBaseName = new Label();
            schemaRootNodeGroup = new GroupBox();
            rootNodeLayout = new TableLayoutPanel();
            schemaLayout.SuspendLayout();
            schemaTabs.SuspendLayout();
            schemaTab.SuspendLayout();
            detailLayout.SuspendLayout();
            filePatternGroup.SuspendLayout();
            filePatternLayout.SuspendLayout();
            schemaRootNodeGroup.SuspendLayout();
            rootNodeLayout.SuspendLayout();
            documentTab.SuspendLayout();
            fileLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            SuspendLayout();
            // 
            // schemaLayout
            // 
            schemaLayout.ColumnCount = 1;
            schemaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaLayout.Controls.Add(templateTitleData, 0, 0);
            schemaLayout.Controls.Add(schemaTabs, 0, 2);
            schemaLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaLayout.Dock = DockStyle.Fill;
            schemaLayout.Location = new Point(0, 25);
            schemaLayout.Name = "schemaLayout";
            schemaLayout.RowCount = 3;
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            schemaLayout.Size = new Size(545, 466);
            schemaLayout.TabIndex = 4;
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
            templateTitleData.Size = new Size(539, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // schemaTabs
            // 
            schemaTabs.Controls.Add(schemaTab);
            schemaTabs.Controls.Add(documentTab);
            schemaTabs.Dock = DockStyle.Fill;
            schemaTabs.Location = new Point(3, 103);
            schemaTabs.Name = "schemaTabs";
            schemaTabs.SelectedIndex = 0;
            schemaTabs.Size = new Size(539, 360);
            schemaTabs.TabIndex = 6;
            // 
            // schemaTab
            // 
            schemaTab.BackColor = SystemColors.Control;
            schemaTab.Controls.Add(detailLayout);
            schemaTab.Location = new Point(4, 24);
            schemaTab.Name = "schemaTab";
            schemaTab.Padding = new Padding(3);
            schemaTab.Size = new Size(531, 332);
            schemaTab.TabIndex = 0;
            schemaTab.Text = "Schema";
            // 
            // detailLayout
            // 
            detailLayout.ColumnCount = 1;
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailLayout.Controls.Add(filePatternGroup, 0, 0);
            detailLayout.Controls.Add(schemaRootNodeGroup, 0, 1);
            detailLayout.Dock = DockStyle.Fill;
            detailLayout.Location = new Point(3, 3);
            detailLayout.Name = "detailLayout";
            detailLayout.RowCount = 3;
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailLayout.Size = new Size(525, 326);
            detailLayout.TabIndex = 7;
            // 
            // filePatternGroup
            // 
            filePatternGroup.AutoSize = true;
            filePatternGroup.Controls.Add(filePatternLayout);
            filePatternGroup.Dock = DockStyle.Fill;
            filePatternGroup.Location = new Point(3, 3);
            filePatternGroup.Name = "filePatternGroup";
            filePatternGroup.Size = new Size(519, 174);
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
            filePatternLayout.Size = new Size(513, 152);
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
            relativePathData.Size = new Size(282, 46);
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
            fileExtensionData.Size = new Size(156, 44);
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
            localPathData.Size = new Size(507, 44);
            localPathData.TabIndex = 7;
            localPathData.WordWrap = false;
            // 
            // schemaRootNodeGroup
            // 
            schemaRootNodeGroup.AutoSize = true;
            schemaRootNodeGroup.Controls.Add(rootNodeLayout);
            schemaRootNodeGroup.Dock = DockStyle.Fill;
            schemaRootNodeGroup.Location = new Point(3, 183);
            schemaRootNodeGroup.Name = "schemaRootNodeGroup";
            schemaRootNodeGroup.Size = new Size(519, 126);
            schemaRootNodeGroup.TabIndex = 5;
            schemaRootNodeGroup.TabStop = false;
            schemaRootNodeGroup.Text = "Root Node";
            // 
            // rootNodeLayout
            // 
            rootNodeLayout.AutoSize = true;
            rootNodeLayout.ColumnCount = 1;
            rootNodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            rootNodeLayout.Controls.Add(rootNodeData, 0, 1);
            rootNodeLayout.Controls.Add(forEachScopeData, 0, 0);
            rootNodeLayout.Dock = DockStyle.Fill;
            rootNodeLayout.Location = new Point(3, 19);
            rootNodeLayout.Name = "rootNodeLayout";
            rootNodeLayout.RowCount = 2;
            rootNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            rootNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            rootNodeLayout.Size = new Size(513, 104);
            rootNodeLayout.TabIndex = 0;
            // 
            // rootNodeData
            // 
            rootNodeData.AutoSize = true;
            rootNodeData.Dock = DockStyle.Fill;
            rootNodeData.HeaderText = "Root Node Name (override)";
            rootNodeData.Location = new Point(3, 55);
            rootNodeData.Multiline = false;
            rootNodeData.Name = "rootNodeData";
            rootNodeData.ReadOnly = false;
            rootNodeData.Size = new Size(507, 46);
            rootNodeData.TabIndex = 0;
            rootNodeData.WordWrap = true;
            // 
            // forEachScopeData
            // 
            forEachScopeData.AutoSize = true;
            forEachScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            forEachScopeData.Dock = DockStyle.Fill;
            forEachScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            forEachScopeData.HeaderText = "Root Node Scope";
            forEachScopeData.Location = new Point(3, 3);
            forEachScopeData.Name = "forEachScopeData";
            forEachScopeData.ReadOnly = false;
            forEachScopeData.Size = new Size(507, 46);
            forEachScopeData.TabIndex = 1;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(fileLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(531, 332);
            documentTab.TabIndex = 1;
            documentTab.Text = "Documents";
            // 
            // fileLayout
            // 
            fileLayout.ColumnCount = 1;
            fileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fileLayout.Controls.Add(objectData, 0, 3);
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
            fileLayout.Size = new Size(525, 326);
            fileLayout.TabIndex = 5;
            // 
            // objectData
            // 
            objectData.AutoSize = true;
            objectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectData.Dock = DockStyle.Fill;
            objectData.DropDownStyle = ComboBoxStyle.DropDown;
            objectData.HeaderText = "Source Object (model)";
            objectData.Location = new Point(3, 277);
            objectData.Name = "objectData";
            objectData.ReadOnly = false;
            objectData.Size = new Size(519, 46);
            objectData.TabIndex = 12;
            // 
            // fileNameData
            // 
            fileNameData.AutoSize = true;
            fileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fileNameData.Dock = DockStyle.Fill;
            fileNameData.HeaderText = "File Name";
            fileNameData.Location = new Point(3, 227);
            fileNameData.Name = "fileNameData";
            fileNameData.ReadOnly = false;
            fileNameData.SelectIcon = (Image)resources.GetObject("fileNameData.SelectIcon");
            fileNameData.Size = new Size(519, 44);
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
            documentData.Size = new Size(519, 193);
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
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(525, 25);
            documentToolStrip.TabIndex = 14;
            documentToolStrip.Text = "Document Tools";
            // 
            // schemaTitleData
            // 
            schemaTitleData.AutoSize = true;
            schemaTitleData.Dock = DockStyle.Fill;
            schemaTitleData.HeaderText = "Schema";
            schemaTitleData.Location = new Point(3, 53);
            schemaTitleData.Multiline = false;
            schemaTitleData.Name = "schemaTitleData";
            schemaTitleData.ReadOnly = false;
            schemaTitleData.Size = new Size(539, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // SchemaDefinition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 491);
            Controls.Add(schemaLayout);
            Name = "SchemaDefinition";
            Text = "SchemaDefinition";
            Load += SchemaDefinition_Load;
            Controls.SetChildIndex(schemaLayout, 0);
            schemaLayout.ResumeLayout(false);
            schemaLayout.PerformLayout();
            schemaTabs.ResumeLayout(false);
            schemaTab.ResumeLayout(false);
            detailLayout.ResumeLayout(false);
            detailLayout.PerformLayout();
            filePatternGroup.ResumeLayout(false);
            filePatternGroup.PerformLayout();
            filePatternLayout.ResumeLayout(false);
            filePatternLayout.PerformLayout();
            schemaRootNodeGroup.ResumeLayout(false);
            schemaRootNodeGroup.PerformLayout();
            rootNodeLayout.ResumeLayout(false);
            rootNodeLayout.PerformLayout();
            documentTab.ResumeLayout(false);
            fileLayout.ResumeLayout(false);
            fileLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel schemaLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
        private Controls.TextBoxData rootNodeData;
        private Controls.ComboBoxData forEachScopeData;
        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData filePrefixData;
        private Controls.TextBoxData fileSuffixData;
        private Label fileBaseName;
        private Controls.TextBoxData fileExtensionData;
        private Controls.TextBoxData localPathData;
        private TableLayoutPanel fileLayout;
        private DataGridView documentData;
        private DataGridViewTextBoxColumn FileNameColumn;
        private Controls.ComboBoxData objectData;
        private TabControl schemaTabs;
        private TabPage schemaTab;
        private TabPage documentTab;
        private Controls.SelectTextBoxData fileNameData;
        private ToolStrip documentToolStrip;
    }
}