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
            Label fileBaseName;
            TableLayoutPanel nodeLayout;
            TableLayoutPanel tableLayoutPanel1;
            TableLayoutPanel renderAsLayout;
            GroupBox objectNodeGroup;
            TableLayoutPanel objectLayout;
            GroupBox groupBox1;
            TableLayoutPanel nodeSummaryLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaDefinition));
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaTabs = new TabControl();
            schemaTab = new TabPage();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            filePrefixData = new DataDictionary.Main.Controls.TextBoxData();
            fileSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            fileExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            schemaNodeTab = new TabPage();
            nodeRenderGroup = new GroupBox();
            renderOrderData = new DataDictionary.Main.Controls.TextBoxData();
            renderNodeTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            renderTypeCodeData = new DataDictionary.Main.Controls.ComboBoxData();
            objectTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            objectPropertyData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            isOverrideData = new CheckBox();
            nodeToolStrip = new ToolStrip();
            nodeNewCommand = new ToolStripButton();
            nodeDeleteCommand = new ToolStripButton();
            schemaNodeTree = new TreeView();
            documentTab = new TabPage();
            fileLayout = new TableLayoutPanel();
            documentToolStrip = new ToolStrip();
            documentNewCommand = new ToolStripButton();
            documentOpenCommand = new ToolStripButton();
            documentDeleteCommand = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            documentBuildCommand = new ToolStripButton();
            documentSaveCommand = new ToolStripButton();
            documentData = new DataGridView();
            objectPathColumn = new DataGridViewTextBoxColumn();
            FileNameColumn = new DataGridViewTextBoxColumn();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            bindingSchema = new BindingSource(components);
            bindingTemplate = new BindingSource(components);
            folderBrowserDialog = new FolderBrowserDialog();
            bindingNode = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            schemaLayout = new TableLayoutPanel();
            detailLayout = new TableLayoutPanel();
            filePatternGroup = new GroupBox();
            filePatternLayout = new TableLayoutPanel();
            fileBaseName = new Label();
            nodeLayout = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            renderAsLayout = new TableLayoutPanel();
            objectNodeGroup = new GroupBox();
            objectLayout = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            nodeSummaryLayout = new TableLayoutPanel();
            schemaLayout.SuspendLayout();
            schemaTabs.SuspendLayout();
            schemaTab.SuspendLayout();
            detailLayout.SuspendLayout();
            filePatternGroup.SuspendLayout();
            filePatternLayout.SuspendLayout();
            schemaNodeTab.SuspendLayout();
            nodeLayout.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            nodeRenderGroup.SuspendLayout();
            renderAsLayout.SuspendLayout();
            objectNodeGroup.SuspendLayout();
            objectLayout.SuspendLayout();
            groupBox1.SuspendLayout();
            nodeSummaryLayout.SuspendLayout();
            nodeToolStrip.SuspendLayout();
            documentTab.SuspendLayout();
            fileLayout.SuspendLayout();
            documentToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
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
            schemaLayout.Size = new Size(582, 629);
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
            templateTitleData.Size = new Size(576, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // schemaTabs
            // 
            schemaTabs.Controls.Add(schemaTab);
            schemaTabs.Controls.Add(schemaNodeTab);
            schemaTabs.Controls.Add(documentTab);
            schemaTabs.Dock = DockStyle.Fill;
            schemaTabs.Location = new Point(3, 103);
            schemaTabs.Name = "schemaTabs";
            schemaTabs.SelectedIndex = 0;
            schemaTabs.Size = new Size(576, 523);
            schemaTabs.TabIndex = 6;
            // 
            // schemaTab
            // 
            schemaTab.BackColor = SystemColors.Control;
            schemaTab.Controls.Add(detailLayout);
            schemaTab.Location = new Point(4, 24);
            schemaTab.Name = "schemaTab";
            schemaTab.Padding = new Padding(3);
            schemaTab.Size = new Size(568, 495);
            schemaTab.TabIndex = 0;
            schemaTab.Text = "Schema";
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
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.Size = new Size(562, 489);
            detailLayout.TabIndex = 7;
            // 
            // filePatternGroup
            // 
            filePatternGroup.AutoSize = true;
            filePatternGroup.Controls.Add(filePatternLayout);
            filePatternGroup.Dock = DockStyle.Fill;
            filePatternGroup.Location = new Point(3, 3);
            filePatternGroup.Name = "filePatternGroup";
            filePatternGroup.Size = new Size(556, 174);
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
            filePatternLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            filePatternLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
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
            filePatternLayout.Size = new Size(550, 152);
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
            rootFolderData.Size = new Size(239, 46);
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
            relativePathData.Location = new Point(248, 3);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(299, 46);
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
            filePrefixData.Size = new Size(146, 44);
            filePrefixData.TabIndex = 2;
            filePrefixData.WordWrap = true;
            // 
            // fileBaseName
            // 
            fileBaseName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            fileBaseName.AutoSize = true;
            fileBaseName.Location = new Point(155, 119);
            fileBaseName.Name = "fileBaseName";
            fileBaseName.Size = new Size(87, 15);
            fileBaseName.TabIndex = 5;
            fileBaseName.Text = "<nodeName/>";
            // 
            // fileSuffixData
            // 
            fileSuffixData.AutoSize = true;
            fileSuffixData.Dock = DockStyle.Fill;
            fileSuffixData.HeaderText = "File Suffix";
            fileSuffixData.Location = new Point(248, 105);
            fileSuffixData.Multiline = false;
            fileSuffixData.Name = "fileSuffixData";
            fileSuffixData.ReadOnly = false;
            fileSuffixData.Size = new Size(146, 44);
            fileSuffixData.TabIndex = 3;
            fileSuffixData.WordWrap = true;
            // 
            // fileExtensionData
            // 
            fileExtensionData.AutoSize = true;
            fileExtensionData.Dock = DockStyle.Fill;
            fileExtensionData.HeaderText = "File Extension";
            fileExtensionData.Location = new Point(400, 105);
            fileExtensionData.Multiline = false;
            fileExtensionData.Name = "fileExtensionData";
            fileExtensionData.ReadOnly = false;
            fileExtensionData.Size = new Size(147, 44);
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
            localPathData.Size = new Size(544, 44);
            localPathData.TabIndex = 7;
            localPathData.WordWrap = false;
            // 
            // schemaNodeTab
            // 
            schemaNodeTab.BackColor = SystemColors.Control;
            schemaNodeTab.Controls.Add(nodeLayout);
            schemaNodeTab.Location = new Point(4, 24);
            schemaNodeTab.Name = "schemaNodeTab";
            schemaNodeTab.Padding = new Padding(3);
            schemaNodeTab.Size = new Size(192, 72);
            schemaNodeTab.TabIndex = 2;
            schemaNodeTab.Text = "Node";
            // 
            // nodeLayout
            // 
            nodeLayout.ColumnCount = 2;
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nodeLayout.ColumnStyles.Add(new ColumnStyle());
            nodeLayout.Controls.Add(tableLayoutPanel1, 1, 1);
            nodeLayout.Controls.Add(nodeToolStrip, 0, 0);
            nodeLayout.Controls.Add(schemaNodeTree, 0, 1);
            nodeLayout.Dock = DockStyle.Fill;
            nodeLayout.Location = new Point(3, 3);
            nodeLayout.Name = "nodeLayout";
            nodeLayout.RowCount = 2;
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.Size = new Size(186, 66);
            nodeLayout.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(nodeRenderGroup, 0, 2);
            tableLayoutPanel1.Controls.Add(objectNodeGroup, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(-179, 28);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(362, 458);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // nodeRenderGroup
            // 
            nodeRenderGroup.AutoSize = true;
            nodeRenderGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeRenderGroup.Controls.Add(renderAsLayout);
            nodeRenderGroup.Dock = DockStyle.Fill;
            nodeRenderGroup.Location = new Point(3, 263);
            nodeRenderGroup.Name = "nodeRenderGroup";
            nodeRenderGroup.Size = new Size(356, 192);
            nodeRenderGroup.TabIndex = 9;
            nodeRenderGroup.TabStop = false;
            nodeRenderGroup.Text = "Render As";
            // 
            // renderAsLayout
            // 
            renderAsLayout.AutoSize = true;
            renderAsLayout.ColumnCount = 1;
            renderAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            renderAsLayout.Controls.Add(renderOrderData, 0, 2);
            renderAsLayout.Controls.Add(renderNodeTypeData, 0, 1);
            renderAsLayout.Controls.Add(renderTypeCodeData, 0, 0);
            renderAsLayout.Dock = DockStyle.Fill;
            renderAsLayout.Location = new Point(3, 19);
            renderAsLayout.Name = "renderAsLayout";
            renderAsLayout.RowCount = 3;
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.Size = new Size(350, 170);
            renderAsLayout.TabIndex = 0;
            // 
            // renderOrderData
            // 
            renderOrderData.AutoSize = true;
            renderOrderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderOrderData.Dock = DockStyle.Fill;
            renderOrderData.HeaderText = "Order";
            renderOrderData.Location = new Point(3, 107);
            renderOrderData.Multiline = false;
            renderOrderData.Name = "renderOrderData";
            renderOrderData.ReadOnly = false;
            renderOrderData.Size = new Size(344, 60);
            renderOrderData.TabIndex = 12;
            renderOrderData.WordWrap = true;
            // 
            // renderNodeTypeData
            // 
            renderNodeTypeData.AutoSize = true;
            renderNodeTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderNodeTypeData.Dock = DockStyle.Fill;
            renderNodeTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            renderNodeTypeData.HeaderText = "Value as";
            renderNodeTypeData.Location = new Point(3, 55);
            renderNodeTypeData.Name = "renderNodeTypeData";
            renderNodeTypeData.ReadOnly = false;
            renderNodeTypeData.Size = new Size(344, 46);
            renderNodeTypeData.TabIndex = 8;
            // 
            // renderTypeCodeData
            // 
            renderTypeCodeData.AutoSize = true;
            renderTypeCodeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderTypeCodeData.Dock = DockStyle.Fill;
            renderTypeCodeData.DropDownStyle = ComboBoxStyle.DropDown;
            renderTypeCodeData.HeaderText = "Data Type (XSD)";
            renderTypeCodeData.Location = new Point(3, 3);
            renderTypeCodeData.Name = "renderTypeCodeData";
            renderTypeCodeData.ReadOnly = false;
            renderTypeCodeData.Size = new Size(344, 46);
            renderTypeCodeData.TabIndex = 11;
            // 
            // objectNodeGroup
            // 
            objectNodeGroup.AutoSize = true;
            objectNodeGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectNodeGroup.Controls.Add(objectLayout);
            objectNodeGroup.Dock = DockStyle.Fill;
            objectNodeGroup.Location = new Point(3, 81);
            objectNodeGroup.Name = "objectNodeGroup";
            objectNodeGroup.Size = new Size(356, 176);
            objectNodeGroup.TabIndex = 8;
            objectNodeGroup.TabStop = false;
            objectNodeGroup.Text = "Data Source (Object)";
            // 
            // objectLayout
            // 
            objectLayout.AutoSize = true;
            objectLayout.ColumnCount = 1;
            objectLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectLayout.Controls.Add(objectTypeData, 0, 2);
            objectLayout.Controls.Add(objectPropertyData, 0, 1);
            objectLayout.Controls.Add(objectScopeData, 0, 0);
            objectLayout.Dock = DockStyle.Fill;
            objectLayout.Location = new Point(3, 19);
            objectLayout.Name = "objectLayout";
            objectLayout.RowCount = 3;
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.Size = new Size(350, 154);
            objectLayout.TabIndex = 0;
            // 
            // objectTypeData
            // 
            objectTypeData.AutoSize = true;
            objectTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectTypeData.Dock = DockStyle.Fill;
            objectTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectTypeData.HeaderText = "Object Data Type";
            objectTypeData.Location = new Point(3, 105);
            objectTypeData.Name = "objectTypeData";
            objectTypeData.ReadOnly = true;
            objectTypeData.Size = new Size(344, 46);
            objectTypeData.TabIndex = 10;
            // 
            // objectPropertyData
            // 
            objectPropertyData.AutoSize = true;
            objectPropertyData.Dock = DockStyle.Fill;
            objectPropertyData.HeaderText = "Object Property";
            objectPropertyData.Location = new Point(3, 55);
            objectPropertyData.Multiline = false;
            objectPropertyData.Name = "objectPropertyData";
            objectPropertyData.ReadOnly = true;
            objectPropertyData.Size = new Size(344, 44);
            objectPropertyData.TabIndex = 6;
            objectPropertyData.WordWrap = true;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectScopeData.HeaderText = "Object Scope";
            objectScopeData.Location = new Point(3, 3);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = true;
            objectScopeData.Size = new Size(344, 46);
            objectScopeData.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(nodeSummaryLayout);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(356, 72);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Node";
            // 
            // nodeSummaryLayout
            // 
            nodeSummaryLayout.AutoSize = true;
            nodeSummaryLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeSummaryLayout.ColumnCount = 2;
            nodeSummaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nodeSummaryLayout.ColumnStyles.Add(new ColumnStyle());
            nodeSummaryLayout.Controls.Add(nodeNameData, 0, 0);
            nodeSummaryLayout.Controls.Add(isOverrideData, 1, 0);
            nodeSummaryLayout.Dock = DockStyle.Fill;
            nodeSummaryLayout.Location = new Point(3, 19);
            nodeSummaryLayout.Name = "nodeSummaryLayout";
            nodeSummaryLayout.RowCount = 1;
            nodeSummaryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeSummaryLayout.Size = new Size(350, 50);
            nodeSummaryLayout.TabIndex = 0;
            // 
            // nodeNameData
            // 
            nodeNameData.AutoSize = true;
            nodeNameData.Dock = DockStyle.Fill;
            nodeNameData.HeaderText = "Node Name";
            nodeNameData.Location = new Point(3, 3);
            nodeNameData.Multiline = false;
            nodeNameData.Name = "nodeNameData";
            nodeNameData.ReadOnly = false;
            nodeNameData.Size = new Size(267, 44);
            nodeNameData.TabIndex = 7;
            nodeNameData.WordWrap = true;
            // 
            // isOverrideData
            // 
            isOverrideData.AutoSize = true;
            isOverrideData.Enabled = false;
            isOverrideData.Location = new Point(276, 3);
            isOverrideData.Name = "isOverrideData";
            isOverrideData.Size = new Size(71, 19);
            isOverrideData.TabIndex = 8;
            isOverrideData.Text = "Override";
            isOverrideData.UseVisualStyleBackColor = true;
            // 
            // nodeToolStrip
            // 
            nodeLayout.SetColumnSpan(nodeToolStrip, 2);
            nodeToolStrip.Items.AddRange(new ToolStripItem[] { nodeNewCommand, nodeDeleteCommand });
            nodeToolStrip.Location = new Point(0, 0);
            nodeToolStrip.Name = "nodeToolStrip";
            nodeToolStrip.Size = new Size(186, 25);
            nodeToolStrip.TabIndex = 0;
            nodeToolStrip.Text = "toolStrip1";
            // 
            // nodeNewCommand
            // 
            nodeNewCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            nodeNewCommand.Image = (Image)resources.GetObject("nodeNewCommand.Image");
            nodeNewCommand.ImageTransparentColor = Color.Magenta;
            nodeNewCommand.Name = "nodeNewCommand";
            nodeNewCommand.Size = new Size(23, 22);
            nodeNewCommand.Text = "New Node Definition (Override)";
            nodeNewCommand.Click += NodeNewCommand_Click;
            // 
            // nodeDeleteCommand
            // 
            nodeDeleteCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            nodeDeleteCommand.Image = (Image)resources.GetObject("nodeDeleteCommand.Image");
            nodeDeleteCommand.ImageTransparentColor = Color.Magenta;
            nodeDeleteCommand.Name = "nodeDeleteCommand";
            nodeDeleteCommand.Size = new Size(23, 22);
            nodeDeleteCommand.Text = "Delete Node Definition (reset to Default)";
            nodeDeleteCommand.Click += NodeDeleteCommand_Click;
            // 
            // schemaNodeTree
            // 
            schemaNodeTree.Dock = DockStyle.Fill;
            schemaNodeTree.Location = new Point(3, 28);
            schemaNodeTree.Name = "schemaNodeTree";
            schemaNodeTree.Size = new Size(1, 458);
            schemaNodeTree.TabIndex = 1;
            schemaNodeTree.BeforeCollapse += SchemaNodeTree_BeforeCollapse;
            schemaNodeTree.BeforeExpand += SchemaNodeTree_BeforeExpand;
            schemaNodeTree.NodeMouseClick += SchemaNodeTree_NodeMouseClick;
            schemaNodeTree.NodeMouseDoubleClick += SchemaNodeTree_NodeMouseDoubleClick;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(fileLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(568, 495);
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
            fileLayout.Size = new Size(562, 489);
            fileLayout.TabIndex = 5;
            // 
            // documentToolStrip
            // 
            documentToolStrip.Items.AddRange(new ToolStripItem[] { documentNewCommand, documentOpenCommand, documentDeleteCommand, toolStripSeparator, documentBuildCommand, documentSaveCommand });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(562, 25);
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
            documentNewCommand.Text = "New Document";
            documentNewCommand.Click += DocumentNewCommand_Click;
            // 
            // documentOpenCommand
            // 
            documentOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentOpenCommand.Image = (Image)resources.GetObject("documentOpenCommand.Image");
            documentOpenCommand.ImageTransparentColor = Color.Magenta;
            documentOpenCommand.Name = "documentOpenCommand";
            documentOpenCommand.Size = new Size(23, 22);
            documentOpenCommand.Text = "Open Document";
            documentOpenCommand.Click += DocumentOpenCommand_Click;
            // 
            // documentDeleteCommand
            // 
            documentDeleteCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentDeleteCommand.Image = (Image)resources.GetObject("documentDeleteCommand.Image");
            documentDeleteCommand.ImageTransparentColor = Color.Magenta;
            documentDeleteCommand.Name = "documentDeleteCommand";
            documentDeleteCommand.Size = new Size(23, 22);
            documentDeleteCommand.Text = "Delete Document";
            documentDeleteCommand.Click += DocumentDeleteCommand_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
            // 
            // documentBuildCommand
            // 
            documentBuildCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentBuildCommand.Image = (Image)resources.GetObject("documentBuildCommand.Image");
            documentBuildCommand.ImageTransparentColor = Color.Magenta;
            documentBuildCommand.Name = "documentBuildCommand";
            documentBuildCommand.Size = new Size(23, 22);
            documentBuildCommand.Text = "Build Documents";
            documentBuildCommand.Click += DocumentBuildCommand_Click;
            // 
            // documentSaveCommand
            // 
            documentSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSaveCommand.Image = (Image)resources.GetObject("documentSaveCommand.Image");
            documentSaveCommand.ImageTransparentColor = Color.Magenta;
            documentSaveCommand.Name = "documentSaveCommand";
            documentSaveCommand.Size = new Size(23, 22);
            documentSaveCommand.Text = "Save All";
            documentSaveCommand.Click += DocumentSaveCommand_Click;
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { objectPathColumn, FileNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 28);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(556, 458);
            documentData.TabIndex = 9;
            // 
            // objectPathColumn
            // 
            objectPathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectPathColumn.DataPropertyName = "ObjectPath";
            objectPathColumn.HeaderText = "Object";
            objectPathColumn.Name = "objectPathColumn";
            objectPathColumn.ReadOnly = true;
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
            schemaTitleData.Size = new Size(576, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // bindingNode
            // 
            bindingNode.CurrentChanged += BindingNode_CurrentChanged;
            // 
            // bindingDocument
            // 
            bindingDocument.CurrentChanged += BindingDocument_CurrentChanged;
            bindingDocument.ListChanged += BindingDocument_ListChanged;
            // 
            // SchemaDefinition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 654);
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
            schemaNodeTab.ResumeLayout(false);
            nodeLayout.ResumeLayout(false);
            nodeLayout.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            nodeRenderGroup.ResumeLayout(false);
            nodeRenderGroup.PerformLayout();
            renderAsLayout.ResumeLayout(false);
            renderAsLayout.PerformLayout();
            objectNodeGroup.ResumeLayout(false);
            objectNodeGroup.PerformLayout();
            objectLayout.ResumeLayout(false);
            objectLayout.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            nodeSummaryLayout.ResumeLayout(false);
            nodeSummaryLayout.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)bindingNode).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel schemaLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
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
        private BindingSource bindingNode;
        private TabPage schemaNodeTab;
        private TreeView schemaNodeTree;
        private Controls.TextBoxData renderOrderData;
        private Controls.ComboBoxData renderNodeTypeData;
        private Controls.ComboBoxData renderTypeCodeData;
        private Controls.ComboBoxData objectTypeData;
        private Controls.TextBoxData objectPropertyData;
        private Controls.ComboBoxData objectScopeData;
        private Controls.TextBoxData nodeNameData;
        private CheckBox isOverrideData;
        private ToolStripButton nodeNewCommand;
        private ToolStripButton nodeDeleteCommand;
        private GroupBox nodeRenderGroup;
        private BindingSource bindingDocument;
        private ToolStripButton documentBuildCommand;
        private ToolStripButton documentDeleteCommand;
        private DataGridViewTextBoxColumn objectPathColumn;
        private DataGridViewTextBoxColumn FileNameColumn;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton documentSaveCommand;
    }
}