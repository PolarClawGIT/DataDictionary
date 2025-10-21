namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
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
            TableLayoutPanel mainLayout;
            TabPage dataSourceTab;
            TableLayoutPanel modelLayout;
            TabPage nodeTab;
            TableLayoutPanel nodeLayout;
            TableLayoutPanel nodeDetailLayout;
            TabPage transformTab;
            TableLayoutPanel transformLayout;
            TabPage documentTab;
            TableLayoutPanel documentLayout;
            TableLayoutPanel optionsLayout;
            TabControl documentTabs;
            TabPage documentPreTransformTab;
            TableLayoutPanel documentGroupLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Template));
            Label documentPlaceholder;
            TabPage documentPostTransformTab;
            TableLayoutPanel scriptLayout;
            Label scriptPlaceHolder;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            templateOptionsTab = new TabControl();
            modelToolStrip = new ToolStrip();
            newDataSourceCommand = new ToolStripButton();
            templateDataSource = new DataGridView();
            dataSourceIdColumn = new DataGridViewComboBoxColumn();
            nodeToolStrip = new ToolStrip();
            newNodeCommand = new ToolStripButton();
            nodeTreeView = new TreeView();
            nodeGroup = new GroupBox();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            nodeRenderAs = new DataDictionary.Main.Controls.ComboBoxData();
            transformExceptionData = new DataDictionary.Main.Controls.TextBoxData();
            transformScriptData = new DataDictionary.Main.Controls.TextBoxData();
            transformToolStrip = new ToolStrip();
            transformCommand = new ToolStripButton();
            documentToolStrip = new ToolStrip();
            documentCommand = new ToolStripButton();
            rootPhysicalDirectory = new DataDictionary.Main.Controls.TextBoxData();
            rootDirectoryData = new DataDictionary.Main.Controls.ComboBoxData();
            breakOnScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            documentDirectoryData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentPrefixData = new DataDictionary.Main.Controls.TextBoxData();
            documentPhysicalDirectory = new DataDictionary.Main.Controls.TextBoxData();
            documentSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            documentExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            scriptingDirectoryData = new DataDictionary.Main.Controls.SelectTextBoxData();
            scriptingPrefixData = new DataDictionary.Main.Controls.TextBoxData();
            scriptingSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            scriptingPhysicalDirectory = new DataDictionary.Main.Controls.TextBoxData();
            scriptingExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            folderBrowserDialog = new FolderBrowserDialog();
            bindingTemplateData = new BindingSource(components);
            bindingNode = new BindingSource(components);
            bindingNodeOwner = new BindingSource(components);
            mainLayout = new TableLayoutPanel();
            dataSourceTab = new TabPage();
            modelLayout = new TableLayoutPanel();
            nodeTab = new TabPage();
            nodeLayout = new TableLayoutPanel();
            nodeDetailLayout = new TableLayoutPanel();
            transformTab = new TabPage();
            transformLayout = new TableLayoutPanel();
            documentTab = new TabPage();
            documentLayout = new TableLayoutPanel();
            optionsLayout = new TableLayoutPanel();
            documentTabs = new TabControl();
            documentPreTransformTab = new TabPage();
            documentGroupLayout = new TableLayoutPanel();
            documentPlaceholder = new Label();
            documentPostTransformTab = new TabPage();
            scriptLayout = new TableLayoutPanel();
            scriptPlaceHolder = new Label();
            mainLayout.SuspendLayout();
            templateOptionsTab.SuspendLayout();
            dataSourceTab.SuspendLayout();
            modelLayout.SuspendLayout();
            modelToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)templateDataSource).BeginInit();
            nodeTab.SuspendLayout();
            nodeLayout.SuspendLayout();
            nodeToolStrip.SuspendLayout();
            nodeGroup.SuspendLayout();
            nodeDetailLayout.SuspendLayout();
            transformTab.SuspendLayout();
            transformLayout.SuspendLayout();
            transformToolStrip.SuspendLayout();
            documentTab.SuspendLayout();
            documentLayout.SuspendLayout();
            documentToolStrip.SuspendLayout();
            optionsLayout.SuspendLayout();
            documentTabs.SuspendLayout();
            documentPreTransformTab.SuspendLayout();
            documentGroupLayout.SuspendLayout();
            documentPostTransformTab.SuspendLayout();
            scriptLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplateData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNodeOwner).BeginInit();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(templateTitleData, 0, 0);
            mainLayout.Controls.Add(templateDescriptionData, 0, 1);
            mainLayout.Controls.Add(templateOptionsTab, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 25);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            mainLayout.Size = new Size(545, 553);
            mainLayout.TabIndex = 4;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template Title";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = false;
            templateTitleData.Size = new Size(539, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = false;
            // 
            // templateDescriptionData
            // 
            templateDescriptionData.AutoSize = true;
            templateDescriptionData.Dock = DockStyle.Fill;
            templateDescriptionData.HeaderText = "Template Description";
            templateDescriptionData.Location = new Point(3, 53);
            templateDescriptionData.Multiline = true;
            templateDescriptionData.Name = "templateDescriptionData";
            templateDescriptionData.ReadOnly = false;
            templateDescriptionData.Size = new Size(539, 144);
            templateDescriptionData.TabIndex = 1;
            templateDescriptionData.WordWrap = true;
            // 
            // templateOptionsTab
            // 
            templateOptionsTab.Controls.Add(dataSourceTab);
            templateOptionsTab.Controls.Add(nodeTab);
            templateOptionsTab.Controls.Add(transformTab);
            templateOptionsTab.Controls.Add(documentTab);
            templateOptionsTab.Dock = DockStyle.Fill;
            templateOptionsTab.Location = new Point(3, 203);
            templateOptionsTab.Name = "templateOptionsTab";
            templateOptionsTab.SelectedIndex = 0;
            templateOptionsTab.Size = new Size(539, 347);
            templateOptionsTab.TabIndex = 2;
            // 
            // dataSourceTab
            // 
            dataSourceTab.BackColor = SystemColors.Control;
            dataSourceTab.Controls.Add(modelLayout);
            dataSourceTab.Location = new Point(4, 24);
            dataSourceTab.Name = "dataSourceTab";
            dataSourceTab.Size = new Size(531, 319);
            dataSourceTab.TabIndex = 1;
            dataSourceTab.Text = "Model (data source)";
            // 
            // modelLayout
            // 
            modelLayout.ColumnCount = 1;
            modelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelLayout.Controls.Add(modelToolStrip, 0, 0);
            modelLayout.Controls.Add(templateDataSource, 0, 1);
            modelLayout.Dock = DockStyle.Fill;
            modelLayout.Location = new Point(0, 0);
            modelLayout.Name = "modelLayout";
            modelLayout.RowCount = 2;
            modelLayout.RowStyles.Add(new RowStyle());
            modelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            modelLayout.Size = new Size(531, 319);
            modelLayout.TabIndex = 0;
            // 
            // modelToolStrip
            // 
            modelToolStrip.Items.AddRange(new ToolStripItem[] { newDataSourceCommand });
            modelToolStrip.Location = new Point(0, 0);
            modelToolStrip.Name = "modelToolStrip";
            modelToolStrip.Size = new Size(531, 25);
            modelToolStrip.TabIndex = 0;
            modelToolStrip.Text = "Data Source";
            // 
            // newDataSourceCommand
            // 
            newDataSourceCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newDataSourceCommand.ImageTransparentColor = Color.Magenta;
            newDataSourceCommand.Name = "newDataSourceCommand";
            newDataSourceCommand.Size = new Size(23, 22);
            newDataSourceCommand.Text = "New Data Source";
            // 
            // templateDataSource
            // 
            templateDataSource.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            templateDataSource.Columns.AddRange(new DataGridViewColumn[] { dataSourceIdColumn });
            templateDataSource.Dock = DockStyle.Fill;
            templateDataSource.Location = new Point(3, 28);
            templateDataSource.Name = "templateDataSource";
            templateDataSource.Size = new Size(525, 288);
            templateDataSource.TabIndex = 1;
            // 
            // dataSourceIdColumn
            // 
            dataSourceIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataSourceIdColumn.DataPropertyName = "DataSourceId";
            dataSourceIdColumn.HeaderText = "Data Source";
            dataSourceIdColumn.Name = "dataSourceIdColumn";
            // 
            // nodeTab
            // 
            nodeTab.BackColor = SystemColors.Control;
            nodeTab.Controls.Add(nodeLayout);
            nodeTab.Location = new Point(4, 24);
            nodeTab.Name = "nodeTab";
            nodeTab.Size = new Size(531, 319);
            nodeTab.TabIndex = 4;
            nodeTab.Text = "Nodes (XSD)";
            // 
            // nodeLayout
            // 
            nodeLayout.ColumnCount = 2;
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            nodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            nodeLayout.Controls.Add(nodeToolStrip, 0, 0);
            nodeLayout.Controls.Add(nodeTreeView, 0, 1);
            nodeLayout.Controls.Add(nodeGroup, 1, 1);
            nodeLayout.Dock = DockStyle.Fill;
            nodeLayout.Location = new Point(0, 0);
            nodeLayout.Name = "nodeLayout";
            nodeLayout.RowCount = 2;
            nodeLayout.RowStyles.Add(new RowStyle());
            nodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeLayout.Size = new Size(531, 319);
            nodeLayout.TabIndex = 0;
            // 
            // nodeToolStrip
            // 
            nodeLayout.SetColumnSpan(nodeToolStrip, 2);
            nodeToolStrip.Items.AddRange(new ToolStripItem[] { newNodeCommand });
            nodeToolStrip.Location = new Point(0, 0);
            nodeToolStrip.Name = "nodeToolStrip";
            nodeToolStrip.Size = new Size(531, 25);
            nodeToolStrip.TabIndex = 0;
            nodeToolStrip.Text = "Node";
            // 
            // newNodeCommand
            // 
            newNodeCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newNodeCommand.ImageTransparentColor = Color.Magenta;
            newNodeCommand.Name = "newNodeCommand";
            newNodeCommand.Size = new Size(23, 22);
            newNodeCommand.Text = "New Node";
            newNodeCommand.Click += NewNodeCommand_Click;
            // 
            // nodeTreeView
            // 
            nodeTreeView.Dock = DockStyle.Fill;
            nodeTreeView.Location = new Point(3, 28);
            nodeTreeView.Name = "nodeTreeView";
            nodeTreeView.Size = new Size(153, 288);
            nodeTreeView.TabIndex = 1;
            nodeTreeView.NodeMouseDoubleClick += NodeTreeView_NodeMouseDoubleClick;
            // 
            // nodeGroup
            // 
            nodeGroup.Controls.Add(nodeDetailLayout);
            nodeGroup.Dock = DockStyle.Fill;
            nodeGroup.Location = new Point(162, 28);
            nodeGroup.Name = "nodeGroup";
            nodeGroup.Size = new Size(366, 288);
            nodeGroup.TabIndex = 2;
            nodeGroup.TabStop = false;
            nodeGroup.Text = "Node";
            // 
            // nodeDetailLayout
            // 
            nodeDetailLayout.ColumnCount = 2;
            nodeDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            nodeDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            nodeDetailLayout.Controls.Add(nodeNameData, 0, 0);
            nodeDetailLayout.Controls.Add(nodeRenderAs, 1, 0);
            nodeDetailLayout.Dock = DockStyle.Fill;
            nodeDetailLayout.Location = new Point(3, 19);
            nodeDetailLayout.Name = "nodeDetailLayout";
            nodeDetailLayout.RowCount = 2;
            nodeDetailLayout.RowStyles.Add(new RowStyle());
            nodeDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nodeDetailLayout.Size = new Size(360, 266);
            nodeDetailLayout.TabIndex = 0;
            // 
            // nodeNameData
            // 
            nodeNameData.AutoSize = true;
            nodeNameData.Dock = DockStyle.Fill;
            nodeNameData.HeaderText = "Name";
            nodeNameData.Location = new Point(3, 3);
            nodeNameData.Multiline = false;
            nodeNameData.Name = "nodeNameData";
            nodeNameData.ReadOnly = true;
            nodeNameData.Size = new Size(246, 46);
            nodeNameData.TabIndex = 0;
            nodeNameData.WordWrap = true;
            // 
            // nodeRenderAs
            // 
            nodeRenderAs.AutoSize = true;
            nodeRenderAs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeRenderAs.Dock = DockStyle.Fill;
            nodeRenderAs.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeRenderAs.HeaderText = "Render As";
            nodeRenderAs.Location = new Point(255, 3);
            nodeRenderAs.Name = "nodeRenderAs";
            nodeRenderAs.ReadOnly = true;
            nodeRenderAs.Size = new Size(102, 46);
            nodeRenderAs.TabIndex = 1;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Size = new Size(192, 72);
            transformTab.TabIndex = 0;
            transformTab.Text = "Transform (XSLT)";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformExceptionData, 0, 2);
            transformLayout.Controls.Add(transformScriptData, 0, 1);
            transformLayout.Controls.Add(transformToolStrip, 0, 0);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(0, 0);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 3;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            transformLayout.Size = new Size(192, 72);
            transformLayout.TabIndex = 0;
            // 
            // transformExceptionData
            // 
            transformExceptionData.AutoSize = true;
            transformExceptionData.Dock = DockStyle.Fill;
            transformExceptionData.HeaderText = "Exception(s)";
            transformExceptionData.Location = new Point(3, 56);
            transformExceptionData.Multiline = true;
            transformExceptionData.Name = "transformExceptionData";
            transformExceptionData.ReadOnly = true;
            transformExceptionData.Size = new Size(186, 13);
            transformExceptionData.TabIndex = 1;
            transformExceptionData.WordWrap = true;
            // 
            // transformScriptData
            // 
            transformScriptData.AutoSize = true;
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.HeaderText = "Transform Script";
            transformScriptData.Location = new Point(3, 28);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.Padding = new Padding(0, 3, 0, 0);
            transformScriptData.ReadOnly = false;
            transformScriptData.Size = new Size(186, 22);
            transformScriptData.TabIndex = 0;
            transformScriptData.WordWrap = false;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Items.AddRange(new ToolStripItem[] { transformCommand });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(192, 25);
            transformToolStrip.TabIndex = 2;
            transformToolStrip.Text = "Transform";
            // 
            // transformCommand
            // 
            transformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformCommand.ImageTransparentColor = Color.Magenta;
            transformCommand.Name = "transformCommand";
            transformCommand.Size = new Size(23, 22);
            transformCommand.Text = "Transform";
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(documentLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Size = new Size(192, 72);
            documentTab.TabIndex = 2;
            documentTab.Text = "Document (result)";
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 1;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.Controls.Add(documentToolStrip, 0, 0);
            documentLayout.Controls.Add(optionsLayout, 0, 1);
            documentLayout.Controls.Add(documentTabs, 0, 2);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 0);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 3;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.Size = new Size(192, 72);
            documentLayout.TabIndex = 0;
            // 
            // documentToolStrip
            // 
            documentLayout.SetColumnSpan(documentToolStrip, 2);
            documentToolStrip.Items.AddRange(new ToolStripItem[] { documentCommand });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(192, 25);
            documentToolStrip.TabIndex = 0;
            documentToolStrip.Text = "Document";
            // 
            // documentCommand
            // 
            documentCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentCommand.ImageTransparentColor = Color.Magenta;
            documentCommand.Name = "documentCommand";
            documentCommand.Size = new Size(23, 22);
            documentCommand.Text = "Document";
            // 
            // optionsLayout
            // 
            optionsLayout.AutoSize = true;
            optionsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsLayout.ColumnCount = 2;
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            optionsLayout.Controls.Add(rootPhysicalDirectory, 0, 1);
            optionsLayout.Controls.Add(rootDirectoryData, 0, 0);
            optionsLayout.Controls.Add(breakOnScopeData, 1, 0);
            optionsLayout.Dock = DockStyle.Fill;
            optionsLayout.Location = new Point(3, 28);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 3;
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            optionsLayout.Size = new Size(186, 102);
            optionsLayout.TabIndex = 2;
            // 
            // rootPhysicalDirectory
            // 
            rootPhysicalDirectory.AutoSize = true;
            rootPhysicalDirectory.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsLayout.SetColumnSpan(rootPhysicalDirectory, 2);
            rootPhysicalDirectory.Dock = DockStyle.Fill;
            rootPhysicalDirectory.HeaderText = "Phyiscal Directory";
            rootPhysicalDirectory.Location = new Point(3, 55);
            rootPhysicalDirectory.Multiline = false;
            rootPhysicalDirectory.Name = "rootPhysicalDirectory";
            rootPhysicalDirectory.ReadOnly = true;
            rootPhysicalDirectory.Size = new Size(180, 44);
            rootPhysicalDirectory.TabIndex = 0;
            rootPhysicalDirectory.WordWrap = true;
            // 
            // rootDirectoryData
            // 
            rootDirectoryData.AutoSize = true;
            rootDirectoryData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootDirectoryData.Dock = DockStyle.Fill;
            rootDirectoryData.DropDownStyle = ComboBoxStyle.DropDownList;
            rootDirectoryData.HeaderText = "Relative Root Directory";
            rootDirectoryData.Location = new Point(3, 3);
            rootDirectoryData.Name = "rootDirectoryData";
            rootDirectoryData.ReadOnly = false;
            rootDirectoryData.Size = new Size(124, 46);
            rootDirectoryData.TabIndex = 1;
            rootDirectoryData.SelectedIndexChanged += RootDirectoryData_SelectedIndexChanged;
            rootDirectoryData.SelectionChangeCommitted += RootDirectoryData_SelectionChangeCommitted;
            // 
            // breakOnScopeData
            // 
            breakOnScopeData.AutoSize = true;
            breakOnScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            breakOnScopeData.Dock = DockStyle.Fill;
            breakOnScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            breakOnScopeData.HeaderText = "New Document On";
            breakOnScopeData.Location = new Point(133, 3);
            breakOnScopeData.Name = "breakOnScopeData";
            breakOnScopeData.ReadOnly = false;
            breakOnScopeData.Size = new Size(50, 46);
            breakOnScopeData.TabIndex = 2;
            // 
            // documentTabs
            // 
            documentTabs.Controls.Add(documentPreTransformTab);
            documentTabs.Controls.Add(documentPostTransformTab);
            documentTabs.Dock = DockStyle.Fill;
            documentTabs.Location = new Point(3, 136);
            documentTabs.Name = "documentTabs";
            documentTabs.SelectedIndex = 0;
            documentTabs.Size = new Size(186, 1);
            documentTabs.TabIndex = 7;
            // 
            // documentPreTransformTab
            // 
            documentPreTransformTab.BackColor = SystemColors.Control;
            documentPreTransformTab.Controls.Add(documentGroupLayout);
            documentPreTransformTab.Location = new Point(4, 24);
            documentPreTransformTab.Name = "documentPreTransformTab";
            documentPreTransformTab.Size = new Size(178, 0);
            documentPreTransformTab.TabIndex = 2;
            documentPreTransformTab.Text = "Pre-transform (XML)";
            // 
            // documentGroupLayout
            // 
            documentGroupLayout.AutoSize = true;
            documentGroupLayout.ColumnCount = 4;
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle());
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle());
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle());
            documentGroupLayout.Controls.Add(documentDirectoryData, 0, 0);
            documentGroupLayout.Controls.Add(documentPlaceholder, 1, 2);
            documentGroupLayout.Controls.Add(documentPrefixData, 0, 2);
            documentGroupLayout.Controls.Add(documentPhysicalDirectory, 0, 1);
            documentGroupLayout.Controls.Add(documentSuffixData, 2, 2);
            documentGroupLayout.Controls.Add(documentExtensionData, 3, 2);
            documentGroupLayout.Dock = DockStyle.Fill;
            documentGroupLayout.Location = new Point(0, 0);
            documentGroupLayout.Name = "documentGroupLayout";
            documentGroupLayout.RowCount = 3;
            documentGroupLayout.RowStyles.Add(new RowStyle());
            documentGroupLayout.RowStyles.Add(new RowStyle());
            documentGroupLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentGroupLayout.Size = new Size(178, 0);
            documentGroupLayout.TabIndex = 1;
            // 
            // documentDirectoryData
            // 
            documentDirectoryData.AutoSize = true;
            documentDirectoryData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentGroupLayout.SetColumnSpan(documentDirectoryData, 4);
            documentDirectoryData.Dock = DockStyle.Fill;
            documentDirectoryData.HeaderText = "XML Documents directory";
            documentDirectoryData.Location = new Point(3, 3);
            documentDirectoryData.Name = "documentDirectoryData";
            documentDirectoryData.ReadOnly = false;
            documentDirectoryData.SelectIcon = (Image)resources.GetObject("documentDirectoryData.SelectIcon");
            documentDirectoryData.Size = new Size(172, 44);
            documentDirectoryData.TabIndex = 6;
            documentDirectoryData.Validated += DocumentDirectoryData_Validated;
            documentDirectoryData.SelectCommand += DocumentDirectoryData_SelectCommand;
            // 
            // documentPlaceholder
            // 
            documentPlaceholder.AutoSize = true;
            documentPlaceholder.Dock = DockStyle.Fill;
            documentPlaceholder.Location = new Point(129, 100);
            documentPlaceholder.Name = "documentPlaceholder";
            documentPlaceholder.Size = new Size(1, 1);
            documentPlaceholder.TabIndex = 5;
            documentPlaceholder.Text = "<element name/>";
            documentPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // documentPrefixData
            // 
            documentPrefixData.AutoSize = true;
            documentPrefixData.Dock = DockStyle.Fill;
            documentPrefixData.HeaderText = "Prefix";
            documentPrefixData.Location = new Point(3, 103);
            documentPrefixData.Multiline = false;
            documentPrefixData.Name = "documentPrefixData";
            documentPrefixData.ReadOnly = false;
            documentPrefixData.Size = new Size(120, 1);
            documentPrefixData.TabIndex = 2;
            documentPrefixData.WordWrap = true;
            // 
            // documentPhysicalDirectory
            // 
            documentPhysicalDirectory.AutoSize = true;
            documentGroupLayout.SetColumnSpan(documentPhysicalDirectory, 4);
            documentPhysicalDirectory.Dock = DockStyle.Fill;
            documentPhysicalDirectory.HeaderText = "Physical Directory";
            documentPhysicalDirectory.Location = new Point(3, 53);
            documentPhysicalDirectory.Multiline = false;
            documentPhysicalDirectory.Name = "documentPhysicalDirectory";
            documentPhysicalDirectory.ReadOnly = true;
            documentPhysicalDirectory.Size = new Size(172, 44);
            documentPhysicalDirectory.TabIndex = 7;
            documentPhysicalDirectory.WordWrap = true;
            // 
            // documentSuffixData
            // 
            documentSuffixData.AutoSize = true;
            documentSuffixData.HeaderText = "Suffix";
            documentSuffixData.Location = new Point(-71, 103);
            documentSuffixData.Multiline = false;
            documentSuffixData.Name = "documentSuffixData";
            documentSuffixData.ReadOnly = false;
            documentSuffixData.Size = new Size(120, 1);
            documentSuffixData.TabIndex = 3;
            documentSuffixData.WordWrap = true;
            // 
            // documentExtensionData
            // 
            documentExtensionData.AutoSize = true;
            documentExtensionData.HeaderText = "Extension";
            documentExtensionData.Location = new Point(55, 103);
            documentExtensionData.Multiline = false;
            documentExtensionData.Name = "documentExtensionData";
            documentExtensionData.ReadOnly = false;
            documentExtensionData.Size = new Size(120, 1);
            documentExtensionData.TabIndex = 4;
            documentExtensionData.WordWrap = true;
            // 
            // documentPostTransformTab
            // 
            documentPostTransformTab.BackColor = SystemColors.Control;
            documentPostTransformTab.Controls.Add(scriptLayout);
            documentPostTransformTab.Location = new Point(4, 24);
            documentPostTransformTab.Name = "documentPostTransformTab";
            documentPostTransformTab.Size = new Size(178, 0);
            documentPostTransformTab.TabIndex = 3;
            documentPostTransformTab.Text = "Post-Transform (script)";
            // 
            // scriptLayout
            // 
            scriptLayout.AutoSize = true;
            scriptLayout.ColumnCount = 4;
            scriptLayout.ColumnStyles.Add(new ColumnStyle());
            scriptLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            scriptLayout.ColumnStyles.Add(new ColumnStyle());
            scriptLayout.ColumnStyles.Add(new ColumnStyle());
            scriptLayout.Controls.Add(scriptingDirectoryData, 0, 0);
            scriptLayout.Controls.Add(scriptingPrefixData, 0, 2);
            scriptLayout.Controls.Add(scriptPlaceHolder, 1, 2);
            scriptLayout.Controls.Add(scriptingSuffixData, 2, 2);
            scriptLayout.Controls.Add(scriptingPhysicalDirectory, 0, 1);
            scriptLayout.Controls.Add(scriptingExtensionData, 3, 2);
            scriptLayout.Dock = DockStyle.Fill;
            scriptLayout.Location = new Point(0, 0);
            scriptLayout.Name = "scriptLayout";
            scriptLayout.RowCount = 3;
            scriptLayout.RowStyles.Add(new RowStyle());
            scriptLayout.RowStyles.Add(new RowStyle());
            scriptLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scriptLayout.Size = new Size(178, 0);
            scriptLayout.TabIndex = 8;
            // 
            // scriptingDirectoryData
            // 
            scriptingDirectoryData.AutoSize = true;
            scriptingDirectoryData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scriptLayout.SetColumnSpan(scriptingDirectoryData, 4);
            scriptingDirectoryData.Dock = DockStyle.Fill;
            scriptingDirectoryData.HeaderText = "Script Directory";
            scriptingDirectoryData.Location = new Point(3, 3);
            scriptingDirectoryData.Name = "scriptingDirectoryData";
            scriptingDirectoryData.ReadOnly = false;
            scriptingDirectoryData.SelectIcon = (Image)resources.GetObject("scriptingDirectoryData.SelectIcon");
            scriptingDirectoryData.Size = new Size(172, 44);
            scriptingDirectoryData.TabIndex = 7;
            scriptingDirectoryData.Validated += ScriptingDirectoryData_Validated;
            scriptingDirectoryData.SelectCommand += ScriptingDirectoryData_SelectCommand;
            // 
            // scriptingPrefixData
            // 
            scriptingPrefixData.AutoSize = true;
            scriptingPrefixData.HeaderText = "Prefix";
            scriptingPrefixData.Location = new Point(3, 103);
            scriptingPrefixData.Multiline = false;
            scriptingPrefixData.Name = "scriptingPrefixData";
            scriptingPrefixData.ReadOnly = false;
            scriptingPrefixData.Size = new Size(120, 1);
            scriptingPrefixData.TabIndex = 2;
            scriptingPrefixData.WordWrap = true;
            // 
            // scriptPlaceHolder
            // 
            scriptPlaceHolder.AutoSize = true;
            scriptPlaceHolder.Dock = DockStyle.Fill;
            scriptPlaceHolder.Location = new Point(129, 100);
            scriptPlaceHolder.Name = "scriptPlaceHolder";
            scriptPlaceHolder.Size = new Size(1, 1);
            scriptPlaceHolder.TabIndex = 6;
            scriptPlaceHolder.Text = "<element name/>";
            scriptPlaceHolder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scriptingSuffixData
            // 
            scriptingSuffixData.AutoSize = true;
            scriptingSuffixData.HeaderText = "Suffix";
            scriptingSuffixData.Location = new Point(-71, 103);
            scriptingSuffixData.Multiline = false;
            scriptingSuffixData.Name = "scriptingSuffixData";
            scriptingSuffixData.ReadOnly = false;
            scriptingSuffixData.Size = new Size(120, 1);
            scriptingSuffixData.TabIndex = 3;
            scriptingSuffixData.WordWrap = true;
            // 
            // scriptingPhysicalDirectory
            // 
            scriptingPhysicalDirectory.AutoSize = true;
            scriptLayout.SetColumnSpan(scriptingPhysicalDirectory, 4);
            scriptingPhysicalDirectory.Dock = DockStyle.Fill;
            scriptingPhysicalDirectory.HeaderText = "Physical Directory";
            scriptingPhysicalDirectory.Location = new Point(3, 53);
            scriptingPhysicalDirectory.Multiline = false;
            scriptingPhysicalDirectory.Name = "scriptingPhysicalDirectory";
            scriptingPhysicalDirectory.ReadOnly = true;
            scriptingPhysicalDirectory.Size = new Size(172, 44);
            scriptingPhysicalDirectory.TabIndex = 8;
            scriptingPhysicalDirectory.WordWrap = true;
            // 
            // scriptingExtensionData
            // 
            scriptingExtensionData.AutoSize = true;
            scriptingExtensionData.HeaderText = "Extension";
            scriptingExtensionData.Location = new Point(55, 103);
            scriptingExtensionData.Multiline = false;
            scriptingExtensionData.Name = "scriptingExtensionData";
            scriptingExtensionData.ReadOnly = false;
            scriptingExtensionData.Size = new Size(120, 1);
            scriptingExtensionData.TabIndex = 4;
            scriptingExtensionData.WordWrap = true;
            // 
            // bindingTemplateData
            // 
            bindingTemplateData.AddingNew += BindingTemplateData_AddingNew;
            // 
            // bindingNode
            // 
            bindingNode.ListChanged += BindingNode_ListChanged;
            // 
            // bindingNodeOwner
            // 
            bindingNodeOwner.ListChanged += BindingNodeOwner_ListChanged;
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 578);
            Controls.Add(mainLayout);
            Name = "Template";
            Text = "Template";
            Load += Template_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            templateOptionsTab.ResumeLayout(false);
            dataSourceTab.ResumeLayout(false);
            modelLayout.ResumeLayout(false);
            modelLayout.PerformLayout();
            modelToolStrip.ResumeLayout(false);
            modelToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)templateDataSource).EndInit();
            nodeTab.ResumeLayout(false);
            nodeLayout.ResumeLayout(false);
            nodeLayout.PerformLayout();
            nodeToolStrip.ResumeLayout(false);
            nodeToolStrip.PerformLayout();
            nodeGroup.ResumeLayout(false);
            nodeDetailLayout.ResumeLayout(false);
            nodeDetailLayout.PerformLayout();
            transformTab.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformToolStrip.ResumeLayout(false);
            transformToolStrip.PerformLayout();
            documentTab.ResumeLayout(false);
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            documentToolStrip.ResumeLayout(false);
            documentToolStrip.PerformLayout();
            optionsLayout.ResumeLayout(false);
            optionsLayout.PerformLayout();
            documentTabs.ResumeLayout(false);
            documentPreTransformTab.ResumeLayout(false);
            documentPreTransformTab.PerformLayout();
            documentGroupLayout.ResumeLayout(false);
            documentGroupLayout.PerformLayout();
            documentPostTransformTab.ResumeLayout(false);
            documentPostTransformTab.PerformLayout();
            scriptLayout.ResumeLayout(false);
            scriptLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplateData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNodeOwner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private TabControl templateOptionsTab;
        private Controls.TextBoxData transformScriptData;
        private Controls.TextBoxData transformExceptionData;
        private ToolStrip transformToolStrip;
        private ToolStrip documentToolStrip;
        private ToolStrip modelToolStrip;
        private ToolStrip nodeToolStrip;
        private TreeView nodeTreeView;
        private GroupBox nodeGroup;
        private Controls.TextBoxData nodeNameData;
        private Controls.ComboBoxData nodeRenderAs;
        private ToolStripButton newNodeCommand;
        private ToolStripButton newDataSourceCommand;
        private ToolStripButton documentCommand;
        private ToolStripButton transformCommand;
        private BindingSource bindingTemplate;
        private TabPage documentPostTransformTab;
        private Controls.TextBoxData documentPrefixData;
        private Controls.TextBoxData documentSuffixData;
        private Controls.TextBoxData documentExtensionData;
        private Controls.TextBoxData scriptingPrefixData;
        private Controls.TextBoxData scriptingSuffixData;
        private Controls.TextBoxData scriptingExtensionData;
        private Controls.TextBoxData rootPhysicalDirectory;
        private Controls.ComboBoxData rootDirectoryData;
        private Controls.ComboBoxData breakOnScopeData;
        private Controls.SelectTextBoxData documentDirectoryData;
        private Controls.SelectTextBoxData scriptingDirectoryData;
        private FolderBrowserDialog folderBrowserDialog;
        private Controls.TextBoxData documentPhysicalDirectory;
        private Controls.TextBoxData scriptingPhysicalDirectory;
        private DataGridView templateDataSource;
        private BindingSource bindingTemplateData;
        private DataGridViewComboBoxColumn dataSourceIdColumn;
        private BindingSource bindingNode;
        private BindingSource bindingNodeOwner;
    }
}