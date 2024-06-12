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
            ListViewGroup listViewGroup1 = new ListViewGroup("Scope Name 1", HorizontalAlignment.Left);
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "Column Name 1", "Column 1" }, -1);
            TableLayoutPanel elementLayout;
            GroupBox elementRenderGroup;
            TableLayoutPanel renderAsLayout;
            GroupBox renderDataAsGroup;
            TableLayoutPanel renderDataAsLayout;
            GroupBox renderTypeGroup;
            TableLayoutPanel renderTypeLayout;
            TableLayoutPanel elementPathLayout;
            BusinessLayer.NamedScope.NamedScopePath namedScopePath1 = new BusinessLayer.NamedScope.NamedScopePath();
            Label scriptLabel;
            TableLayoutPanel dataPreviewLayout;
            TableLayoutPanel tranformsLayout;
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
            documentOptionTab = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            solutionDirectoryData = new Controls.TextBoxData();
            tableLayoutPanel2 = new TableLayoutPanel();
            comboBox1 = new ComboBox();
            documentBreakData = new CheckBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            scriptDirectoryData = new Controls.TextBoxData();
            relativeSolutionPath = new Controls.TextBoxData();
            solutionFolderCommand = new Button();
            scriptDirectoryCommand = new Button();
            scriptPrefixData = new Controls.TextBoxData();
            scriptSuffixData = new Controls.TextBoxData();
            scriptExtensionData = new Controls.TextBoxData();
            documentExtensionData = new Controls.TextBoxData();
            documentSuffixData = new Controls.TextBoxData();
            documentLabel = new Label();
            documentPrefixData = new Controls.TextBoxData();
            documentFolderCommand = new Button();
            documentDirectoryData = new Controls.TextBoxData();
            dataPreviewTab = new TabPage();
            dataPreviewCommands = new ToolStrip();
            dataPrevieweBuild = new ToolStripButton();
            dataPreviewSave = new ToolStripButton();
            previewDocumentXML = new DataGridView();
            documentElementColumn = new DataGridViewTextBoxColumn();
            documentFileName = new DataGridViewTextBoxColumn();
            previewData = new TextBox();
            transformTab = new TabPage();
            transformCommands = new ToolStrip();
            transformFormat = new ToolStripButton();
            transformRenderXML = new ToolStripButton();
            transformRenderText = new ToolStripButton();
            transformScriptData = new TextBox();
            transformException = new Controls.TextBoxData();
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
            scriptLabel = new Label();
            dataPreviewLayout = new TableLayoutPanel();
            tranformsLayout = new TableLayoutPanel();
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
            documentOptionTab.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            dataPreviewTab.SuspendLayout();
            dataPreviewLayout.SuspendLayout();
            dataPreviewCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewDocumentXML).BeginInit();
            transformTab.SuspendLayout();
            tranformsLayout.SuspendLayout();
            transformCommands.SuspendLayout();
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
            templateTabs.Controls.Add(documentOptionTab);
            templateTabs.Controls.Add(dataPreviewTab);
            templateTabs.Controls.Add(transformTab);
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
            listViewGroup1.Header = "Scope Name 1";
            listViewGroup1.Name = "sampleScope";
            elementSelection.Groups.AddRange(new ListViewGroup[] { listViewGroup1 });
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            elementSelection.Items.AddRange(new ListViewItem[] { listViewItem1 });
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
            pathTab.Size = new Size(192, 72);
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
            elementPathLayout.Size = new Size(186, 66);
            elementPathLayout.TabIndex = 0;
            // 
            // namedScopeData
            // 
            namedScopeData.ApplyImage = Properties.Resources.NewXPath;
            namedScopeData.ApplyText = "apply";
            namedScopeData.Dock = DockStyle.Fill;
            namedScopeData.HeaderText = "Selected Item";
            namedScopeData.Location = new Point(3, 36);
            namedScopeData.Name = "namedScopeData";
            namedScopeData.ReadOnly = false;
            namedScopeData.Scope = DataLayer.ApplicationData.Scope.ScopeType.Null;
            namedScopeData.ScopePath = namedScopePath1;
            namedScopeData.Size = new Size(180, 27);
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
            selectionItemData.Size = new Size(180, 27);
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
            // documentOptionTab
            // 
            documentOptionTab.BackColor = SystemColors.Control;
            documentOptionTab.Controls.Add(tableLayoutPanel1);
            documentOptionTab.Location = new Point(4, 24);
            documentOptionTab.Name = "documentOptionTab";
            documentOptionTab.Padding = new Padding(3);
            documentOptionTab.Size = new Size(192, 72);
            documentOptionTab.TabIndex = 5;
            documentOptionTab.Text = "Document Options";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(solutionDirectoryData, 3, 1);
            tableLayoutPanel1.Controls.Add(scriptDirectoryData, 0, 3);
            tableLayoutPanel1.Controls.Add(relativeSolutionPath, 3, 0);
            tableLayoutPanel1.Controls.Add(solutionFolderCommand, 6, 0);
            tableLayoutPanel1.Controls.Add(scriptDirectoryCommand, 1, 3);
            tableLayoutPanel1.Controls.Add(scriptPrefixData, 2, 3);
            tableLayoutPanel1.Controls.Add(scriptLabel, 3, 3);
            tableLayoutPanel1.Controls.Add(scriptSuffixData, 4, 3);
            tableLayoutPanel1.Controls.Add(scriptExtensionData, 5, 3);
            tableLayoutPanel1.Controls.Add(documentExtensionData, 5, 2);
            tableLayoutPanel1.Controls.Add(documentSuffixData, 4, 2);
            tableLayoutPanel1.Controls.Add(documentLabel, 3, 2);
            tableLayoutPanel1.Controls.Add(documentPrefixData, 2, 2);
            tableLayoutPanel1.Controls.Add(documentFolderCommand, 1, 2);
            tableLayoutPanel1.Controls.Add(documentDirectoryData, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(186, 66);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // solutionDirectoryData
            // 
            solutionDirectoryData.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(solutionDirectoryData, 3);
            solutionDirectoryData.Dock = DockStyle.Fill;
            solutionDirectoryData.HeaderText = "Solution Folder";
            solutionDirectoryData.Location = new Point(102, 53);
            solutionDirectoryData.Multiline = false;
            solutionDirectoryData.Name = "solutionDirectoryData";
            solutionDirectoryData.ReadOnly = false;
            solutionDirectoryData.Size = new Size(52, 50);
            solutionDirectoryData.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 3);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(documentBreakData, 0, 0);
            tableLayoutPanel2.Controls.Add(comboBox1, 0, 1);
            tableLayoutPanel2.Controls.Add(checkBox1, 1, 0);
            tableLayoutPanel2.Controls.Add(checkBox2, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(93, 100);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // comboBox1
            // 
            tableLayoutPanel2.SetColumnSpan(comboBox1, 3);
            comboBox1.Dock = DockStyle.Fill;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(3, 28);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(87, 23);
            comboBox1.TabIndex = 1;
            // 
            // documentBreakData
            // 
            documentBreakData.AutoSize = true;
            documentBreakData.Location = new Point(3, 3);
            documentBreakData.Name = "documentBreakData";
            documentBreakData.Size = new Size(107, 19);
            documentBreakData.TabIndex = 0;
            documentBreakData.Text = "Break on Scope";
            documentBreakData.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(116, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(96, 19);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "script as XML";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(218, 3);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(1, 19);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "script as Text";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // scriptDirectoryData
            // 
            scriptDirectoryData.AutoSize = true;
            scriptDirectoryData.Dock = DockStyle.Fill;
            scriptDirectoryData.HeaderText = "Script Location";
            scriptDirectoryData.Location = new Point(3, 274);
            scriptDirectoryData.Multiline = false;
            scriptDirectoryData.Name = "scriptDirectoryData";
            scriptDirectoryData.ReadOnly = false;
            scriptDirectoryData.Size = new Size(46, 159);
            scriptDirectoryData.TabIndex = 4;
            // 
            // relativeSolutionPath
            // 
            relativeSolutionPath.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(relativeSolutionPath, 3);
            relativeSolutionPath.Dock = DockStyle.Fill;
            relativeSolutionPath.HeaderText = "Relative Path";
            relativeSolutionPath.Location = new Point(102, 3);
            relativeSolutionPath.Multiline = false;
            relativeSolutionPath.Name = "relativeSolutionPath";
            relativeSolutionPath.ReadOnly = true;
            relativeSolutionPath.Size = new Size(52, 44);
            relativeSolutionPath.TabIndex = 2;
            // 
            // solutionFolderCommand
            // 
            solutionFolderCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            solutionFolderCommand.AutoSize = true;
            solutionFolderCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            solutionFolderCommand.Image = Properties.Resources.FolderOpened;
            solutionFolderCommand.Location = new Point(160, 25);
            solutionFolderCommand.Name = "solutionFolderCommand";
            solutionFolderCommand.Size = new Size(22, 22);
            solutionFolderCommand.TabIndex = 1;
            solutionFolderCommand.TextImageRelation = TextImageRelation.ImageBeforeText;
            solutionFolderCommand.UseVisualStyleBackColor = true;
            // 
            // scriptDirectoryCommand
            // 
            scriptDirectoryCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            scriptDirectoryCommand.AutoSize = true;
            scriptDirectoryCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scriptDirectoryCommand.Image = Properties.Resources.FolderOpened;
            scriptDirectoryCommand.Location = new Point(55, 411);
            scriptDirectoryCommand.Name = "scriptDirectoryCommand";
            scriptDirectoryCommand.Size = new Size(22, 22);
            scriptDirectoryCommand.TabIndex = 9;
            scriptDirectoryCommand.UseVisualStyleBackColor = true;
            // 
            // scriptPrefixData
            // 
            scriptPrefixData.AutoSize = true;
            scriptPrefixData.Dock = DockStyle.Fill;
            scriptPrefixData.HeaderText = "Prefix";
            scriptPrefixData.Location = new Point(83, 274);
            scriptPrefixData.Multiline = false;
            scriptPrefixData.Name = "scriptPrefixData";
            scriptPrefixData.ReadOnly = false;
            scriptPrefixData.Size = new Size(13, 159);
            scriptPrefixData.TabIndex = 5;
            // 
            // scriptLabel
            // 
            scriptLabel.Anchor = AnchorStyles.None;
            scriptLabel.AutoSize = true;
            scriptLabel.Location = new Point(102, 271);
            scriptLabel.Name = "scriptLabel";
            scriptLabel.Size = new Size(20, 165);
            scriptLabel.TabIndex = 11;
            scriptLabel.Text = "<Element Name>";
            // 
            // scriptSuffixData
            // 
            scriptSuffixData.AutoSize = true;
            scriptSuffixData.Dock = DockStyle.Fill;
            scriptSuffixData.HeaderText = "Suffix";
            scriptSuffixData.Location = new Point(128, 274);
            scriptSuffixData.Multiline = false;
            scriptSuffixData.Name = "scriptSuffixData";
            scriptSuffixData.ReadOnly = false;
            scriptSuffixData.Size = new Size(13, 159);
            scriptSuffixData.TabIndex = 6;
            // 
            // scriptExtensionData
            // 
            scriptExtensionData.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(scriptExtensionData, 2);
            scriptExtensionData.Dock = DockStyle.Fill;
            scriptExtensionData.HeaderText = "Extension";
            scriptExtensionData.Location = new Point(147, 274);
            scriptExtensionData.Multiline = false;
            scriptExtensionData.Name = "scriptExtensionData";
            scriptExtensionData.ReadOnly = false;
            scriptExtensionData.Size = new Size(36, 159);
            scriptExtensionData.TabIndex = 7;
            // 
            // documentExtensionData
            // 
            documentExtensionData.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(documentExtensionData, 2);
            documentExtensionData.Dock = DockStyle.Fill;
            documentExtensionData.HeaderText = "Extension";
            documentExtensionData.Location = new Point(147, 109);
            documentExtensionData.Multiline = false;
            documentExtensionData.Name = "documentExtensionData";
            documentExtensionData.ReadOnly = false;
            documentExtensionData.Size = new Size(36, 159);
            documentExtensionData.TabIndex = 3;
            // 
            // documentSuffixData
            // 
            documentSuffixData.AutoSize = true;
            documentSuffixData.Dock = DockStyle.Fill;
            documentSuffixData.HeaderText = "Suffix";
            documentSuffixData.Location = new Point(128, 109);
            documentSuffixData.Multiline = false;
            documentSuffixData.Name = "documentSuffixData";
            documentSuffixData.ReadOnly = false;
            documentSuffixData.Size = new Size(13, 159);
            documentSuffixData.TabIndex = 2;
            // 
            // documentLabel
            // 
            documentLabel.Anchor = AnchorStyles.None;
            documentLabel.AutoSize = true;
            documentLabel.Location = new Point(102, 106);
            documentLabel.Name = "documentLabel";
            documentLabel.Size = new Size(20, 165);
            documentLabel.TabIndex = 10;
            documentLabel.Text = "<Element Name>";
            // 
            // documentPrefixData
            // 
            documentPrefixData.AutoSize = true;
            documentPrefixData.Dock = DockStyle.Fill;
            documentPrefixData.HeaderText = "Prefix";
            documentPrefixData.Location = new Point(83, 109);
            documentPrefixData.Multiline = false;
            documentPrefixData.Name = "documentPrefixData";
            documentPrefixData.ReadOnly = false;
            documentPrefixData.Size = new Size(13, 159);
            documentPrefixData.TabIndex = 1;
            // 
            // documentFolderCommand
            // 
            documentFolderCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            documentFolderCommand.AutoSize = true;
            documentFolderCommand.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentFolderCommand.Image = Properties.Resources.FolderOpened;
            documentFolderCommand.Location = new Point(55, 246);
            documentFolderCommand.Name = "documentFolderCommand";
            documentFolderCommand.Size = new Size(22, 22);
            documentFolderCommand.TabIndex = 8;
            documentFolderCommand.UseVisualStyleBackColor = true;
            // 
            // documentDirectoryData
            // 
            documentDirectoryData.AutoSize = true;
            documentDirectoryData.Dock = DockStyle.Fill;
            documentDirectoryData.HeaderText = "Data Location";
            documentDirectoryData.Location = new Point(3, 109);
            documentDirectoryData.Multiline = false;
            documentDirectoryData.Name = "documentDirectoryData";
            documentDirectoryData.ReadOnly = false;
            documentDirectoryData.Size = new Size(46, 159);
            documentDirectoryData.TabIndex = 0;
            // 
            // dataPreviewTab
            // 
            dataPreviewTab.BackColor = SystemColors.Control;
            dataPreviewTab.Controls.Add(dataPreviewLayout);
            dataPreviewTab.Location = new Point(4, 24);
            dataPreviewTab.Name = "dataPreviewTab";
            dataPreviewTab.Size = new Size(192, 72);
            dataPreviewTab.TabIndex = 4;
            dataPreviewTab.Text = "Data Preview (XML)";
            // 
            // dataPreviewLayout
            // 
            dataPreviewLayout.ColumnCount = 1;
            dataPreviewLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dataPreviewLayout.Controls.Add(dataPreviewCommands, 0, 0);
            dataPreviewLayout.Controls.Add(previewDocumentXML, 0, 1);
            dataPreviewLayout.Controls.Add(previewData, 0, 2);
            dataPreviewLayout.Dock = DockStyle.Fill;
            dataPreviewLayout.Location = new Point(0, 0);
            dataPreviewLayout.Name = "dataPreviewLayout";
            dataPreviewLayout.RowCount = 3;
            dataPreviewLayout.RowStyles.Add(new RowStyle());
            dataPreviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            dataPreviewLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            dataPreviewLayout.Size = new Size(192, 72);
            dataPreviewLayout.TabIndex = 0;
            // 
            // dataPreviewCommands
            // 
            dataPreviewCommands.GripStyle = ToolStripGripStyle.Hidden;
            dataPreviewCommands.Items.AddRange(new ToolStripItem[] { dataPrevieweBuild, dataPreviewSave });
            dataPreviewCommands.Location = new Point(0, 0);
            dataPreviewCommands.Name = "dataPreviewCommands";
            dataPreviewCommands.Size = new Size(192, 25);
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
            // previewDocumentXML
            // 
            previewDocumentXML.AllowUserToAddRows = false;
            previewDocumentXML.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previewDocumentXML.Columns.AddRange(new DataGridViewColumn[] { documentElementColumn, documentFileName });
            previewDocumentXML.Dock = DockStyle.Fill;
            previewDocumentXML.Location = new Point(3, 28);
            previewDocumentXML.Name = "previewDocumentXML";
            previewDocumentXML.Size = new Size(186, 8);
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
            // previewData
            // 
            previewData.Dock = DockStyle.Fill;
            previewData.Location = new Point(3, 42);
            previewData.Multiline = true;
            previewData.Name = "previewData";
            previewData.ReadOnly = true;
            previewData.ScrollBars = ScrollBars.Both;
            previewData.Size = new Size(186, 27);
            previewData.TabIndex = 1;
            previewData.WordWrap = false;
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
            // templateCommands
            // 
            templateCommands.Items.AddRange(new ToolStripItem[] { deleteTemplateCommand });
            templateCommands.Name = "templateCommands";
            templateCommands.Size = new Size(181, 48);
            // 
            // deleteTemplateCommand
            // 
            deleteTemplateCommand.Image = Properties.Resources.DeleteXMLSchema;
            deleteTemplateCommand.Name = "deleteTemplateCommand";
            deleteTemplateCommand.Size = new Size(180, 22);
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
            documentOptionTab.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            dataPreviewTab.ResumeLayout(false);
            dataPreviewLayout.ResumeLayout(false);
            dataPreviewLayout.PerformLayout();
            dataPreviewCommands.ResumeLayout(false);
            dataPreviewCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewDocumentXML).EndInit();
            transformTab.ResumeLayout(false);
            tranformsLayout.ResumeLayout(false);
            tranformsLayout.PerformLayout();
            transformCommands.ResumeLayout(false);
            transformCommands.PerformLayout();
            templateCommands.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private TabControl templateTabs;
        private TabPage definitionTab;
        private TabPage pathTab;
        private TabPage transformTab;
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
        private CheckBox renderIsTypeData;
        private Controls.ComboBoxData elementTypeData;
        private ListView elementSelection;
        private ColumnHeader columnName;
        private DataGridView selectionItemData;
        private Controls.NamedScopeData namedScopeData;
        private ToolStrip dataPreviewCommands;
        private ToolStripButton dataPrevieweBuild;
        private ToolStripButton dataPreviewSave;
        private TextBox previewData;
        private ToolStrip transformCommands;
        private ToolStripButton transformFormat;
        private ToolStripButton transformRenderXML;
        private ToolStripButton transformRenderText;
        private TextBox transformScriptData;
        private Controls.TextBoxData transformException;
        private ContextMenuStrip templateCommands;
        private ToolStripMenuItem deleteTemplateCommand;
        private FolderBrowserDialog folderBrowserDialog;
        private SaveFileDialog saveFileDialog;
        private DataGridViewTextBoxColumn scopeNameData;
        private DataGridViewTextBoxColumn selectionMemberData;
        private DataGridView previewDocumentXML;
        private DataGridViewTextBoxColumn documentElementColumn;
        private DataGridViewTextBoxColumn documentFileName;
        private TabPage documentOptionTab;
        private CheckBox documentBreakData;
        private Controls.TextBoxData solutionDirectoryData;
        private Button solutionFolderCommand;
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.TextBoxData documentDirectoryData;
        private Controls.TextBoxData documentPrefixData;
        private Controls.TextBoxData documentSuffixData;
        private Controls.TextBoxData documentExtensionData;
        private Controls.TextBoxData scriptDirectoryData;
        private Controls.TextBoxData scriptPrefixData;
        private Controls.TextBoxData scriptExtensionData;
        private Controls.TextBoxData scriptSuffixData;
        private Button documentFolderCommand;
        private Button scriptDirectoryCommand;
        private Label documentLabel;
        private ComboBox comboBox1;
        private Controls.TextBoxData relativeSolutionPath;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
    }
}