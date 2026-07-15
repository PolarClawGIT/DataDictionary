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
            GroupBox nodeGroup;
            TableLayoutPanel nodeLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaTabs = new TabControl();
            schemaTab = new TabPage();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            filePrefixData = new DataDictionary.Main.Controls.TextBoxData();
            fileSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            fileExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            renderValueAsData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            objectPropertyData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            forEachScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodesTree = new DataDictionary.Main.Controls.SchemaNodeTreeView();
            documentTab = new TabPage();
            fileLayout = new TableLayoutPanel();
            documentToolStrip = new ToolStrip();
            documentBuildCommand = new ToolStripButton();
            documentNewCommand = new ToolStripButton();
            documentOpenCommand = new ToolStripButton();
            documentData = new DataGridView();
            objectNameColumn = new DataGridViewTextBoxColumn();
            FileNameColumn = new DataGridViewTextBoxColumn();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            bindingSchema = new BindingSource(components);
            bindingTemplate = new BindingSource(components);
            folderBrowserDialog = new FolderBrowserDialog();
            bindingNode = new BindingSource(components);
            schemaLayout = new TableLayoutPanel();
            detailLayout = new TableLayoutPanel();
            filePatternGroup = new GroupBox();
            filePatternLayout = new TableLayoutPanel();
            fileBaseName = new Label();
            nodeGroup = new GroupBox();
            nodeLayout = new TableLayoutPanel();
            schemaLayout.SuspendLayout();
            schemaTabs.SuspendLayout();
            schemaTab.SuspendLayout();
            detailLayout.SuspendLayout();
            filePatternGroup.SuspendLayout();
            filePatternLayout.SuspendLayout();
            nodeGroup.SuspendLayout();
            nodeLayout.SuspendLayout();
            documentTab.SuspendLayout();
            fileLayout.SuspendLayout();
            documentToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).BeginInit();
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
            schemaLayout.Size = new Size(468, 646);
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
            templateTitleData.Size = new Size(462, 44);
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
            schemaTabs.Size = new Size(462, 540);
            schemaTabs.TabIndex = 6;
            // 
            // schemaTab
            // 
            schemaTab.BackColor = SystemColors.Control;
            schemaTab.Controls.Add(detailLayout);
            schemaTab.Location = new Point(4, 24);
            schemaTab.Name = "schemaTab";
            schemaTab.Padding = new Padding(3);
            schemaTab.Size = new Size(454, 512);
            schemaTab.TabIndex = 0;
            schemaTab.Text = "Schema";
            // 
            // detailLayout
            // 
            detailLayout.ColumnCount = 1;
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailLayout.Controls.Add(filePatternGroup, 0, 0);
            detailLayout.Controls.Add(nodeGroup, 0, 1);
            detailLayout.Dock = DockStyle.Fill;
            detailLayout.Location = new Point(3, 3);
            detailLayout.Name = "detailLayout";
            detailLayout.RowCount = 2;
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.Size = new Size(448, 506);
            detailLayout.TabIndex = 7;
            // 
            // filePatternGroup
            // 
            filePatternGroup.AutoSize = true;
            filePatternGroup.Controls.Add(filePatternLayout);
            filePatternGroup.Dock = DockStyle.Fill;
            filePatternGroup.Location = new Point(3, 3);
            filePatternGroup.Name = "filePatternGroup";
            filePatternGroup.Size = new Size(442, 174);
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
            filePatternLayout.Size = new Size(436, 152);
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
            rootFolderData.Size = new Size(205, 46);
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
            relativePathData.Location = new Point(214, 3);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(219, 46);
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
            filePrefixData.Size = new Size(106, 44);
            filePrefixData.TabIndex = 2;
            filePrefixData.WordWrap = true;
            // 
            // fileBaseName
            // 
            fileBaseName.Anchor = AnchorStyles.None;
            fileBaseName.AutoSize = true;
            fileBaseName.Location = new Point(115, 119);
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
            fileSuffixData.Location = new Point(214, 105);
            fileSuffixData.Multiline = false;
            fileSuffixData.Name = "fileSuffixData";
            fileSuffixData.ReadOnly = false;
            fileSuffixData.Size = new Size(106, 44);
            fileSuffixData.TabIndex = 3;
            fileSuffixData.WordWrap = true;
            // 
            // fileExtensionData
            // 
            fileExtensionData.AutoSize = true;
            fileExtensionData.Dock = DockStyle.Fill;
            fileExtensionData.HeaderText = "File Extension";
            fileExtensionData.Location = new Point(326, 105);
            fileExtensionData.Multiline = false;
            fileExtensionData.Name = "fileExtensionData";
            fileExtensionData.ReadOnly = false;
            fileExtensionData.Size = new Size(107, 44);
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
            localPathData.Size = new Size(430, 44);
            localPathData.TabIndex = 7;
            localPathData.WordWrap = false;
            // 
            // nodeGroup
            // 
            nodeGroup.AutoSize = true;
            nodeGroup.Controls.Add(nodeLayout);
            nodeGroup.Dock = DockStyle.Fill;
            nodeGroup.Location = new Point(3, 183);
            nodeGroup.Name = "nodeGroup";
            nodeGroup.Size = new Size(442, 320);
            nodeGroup.TabIndex = 6;
            nodeGroup.TabStop = false;
            nodeGroup.Text = "Nodes";
            // 
            // nodeLayout
            // 
            nodeLayout.AutoSize = true;
            nodeLayout.ColumnCount = 2;
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            nodeLayout.Controls.Add(renderValueAsData, 1, 4);
            nodeLayout.Controls.Add(nodeNameData, 1, 3);
            nodeLayout.Controls.Add(objectPropertyData, 1, 2);
            nodeLayout.Controls.Add(objectScopeData, 1, 1);
            nodeLayout.Controls.Add(forEachScopeData, 1, 0);
            nodeLayout.Controls.Add(nodesTree, 0, 0);
            nodeLayout.Dock = DockStyle.Fill;
            nodeLayout.Location = new Point(3, 19);
            nodeLayout.Name = "nodeLayout";
            nodeLayout.RowCount = 5;
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            nodeLayout.Size = new Size(436, 298);
            nodeLayout.TabIndex = 6;
            // 
            // renderValueAsData
            // 
            renderValueAsData.AutoSize = true;
            renderValueAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderValueAsData.Dock = DockStyle.Fill;
            renderValueAsData.DropDownStyle = ComboBoxStyle.DropDown;
            renderValueAsData.HeaderText = "Render Value as";
            renderValueAsData.Location = new Point(221, 207);
            renderValueAsData.Name = "renderValueAsData";
            renderValueAsData.ReadOnly = false;
            renderValueAsData.Size = new Size(212, 88);
            renderValueAsData.TabIndex = 7;
            // 
            // nodeNameData
            // 
            nodeNameData.AutoSize = true;
            nodeNameData.Dock = DockStyle.Fill;
            nodeNameData.HeaderText = "Node Name";
            nodeNameData.Location = new Point(221, 157);
            nodeNameData.Multiline = false;
            nodeNameData.Name = "nodeNameData";
            nodeNameData.ReadOnly = false;
            nodeNameData.Size = new Size(212, 44);
            nodeNameData.TabIndex = 6;
            nodeNameData.WordWrap = true;
            // 
            // objectPropertyData
            // 
            objectPropertyData.AutoSize = true;
            objectPropertyData.Dock = DockStyle.Fill;
            objectPropertyData.HeaderText = "Object Property";
            objectPropertyData.Location = new Point(221, 107);
            objectPropertyData.Multiline = false;
            objectPropertyData.Name = "objectPropertyData";
            objectPropertyData.ReadOnly = false;
            objectPropertyData.Size = new Size(212, 44);
            objectPropertyData.TabIndex = 5;
            objectPropertyData.WordWrap = true;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectScopeData.HeaderText = "Object Scope";
            objectScopeData.Location = new Point(221, 55);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = false;
            objectScopeData.Size = new Size(212, 46);
            objectScopeData.TabIndex = 4;
            // 
            // forEachScopeData
            // 
            forEachScopeData.AutoSize = true;
            forEachScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            forEachScopeData.Dock = DockStyle.Fill;
            forEachScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            forEachScopeData.HeaderText = "Root Node Scope (for each)";
            forEachScopeData.Location = new Point(221, 3);
            forEachScopeData.Name = "forEachScopeData";
            forEachScopeData.ReadOnly = false;
            forEachScopeData.Size = new Size(212, 46);
            forEachScopeData.TabIndex = 1;
            // 
            // nodesTree
            // 
            nodesTree.AutoSize = true;
            nodesTree.Dock = DockStyle.Fill;
            nodesTree.HeaderText = "(header)";
            nodesTree.Location = new Point(3, 3);
            nodesTree.Name = "nodesTree";
            nodeLayout.SetRowSpan(nodesTree, 5);
            nodesTree.Size = new Size(212, 292);
            nodesTree.TabIndex = 5;
            nodesTree.OnNodeSelected += NodesTree_OnNodeSelected;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(fileLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(192, 72);
            documentTab.TabIndex = 1;
            documentTab.Text = "Documents";
            // 
            // fileLayout
            // 
            fileLayout.ColumnCount = 1;
            fileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fileLayout.Controls.Add(documentToolStrip, 0, 0);
            fileLayout.Controls.Add(documentData, 0, 1);
            fileLayout.Dock = DockStyle.Fill;
            fileLayout.Location = new Point(3, 3);
            fileLayout.Name = "fileLayout";
            fileLayout.RowCount = 2;
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            fileLayout.Size = new Size(186, 66);
            fileLayout.TabIndex = 5;
            // 
            // documentToolStrip
            // 
            documentToolStrip.Items.AddRange(new ToolStripItem[] { documentBuildCommand, documentNewCommand, documentOpenCommand });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(186, 25);
            documentToolStrip.TabIndex = 15;
            documentToolStrip.Text = "Document Tools";
            // 
            // documentBuildCommand
            // 
            documentBuildCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentBuildCommand.Image = (Image)resources.GetObject("documentBuildCommand.Image");
            documentBuildCommand.ImageTransparentColor = Color.Magenta;
            documentBuildCommand.Name = "documentBuildCommand";
            documentBuildCommand.Size = new Size(23, 22);
            documentBuildCommand.Text = "Build";
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
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { objectNameColumn, FileNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 28);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(180, 35);
            documentData.TabIndex = 9;
            // 
            // objectNameColumn
            // 
            objectNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectNameColumn.DataPropertyName = "ObjectName";
            objectNameColumn.HeaderText = "Object Name";
            objectNameColumn.Name = "objectNameColumn";
            objectNameColumn.ReadOnly = true;
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
            schemaTitleData.Size = new Size(462, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // SchemaDefinition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(468, 671);
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
            nodeGroup.ResumeLayout(false);
            nodeGroup.PerformLayout();
            nodeLayout.ResumeLayout(false);
            nodeLayout.PerformLayout();
            documentTab.ResumeLayout(false);
            fileLayout.ResumeLayout(false);
            fileLayout.PerformLayout();
            documentToolStrip.ResumeLayout(false);
            documentToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel schemaLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
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
        private TabControl schemaTabs;
        private TabPage schemaTab;
        private TabPage documentTab;
        private ToolStrip documentToolStrip;
        private ToolStripButton documentNewCommand;
        private ToolStripButton documentOpenCommand;
        private TabPage nodeTab;
        private ToolStrip nodeToolStrip;
        private BindingSource bindingSchema;
        private BindingSource bindingTemplate;
        private FolderBrowserDialog folderBrowserDialog;
        private DataGridViewTextBoxColumn objectNameColumn;
        private DataGridViewTextBoxColumn FileNameColumn;
        private TableLayoutPanel nodeLayout;
        private ToolStripButton documentBuildCommand;
        private Controls.ComboBoxData objectScopeData;
        private Controls.TextBoxData objectPropertyData;
        private Controls.TextBoxData nodeNameData;
        private Controls.ComboBoxData renderValueAsData;
        private BindingSource bindingNode;
        private Controls.SchemaNodeTreeView nodesTree;
    }
}