namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingTemplate
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
            TableLayoutPanel templateLayoutPanel;
            TableLayoutPanel documentLayout;
            TableLayoutPanel documentGroupLayout;
            ListViewGroup listViewGroup1 = new ListViewGroup("Scope Name 1", HorizontalAlignment.Left);
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "Property Name 1", "Column 1" }, -1);
            GroupBox attributeGroup;
            TableLayoutPanel elementPathLayout;
            BusinessLayer.NamedScope.NamedScopePath namedScopePath1 = new BusinessLayer.NamedScope.NamedScopePath();
            TableLayoutPanel transformLayout;
            TableLayoutPanel documentTemplateLayout;
            templateTitleData = new Controls.TextBoxData();
            templateDescriptionData = new Controls.TextBoxData();
            templateTabs = new TabControl();
            documentSettingTab = new TabPage();
            documentGroup = new GroupBox();
            documentDirectoryData = new Controls.TextBoxData();
            documentPrefixData = new Controls.TextBoxData();
            documentSuffixData = new Controls.TextBoxData();
            documentExtensionData = new Controls.TextBoxData();
            documentDirectoryPicker = new Button();
            breakOnScopeData = new Controls.ComboBoxData();
            rootDirectoryData = new Controls.ComboBoxData();
            rootDirectoryExpanded = new Controls.TextBoxData();
            scriptingGroup = new GroupBox();
            scriptingGroupLayout = new TableLayoutPanel();
            scriptingExtensionData = new Controls.TextBoxData();
            scriptingSuffixData = new Controls.TextBoxData();
            scriptingPrefixData = new Controls.TextBoxData();
            scriptingDirectoryData = new Controls.TextBoxData();
            scriptAsData = new Controls.ComboBoxData();
            scriptingDirectoryPicker = new Button();
            nodeDefinitionTab = new TabPage();
            schemaDefinitionLayout = new SplitContainer();
            elementSelection = new ListView();
            columnName = new ColumnHeader();
            schemaNodeLayout = new TableLayoutPanel();
            propertyNameData = new Controls.TextBoxData();
            nodeNameData = new Controls.TextBoxData();
            nodeValueAsData = new Controls.ComboBoxData();
            attributeData = new DataGridView();
            attributeNameColumn = new DataGridViewTextBoxColumn();
            attributeValueColumn = new DataGridViewTextBoxColumn();
            attributePropertyColumn = new DataGridViewComboBoxColumn();
            propertyScopeData = new Controls.ComboBoxData();
            dataSelectionTab = new TabPage();
            templatePathData = new DataGridView();
            scopeNameData = new DataGridViewTextBoxColumn();
            selectionMemberData = new DataGridViewTextBoxColumn();
            templatePathSelect = new Controls.NamedScopeData();
            transformTab = new TabPage();
            transformToolStrip = new ToolStrip();
            transformParseCommand = new ToolStripButton();
            transformImportCommand = new ToolStripButton();
            transformExportCommand = new ToolStripButton();
            transformFilePath = new ToolStripLabel();
            transformExceptionData = new Controls.TextBoxData();
            transformScriptData = new TextBox();
            documentsTab = new TabPage();
            documentToolStrip = new ToolStrip();
            documentBuildComand = new ToolStripButton();
            documentSaveXMLCommand = new ToolStripButton();
            documentSaveScriptCommand = new ToolStripButton();
            documentSaveAllCommand = new ToolStripButton();
            documentStatus = new ToolStripLabel();
            documentData = new DataGridView();
            documentElementColumn = new DataGridViewTextBoxColumn();
            documentNameColumn = new DataGridViewTextBoxColumn();
            documentScriptName = new DataGridViewTextBoxColumn();
            documentException = new Controls.TextBoxData();
            documentXMLData = new Controls.TextBoxData();
            documentScriptData = new Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            templateToolStrip = new ContextMenuStrip(components);
            deleteTemplateCommand = new ToolStripMenuItem();
            folderBrowserDialog = new FolderBrowserDialog();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            bindingPath = new BindingSource(components);
            bindingNode = new BindingSource(components);
            bindingAttribute = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            templateLayoutPanel = new TableLayoutPanel();
            documentLayout = new TableLayoutPanel();
            documentGroupLayout = new TableLayoutPanel();
            attributeGroup = new GroupBox();
            elementPathLayout = new TableLayoutPanel();
            transformLayout = new TableLayoutPanel();
            documentTemplateLayout = new TableLayoutPanel();
            templateLayoutPanel.SuspendLayout();
            templateTabs.SuspendLayout();
            documentSettingTab.SuspendLayout();
            documentLayout.SuspendLayout();
            documentGroup.SuspendLayout();
            documentGroupLayout.SuspendLayout();
            scriptingGroup.SuspendLayout();
            scriptingGroupLayout.SuspendLayout();
            nodeDefinitionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)schemaDefinitionLayout).BeginInit();
            schemaDefinitionLayout.Panel1.SuspendLayout();
            schemaDefinitionLayout.Panel2.SuspendLayout();
            schemaDefinitionLayout.SuspendLayout();
            schemaNodeLayout.SuspendLayout();
            attributeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributeData).BeginInit();
            dataSelectionTab.SuspendLayout();
            elementPathLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)templatePathData).BeginInit();
            transformTab.SuspendLayout();
            transformLayout.SuspendLayout();
            transformToolStrip.SuspendLayout();
            documentsTab.SuspendLayout();
            documentTemplateLayout.SuspendLayout();
            documentToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            templateToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingPath).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
            SuspendLayout();
            // 
            // templateLayoutPanel
            // 
            templateLayoutPanel.ColumnCount = 1;
            templateLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateLayoutPanel.Controls.Add(templateTitleData, 0, 0);
            templateLayoutPanel.Controls.Add(templateDescriptionData, 0, 1);
            templateLayoutPanel.Controls.Add(templateTabs, 0, 2);
            templateLayoutPanel.Dock = DockStyle.Fill;
            templateLayoutPanel.Location = new Point(0, 25);
            templateLayoutPanel.Name = "templateLayoutPanel";
            templateLayoutPanel.RowCount = 3;
            templateLayoutPanel.RowStyles.Add(new RowStyle());
            templateLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            templateLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            templateLayoutPanel.Size = new Size(863, 689);
            templateLayoutPanel.TabIndex = 5;
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
            templateTitleData.Size = new Size(857, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
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
            templateDescriptionData.Size = new Size(857, 121);
            templateDescriptionData.TabIndex = 1;
            templateDescriptionData.WordWrap = true;
            // 
            // templateTabs
            // 
            templateTabs.Controls.Add(documentSettingTab);
            templateTabs.Controls.Add(nodeDefinitionTab);
            templateTabs.Controls.Add(dataSelectionTab);
            templateTabs.Controls.Add(transformTab);
            templateTabs.Controls.Add(documentsTab);
            templateTabs.Dock = DockStyle.Fill;
            templateTabs.Location = new Point(3, 180);
            templateTabs.Name = "templateTabs";
            templateTabs.SelectedIndex = 0;
            templateTabs.Size = new Size(857, 506);
            templateTabs.TabIndex = 2;
            // 
            // documentSettingTab
            // 
            documentSettingTab.BackColor = SystemColors.Control;
            documentSettingTab.Controls.Add(documentLayout);
            documentSettingTab.Location = new Point(4, 24);
            documentSettingTab.Name = "documentSettingTab";
            documentSettingTab.Padding = new Padding(3);
            documentSettingTab.Size = new Size(849, 478);
            documentSettingTab.TabIndex = 0;
            documentSettingTab.Text = "Document settings";
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 2;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.Controls.Add(documentGroup, 0, 2);
            documentLayout.Controls.Add(breakOnScopeData, 1, 0);
            documentLayout.Controls.Add(rootDirectoryData, 0, 0);
            documentLayout.Controls.Add(rootDirectoryExpanded, 0, 1);
            documentLayout.Controls.Add(scriptingGroup, 1, 1);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(3, 3);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 3;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            documentLayout.Size = new Size(843, 472);
            documentLayout.TabIndex = 0;
            // 
            // documentGroup
            // 
            documentGroup.AutoSize = true;
            documentGroup.Controls.Add(documentGroupLayout);
            documentGroup.Dock = DockStyle.Fill;
            documentGroup.Location = new Point(3, 105);
            documentGroup.Name = "documentGroup";
            documentGroup.Size = new Size(415, 364);
            documentGroup.TabIndex = 4;
            documentGroup.TabStop = false;
            documentGroup.Text = "Document (xml)";
            // 
            // documentGroupLayout
            // 
            documentGroupLayout.ColumnCount = 4;
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            documentGroupLayout.ColumnStyles.Add(new ColumnStyle());
            documentGroupLayout.Controls.Add(documentDirectoryData, 0, 0);
            documentGroupLayout.Controls.Add(documentPrefixData, 0, 1);
            documentGroupLayout.Controls.Add(documentSuffixData, 1, 1);
            documentGroupLayout.Controls.Add(documentExtensionData, 2, 1);
            documentGroupLayout.Controls.Add(documentDirectoryPicker, 3, 0);
            documentGroupLayout.Dock = DockStyle.Fill;
            documentGroupLayout.Location = new Point(3, 19);
            documentGroupLayout.Name = "documentGroupLayout";
            documentGroupLayout.RowCount = 2;
            documentGroupLayout.RowStyles.Add(new RowStyle());
            documentGroupLayout.RowStyles.Add(new RowStyle());
            documentGroupLayout.Size = new Size(409, 342);
            documentGroupLayout.TabIndex = 0;
            // 
            // documentDirectoryData
            // 
            documentDirectoryData.AutoSize = true;
            documentGroupLayout.SetColumnSpan(documentDirectoryData, 3);
            documentDirectoryData.Dock = DockStyle.Fill;
            documentDirectoryData.HeaderText = "Document Directory (relative)";
            documentDirectoryData.Location = new Point(3, 3);
            documentDirectoryData.Multiline = false;
            documentDirectoryData.Name = "documentDirectoryData";
            documentDirectoryData.ReadOnly = false;
            documentDirectoryData.Size = new Size(320, 44);
            documentDirectoryData.TabIndex = 0;
            documentDirectoryData.WordWrap = true;
            // 
            // documentPrefixData
            // 
            documentPrefixData.AutoSize = true;
            documentPrefixData.Dock = DockStyle.Fill;
            documentPrefixData.HeaderText = "Prefix";
            documentPrefixData.Location = new Point(3, 53);
            documentPrefixData.Multiline = false;
            documentPrefixData.Name = "documentPrefixData";
            documentPrefixData.ReadOnly = false;
            documentPrefixData.Size = new Size(141, 286);
            documentPrefixData.TabIndex = 2;
            documentPrefixData.WordWrap = true;
            // 
            // documentSuffixData
            // 
            documentSuffixData.AutoSize = true;
            documentSuffixData.Dock = DockStyle.Fill;
            documentSuffixData.HeaderText = "Suffix";
            documentSuffixData.Location = new Point(150, 53);
            documentSuffixData.Multiline = false;
            documentSuffixData.Name = "documentSuffixData";
            documentSuffixData.ReadOnly = false;
            documentSuffixData.Size = new Size(141, 286);
            documentSuffixData.TabIndex = 3;
            documentSuffixData.WordWrap = true;
            // 
            // documentExtensionData
            // 
            documentExtensionData.AutoSize = true;
            documentGroupLayout.SetColumnSpan(documentExtensionData, 2);
            documentExtensionData.Dock = DockStyle.Fill;
            documentExtensionData.HeaderText = "Extension";
            documentExtensionData.Location = new Point(297, 53);
            documentExtensionData.Multiline = false;
            documentExtensionData.Name = "documentExtensionData";
            documentExtensionData.ReadOnly = false;
            documentExtensionData.Size = new Size(109, 286);
            documentExtensionData.TabIndex = 4;
            documentExtensionData.WordWrap = true;
            // 
            // documentDirectoryPicker
            // 
            documentDirectoryPicker.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            documentDirectoryPicker.Image = Properties.Resources.FolderOpened;
            documentDirectoryPicker.Location = new Point(331, 24);
            documentDirectoryPicker.Name = "documentDirectoryPicker";
            documentDirectoryPicker.Size = new Size(75, 23);
            documentDirectoryPicker.TabIndex = 1;
            documentDirectoryPicker.Text = "Select";
            documentDirectoryPicker.TextImageRelation = TextImageRelation.ImageBeforeText;
            documentDirectoryPicker.UseVisualStyleBackColor = true;
            documentDirectoryPicker.Click += DocumentDirectoryPicker_Click;
            // 
            // breakOnScopeData
            // 
            breakOnScopeData.AutoSize = true;
            breakOnScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            breakOnScopeData.Dock = DockStyle.Fill;
            breakOnScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            breakOnScopeData.HeaderText = "new Document on Scope";
            breakOnScopeData.Location = new Point(424, 3);
            breakOnScopeData.Name = "breakOnScopeData";
            breakOnScopeData.ReadOnly = false;
            breakOnScopeData.Size = new Size(416, 46);
            breakOnScopeData.TabIndex = 0;
            // 
            // rootDirectoryData
            // 
            rootDirectoryData.AutoSize = true;
            rootDirectoryData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootDirectoryData.Dock = DockStyle.Fill;
            rootDirectoryData.DropDownStyle = ComboBoxStyle.DropDownList;
            rootDirectoryData.HeaderText = "Root Folder";
            rootDirectoryData.Location = new Point(3, 3);
            rootDirectoryData.Name = "rootDirectoryData";
            rootDirectoryData.ReadOnly = false;
            rootDirectoryData.Size = new Size(415, 46);
            rootDirectoryData.TabIndex = 1;
            rootDirectoryData.SelectedIndexChanged += RootDirectoryData_SelectedIndexChanged;
            rootDirectoryData.SelectionChangeCommitted += RootDirectoryData_SelectionChangeCommitted;
            // 
            // rootDirectoryExpanded
            // 
            rootDirectoryExpanded.AutoSize = true;
            rootDirectoryExpanded.Dock = DockStyle.Fill;
            rootDirectoryExpanded.HeaderText = "local Root Directory";
            rootDirectoryExpanded.Location = new Point(3, 55);
            rootDirectoryExpanded.Multiline = false;
            rootDirectoryExpanded.Name = "rootDirectoryExpanded";
            rootDirectoryExpanded.ReadOnly = true;
            rootDirectoryExpanded.Size = new Size(415, 44);
            rootDirectoryExpanded.TabIndex = 2;
            rootDirectoryExpanded.WordWrap = true;
            // 
            // scriptingGroup
            // 
            scriptingGroup.AutoSize = true;
            scriptingGroup.Controls.Add(scriptingGroupLayout);
            scriptingGroup.Dock = DockStyle.Fill;
            scriptingGroup.Location = new Point(424, 55);
            scriptingGroup.Name = "scriptingGroup";
            documentLayout.SetRowSpan(scriptingGroup, 2);
            scriptingGroup.Size = new Size(416, 414);
            scriptingGroup.TabIndex = 5;
            scriptingGroup.TabStop = false;
            scriptingGroup.Text = "Scripting (txt ...)";
            // 
            // scriptingGroupLayout
            // 
            scriptingGroupLayout.ColumnCount = 4;
            scriptingGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            scriptingGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            scriptingGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            scriptingGroupLayout.ColumnStyles.Add(new ColumnStyle());
            scriptingGroupLayout.Controls.Add(scriptingExtensionData, 2, 2);
            scriptingGroupLayout.Controls.Add(scriptingSuffixData, 1, 2);
            scriptingGroupLayout.Controls.Add(scriptingPrefixData, 0, 2);
            scriptingGroupLayout.Controls.Add(scriptingDirectoryData, 0, 1);
            scriptingGroupLayout.Controls.Add(scriptAsData, 0, 0);
            scriptingGroupLayout.Controls.Add(scriptingDirectoryPicker, 3, 1);
            scriptingGroupLayout.Dock = DockStyle.Fill;
            scriptingGroupLayout.Location = new Point(3, 19);
            scriptingGroupLayout.Name = "scriptingGroupLayout";
            scriptingGroupLayout.RowCount = 3;
            scriptingGroupLayout.RowStyles.Add(new RowStyle());
            scriptingGroupLayout.RowStyles.Add(new RowStyle());
            scriptingGroupLayout.RowStyles.Add(new RowStyle());
            scriptingGroupLayout.Size = new Size(410, 392);
            scriptingGroupLayout.TabIndex = 0;
            // 
            // scriptingExtensionData
            // 
            scriptingExtensionData.AutoSize = true;
            scriptingGroupLayout.SetColumnSpan(scriptingExtensionData, 2);
            scriptingExtensionData.Dock = DockStyle.Fill;
            scriptingExtensionData.HeaderText = "Extension";
            scriptingExtensionData.Location = new Point(299, 105);
            scriptingExtensionData.Multiline = false;
            scriptingExtensionData.Name = "scriptingExtensionData";
            scriptingExtensionData.ReadOnly = false;
            scriptingExtensionData.Size = new Size(108, 284);
            scriptingExtensionData.TabIndex = 5;
            scriptingExtensionData.WordWrap = true;
            // 
            // scriptingSuffixData
            // 
            scriptingSuffixData.AutoSize = true;
            scriptingSuffixData.Dock = DockStyle.Fill;
            scriptingSuffixData.HeaderText = "Suffix";
            scriptingSuffixData.Location = new Point(151, 105);
            scriptingSuffixData.Multiline = false;
            scriptingSuffixData.Name = "scriptingSuffixData";
            scriptingSuffixData.ReadOnly = false;
            scriptingSuffixData.Size = new Size(142, 284);
            scriptingSuffixData.TabIndex = 4;
            scriptingSuffixData.WordWrap = true;
            // 
            // scriptingPrefixData
            // 
            scriptingPrefixData.AutoSize = true;
            scriptingPrefixData.Dock = DockStyle.Fill;
            scriptingPrefixData.HeaderText = "Prefix";
            scriptingPrefixData.Location = new Point(3, 105);
            scriptingPrefixData.Multiline = false;
            scriptingPrefixData.Name = "scriptingPrefixData";
            scriptingPrefixData.ReadOnly = false;
            scriptingPrefixData.Size = new Size(142, 284);
            scriptingPrefixData.TabIndex = 3;
            scriptingPrefixData.WordWrap = true;
            // 
            // scriptingDirectoryData
            // 
            scriptingDirectoryData.AutoSize = true;
            scriptingGroupLayout.SetColumnSpan(scriptingDirectoryData, 3);
            scriptingDirectoryData.Dock = DockStyle.Fill;
            scriptingDirectoryData.HeaderText = "Scripting Directory (relative)";
            scriptingDirectoryData.Location = new Point(3, 55);
            scriptingDirectoryData.Multiline = false;
            scriptingDirectoryData.Name = "scriptingDirectoryData";
            scriptingDirectoryData.ReadOnly = false;
            scriptingDirectoryData.Size = new Size(322, 44);
            scriptingDirectoryData.TabIndex = 0;
            scriptingDirectoryData.WordWrap = true;
            // 
            // scriptAsData
            // 
            scriptAsData.AutoSize = true;
            scriptAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scriptAsData.DropDownStyle = ComboBoxStyle.DropDownList;
            scriptAsData.HeaderText = "Script As";
            scriptAsData.Location = new Point(3, 3);
            scriptAsData.Name = "scriptAsData";
            scriptAsData.ReadOnly = false;
            scriptAsData.Size = new Size(129, 46);
            scriptAsData.TabIndex = 3;
            scriptAsData.SelectedIndexChanged += ScriptAsData_SelectedIndexChanged;
            scriptAsData.SelectionChangeCommitted += ScriptAsData_SelectionChangeCommitted;
            // 
            // scriptingDirectoryPicker
            // 
            scriptingDirectoryPicker.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            scriptingDirectoryPicker.Image = Properties.Resources.FolderOpened;
            scriptingDirectoryPicker.Location = new Point(332, 76);
            scriptingDirectoryPicker.Name = "scriptingDirectoryPicker";
            scriptingDirectoryPicker.Size = new Size(75, 23);
            scriptingDirectoryPicker.TabIndex = 2;
            scriptingDirectoryPicker.Text = "Select";
            scriptingDirectoryPicker.TextImageRelation = TextImageRelation.ImageBeforeText;
            scriptingDirectoryPicker.UseVisualStyleBackColor = true;
            scriptingDirectoryPicker.Click += ScriptingDirectoryPicker_Click;
            // 
            // nodeDefinitionTab
            // 
            nodeDefinitionTab.BackColor = SystemColors.Control;
            nodeDefinitionTab.Controls.Add(schemaDefinitionLayout);
            nodeDefinitionTab.Location = new Point(4, 24);
            nodeDefinitionTab.Name = "nodeDefinitionTab";
            nodeDefinitionTab.Size = new Size(192, 72);
            nodeDefinitionTab.TabIndex = 2;
            nodeDefinitionTab.Text = "Node Definition (XSD)";
            // 
            // schemaDefinitionLayout
            // 
            schemaDefinitionLayout.BorderStyle = BorderStyle.FixedSingle;
            schemaDefinitionLayout.Dock = DockStyle.Fill;
            schemaDefinitionLayout.Location = new Point(0, 0);
            schemaDefinitionLayout.Name = "schemaDefinitionLayout";
            // 
            // schemaDefinitionLayout.Panel1
            // 
            schemaDefinitionLayout.Panel1.Controls.Add(elementSelection);
            // 
            // schemaDefinitionLayout.Panel2
            // 
            schemaDefinitionLayout.Panel2.Controls.Add(schemaNodeLayout);
            schemaDefinitionLayout.Size = new Size(192, 72);
            schemaDefinitionLayout.SplitterDistance = 64;
            schemaDefinitionLayout.TabIndex = 1;
            // 
            // elementSelection
            // 
            elementSelection.CheckBoxes = true;
            elementSelection.Columns.AddRange(new ColumnHeader[] { columnName });
            elementSelection.Dock = DockStyle.Fill;
            listViewGroup1.Header = "Scope Name 1";
            listViewGroup1.Name = "sampleScope";
            elementSelection.Groups.AddRange(new ListViewGroup[] { listViewGroup1 });
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            elementSelection.Items.AddRange(new ListViewItem[] { listViewItem1 });
            elementSelection.Location = new Point(0, 0);
            elementSelection.MultiSelect = false;
            elementSelection.Name = "elementSelection";
            elementSelection.Size = new Size(62, 70);
            elementSelection.TabIndex = 2;
            elementSelection.UseCompatibleStateImageBehavior = false;
            elementSelection.View = View.Details;
            elementSelection.ItemCheck += ElementSelection_ItemCheck;
            elementSelection.ItemChecked += ElementSelection_ItemChecked;
            elementSelection.ItemSelectionChanged += ElementSelection_ItemSelectionChanged;
            elementSelection.SelectedIndexChanged += ElementSelection_SelectedIndexChanged;
            elementSelection.VisibleChanged += ElementSelection_VisibleChanged;
            // 
            // columnName
            // 
            columnName.Text = "Column";
            columnName.Width = 300;
            // 
            // schemaNodeLayout
            // 
            schemaNodeLayout.ColumnCount = 2;
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            schemaNodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            schemaNodeLayout.Controls.Add(propertyNameData, 0, 1);
            schemaNodeLayout.Controls.Add(nodeNameData, 0, 2);
            schemaNodeLayout.Controls.Add(nodeValueAsData, 1, 2);
            schemaNodeLayout.Controls.Add(attributeGroup, 0, 4);
            schemaNodeLayout.Controls.Add(propertyScopeData, 0, 0);
            schemaNodeLayout.Dock = DockStyle.Fill;
            schemaNodeLayout.Location = new Point(0, 0);
            schemaNodeLayout.Name = "schemaNodeLayout";
            schemaNodeLayout.RowCount = 5;
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle());
            schemaNodeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            schemaNodeLayout.Size = new Size(122, 70);
            schemaNodeLayout.TabIndex = 0;
            // 
            // propertyNameData
            // 
            propertyNameData.AutoSize = true;
            schemaNodeLayout.SetColumnSpan(propertyNameData, 2);
            propertyNameData.Dock = DockStyle.Fill;
            propertyNameData.HeaderText = "Property Name";
            propertyNameData.Location = new Point(3, 55);
            propertyNameData.Multiline = false;
            propertyNameData.Name = "propertyNameData";
            propertyNameData.ReadOnly = true;
            propertyNameData.Size = new Size(116, 44);
            propertyNameData.TabIndex = 1;
            propertyNameData.WordWrap = true;
            // 
            // nodeNameData
            // 
            nodeNameData.AutoSize = true;
            nodeNameData.Dock = DockStyle.Fill;
            nodeNameData.HeaderText = "Element Name";
            nodeNameData.Location = new Point(3, 105);
            nodeNameData.Multiline = false;
            nodeNameData.Name = "nodeNameData";
            nodeNameData.ReadOnly = false;
            nodeNameData.Size = new Size(67, 46);
            nodeNameData.TabIndex = 2;
            nodeNameData.WordWrap = true;
            // 
            // nodeValueAsData
            // 
            nodeValueAsData.AutoSize = true;
            nodeValueAsData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            nodeValueAsData.Dock = DockStyle.Fill;
            nodeValueAsData.DropDownStyle = ComboBoxStyle.DropDownList;
            nodeValueAsData.HeaderText = "render Data as";
            nodeValueAsData.Location = new Point(76, 105);
            nodeValueAsData.Name = "nodeValueAsData";
            nodeValueAsData.ReadOnly = false;
            nodeValueAsData.Size = new Size(43, 46);
            nodeValueAsData.TabIndex = 5;
            // 
            // attributeGroup
            // 
            schemaNodeLayout.SetColumnSpan(attributeGroup, 2);
            attributeGroup.Controls.Add(attributeData);
            attributeGroup.Dock = DockStyle.Fill;
            attributeGroup.Location = new Point(3, 157);
            attributeGroup.Name = "attributeGroup";
            attributeGroup.Size = new Size(116, 14);
            attributeGroup.TabIndex = 6;
            attributeGroup.TabStop = false;
            attributeGroup.Text = "Attributes";
            // 
            // attributeData
            // 
            attributeData.Columns.AddRange(new DataGridViewColumn[] { attributeNameColumn, attributeValueColumn, attributePropertyColumn });
            attributeData.Dock = DockStyle.Fill;
            attributeData.Location = new Point(3, 19);
            attributeData.Name = "attributeData";
            attributeData.Size = new Size(110, 0);
            attributeData.TabIndex = 0;
            // 
            // attributeNameColumn
            // 
            attributeNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeNameColumn.DataPropertyName = "AttributeName";
            attributeNameColumn.FillWeight = 50F;
            attributeNameColumn.HeaderText = "Attribute Name";
            attributeNameColumn.Name = "attributeNameColumn";
            // 
            // attributeValueColumn
            // 
            attributeValueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributeValueColumn.DataPropertyName = "AttributeValue";
            attributeValueColumn.HeaderText = "Default Value";
            attributeValueColumn.Name = "attributeValueColumn";
            // 
            // attributePropertyColumn
            // 
            attributePropertyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            attributePropertyColumn.DataPropertyName = "PropertyId";
            attributePropertyColumn.FillWeight = 50F;
            attributePropertyColumn.HeaderText = "use Property Value";
            attributePropertyColumn.Name = "attributePropertyColumn";
            // 
            // propertyScopeData
            // 
            propertyScopeData.AutoSize = true;
            propertyScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            propertyScopeData.Dock = DockStyle.Fill;
            propertyScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            propertyScopeData.HeaderText = "Property Scope";
            propertyScopeData.Location = new Point(3, 3);
            propertyScopeData.Name = "propertyScopeData";
            propertyScopeData.ReadOnly = true;
            propertyScopeData.Size = new Size(67, 46);
            propertyScopeData.TabIndex = 7;
            // 
            // dataSelectionTab
            // 
            dataSelectionTab.BackColor = SystemColors.Control;
            dataSelectionTab.Controls.Add(elementPathLayout);
            dataSelectionTab.Location = new Point(4, 24);
            dataSelectionTab.Name = "dataSelectionTab";
            dataSelectionTab.Size = new Size(192, 72);
            dataSelectionTab.TabIndex = 3;
            dataSelectionTab.Text = "Data Selection (XPath)";
            // 
            // elementPathLayout
            // 
            elementPathLayout.ColumnCount = 2;
            elementPathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            elementPathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            elementPathLayout.Controls.Add(templatePathData, 0, 0);
            elementPathLayout.Controls.Add(templatePathSelect, 1, 0);
            elementPathLayout.Dock = DockStyle.Fill;
            elementPathLayout.Location = new Point(0, 0);
            elementPathLayout.Name = "elementPathLayout";
            elementPathLayout.RowCount = 1;
            elementPathLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            elementPathLayout.Size = new Size(192, 72);
            elementPathLayout.TabIndex = 1;
            // 
            // templatePathData
            // 
            templatePathData.AllowUserToAddRows = false;
            templatePathData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            templatePathData.Columns.AddRange(new DataGridViewColumn[] { scopeNameData, selectionMemberData });
            templatePathData.Dock = DockStyle.Fill;
            templatePathData.Location = new Point(3, 3);
            templatePathData.Name = "templatePathData";
            templatePathData.Size = new Size(90, 66);
            templatePathData.TabIndex = 5;
            // 
            // scopeNameData
            // 
            scopeNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeNameData.DataPropertyName = "PathScope";
            scopeNameData.FillWeight = 50F;
            scopeNameData.HeaderText = "Scope";
            scopeNameData.Name = "scopeNameData";
            // 
            // selectionMemberData
            // 
            selectionMemberData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            selectionMemberData.DataPropertyName = "PathName";
            selectionMemberData.HeaderText = "Path";
            selectionMemberData.Name = "selectionMemberData";
            // 
            // templatePathSelect
            // 
            templatePathSelect.ApplyImage = Properties.Resources.NewXPath;
            templatePathSelect.ApplyText = "apply";
            templatePathSelect.Dock = DockStyle.Fill;
            templatePathSelect.HeaderText = "Selected Path";
            templatePathSelect.Location = new Point(99, 3);
            templatePathSelect.Name = "templatePathSelect";
            templatePathSelect.ReadOnly = false;
            templatePathSelect.Scope = DataLayer.ApplicationData.Scope.ScopeType.Null;
            templatePathSelect.ScopePath = namedScopePath1;
            templatePathSelect.Size = new Size(90, 66);
            templatePathSelect.TabIndex = 6;
            templatePathSelect.OnApply += NamedScopeData_OnApply;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(192, 72);
            transformTab.TabIndex = 1;
            transformTab.Text = "Transform (XSLT)";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformToolStrip, 0, 0);
            transformLayout.Controls.Add(transformExceptionData, 0, 2);
            transformLayout.Controls.Add(transformScriptData, 0, 1);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(3, 3);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 3;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            transformLayout.Size = new Size(186, 66);
            transformLayout.TabIndex = 0;
            // 
            // transformToolStrip
            // 
            transformToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            transformToolStrip.Items.AddRange(new ToolStripItem[] { transformParseCommand, transformImportCommand, transformExportCommand, transformFilePath });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(186, 25);
            transformToolStrip.Stretch = true;
            transformToolStrip.TabIndex = 0;
            transformToolStrip.Text = "Transform Tool Strip";
            transformToolStrip.Resize += TransformToolStrip_Resize;
            // 
            // transformParseCommand
            // 
            transformParseCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformParseCommand.Image = Properties.Resources.XSLTransform;
            transformParseCommand.ImageTransparentColor = Color.Magenta;
            transformParseCommand.Name = "transformParseCommand";
            transformParseCommand.Size = new Size(23, 22);
            transformParseCommand.Text = "Parse Transform";
            transformParseCommand.Click += TransformParseCommand_Click;
            // 
            // transformImportCommand
            // 
            transformImportCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformImportCommand.Image = Properties.Resources.OpenFile;
            transformImportCommand.ImageTransparentColor = Color.Magenta;
            transformImportCommand.Name = "transformImportCommand";
            transformImportCommand.Size = new Size(23, 22);
            transformImportCommand.Text = "Import File";
            transformImportCommand.Click += TransformImportCommand_Click;
            // 
            // transformExportCommand
            // 
            transformExportCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformExportCommand.Image = Properties.Resources.Save;
            transformExportCommand.ImageTransparentColor = Color.Magenta;
            transformExportCommand.Name = "transformExportCommand";
            transformExportCommand.Size = new Size(23, 22);
            transformExportCommand.Text = "Export File";
            transformExportCommand.Click += TransformExportCommand_Click;
            // 
            // transformFilePath
            // 
            transformFilePath.Alignment = ToolStripItemAlignment.Right;
            transformFilePath.Name = "transformFilePath";
            transformFilePath.Size = new Size(72, 22);
            transformFilePath.Text = "dummy text";
            // 
            // transformExceptionData
            // 
            transformExceptionData.AutoSize = true;
            transformExceptionData.Dock = DockStyle.Fill;
            transformExceptionData.HeaderText = "Exception";
            transformExceptionData.Location = new Point(3, 58);
            transformExceptionData.Multiline = true;
            transformExceptionData.Name = "transformExceptionData";
            transformExceptionData.ReadOnly = true;
            transformExceptionData.Size = new Size(180, 5);
            transformExceptionData.TabIndex = 2;
            transformExceptionData.WordWrap = false;
            // 
            // transformScriptData
            // 
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.Location = new Point(3, 28);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.ScrollBars = ScrollBars.Both;
            transformScriptData.Size = new Size(180, 24);
            transformScriptData.TabIndex = 3;
            transformScriptData.WordWrap = false;
            // 
            // documentsTab
            // 
            documentsTab.BackColor = SystemColors.Control;
            documentsTab.Controls.Add(documentTemplateLayout);
            documentsTab.Location = new Point(4, 24);
            documentsTab.Name = "documentsTab";
            documentsTab.Size = new Size(849, 478);
            documentsTab.TabIndex = 4;
            documentsTab.Text = "Documents (XML & Script)";
            // 
            // documentTemplateLayout
            // 
            documentTemplateLayout.ColumnCount = 2;
            documentTemplateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentTemplateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentTemplateLayout.Controls.Add(documentToolStrip, 0, 0);
            documentTemplateLayout.Controls.Add(documentData, 0, 1);
            documentTemplateLayout.Controls.Add(documentException, 0, 3);
            documentTemplateLayout.Controls.Add(documentXMLData, 0, 2);
            documentTemplateLayout.Controls.Add(documentScriptData, 1, 2);
            documentTemplateLayout.Dock = DockStyle.Fill;
            documentTemplateLayout.Location = new Point(0, 0);
            documentTemplateLayout.Name = "documentTemplateLayout";
            documentTemplateLayout.RowCount = 4;
            documentTemplateLayout.RowStyles.Add(new RowStyle());
            documentTemplateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            documentTemplateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            documentTemplateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            documentTemplateLayout.Size = new Size(849, 478);
            documentTemplateLayout.TabIndex = 0;
            // 
            // documentToolStrip
            // 
            documentTemplateLayout.SetColumnSpan(documentToolStrip, 2);
            documentToolStrip.Items.AddRange(new ToolStripItem[] { documentBuildComand, documentSaveXMLCommand, documentSaveScriptCommand, documentSaveAllCommand, documentStatus });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(849, 25);
            documentToolStrip.TabIndex = 0;
            documentToolStrip.Text = "toolStrip1";
            // 
            // documentBuildComand
            // 
            documentBuildComand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentBuildComand.Image = Properties.Resources.NewXmlFile;
            documentBuildComand.ImageTransparentColor = Color.Magenta;
            documentBuildComand.Name = "documentBuildComand";
            documentBuildComand.Size = new Size(23, 22);
            documentBuildComand.Text = "build Documents";
            documentBuildComand.Click += DocumentBuildComand_Click;
            // 
            // documentSaveXMLCommand
            // 
            documentSaveXMLCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSaveXMLCommand.Image = Properties.Resources.SaveXmlFile;
            documentSaveXMLCommand.ImageTransparentColor = Color.Magenta;
            documentSaveXMLCommand.Name = "documentSaveXMLCommand";
            documentSaveXMLCommand.Size = new Size(23, 22);
            documentSaveXMLCommand.Text = "Save XML";
            documentSaveXMLCommand.Click += DocumentSaveXMLCommand_Click;
            // 
            // documentSaveScriptCommand
            // 
            documentSaveScriptCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSaveScriptCommand.Image = Properties.Resources.SaveScript;
            documentSaveScriptCommand.ImageTransparentColor = Color.Magenta;
            documentSaveScriptCommand.Name = "documentSaveScriptCommand";
            documentSaveScriptCommand.Size = new Size(23, 22);
            documentSaveScriptCommand.Text = "Save Script";
            documentSaveScriptCommand.Click += DocumentSaveScriptCommand_Click;
            // 
            // documentSaveAllCommand
            // 
            documentSaveAllCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSaveAllCommand.Image = Properties.Resources.SaveAll;
            documentSaveAllCommand.ImageTransparentColor = Color.Magenta;
            documentSaveAllCommand.Name = "documentSaveAllCommand";
            documentSaveAllCommand.Size = new Size(23, 22);
            documentSaveAllCommand.Text = "Save All";
            documentSaveAllCommand.Click += DocumentSaveAllCommand_Click;
            // 
            // documentStatus
            // 
            documentStatus.Alignment = ToolStripItemAlignment.Right;
            documentStatus.Name = "documentStatus";
            documentStatus.Size = new Size(72, 22);
            documentStatus.Text = "dummy text";
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { documentElementColumn, documentNameColumn, documentScriptName });
            documentTemplateLayout.SetColumnSpan(documentData, 2);
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 28);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(843, 107);
            documentData.TabIndex = 1;
            documentData.SelectionChanged += DocumentData_SelectionChanged;
            // 
            // documentElementColumn
            // 
            documentElementColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentElementColumn.DataPropertyName = "ElementName";
            documentElementColumn.FillWeight = 50F;
            documentElementColumn.HeaderText = "Element Name";
            documentElementColumn.Name = "documentElementColumn";
            documentElementColumn.ReadOnly = true;
            // 
            // documentNameColumn
            // 
            documentNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentNameColumn.DataPropertyName = "DocumentName";
            documentNameColumn.HeaderText = "Document Name";
            documentNameColumn.Name = "documentNameColumn";
            documentNameColumn.ReadOnly = true;
            // 
            // documentScriptName
            // 
            documentScriptName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentScriptName.DataPropertyName = "ScriptName";
            documentScriptName.HeaderText = "Script Name";
            documentScriptName.Name = "documentScriptName";
            documentScriptName.ReadOnly = true;
            // 
            // documentException
            // 
            documentException.AutoSize = true;
            documentTemplateLayout.SetColumnSpan(documentException, 2);
            documentException.Dock = DockStyle.Fill;
            documentException.HeaderText = "Exception";
            documentException.Location = new Point(3, 367);
            documentException.Multiline = true;
            documentException.Name = "documentException";
            documentException.ReadOnly = true;
            documentException.Size = new Size(843, 108);
            documentException.TabIndex = 3;
            documentException.WordWrap = false;
            // 
            // documentXMLData
            // 
            documentXMLData.AutoSize = true;
            documentXMLData.Dock = DockStyle.Fill;
            documentXMLData.HeaderText = "XML";
            documentXMLData.Location = new Point(3, 141);
            documentXMLData.Multiline = true;
            documentXMLData.Name = "documentXMLData";
            documentXMLData.ReadOnly = true;
            documentXMLData.Size = new Size(418, 220);
            documentXMLData.TabIndex = 4;
            documentXMLData.WordWrap = false;
            // 
            // documentScriptData
            // 
            documentScriptData.AutoSize = true;
            documentScriptData.Dock = DockStyle.Fill;
            documentScriptData.HeaderText = "Script";
            documentScriptData.Location = new Point(427, 141);
            documentScriptData.Multiline = true;
            documentScriptData.Name = "documentScriptData";
            documentScriptData.ReadOnly = true;
            documentScriptData.Size = new Size(419, 220);
            documentScriptData.TabIndex = 5;
            documentScriptData.WordWrap = false;
            // 
            // templateToolStrip
            // 
            templateToolStrip.Items.AddRange(new ToolStripItem[] { deleteTemplateCommand });
            templateToolStrip.Name = "templateToolStrip";
            templateToolStrip.Size = new Size(159, 26);
            // 
            // deleteTemplateCommand
            // 
            deleteTemplateCommand.Image = Properties.Resources.DeleteXSLTransform;
            deleteTemplateCommand.Name = "deleteTemplateCommand";
            deleteTemplateCommand.Size = new Size(158, 22);
            deleteTemplateCommand.Text = "Delete Template";
            deleteTemplateCommand.Click += DeleteTemplateCommand_Click;
            // 
            // bindingPath
            // 
            bindingPath.AddingNew += BindingPath_AddingNew;
            bindingPath.CurrentChanged += BindingPath_CurrentChanged;
            // 
            // bindingNode
            // 
            bindingNode.AddingNew += BindingNode_AddingNew;
            // 
            // bindingAttribute
            // 
            bindingAttribute.AddingNew += BindingAttribute_AddingNew;
            // 
            // ScriptingTemplate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(863, 714);
            Controls.Add(templateLayoutPanel);
            Name = "ScriptingTemplate";
            Text = "ScriptingTemplate";
            Load += ScriptingTemplate_Load;
            Controls.SetChildIndex(templateLayoutPanel, 0);
            templateLayoutPanel.ResumeLayout(false);
            templateLayoutPanel.PerformLayout();
            templateTabs.ResumeLayout(false);
            documentSettingTab.ResumeLayout(false);
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            documentGroup.ResumeLayout(false);
            documentGroupLayout.ResumeLayout(false);
            documentGroupLayout.PerformLayout();
            scriptingGroup.ResumeLayout(false);
            scriptingGroupLayout.ResumeLayout(false);
            scriptingGroupLayout.PerformLayout();
            nodeDefinitionTab.ResumeLayout(false);
            schemaDefinitionLayout.Panel1.ResumeLayout(false);
            schemaDefinitionLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)schemaDefinitionLayout).EndInit();
            schemaDefinitionLayout.ResumeLayout(false);
            schemaNodeLayout.ResumeLayout(false);
            schemaNodeLayout.PerformLayout();
            attributeGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attributeData).EndInit();
            dataSelectionTab.ResumeLayout(false);
            elementPathLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)templatePathData).EndInit();
            transformTab.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformToolStrip.ResumeLayout(false);
            transformToolStrip.PerformLayout();
            documentsTab.ResumeLayout(false);
            documentTemplateLayout.ResumeLayout(false);
            documentTemplateLayout.PerformLayout();
            documentToolStrip.ResumeLayout(false);
            documentToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            templateToolStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingPath).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNode).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingAttribute).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource bindingTemplate;
        private ContextMenuStrip templateToolStrip;
        private TableLayoutPanel templateLayoutPanel;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private TabControl templateTabs;
        private TabPage documentSettingTab;
        private TabPage transformTab;
        private TableLayoutPanel documentLayout;
        private Controls.ComboBoxData breakOnScopeData;
        private Controls.ComboBoxData rootDirectoryData;
        private Controls.TextBoxData rootDirectoryExpanded;
        private Controls.ComboBoxData scriptAsData;
        private GroupBox documentGroup;
        private GroupBox scriptingGroup;
        private TableLayoutPanel documentGroupLayout;
        private Controls.TextBoxData documentDirectoryData;
        private Button documentDirectoryPicker;
        private Controls.TextBoxData documentPrefixData;
        private Controls.TextBoxData documentSuffixData;
        private Controls.TextBoxData documentExtensionData;
        private TableLayoutPanel scriptingGroupLayout;
        private Controls.TextBoxData scriptingDirectoryData;
        private Button scriptingDirectoryPicker;
        private Controls.TextBoxData scriptingPrefixData;
        private Controls.TextBoxData scriptingSuffixData;
        private Controls.TextBoxData scriptingExtensionData;
        private TableLayoutPanel transformLayout;
        private ToolStrip transformToolStrip;
        private Controls.TextBoxData transformExceptionData;
        private TextBox transformScriptData;
        private ToolStripButton transformParseCommand;
        private ToolStripButton transformImportCommand;
        private ToolStripButton transformExportCommand;
        private TabPage nodeDefinitionTab;
        private TabPage dataSelectionTab;
        private TabPage documentsTab;
        private ToolStripMenuItem deleteTemplateCommand;
        private FolderBrowserDialog folderBrowserDialog;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Controls.NamedScopeData templatePathSelect;
        private DataGridView templatePathData;
        private BindingSource bindingPath;
        private DataGridViewTextBoxColumn scopeNameData;
        private DataGridViewTextBoxColumn selectionMemberData;
        private ListView elementSelection;
        private ColumnHeader columnName;
        private SplitContainer schemaDefinitionLayout;
        private TableLayoutPanel schemaNodeLayout;
        private Controls.TextBoxData propertyNameData;
        private Controls.TextBoxData nodeNameData;
        private Controls.ComboBoxData nodeValueAsData;
        private BindingSource bindingNode;
        private DataGridView attributeData;
        private BindingSource bindingAttribute;
        private Controls.ComboBoxData propertyScopeData;
        private ToolStrip documentToolStrip;
        private DataGridView documentData;
        private Controls.TextBoxData documentException;
        private ToolStripButton documentBuildComand;
        private ToolStripButton documentSaveXMLCommand;
        private ToolStripButton documentSaveAllCommand;
        private ToolStripButton documentSaveScriptCommand;
        private Controls.TextBoxData documentXMLData;
        private Controls.TextBoxData documentScriptData;
        private BindingSource bindingDocument;
        private DataGridViewTextBoxColumn documentElementColumn;
        private DataGridViewTextBoxColumn documentNameColumn;
        private DataGridViewTextBoxColumn documentScriptName;
        private DataGridViewTextBoxColumn attributeNameColumn;
        private DataGridViewTextBoxColumn attributeValueColumn;
        private DataGridViewComboBoxColumn attributePropertyColumn;
        private ToolStripLabel documentStatus;
        private ToolStripLabel transformFilePath;
    }
}