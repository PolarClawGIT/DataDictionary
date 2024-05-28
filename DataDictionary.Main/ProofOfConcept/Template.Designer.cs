namespace DataDictionary.Main.Forms.ProofOfConcept
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
            TableLayoutPanel templateLayout;
            ListViewGroup listViewGroup2 = new ListViewGroup("Scope Name 1", HorizontalAlignment.Left);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "Column Name 1", "Column 1" }, -1);
            TableLayoutPanel elementLayout;
            GroupBox elementRenderGroup;
            TableLayoutPanel renderAsLayout;
            GroupBox renderDataAsGroup;
            TableLayoutPanel renderDataAsLayout;
            GroupBox renderTypeGroup;
            TableLayoutPanel renderTypeLayout;
            TableLayoutPanel elementPathLayout;
            BusinessLayer.NamedScope.NamedScopePath namedScopePath2 = new BusinessLayer.NamedScope.NamedScopePath();
            TableLayoutPanel dataPreviewLayout;
            GroupBox documentOptionGroup;
            TableLayoutPanel documentOptionLayout;
            Label documentNameElement;
            TableLayoutPanel tranformsLayout;
            TableLayoutPanel documentLayout;
            templateTitleData = new Controls.TextBoxData();
            templateDescriptionData = new Controls.TextBoxData();
            templateTabs = new TabControl();
            definitionTab = new TabPage();
            elementDefinitionSplit = new SplitContainer();
            elementSelection = new ListView();
            columnName = new ColumnHeader();
            renderAsAttribute = new CheckBox();
            renderAsElement = new CheckBox();
            elementScopeData = new Controls.TextBoxData();
            elementColumnData = new Controls.TextBoxData();
            elementNameData = new Controls.TextBoxData();
            renderDataAsText = new CheckBox();
            renderDataAsCData = new CheckBox();
            renderDataAsXml = new CheckBox();
            renderIsTypeData = new CheckBox();
            elementTypeData = new Controls.ComboBoxData();
            pathTab = new TabPage();
            namedScopeData = new Controls.NamedScopeData();
            selectionItemData = new DataGridView();
            scopeNameData = new DataGridViewTextBoxColumn();
            selectionMemberData = new DataGridViewTextBoxColumn();
            dataPreviewTab = new TabPage();
            dataPreviewCommands = new ToolStrip();
            dataPrevieweBuild = new ToolStripButton();
            dataPreviewSave = new ToolStripButton();
            documentBreakData = new CheckBox();
            documentScopeData = new Controls.ComboBoxData();
            documentNamePrefixData = new Controls.TextBoxData();
            documentXmlExtension = new Controls.TextBoxData();
            documentNameExtension = new Controls.TextBoxData();
            documentNameSuffix = new Controls.TextBoxData();
            previewData = new TextBox();
            previewDocumentXML = new DataGridView();
            documentElementColumn = new DataGridViewTextBoxColumn();
            documentFileName = new DataGridViewTextBoxColumn();
            transformTab = new TabPage();
            transformCommands = new ToolStrip();
            transformFormat = new ToolStripButton();
            transformRenderXML = new ToolStripButton();
            transformRenderText = new ToolStripButton();
            transformScriptData = new TextBox();
            transformException = new Controls.TextBoxData();
            documentTab = new TabPage();
            documentData = new DataGridView();
            elementNameColumn = new DataGridViewTextBoxColumn();
            documentNameColumn = new DataGridViewTextBoxColumn();
            textBoxData1 = new Controls.TextBoxData();
            documentPreview = new TextBox();
            documentCommands = new ToolStrip();
            documentsBuild = new ToolStripButton();
            documentSave = new ToolStripButton();
            documentSaveAs = new ToolStripButton();
            documentDirectory = new ToolStripTextBox();
            documentDirectorySet = new ToolStripButton();
            documentSaveAll = new ToolStripButton();
            templateCommands = new ContextMenuStrip(components);
            deleteTemplateCommand = new ToolStripMenuItem();
            folderBrowserDialog = new FolderBrowserDialog();
            saveFileDialog = new SaveFileDialog();
            templateLayout = new TableLayoutPanel();
            elementLayout = new TableLayoutPanel();
            elementRenderGroup = new GroupBox();
            renderAsLayout = new TableLayoutPanel();
            renderDataAsGroup = new GroupBox();
            renderDataAsLayout = new TableLayoutPanel();
            renderTypeGroup = new GroupBox();
            renderTypeLayout = new TableLayoutPanel();
            elementPathLayout = new TableLayoutPanel();
            dataPreviewLayout = new TableLayoutPanel();
            documentOptionGroup = new GroupBox();
            documentOptionLayout = new TableLayoutPanel();
            documentNameElement = new Label();
            tranformsLayout = new TableLayoutPanel();
            documentLayout = new TableLayoutPanel();
            templateLayout.SuspendLayout();
            templateTabs.SuspendLayout();
            definitionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)elementDefinitionSplit).BeginInit();
            elementDefinitionSplit.Panel1.SuspendLayout();
            elementDefinitionSplit.Panel2.SuspendLayout();
            elementDefinitionSplit.SuspendLayout();
            elementLayout.SuspendLayout();
            elementRenderGroup.SuspendLayout();
            renderAsLayout.SuspendLayout();
            renderDataAsGroup.SuspendLayout();
            renderDataAsLayout.SuspendLayout();
            renderTypeGroup.SuspendLayout();
            renderTypeLayout.SuspendLayout();
            pathTab.SuspendLayout();
            elementPathLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)selectionItemData).BeginInit();
            dataPreviewTab.SuspendLayout();
            dataPreviewLayout.SuspendLayout();
            dataPreviewCommands.SuspendLayout();
            documentOptionGroup.SuspendLayout();
            documentOptionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewDocumentXML).BeginInit();
            transformTab.SuspendLayout();
            tranformsLayout.SuspendLayout();
            transformCommands.SuspendLayout();
            documentTab.SuspendLayout();
            documentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            documentCommands.SuspendLayout();
            templateCommands.SuspendLayout();
            SuspendLayout();
            // 
            // templateLayout
            // 
            templateLayout.ColumnCount = 1;
            templateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateLayout.Controls.Add(templateTitleData, 0, 0);
            templateLayout.Controls.Add(templateDescriptionData, 0, 1);
            templateLayout.Controls.Add(templateTabs, 0, 2);
            templateLayout.Dock = DockStyle.Fill;
            templateLayout.Location = new Point(0, 25);
            templateLayout.Name = "templateLayout";
            templateLayout.RowCount = 3;
            templateLayout.RowStyles.Add(new RowStyle());
            templateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            templateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            templateLayout.Size = new Size(788, 631);
            templateLayout.TabIndex = 0;
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
            templateTitleData.Size = new Size(782, 44);
            templateTitleData.TabIndex = 0;
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
            templateDescriptionData.Size = new Size(782, 110);
            templateDescriptionData.TabIndex = 1;
            // 
            // templateTabs
            // 
            templateTabs.Controls.Add(definitionTab);
            templateTabs.Controls.Add(pathTab);
            templateTabs.Controls.Add(dataPreviewTab);
            templateTabs.Controls.Add(transformTab);
            templateTabs.Controls.Add(documentTab);
            templateTabs.Dock = DockStyle.Fill;
            templateTabs.Location = new Point(3, 169);
            templateTabs.Name = "templateTabs";
            templateTabs.SelectedIndex = 0;
            templateTabs.Size = new Size(782, 459);
            templateTabs.TabIndex = 2;
            // 
            // definitionTab
            // 
            definitionTab.BackColor = SystemColors.Control;
            definitionTab.Controls.Add(elementDefinitionSplit);
            definitionTab.Location = new Point(4, 24);
            definitionTab.Name = "definitionTab";
            definitionTab.Padding = new Padding(3);
            definitionTab.Size = new Size(774, 431);
            definitionTab.TabIndex = 0;
            definitionTab.Text = "Element Definition (XSD)";
            // 
            // elementDefinitionSplit
            // 
            elementDefinitionSplit.Dock = DockStyle.Fill;
            elementDefinitionSplit.Location = new Point(3, 3);
            elementDefinitionSplit.Name = "elementDefinitionSplit";
            // 
            // elementDefinitionSplit.Panel1
            // 
            elementDefinitionSplit.Panel1.Controls.Add(elementSelection);
            // 
            // elementDefinitionSplit.Panel2
            // 
            elementDefinitionSplit.Panel2.Controls.Add(elementLayout);
            elementDefinitionSplit.Size = new Size(768, 425);
            elementDefinitionSplit.SplitterDistance = 256;
            elementDefinitionSplit.TabIndex = 0;
            // 
            // elementSelection
            // 
            elementSelection.CheckBoxes = true;
            elementSelection.Columns.AddRange(new ColumnHeader[] { columnName });
            elementSelection.Dock = DockStyle.Fill;
            listViewGroup2.Header = "Scope Name 1";
            listViewGroup2.Name = "sampleScope";
            elementSelection.Groups.AddRange(new ListViewGroup[] { listViewGroup2 });
            listViewItem2.Group = listViewGroup2;
            listViewItem2.StateImageIndex = 0;
            elementSelection.Items.AddRange(new ListViewItem[] { listViewItem2 });
            elementSelection.Location = new Point(0, 0);
            elementSelection.MultiSelect = false;
            elementSelection.Name = "elementSelection";
            elementSelection.Size = new Size(256, 425);
            elementSelection.TabIndex = 1;
            elementSelection.UseCompatibleStateImageBehavior = false;
            elementSelection.View = View.Details;
            // 
            // columnName
            // 
            columnName.Text = "Column";
            columnName.Width = 300;
            // 
            // elementLayout
            // 
            elementLayout.ColumnCount = 2;
            elementLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            elementLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            elementLayout.Controls.Add(elementRenderGroup, 0, 3);
            elementLayout.Controls.Add(elementScopeData, 0, 0);
            elementLayout.Controls.Add(elementColumnData, 0, 1);
            elementLayout.Controls.Add(elementNameData, 0, 2);
            elementLayout.Controls.Add(renderDataAsGroup, 0, 4);
            elementLayout.Controls.Add(renderTypeGroup, 1, 3);
            elementLayout.Dock = DockStyle.Fill;
            elementLayout.Location = new Point(0, 0);
            elementLayout.Name = "elementLayout";
            elementLayout.RowCount = 7;
            elementLayout.RowStyles.Add(new RowStyle());
            elementLayout.RowStyles.Add(new RowStyle());
            elementLayout.RowStyles.Add(new RowStyle());
            elementLayout.RowStyles.Add(new RowStyle());
            elementLayout.RowStyles.Add(new RowStyle());
            elementLayout.RowStyles.Add(new RowStyle());
            elementLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            elementLayout.Size = new Size(508, 425);
            elementLayout.TabIndex = 0;
            // 
            // elementRenderGroup
            // 
            elementRenderGroup.AutoSize = true;
            elementRenderGroup.Controls.Add(renderAsLayout);
            elementRenderGroup.Dock = DockStyle.Fill;
            elementRenderGroup.Location = new Point(3, 153);
            elementRenderGroup.Name = "elementRenderGroup";
            elementRenderGroup.Size = new Size(248, 47);
            elementRenderGroup.TabIndex = 6;
            elementRenderGroup.TabStop = false;
            elementRenderGroup.Text = "render As";
            // 
            // renderAsLayout
            // 
            renderAsLayout.AutoSize = true;
            renderAsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderAsLayout.ColumnCount = 3;
            renderAsLayout.ColumnStyles.Add(new ColumnStyle());
            renderAsLayout.ColumnStyles.Add(new ColumnStyle());
            renderAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            renderAsLayout.Controls.Add(renderAsAttribute, 1, 0);
            renderAsLayout.Controls.Add(renderAsElement, 0, 0);
            renderAsLayout.Dock = DockStyle.Fill;
            renderAsLayout.Location = new Point(3, 19);
            renderAsLayout.Name = "renderAsLayout";
            renderAsLayout.RowCount = 1;
            renderAsLayout.RowStyles.Add(new RowStyle());
            renderAsLayout.Size = new Size(242, 25);
            renderAsLayout.TabIndex = 0;
            // 
            // renderAsAttribute
            // 
            renderAsAttribute.AutoSize = true;
            renderAsAttribute.Location = new Point(78, 3);
            renderAsAttribute.Name = "renderAsAttribute";
            renderAsAttribute.Size = new Size(73, 19);
            renderAsAttribute.TabIndex = 2;
            renderAsAttribute.Text = "Attribute";
            renderAsAttribute.UseVisualStyleBackColor = true;
            // 
            // renderAsElement
            // 
            renderAsElement.AutoSize = true;
            renderAsElement.Location = new Point(3, 3);
            renderAsElement.Name = "renderAsElement";
            renderAsElement.Size = new Size(69, 19);
            renderAsElement.TabIndex = 3;
            renderAsElement.Text = "Element";
            renderAsElement.UseVisualStyleBackColor = true;
            // 
            // elementScopeData
            // 
            elementScopeData.AutoSize = true;
            elementLayout.SetColumnSpan(elementScopeData, 2);
            elementScopeData.Dock = DockStyle.Fill;
            elementScopeData.HeaderText = "Scope";
            elementScopeData.Location = new Point(3, 3);
            elementScopeData.Multiline = false;
            elementScopeData.Name = "elementScopeData";
            elementScopeData.ReadOnly = true;
            elementScopeData.Size = new Size(502, 44);
            elementScopeData.TabIndex = 0;
            // 
            // elementColumnData
            // 
            elementColumnData.AutoSize = true;
            elementLayout.SetColumnSpan(elementColumnData, 2);
            elementColumnData.Dock = DockStyle.Fill;
            elementColumnData.HeaderText = "Column";
            elementColumnData.Location = new Point(3, 53);
            elementColumnData.Multiline = false;
            elementColumnData.Name = "elementColumnData";
            elementColumnData.ReadOnly = true;
            elementColumnData.Size = new Size(502, 44);
            elementColumnData.TabIndex = 1;
            // 
            // elementNameData
            // 
            elementNameData.AutoSize = true;
            elementLayout.SetColumnSpan(elementNameData, 2);
            elementNameData.Dock = DockStyle.Fill;
            elementNameData.HeaderText = "Element Name";
            elementNameData.Location = new Point(3, 103);
            elementNameData.Multiline = false;
            elementNameData.Name = "elementNameData";
            elementNameData.ReadOnly = false;
            elementNameData.Size = new Size(502, 44);
            elementNameData.TabIndex = 2;
            // 
            // renderDataAsGroup
            // 
            renderDataAsGroup.AutoSize = true;
            renderDataAsGroup.Controls.Add(renderDataAsLayout);
            renderDataAsGroup.Dock = DockStyle.Fill;
            renderDataAsGroup.Location = new Point(3, 206);
            renderDataAsGroup.Name = "renderDataAsGroup";
            renderDataAsGroup.Size = new Size(248, 47);
            renderDataAsGroup.TabIndex = 7;
            renderDataAsGroup.TabStop = false;
            renderDataAsGroup.Text = "render Data As";
            // 
            // renderDataAsLayout
            // 
            renderDataAsLayout.AutoSize = true;
            renderDataAsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            renderDataAsLayout.ColumnCount = 3;
            renderDataAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            renderDataAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            renderDataAsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            renderDataAsLayout.Controls.Add(renderDataAsText, 0, 0);
            renderDataAsLayout.Controls.Add(renderDataAsCData, 1, 0);
            renderDataAsLayout.Controls.Add(renderDataAsXml, 2, 0);
            renderDataAsLayout.Dock = DockStyle.Fill;
            renderDataAsLayout.Location = new Point(3, 19);
            renderDataAsLayout.Name = "renderDataAsLayout";
            renderDataAsLayout.RowCount = 1;
            renderDataAsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            renderDataAsLayout.Size = new Size(242, 25);
            renderDataAsLayout.TabIndex = 0;
            // 
            // renderDataAsText
            // 
            renderDataAsText.AutoSize = true;
            renderDataAsText.Location = new Point(3, 3);
            renderDataAsText.Name = "renderDataAsText";
            renderDataAsText.Size = new Size(47, 19);
            renderDataAsText.TabIndex = 0;
            renderDataAsText.Text = "Text";
            renderDataAsText.UseVisualStyleBackColor = true;
            // 
            // renderDataAsCData
            // 
            renderDataAsCData.AutoSize = true;
            renderDataAsCData.Location = new Point(83, 3);
            renderDataAsCData.Name = "renderDataAsCData";
            renderDataAsCData.Size = new Size(58, 19);
            renderDataAsCData.TabIndex = 1;
            renderDataAsCData.Text = "CData";
            renderDataAsCData.UseVisualStyleBackColor = true;
            // 
            // renderDataAsXml
            // 
            renderDataAsXml.AutoSize = true;
            renderDataAsXml.Location = new Point(163, 3);
            renderDataAsXml.Name = "renderDataAsXml";
            renderDataAsXml.Size = new Size(50, 19);
            renderDataAsXml.TabIndex = 2;
            renderDataAsXml.Text = "XML";
            renderDataAsXml.UseVisualStyleBackColor = true;
            // 
            // renderTypeGroup
            // 
            renderTypeGroup.Controls.Add(renderTypeLayout);
            renderTypeGroup.Dock = DockStyle.Fill;
            renderTypeGroup.Location = new Point(257, 153);
            renderTypeGroup.Name = "renderTypeGroup";
            elementLayout.SetRowSpan(renderTypeGroup, 2);
            renderTypeGroup.Size = new Size(248, 100);
            renderTypeGroup.TabIndex = 8;
            renderTypeGroup.TabStop = false;
            renderTypeGroup.Text = "render Type";
            // 
            // renderTypeLayout
            // 
            renderTypeLayout.ColumnCount = 1;
            renderTypeLayout.ColumnStyles.Add(new ColumnStyle());
            renderTypeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            renderTypeLayout.Controls.Add(renderIsTypeData, 0, 0);
            renderTypeLayout.Controls.Add(elementTypeData, 0, 1);
            renderTypeLayout.Dock = DockStyle.Fill;
            renderTypeLayout.Location = new Point(3, 19);
            renderTypeLayout.Name = "renderTypeLayout";
            renderTypeLayout.RowCount = 2;
            renderTypeLayout.RowStyles.Add(new RowStyle());
            renderTypeLayout.RowStyles.Add(new RowStyle());
            renderTypeLayout.Size = new Size(242, 78);
            renderTypeLayout.TabIndex = 0;
            // 
            // renderIsTypeData
            // 
            renderIsTypeData.AutoSize = true;
            renderIsTypeData.Location = new Point(3, 3);
            renderIsTypeData.Name = "renderIsTypeData";
            renderIsTypeData.Size = new Size(87, 19);
            renderIsTypeData.TabIndex = 0;
            renderIsTypeData.Text = "is Rendered";
            renderIsTypeData.UseVisualStyleBackColor = true;
            // 
            // elementTypeData
            // 
            elementTypeData.AutoSize = true;
            elementTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            elementTypeData.Dock = DockStyle.Fill;
            elementTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            elementTypeData.HeaderText = "render Type";
            elementTypeData.Location = new Point(3, 28);
            elementTypeData.Name = "elementTypeData";
            elementTypeData.ReadOnly = false;
            elementTypeData.Size = new Size(236, 47);
            elementTypeData.TabIndex = 4;
            // 
            // pathTab
            // 
            pathTab.BackColor = SystemColors.Control;
            pathTab.Controls.Add(elementPathLayout);
            pathTab.Location = new Point(4, 24);
            pathTab.Name = "pathTab";
            pathTab.Padding = new Padding(3);
            pathTab.Size = new Size(774, 431);
            pathTab.TabIndex = 1;
            pathTab.Text = "Element Paths";
            // 
            // elementPathLayout
            // 
            elementPathLayout.ColumnCount = 1;
            elementPathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            elementPathLayout.Controls.Add(namedScopeData, 0, 1);
            elementPathLayout.Controls.Add(selectionItemData, 0, 0);
            elementPathLayout.Dock = DockStyle.Fill;
            elementPathLayout.Location = new Point(3, 3);
            elementPathLayout.Name = "elementPathLayout";
            elementPathLayout.RowCount = 2;
            elementPathLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            elementPathLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            elementPathLayout.Size = new Size(768, 425);
            elementPathLayout.TabIndex = 0;
            // 
            // namedScopeData
            // 
            namedScopeData.ApplyImage = Properties.Resources.NewXPath;
            namedScopeData.ApplyText = "apply";
            namedScopeData.Dock = DockStyle.Fill;
            namedScopeData.HeaderText = "Selected Item";
            namedScopeData.Location = new Point(3, 215);
            namedScopeData.Name = "namedScopeData";
            namedScopeData.ReadOnly = false;
            namedScopeData.Scope = DataLayer.ApplicationData.Scope.ScopeType.Null;
            namedScopeData.ScopePath = namedScopePath2;
            namedScopeData.Size = new Size(762, 207);
            namedScopeData.TabIndex = 6;
            // 
            // selectionItemData
            // 
            selectionItemData.AllowUserToAddRows = false;
            selectionItemData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            selectionItemData.Columns.AddRange(new DataGridViewColumn[] { scopeNameData, selectionMemberData });
            selectionItemData.Dock = DockStyle.Fill;
            selectionItemData.Location = new Point(3, 3);
            selectionItemData.Name = "selectionItemData";
            selectionItemData.Size = new Size(762, 206);
            selectionItemData.TabIndex = 5;
            // 
            // scopeNameData
            // 
            scopeNameData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            scopeNameData.DataPropertyName = "Scope";
            scopeNameData.FillWeight = 50F;
            scopeNameData.HeaderText = "Scope";
            scopeNameData.Name = "scopeNameData";
            // 
            // selectionMemberData
            // 
            selectionMemberData.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            selectionMemberData.DataPropertyName = "SelectionPath";
            selectionMemberData.HeaderText = "Path";
            selectionMemberData.Name = "selectionMemberData";
            // 
            // dataPreviewTab
            // 
            dataPreviewTab.BackColor = SystemColors.Control;
            dataPreviewTab.Controls.Add(dataPreviewLayout);
            dataPreviewTab.Location = new Point(4, 24);
            dataPreviewTab.Name = "dataPreviewTab";
            dataPreviewTab.Size = new Size(774, 431);
            dataPreviewTab.TabIndex = 4;
            dataPreviewTab.Text = "Data Preview";
            // 
            // dataPreviewLayout
            // 
            dataPreviewLayout.ColumnCount = 1;
            dataPreviewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dataPreviewLayout.Controls.Add(dataPreviewCommands, 0, 0);
            dataPreviewLayout.Controls.Add(documentOptionGroup, 0, 1);
            dataPreviewLayout.Controls.Add(previewData, 0, 3);
            dataPreviewLayout.Controls.Add(previewDocumentXML, 0, 2);
            dataPreviewLayout.Dock = DockStyle.Fill;
            dataPreviewLayout.Location = new Point(0, 0);
            dataPreviewLayout.Name = "dataPreviewLayout";
            dataPreviewLayout.RowCount = 4;
            dataPreviewLayout.RowStyles.Add(new RowStyle());
            dataPreviewLayout.RowStyles.Add(new RowStyle());
            dataPreviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            dataPreviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            dataPreviewLayout.Size = new Size(774, 431);
            dataPreviewLayout.TabIndex = 0;
            // 
            // dataPreviewCommands
            // 
            dataPreviewCommands.GripStyle = ToolStripGripStyle.Hidden;
            dataPreviewCommands.Items.AddRange(new ToolStripItem[] { dataPrevieweBuild, dataPreviewSave });
            dataPreviewCommands.Location = new Point(0, 0);
            dataPreviewCommands.Name = "dataPreviewCommands";
            dataPreviewCommands.Size = new Size(774, 25);
            dataPreviewCommands.TabIndex = 0;
            dataPreviewCommands.Text = "Data Preview Tools";
            // 
            // dataPrevieweBuild
            // 
            dataPrevieweBuild.DisplayStyle = ToolStripItemDisplayStyle.Image;
            dataPrevieweBuild.Image = Properties.Resources.BuildDefinition;
            dataPrevieweBuild.ImageTransparentColor = Color.Magenta;
            dataPrevieweBuild.Name = "dataPrevieweBuild";
            dataPrevieweBuild.Size = new Size(23, 22);
            dataPrevieweBuild.Text = "Build";
            dataPrevieweBuild.ToolTipText = "Build Data Preview";
            dataPrevieweBuild.Click += dataPrevieweBuild_Click;
            // 
            // dataPreviewSave
            // 
            dataPreviewSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            dataPreviewSave.Image = Properties.Resources.SaveXmlFile;
            dataPreviewSave.ImageTransparentColor = Color.Magenta;
            dataPreviewSave.Name = "dataPreviewSave";
            dataPreviewSave.Size = new Size(23, 22);
            dataPreviewSave.Text = "Save Preview";
            dataPreviewSave.Click += dataPreviewSave_Click;
            // 
            // documentOptionGroup
            // 
            documentOptionGroup.AutoSize = true;
            documentOptionGroup.Controls.Add(documentOptionLayout);
            documentOptionGroup.Dock = DockStyle.Fill;
            documentOptionGroup.Location = new Point(3, 28);
            documentOptionGroup.Name = "documentOptionGroup";
            documentOptionGroup.Size = new Size(768, 122);
            documentOptionGroup.TabIndex = 9;
            documentOptionGroup.TabStop = false;
            documentOptionGroup.Text = "Document Option";
            // 
            // documentOptionLayout
            // 
            documentOptionLayout.AutoSize = true;
            documentOptionLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentOptionLayout.ColumnCount = 4;
            documentOptionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            documentOptionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            documentOptionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            documentOptionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            documentOptionLayout.Controls.Add(documentBreakData, 0, 0);
            documentOptionLayout.Controls.Add(documentScopeData, 1, 0);
            documentOptionLayout.Controls.Add(documentNamePrefixData, 0, 1);
            documentOptionLayout.Controls.Add(documentXmlExtension, 3, 0);
            documentOptionLayout.Controls.Add(documentNameExtension, 3, 1);
            documentOptionLayout.Controls.Add(documentNameSuffix, 2, 1);
            documentOptionLayout.Controls.Add(documentNameElement, 1, 1);
            documentOptionLayout.Dock = DockStyle.Fill;
            documentOptionLayout.Location = new Point(3, 19);
            documentOptionLayout.Name = "documentOptionLayout";
            documentOptionLayout.RowCount = 2;
            documentOptionLayout.RowStyles.Add(new RowStyle());
            documentOptionLayout.RowStyles.Add(new RowStyle());
            documentOptionLayout.Size = new Size(762, 100);
            documentOptionLayout.TabIndex = 0;
            // 
            // documentBreakData
            // 
            documentBreakData.AutoSize = true;
            documentBreakData.Location = new Point(3, 3);
            documentBreakData.Name = "documentBreakData";
            documentBreakData.Size = new Size(114, 19);
            documentBreakData.TabIndex = 0;
            documentBreakData.Text = "Document Break";
            documentBreakData.UseVisualStyleBackColor = true;
            // 
            // documentScopeData
            // 
            documentScopeData.AutoSize = true;
            documentScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentOptionLayout.SetColumnSpan(documentScopeData, 2);
            documentScopeData.Dock = DockStyle.Fill;
            documentScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            documentScopeData.HeaderText = "Break on Scope";
            documentScopeData.Location = new Point(193, 3);
            documentScopeData.Name = "documentScopeData";
            documentScopeData.ReadOnly = false;
            documentScopeData.Size = new Size(412, 44);
            documentScopeData.TabIndex = 6;
            // 
            // documentNamePrefixData
            // 
            documentNamePrefixData.AutoSize = true;
            documentNamePrefixData.Dock = DockStyle.Fill;
            documentNamePrefixData.HeaderText = "File Prefix";
            documentNamePrefixData.Location = new Point(3, 53);
            documentNamePrefixData.Multiline = false;
            documentNamePrefixData.Name = "documentNamePrefixData";
            documentNamePrefixData.ReadOnly = false;
            documentNamePrefixData.Size = new Size(184, 44);
            documentNamePrefixData.TabIndex = 1;
            // 
            // documentXmlExtension
            // 
            documentXmlExtension.AutoSize = true;
            documentXmlExtension.Dock = DockStyle.Fill;
            documentXmlExtension.HeaderText = "XML Extension";
            documentXmlExtension.Location = new Point(611, 3);
            documentXmlExtension.Multiline = false;
            documentXmlExtension.Name = "documentXmlExtension";
            documentXmlExtension.ReadOnly = false;
            documentXmlExtension.Size = new Size(148, 44);
            documentXmlExtension.TabIndex = 7;
            // 
            // documentNameExtension
            // 
            documentNameExtension.AutoSize = true;
            documentNameExtension.Dock = DockStyle.Fill;
            documentNameExtension.HeaderText = "Document Extension";
            documentNameExtension.Location = new Point(611, 53);
            documentNameExtension.Multiline = false;
            documentNameExtension.Name = "documentNameExtension";
            documentNameExtension.ReadOnly = false;
            documentNameExtension.Size = new Size(148, 44);
            documentNameExtension.TabIndex = 4;
            // 
            // documentNameSuffix
            // 
            documentNameSuffix.AutoSize = true;
            documentNameSuffix.Dock = DockStyle.Fill;
            documentNameSuffix.HeaderText = "File Suffix";
            documentNameSuffix.Location = new Point(421, 53);
            documentNameSuffix.Multiline = false;
            documentNameSuffix.Name = "documentNameSuffix";
            documentNameSuffix.ReadOnly = false;
            documentNameSuffix.Size = new Size(184, 44);
            documentNameSuffix.TabIndex = 3;
            // 
            // documentNameElement
            // 
            documentNameElement.Anchor = AnchorStyles.None;
            documentNameElement.AutoSize = true;
            documentNameElement.Location = new Point(253, 67);
            documentNameElement.Name = "documentNameElement";
            documentNameElement.Size = new Size(101, 15);
            documentNameElement.TabIndex = 8;
            documentNameElement.Text = "<Element Name>";
            // 
            // previewData
            // 
            previewData.Dock = DockStyle.Fill;
            previewData.Location = new Point(3, 267);
            previewData.Multiline = true;
            previewData.Name = "previewData";
            previewData.ReadOnly = true;
            previewData.ScrollBars = ScrollBars.Both;
            previewData.Size = new Size(768, 161);
            previewData.TabIndex = 1;
            previewData.WordWrap = false;
            // 
            // previewDocumentXML
            // 
            previewDocumentXML.AllowUserToAddRows = false;
            previewDocumentXML.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previewDocumentXML.Columns.AddRange(new DataGridViewColumn[] { documentElementColumn, documentFileName });
            previewDocumentXML.Dock = DockStyle.Fill;
            previewDocumentXML.Location = new Point(3, 156);
            previewDocumentXML.Name = "previewDocumentXML";
            previewDocumentXML.Size = new Size(768, 105);
            previewDocumentXML.TabIndex = 10;
            // 
            // documentElementColumn
            // 
            documentElementColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentElementColumn.FillWeight = 50F;
            documentElementColumn.HeaderText = "Element Name";
            documentElementColumn.Name = "documentElementColumn";
            // 
            // documentFileName
            // 
            documentFileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentFileName.HeaderText = "Document File Name";
            documentFileName.Name = "documentFileName";
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(tranformsLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Size = new Size(774, 431);
            transformTab.TabIndex = 2;
            transformTab.Text = "Transform (XSL)";
            // 
            // tranformsLayout
            // 
            tranformsLayout.ColumnCount = 1;
            tranformsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tranformsLayout.Controls.Add(transformCommands, 0, 0);
            tranformsLayout.Controls.Add(transformScriptData, 0, 1);
            tranformsLayout.Controls.Add(transformException, 0, 2);
            tranformsLayout.Dock = DockStyle.Fill;
            tranformsLayout.Location = new Point(0, 0);
            tranformsLayout.Name = "tranformsLayout";
            tranformsLayout.RowCount = 3;
            tranformsLayout.RowStyles.Add(new RowStyle());
            tranformsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tranformsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tranformsLayout.Size = new Size(774, 431);
            tranformsLayout.TabIndex = 0;
            // 
            // transformCommands
            // 
            transformCommands.GripStyle = ToolStripGripStyle.Hidden;
            transformCommands.Items.AddRange(new ToolStripItem[] { transformFormat, transformRenderXML, transformRenderText });
            transformCommands.Location = new Point(0, 0);
            transformCommands.Name = "transformCommands";
            transformCommands.Size = new Size(774, 25);
            transformCommands.TabIndex = 0;
            transformCommands.Text = "transform Tools";
            // 
            // transformFormat
            // 
            transformFormat.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformFormat.Image = Properties.Resources.XSLTransform;
            transformFormat.ImageTransparentColor = Color.Magenta;
            transformFormat.Name = "transformFormat";
            transformFormat.Size = new Size(23, 22);
            transformFormat.Text = "Build";
            transformFormat.Click += transformFormat_Click;
            // 
            // transformRenderXML
            // 
            transformRenderXML.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformRenderXML.Image = Properties.Resources.XmlFile;
            transformRenderXML.ImageTransparentColor = Color.Magenta;
            transformRenderXML.Name = "transformRenderXML";
            transformRenderXML.Size = new Size(23, 22);
            transformRenderXML.Text = "render As XML";
            transformRenderXML.Click += transformRenderXML_Click;
            // 
            // transformRenderText
            // 
            transformRenderText.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformRenderText.Image = Properties.Resources.TextFile;
            transformRenderText.ImageTransparentColor = Color.Magenta;
            transformRenderText.Name = "transformRenderText";
            transformRenderText.Size = new Size(23, 22);
            transformRenderText.Text = "render As Text";
            transformRenderText.Click += transformRenderText_Click;
            // 
            // transformScriptData
            // 
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.Location = new Point(3, 28);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.ScrollBars = ScrollBars.Both;
            transformScriptData.Size = new Size(768, 278);
            transformScriptData.TabIndex = 1;
            transformScriptData.WordWrap = false;
            // 
            // transformException
            // 
            transformException.AutoSize = true;
            transformException.Dock = DockStyle.Fill;
            transformException.HeaderText = "Exception/Warning";
            transformException.Location = new Point(3, 312);
            transformException.Multiline = true;
            transformException.Name = "transformException";
            transformException.ReadOnly = true;
            transformException.Size = new Size(768, 116);
            transformException.TabIndex = 2;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(documentLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Size = new Size(774, 431);
            documentTab.TabIndex = 3;
            documentTab.Text = "Documents";
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 1;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.Controls.Add(documentData, 0, 1);
            documentLayout.Controls.Add(textBoxData1, 0, 3);
            documentLayout.Controls.Add(documentPreview, 0, 2);
            documentLayout.Controls.Add(documentCommands, 0, 0);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 0);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 4;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            documentLayout.Size = new Size(774, 431);
            documentLayout.TabIndex = 0;
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { elementNameColumn, documentNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 28);
            documentData.Name = "documentData";
            documentData.Size = new Size(768, 115);
            documentData.TabIndex = 0;
            // 
            // elementNameColumn
            // 
            elementNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            elementNameColumn.FillWeight = 50F;
            elementNameColumn.HeaderText = "Element Name";
            elementNameColumn.Name = "elementNameColumn";
            // 
            // documentNameColumn
            // 
            documentNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentNameColumn.HeaderText = "Document Name";
            documentNameColumn.Name = "documentNameColumn";
            documentNameColumn.ReadOnly = true;
            // 
            // textBoxData1
            // 
            textBoxData1.AutoSize = true;
            textBoxData1.Dock = DockStyle.Fill;
            textBoxData1.HeaderText = "Exception/Warning";
            textBoxData1.Location = new Point(3, 352);
            textBoxData1.Multiline = true;
            textBoxData1.Name = "textBoxData1";
            textBoxData1.ReadOnly = true;
            textBoxData1.Size = new Size(768, 76);
            textBoxData1.TabIndex = 2;
            // 
            // documentPreview
            // 
            documentPreview.Dock = DockStyle.Fill;
            documentPreview.Location = new Point(3, 149);
            documentPreview.Multiline = true;
            documentPreview.Name = "documentPreview";
            documentPreview.ReadOnly = true;
            documentPreview.Size = new Size(768, 197);
            documentPreview.TabIndex = 3;
            documentPreview.WordWrap = false;
            // 
            // documentCommands
            // 
            documentCommands.GripStyle = ToolStripGripStyle.Hidden;
            documentCommands.Items.AddRange(new ToolStripItem[] { documentsBuild, documentSave, documentSaveAs, documentDirectory, documentDirectorySet, documentSaveAll });
            documentCommands.Location = new Point(0, 0);
            documentCommands.Name = "documentCommands";
            documentCommands.Size = new Size(774, 25);
            documentCommands.TabIndex = 1;
            documentCommands.Text = "Document Tools";
            // 
            // documentsBuild
            // 
            documentsBuild.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentsBuild.Image = Properties.Resources.BuildDefinition;
            documentsBuild.ImageTransparentColor = Color.Magenta;
            documentsBuild.Name = "documentsBuild";
            documentsBuild.Size = new Size(23, 24);
            documentsBuild.Text = "Build Documents";
            documentsBuild.Click += documentsBuild_Click;
            // 
            // documentSave
            // 
            documentSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSave.Image = Properties.Resources.Save;
            documentSave.ImageTransparentColor = Color.Magenta;
            documentSave.Name = "documentSave";
            documentSave.Size = new Size(23, 24);
            documentSave.Text = "Save Document";
            documentSave.Click += documentSave_Click;
            // 
            // documentSaveAs
            // 
            documentSaveAs.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSaveAs.Image = Properties.Resources.SaveAs;
            documentSaveAs.ImageTransparentColor = Color.Magenta;
            documentSaveAs.Name = "documentSaveAs";
            documentSaveAs.Size = new Size(23, 24);
            documentSaveAs.Text = "Save As";
            documentSaveAs.Click += documentSaveAs_Click;
            // 
            // documentDirectory
            // 
            documentDirectory.AutoSize = false;
            documentDirectory.Name = "documentDirectory";
            documentDirectory.ReadOnly = true;
            documentDirectory.Size = new Size(400, 25);
            documentDirectory.Text = "{current file path or last path used?}";
            // 
            // documentDirectorySet
            // 
            documentDirectorySet.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentDirectorySet.Image = Properties.Resources.FolderOpened;
            documentDirectorySet.ImageTransparentColor = Color.Magenta;
            documentDirectorySet.Name = "documentDirectorySet";
            documentDirectorySet.Size = new Size(23, 24);
            documentDirectorySet.Text = "Set Document Directory";
            documentDirectorySet.Click += documentDirectorySet_Click;
            // 
            // documentSaveAll
            // 
            documentSaveAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSaveAll.Image = Properties.Resources.SaveAll;
            documentSaveAll.ImageTransparentColor = Color.Magenta;
            documentSaveAll.Name = "documentSaveAll";
            documentSaveAll.Size = new Size(23, 24);
            documentSaveAll.Text = "Save All";
            documentSaveAll.Click += documentSaveAll_Click;
            // 
            // templateCommands
            // 
            templateCommands.Items.AddRange(new ToolStripItem[] { deleteTemplateCommand });
            templateCommands.Name = "templateCommands";
            templateCommands.Size = new Size(159, 26);
            // 
            // deleteTemplateCommand
            // 
            deleteTemplateCommand.Image = Properties.Resources.DeleteXMLSchema;
            deleteTemplateCommand.Name = "deleteTemplateCommand";
            deleteTemplateCommand.Size = new Size(158, 22);
            deleteTemplateCommand.Text = "Delete Template";
            deleteTemplateCommand.Click += deleteTemplateCommand_Click;
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(788, 656);
            Controls.Add(templateLayout);
            Name = "Template";
            Text = "Template";
            Load += Template_Load;
            Controls.SetChildIndex(templateLayout, 0);
            templateLayout.ResumeLayout(false);
            templateLayout.PerformLayout();
            templateTabs.ResumeLayout(false);
            definitionTab.ResumeLayout(false);
            elementDefinitionSplit.Panel1.ResumeLayout(false);
            elementDefinitionSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)elementDefinitionSplit).EndInit();
            elementDefinitionSplit.ResumeLayout(false);
            elementLayout.ResumeLayout(false);
            elementLayout.PerformLayout();
            elementRenderGroup.ResumeLayout(false);
            elementRenderGroup.PerformLayout();
            renderAsLayout.ResumeLayout(false);
            renderAsLayout.PerformLayout();
            renderDataAsGroup.ResumeLayout(false);
            renderDataAsGroup.PerformLayout();
            renderDataAsLayout.ResumeLayout(false);
            renderDataAsLayout.PerformLayout();
            renderTypeGroup.ResumeLayout(false);
            renderTypeLayout.ResumeLayout(false);
            renderTypeLayout.PerformLayout();
            pathTab.ResumeLayout(false);
            elementPathLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)selectionItemData).EndInit();
            dataPreviewTab.ResumeLayout(false);
            dataPreviewLayout.ResumeLayout(false);
            dataPreviewLayout.PerformLayout();
            dataPreviewCommands.ResumeLayout(false);
            dataPreviewCommands.PerformLayout();
            documentOptionGroup.ResumeLayout(false);
            documentOptionGroup.PerformLayout();
            documentOptionLayout.ResumeLayout(false);
            documentOptionLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewDocumentXML).EndInit();
            transformTab.ResumeLayout(false);
            tranformsLayout.ResumeLayout(false);
            tranformsLayout.PerformLayout();
            transformCommands.ResumeLayout(false);
            transformCommands.PerformLayout();
            documentTab.ResumeLayout(false);
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            documentCommands.ResumeLayout(false);
            documentCommands.PerformLayout();
            templateCommands.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel templateLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private TabControl templateTabs;
        private TabPage definitionTab;
        private TabPage pathTab;
        private TabPage transformTab;
        private TabPage documentTab;
        private TabPage dataPreviewTab;
        private SplitContainer elementDefinitionSplit;
        private Controls.TextBoxData elementScopeData;
        private Controls.TextBoxData elementColumnData;
        private Controls.TextBoxData elementNameData;
        private CheckBox renderAsAttribute;
        private CheckBox renderAsElement;
        private CheckBox renderDataAsText;
        private CheckBox renderDataAsCData;
        private CheckBox renderDataAsXml;
        private GroupBox renderTypeGroup;
        private TableLayoutPanel renderTypeLayout;
        private CheckBox renderIsTypeData;
        private Controls.ComboBoxData elementTypeData;
        private ListView elementSelection;
        private ColumnHeader columnName;
        private TableLayoutPanel elementPathLayout;
        private DataGridView selectionItemData;
        private Controls.NamedScopeData namedScopeData;
        private ToolStrip dataPreviewCommands;
        private ToolStripButton dataPrevieweBuild;
        private ToolStripButton dataPreviewSave;
        private TextBox previewData;
        private TableLayoutPanel documentOptionLayout;
        private CheckBox documentBreakData;
        private TableLayoutPanel tranformsLayout;
        private ToolStrip transformCommands;
        private ToolStripButton transformFormat;
        private ToolStripButton transformRenderXML;
        private ToolStripButton transformRenderText;
        private TextBox transformScriptData;
        private Controls.TextBoxData transformException;
        private TableLayoutPanel documentLayout;
        private DataGridView documentData;
        private ToolStrip documentCommands;
        private Controls.TextBoxData textBoxData1;
        private TextBox documentPreview;
        private ToolStripButton documentsBuild;
        private ToolStripButton documentSave;
        private ToolStripButton documentSaveAs;
        private ToolStripButton documentSaveAll;
        private ContextMenuStrip templateCommands;
        private ToolStripMenuItem deleteTemplateCommand;
        private ToolStripTextBox documentDirectory;
        private FolderBrowserDialog folderBrowserDialog;
        private SaveFileDialog saveFileDialog;
        private ToolStripButton documentDirectorySet;
        private DataGridViewTextBoxColumn elementNameColumn;
        private DataGridViewTextBoxColumn documentNameColumn;
        private DataGridViewTextBoxColumn scopeNameData;
        private DataGridViewTextBoxColumn selectionMemberData;
        private Controls.ComboBoxData documentScopeData;
        private DataGridView previewDocumentXML;
        private Controls.TextBoxData documentXmlExtension;
        private Controls.TextBoxData documentNamePrefixData;
        private Controls.TextBoxData documentNameExtension;
        private Controls.TextBoxData documentNameSuffix;
        private DataGridViewTextBoxColumn documentElementColumn;
        private DataGridViewTextBoxColumn documentFileName;
    }
}