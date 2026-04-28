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
            components = new System.ComponentModel.Container();
            TableLayoutPanel schemaLayout;
            TableLayoutPanel detailLayout;
            GroupBox filePatternGroup;
            TableLayoutPanel filePatternLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaDefinition));
            Label fileBaseName;
            GroupBox templateObjectGroup;
            TableLayoutPanel templateObjectLayout;
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
            objectData = new DataGridView();
            objectScopeColumn = new DataGridViewComboBoxColumn();
            objectNameColumn = new DataGridViewTextBoxColumn();
            objectToolStrip = new ToolStrip();
            openObjectCommand = new ToolStripButton();
            nodeTab = new TabPage();
            nodesTree = new TreeView();
            rootNodeData = new DataDictionary.Main.Controls.TextBoxData();
            forEachScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeToolStrip = new ToolStrip();
            openNodeCommand = new ToolStripButton();
            documentTab = new TabPage();
            fileLayout = new TableLayoutPanel();
            documentToolStrip = new ToolStrip();
            documentNewCommand = new ToolStripButton();
            documentOpenCommand = new ToolStripButton();
            documentObjectData = new DataDictionary.Main.Controls.ComboBoxData();
            documentFileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            bindingSchema = new BindingSource(components);
            bindingTemplate = new BindingSource(components);
            folderBrowserDialog = new FolderBrowserDialog();
            bindingObject = new BindingSource(components);
            schemaLayout = new TableLayoutPanel();
            detailLayout = new TableLayoutPanel();
            filePatternGroup = new GroupBox();
            filePatternLayout = new TableLayoutPanel();
            fileBaseName = new Label();
            templateObjectGroup = new GroupBox();
            templateObjectLayout = new TableLayoutPanel();
            rootNodeLayout = new TableLayoutPanel();
            schemaLayout.SuspendLayout();
            schemaTabs.SuspendLayout();
            schemaTab.SuspendLayout();
            detailLayout.SuspendLayout();
            filePatternGroup.SuspendLayout();
            filePatternLayout.SuspendLayout();
            templateObjectGroup.SuspendLayout();
            templateObjectLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).BeginInit();
            objectToolStrip.SuspendLayout();
            nodeTab.SuspendLayout();
            rootNodeLayout.SuspendLayout();
            nodeToolStrip.SuspendLayout();
            documentTab.SuspendLayout();
            fileLayout.SuspendLayout();
            documentToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).BeginInit();
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
            schemaLayout.Size = new Size(558, 644);
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
            templateTitleData.Size = new Size(552, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // schemaTabs
            // 
            schemaTabs.Controls.Add(schemaTab);
            schemaTabs.Controls.Add(nodeTab);
            schemaTabs.Controls.Add(documentTab);
            schemaTabs.Dock = DockStyle.Fill;
            schemaTabs.Location = new Point(3, 103);
            schemaTabs.Name = "schemaTabs";
            schemaTabs.SelectedIndex = 0;
            schemaTabs.Size = new Size(552, 538);
            schemaTabs.TabIndex = 6;
            // 
            // schemaTab
            // 
            schemaTab.BackColor = SystemColors.Control;
            schemaTab.Controls.Add(detailLayout);
            schemaTab.Location = new Point(4, 24);
            schemaTab.Name = "schemaTab";
            schemaTab.Padding = new Padding(3);
            schemaTab.Size = new Size(544, 510);
            schemaTab.TabIndex = 0;
            schemaTab.Text = "Schema";
            // 
            // detailLayout
            // 
            detailLayout.ColumnCount = 1;
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailLayout.Controls.Add(filePatternGroup, 0, 0);
            detailLayout.Controls.Add(templateObjectGroup, 0, 1);
            detailLayout.Dock = DockStyle.Fill;
            detailLayout.Location = new Point(3, 3);
            detailLayout.Name = "detailLayout";
            detailLayout.RowCount = 2;
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.Size = new Size(538, 504);
            detailLayout.TabIndex = 7;
            // 
            // filePatternGroup
            // 
            filePatternGroup.AutoSize = true;
            filePatternGroup.Controls.Add(filePatternLayout);
            filePatternGroup.Dock = DockStyle.Fill;
            filePatternGroup.Location = new Point(3, 3);
            filePatternGroup.Name = "filePatternGroup";
            filePatternGroup.Size = new Size(532, 174);
            filePatternGroup.TabIndex = 5;
            filePatternGroup.TabStop = false;
            filePatternGroup.Text = "File Pattern";
            // 
            // filePatternLayout
            // 
            filePatternLayout.AutoSize = true;
            filePatternLayout.ColumnCount = 4;
            filePatternLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            filePatternLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
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
            filePatternLayout.Size = new Size(526, 152);
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
            rootFolderData.Size = new Size(235, 46);
            rootFolderData.TabIndex = 0;
            rootFolderData.Validated += RootFolderData_Validated;
            // 
            // relativePathData
            // 
            relativePathData.AutoSize = true;
            relativePathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            filePatternLayout.SetColumnSpan(relativePathData, 2);
            relativePathData.Dock = DockStyle.Fill;
            relativePathData.HeaderText = "Relative Path";
            relativePathData.Location = new Point(244, 3);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(279, 46);
            relativePathData.TabIndex = 1;
            relativePathData.Validated += RelativePathData_Validated;
            relativePathData.SelectCommand += RelativePathData_SelectCommand;
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
            filePrefixData.Size = new Size(136, 44);
            filePrefixData.TabIndex = 2;
            filePrefixData.WordWrap = true;
            // 
            // fileBaseName
            // 
            fileBaseName.Anchor = AnchorStyles.None;
            fileBaseName.AutoSize = true;
            fileBaseName.Location = new Point(145, 119);
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
            fileSuffixData.Location = new Point(244, 105);
            fileSuffixData.Multiline = false;
            fileSuffixData.Name = "fileSuffixData";
            fileSuffixData.ReadOnly = false;
            fileSuffixData.Size = new Size(136, 44);
            fileSuffixData.TabIndex = 3;
            fileSuffixData.WordWrap = true;
            // 
            // fileExtensionData
            // 
            fileExtensionData.AutoSize = true;
            fileExtensionData.Dock = DockStyle.Fill;
            fileExtensionData.HeaderText = "File Extension";
            fileExtensionData.Location = new Point(386, 105);
            fileExtensionData.Multiline = false;
            fileExtensionData.Name = "fileExtensionData";
            fileExtensionData.ReadOnly = false;
            fileExtensionData.Size = new Size(137, 44);
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
            localPathData.Size = new Size(520, 44);
            localPathData.TabIndex = 7;
            localPathData.WordWrap = false;
            // 
            // templateObjectGroup
            // 
            templateObjectGroup.AutoSize = true;
            templateObjectGroup.Controls.Add(templateObjectLayout);
            templateObjectGroup.Dock = DockStyle.Fill;
            templateObjectGroup.Location = new Point(3, 183);
            templateObjectGroup.Name = "templateObjectGroup";
            templateObjectGroup.Size = new Size(532, 318);
            templateObjectGroup.TabIndex = 6;
            templateObjectGroup.TabStop = false;
            templateObjectGroup.Text = "Template Objects";
            // 
            // templateObjectLayout
            // 
            templateObjectLayout.ColumnCount = 1;
            templateObjectLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateObjectLayout.Controls.Add(objectData, 0, 1);
            templateObjectLayout.Controls.Add(objectToolStrip, 0, 0);
            templateObjectLayout.Dock = DockStyle.Fill;
            templateObjectLayout.Location = new Point(3, 19);
            templateObjectLayout.Name = "templateObjectLayout";
            templateObjectLayout.RowCount = 2;
            templateObjectLayout.RowStyles.Add(new RowStyle());
            templateObjectLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            templateObjectLayout.Size = new Size(526, 296);
            templateObjectLayout.TabIndex = 10;
            // 
            // objectData
            // 
            objectData.AllowUserToAddRows = false;
            objectData.AllowUserToDeleteRows = false;
            objectData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectData.Columns.AddRange(new DataGridViewColumn[] { objectScopeColumn, objectNameColumn });
            templateObjectLayout.SetColumnSpan(objectData, 2);
            objectData.Dock = DockStyle.Fill;
            objectData.Location = new Point(3, 28);
            objectData.Name = "objectData";
            objectData.ReadOnly = true;
            objectData.Size = new Size(520, 265);
            objectData.TabIndex = 10;
            // 
            // objectScopeColumn
            // 
            objectScopeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectScopeColumn.DataPropertyName = "ObjectScope";
            objectScopeColumn.FillWeight = 40F;
            objectScopeColumn.HeaderText = "Object Scope";
            objectScopeColumn.Name = "objectScopeColumn";
            objectScopeColumn.ReadOnly = true;
            // 
            // objectNameColumn
            // 
            objectNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectNameColumn.DataPropertyName = "ObjectName";
            objectNameColumn.FillWeight = 60F;
            objectNameColumn.HeaderText = "Object Name";
            objectNameColumn.Name = "objectNameColumn";
            objectNameColumn.ReadOnly = true;
            // 
            // objectToolStrip
            // 
            objectToolStrip.Items.AddRange(new ToolStripItem[] { openObjectCommand });
            objectToolStrip.Location = new Point(0, 0);
            objectToolStrip.Name = "objectToolStrip";
            objectToolStrip.Size = new Size(526, 25);
            objectToolStrip.TabIndex = 0;
            objectToolStrip.Text = "toolStrip1";
            // 
            // openObjectCommand
            // 
            openObjectCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openObjectCommand.Image = (Image)resources.GetObject("openObjectCommand.Image");
            openObjectCommand.ImageTransparentColor = Color.Magenta;
            openObjectCommand.Name = "openObjectCommand";
            openObjectCommand.Size = new Size(23, 22);
            openObjectCommand.Text = "open Object";
            openObjectCommand.Click += OpenObjectCommand_Click;
            // 
            // nodeTab
            // 
            nodeTab.BackColor = SystemColors.Control;
            nodeTab.Controls.Add(rootNodeLayout);
            nodeTab.Location = new Point(4, 24);
            nodeTab.Name = "nodeTab";
            nodeTab.Padding = new Padding(3);
            nodeTab.Size = new Size(544, 510);
            nodeTab.TabIndex = 2;
            nodeTab.Text = "Nodes";
            // 
            // rootNodeLayout
            // 
            rootNodeLayout.AutoSize = true;
            rootNodeLayout.ColumnCount = 1;
            rootNodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootNodeLayout.Controls.Add(nodesTree, 0, 3);
            rootNodeLayout.Controls.Add(rootNodeData, 0, 2);
            rootNodeLayout.Controls.Add(forEachScopeData, 0, 1);
            rootNodeLayout.Controls.Add(nodeToolStrip, 0, 0);
            rootNodeLayout.Dock = DockStyle.Fill;
            rootNodeLayout.Location = new Point(3, 3);
            rootNodeLayout.Name = "rootNodeLayout";
            rootNodeLayout.RowCount = 4;
            rootNodeLayout.RowStyles.Add(new RowStyle());
            rootNodeLayout.RowStyles.Add(new RowStyle());
            rootNodeLayout.RowStyles.Add(new RowStyle());
            rootNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootNodeLayout.Size = new Size(538, 504);
            rootNodeLayout.TabIndex = 0;
            // 
            // nodesTree
            // 
            nodesTree.Dock = DockStyle.Fill;
            nodesTree.Location = new Point(3, 130);
            nodesTree.Name = "nodesTree";
            nodesTree.Size = new Size(532, 371);
            nodesTree.TabIndex = 2;
            // 
            // rootNodeData
            // 
            rootNodeData.AutoSize = true;
            rootNodeData.Dock = DockStyle.Fill;
            rootNodeData.HeaderText = "Root Node Name (override)";
            rootNodeData.Location = new Point(3, 80);
            rootNodeData.Multiline = false;
            rootNodeData.Name = "rootNodeData";
            rootNodeData.ReadOnly = false;
            rootNodeData.Size = new Size(532, 44);
            rootNodeData.TabIndex = 0;
            rootNodeData.WordWrap = true;
            // 
            // forEachScopeData
            // 
            forEachScopeData.AutoSize = true;
            forEachScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            forEachScopeData.Dock = DockStyle.Fill;
            forEachScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            forEachScopeData.HeaderText = "Root Node Scope (for each)";
            forEachScopeData.Location = new Point(3, 28);
            forEachScopeData.Name = "forEachScopeData";
            forEachScopeData.ReadOnly = false;
            forEachScopeData.Size = new Size(532, 46);
            forEachScopeData.TabIndex = 1;
            // 
            // nodeToolStrip
            // 
            nodeToolStrip.Items.AddRange(new ToolStripItem[] { openNodeCommand });
            nodeToolStrip.Location = new Point(0, 0);
            nodeToolStrip.Name = "nodeToolStrip";
            nodeToolStrip.Size = new Size(538, 25);
            nodeToolStrip.TabIndex = 3;
            nodeToolStrip.Text = "toolStrip1";
            // 
            // openNodeCommand
            // 
            openNodeCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openNodeCommand.Image = (Image)resources.GetObject("openNodeCommand.Image");
            openNodeCommand.ImageTransparentColor = Color.Magenta;
            openNodeCommand.Name = "openNodeCommand";
            openNodeCommand.Size = new Size(23, 22);
            openNodeCommand.Text = "open Nodes";
            openNodeCommand.Click += OpenNodeCommand_Click;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(fileLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(544, 510);
            documentTab.TabIndex = 1;
            documentTab.Text = "Documents";
            // 
            // fileLayout
            // 
            fileLayout.ColumnCount = 1;
            fileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fileLayout.Controls.Add(documentToolStrip, 0, 0);
            fileLayout.Controls.Add(documentObjectData, 0, 3);
            fileLayout.Controls.Add(documentFileNameData, 0, 2);
            fileLayout.Controls.Add(documentData, 0, 1);
            fileLayout.Dock = DockStyle.Fill;
            fileLayout.Location = new Point(3, 3);
            fileLayout.Name = "fileLayout";
            fileLayout.RowCount = 4;
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.Size = new Size(538, 504);
            fileLayout.TabIndex = 5;
            // 
            // documentToolStrip
            // 
            documentToolStrip.Items.AddRange(new ToolStripItem[] { documentNewCommand, documentOpenCommand });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(538, 25);
            documentToolStrip.TabIndex = 15;
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
            // documentObjectData
            // 
            documentObjectData.AutoSize = true;
            documentObjectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentObjectData.Dock = DockStyle.Fill;
            documentObjectData.DropDownStyle = ComboBoxStyle.DropDown;
            documentObjectData.HeaderText = "Source Object (model)";
            documentObjectData.Location = new Point(3, 455);
            documentObjectData.Name = "documentObjectData";
            documentObjectData.ReadOnly = false;
            documentObjectData.Size = new Size(532, 46);
            documentObjectData.TabIndex = 12;
            // 
            // documentFileNameData
            // 
            documentFileNameData.AutoSize = true;
            documentFileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentFileNameData.Dock = DockStyle.Fill;
            documentFileNameData.HeaderText = "File Name";
            documentFileNameData.Location = new Point(3, 405);
            documentFileNameData.Name = "documentFileNameData";
            documentFileNameData.ReadOnly = false;
            documentFileNameData.SelectIcon = (Image)resources.GetObject("documentFileNameData.SelectIcon");
            documentFileNameData.Size = new Size(532, 44);
            documentFileNameData.TabIndex = 13;
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
            documentData.Size = new Size(532, 371);
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
            // schemaTitleData
            // 
            schemaTitleData.AutoSize = true;
            schemaTitleData.Dock = DockStyle.Fill;
            schemaTitleData.HeaderText = "Schema";
            schemaTitleData.Location = new Point(3, 53);
            schemaTitleData.Multiline = false;
            schemaTitleData.Name = "schemaTitleData";
            schemaTitleData.ReadOnly = false;
            schemaTitleData.Size = new Size(552, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // SchemaDefinition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 669);
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
            templateObjectGroup.ResumeLayout(false);
            templateObjectLayout.ResumeLayout(false);
            templateObjectLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).EndInit();
            objectToolStrip.ResumeLayout(false);
            objectToolStrip.PerformLayout();
            nodeTab.ResumeLayout(false);
            nodeTab.PerformLayout();
            rootNodeLayout.ResumeLayout(false);
            rootNodeLayout.PerformLayout();
            nodeToolStrip.ResumeLayout(false);
            nodeToolStrip.PerformLayout();
            documentTab.ResumeLayout(false);
            fileLayout.ResumeLayout(false);
            fileLayout.PerformLayout();
            documentToolStrip.ResumeLayout(false);
            documentToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).EndInit();
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
        private Controls.ComboBoxData documentObjectData;
        private TabControl schemaTabs;
        private TabPage schemaTab;
        private TabPage documentTab;
        private Controls.SelectTextBoxData documentFileNameData;
        private ToolStrip documentToolStrip;
        private ToolStripButton documentNewCommand;
        private ToolStripButton documentOpenCommand;
        private TabPage nodeTab;
        private TreeView nodesTree;
        private ToolStrip nodeToolStrip;
        private ToolStripButton openNodeCommand;
        private BindingSource bindingSchema;
        private BindingSource bindingTemplate;
        private FolderBrowserDialog folderBrowserDialog;
        private BindingSource bindingObject;
        private DataGridView objectData;
        private DataGridViewComboBoxColumn objectScopeColumn;
        private DataGridViewTextBoxColumn objectNameColumn;
        private ToolStrip objectToolStrip;
        private ToolStripButton openObjectCommand;
    }
}