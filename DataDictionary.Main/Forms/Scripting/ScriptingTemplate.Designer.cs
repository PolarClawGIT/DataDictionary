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
            TableLayoutPanel elementPathLayout;
            BusinessLayer.NamedScope.NamedScopePath namedScopePath2 = new BusinessLayer.NamedScope.NamedScopePath();
            TableLayoutPanel transformLayout;
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
            elementTab = new TabPage();
            dataSelectionTab = new TabPage();
            templatePathSelect = new Controls.NamedScopeData();
            templatePathData = new DataGridView();
            scopeNameData = new DataGridViewTextBoxColumn();
            selectionMemberData = new DataGridViewTextBoxColumn();
            transformTab = new TabPage();
            transformToolStrip = new ToolStrip();
            transformParseCommand = new ToolStripButton();
            transformImportCommand = new ToolStripButton();
            transformExportCommand = new ToolStripButton();
            transformFilePath = new ToolStripTextBox();
            transformExceptionData = new Controls.TextBoxData();
            transformScriptData = new TextBox();
            documentsTab = new TabPage();
            scriptsTab = new TabPage();
            bindingTemplate = new BindingSource(components);
            templateToolStrip = new ContextMenuStrip(components);
            deleteTemplateCommand = new ToolStripMenuItem();
            folderBrowserDialog = new FolderBrowserDialog();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            bindingPath = new BindingSource(components);
            templateLayoutPanel = new TableLayoutPanel();
            documentLayout = new TableLayoutPanel();
            documentGroupLayout = new TableLayoutPanel();
            elementPathLayout = new TableLayoutPanel();
            transformLayout = new TableLayoutPanel();
            templateLayoutPanel.SuspendLayout();
            templateTabs.SuspendLayout();
            documentSettingTab.SuspendLayout();
            documentLayout.SuspendLayout();
            documentGroup.SuspendLayout();
            documentGroupLayout.SuspendLayout();
            scriptingGroup.SuspendLayout();
            scriptingGroupLayout.SuspendLayout();
            dataSelectionTab.SuspendLayout();
            elementPathLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)templatePathData).BeginInit();
            transformTab.SuspendLayout();
            transformLayout.SuspendLayout();
            transformToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            templateToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingPath).BeginInit();
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
            templateLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            templateLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
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
            templateDescriptionData.Size = new Size(857, 185);
            templateDescriptionData.TabIndex = 1;
            // 
            // templateTabs
            // 
            templateTabs.Controls.Add(documentSettingTab);
            templateTabs.Controls.Add(elementTab);
            templateTabs.Controls.Add(dataSelectionTab);
            templateTabs.Controls.Add(transformTab);
            templateTabs.Controls.Add(documentsTab);
            templateTabs.Controls.Add(scriptsTab);
            templateTabs.Dock = DockStyle.Fill;
            templateTabs.Location = new Point(3, 244);
            templateTabs.Name = "templateTabs";
            templateTabs.SelectedIndex = 0;
            templateTabs.Size = new Size(857, 442);
            templateTabs.TabIndex = 2;
            // 
            // documentSettingTab
            // 
            documentSettingTab.BackColor = SystemColors.Control;
            documentSettingTab.Controls.Add(documentLayout);
            documentSettingTab.Location = new Point(4, 24);
            documentSettingTab.Name = "documentSettingTab";
            documentSettingTab.Padding = new Padding(3);
            documentSettingTab.Size = new Size(849, 414);
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
            documentLayout.Size = new Size(843, 408);
            documentLayout.TabIndex = 0;
            // 
            // documentGroup
            // 
            documentGroup.AutoSize = true;
            documentGroup.Controls.Add(documentGroupLayout);
            documentGroup.Dock = DockStyle.Fill;
            documentGroup.Location = new Point(3, 105);
            documentGroup.Name = "documentGroup";
            documentGroup.Size = new Size(415, 300);
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
            documentGroupLayout.Size = new Size(409, 278);
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
            documentPrefixData.Size = new Size(141, 222);
            documentPrefixData.TabIndex = 2;
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
            documentSuffixData.Size = new Size(141, 222);
            documentSuffixData.TabIndex = 3;
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
            documentExtensionData.Size = new Size(109, 222);
            documentExtensionData.TabIndex = 4;
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
            // 
            // scriptingGroup
            // 
            scriptingGroup.AutoSize = true;
            scriptingGroup.Controls.Add(scriptingGroupLayout);
            scriptingGroup.Dock = DockStyle.Fill;
            scriptingGroup.Location = new Point(424, 55);
            scriptingGroup.Name = "scriptingGroup";
            documentLayout.SetRowSpan(scriptingGroup, 2);
            scriptingGroup.Size = new Size(416, 350);
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
            scriptingGroupLayout.Size = new Size(410, 328);
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
            scriptingExtensionData.Size = new Size(108, 220);
            scriptingExtensionData.TabIndex = 5;
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
            scriptingSuffixData.Size = new Size(142, 220);
            scriptingSuffixData.TabIndex = 4;
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
            scriptingPrefixData.Size = new Size(142, 220);
            scriptingPrefixData.TabIndex = 3;
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
            // elementTab
            // 
            elementTab.BackColor = SystemColors.Control;
            elementTab.Location = new Point(4, 24);
            elementTab.Name = "elementTab";
            elementTab.Size = new Size(849, 414);
            elementTab.TabIndex = 2;
            elementTab.Text = "Schema Definition (XSD)";
            // 
            // dataSelectionTab
            // 
            dataSelectionTab.BackColor = SystemColors.Control;
            dataSelectionTab.Controls.Add(elementPathLayout);
            dataSelectionTab.Location = new Point(4, 24);
            dataSelectionTab.Name = "dataSelectionTab";
            dataSelectionTab.Size = new Size(849, 414);
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
            elementPathLayout.Size = new Size(849, 414);
            elementPathLayout.TabIndex = 1;
            // 
            // templatePathSelect
            // 
            templatePathSelect.ApplyImage = Properties.Resources.NewXPath;
            templatePathSelect.ApplyText = "apply";
            templatePathSelect.Dock = DockStyle.Fill;
            templatePathSelect.HeaderText = "Selected Path";
            templatePathSelect.Location = new Point(427, 3);
            templatePathSelect.Name = "templatePathSelect";
            templatePathSelect.ReadOnly = false;
            templatePathSelect.Scope = DataLayer.ApplicationData.Scope.ScopeType.Null;
            templatePathSelect.ScopePath = namedScopePath2;
            templatePathSelect.Size = new Size(419, 408);
            templatePathSelect.TabIndex = 6;
            templatePathSelect.OnApply += NamedScopeData_OnApply;
            // 
            // templatePathData
            // 
            templatePathData.AllowUserToAddRows = false;
            templatePathData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            templatePathData.Columns.AddRange(new DataGridViewColumn[] { scopeNameData, selectionMemberData });
            templatePathData.Dock = DockStyle.Fill;
            templatePathData.Location = new Point(3, 3);
            templatePathData.Name = "templatePathData";
            templatePathData.Size = new Size(418, 408);
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
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(849, 414);
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
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            transformLayout.Size = new Size(843, 408);
            transformLayout.TabIndex = 0;
            // 
            // transformToolStrip
            // 
            transformToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            transformToolStrip.Items.AddRange(new ToolStripItem[] { transformParseCommand, transformImportCommand, transformExportCommand, transformFilePath });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(843, 25);
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
            transformParseCommand.Size = new Size(23, 24);
            transformParseCommand.Text = "Parse Transform";
            transformParseCommand.Click += TransformParseCommand_Click;
            // 
            // transformImportCommand
            // 
            transformImportCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformImportCommand.Image = Properties.Resources.OpenFile;
            transformImportCommand.ImageTransparentColor = Color.Magenta;
            transformImportCommand.Name = "transformImportCommand";
            transformImportCommand.Size = new Size(23, 24);
            transformImportCommand.Text = "Import File";
            transformImportCommand.Click += TransformImportCommand_Click;
            // 
            // transformExportCommand
            // 
            transformExportCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformExportCommand.Image = Properties.Resources.Save;
            transformExportCommand.ImageTransparentColor = Color.Magenta;
            transformExportCommand.Name = "transformExportCommand";
            transformExportCommand.Size = new Size(23, 24);
            transformExportCommand.Text = "Export File";
            transformExportCommand.Click += TransformExportCommand_Click;
            // 
            // transformFilePath
            // 
            transformFilePath.Alignment = ToolStripItemAlignment.Right;
            transformFilePath.AutoSize = false;
            transformFilePath.Name = "transformFilePath";
            transformFilePath.ReadOnly = true;
            transformFilePath.Size = new Size(500, 25);
            // 
            // transformExceptionData
            // 
            transformExceptionData.AutoSize = true;
            transformExceptionData.Dock = DockStyle.Fill;
            transformExceptionData.HeaderText = "Exception";
            transformExceptionData.Location = new Point(3, 334);
            transformExceptionData.Multiline = true;
            transformExceptionData.Name = "transformExceptionData";
            transformExceptionData.ReadOnly = true;
            transformExceptionData.Size = new Size(837, 71);
            transformExceptionData.TabIndex = 2;
            // 
            // transformScriptData
            // 
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.Location = new Point(3, 28);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.ScrollBars = ScrollBars.Both;
            transformScriptData.Size = new Size(837, 300);
            transformScriptData.TabIndex = 3;
            // 
            // documentsTab
            // 
            documentsTab.BackColor = SystemColors.Control;
            documentsTab.Location = new Point(4, 24);
            documentsTab.Name = "documentsTab";
            documentsTab.Size = new Size(192, 72);
            documentsTab.TabIndex = 4;
            documentsTab.Text = "Documents (XML)";
            // 
            // scriptsTab
            // 
            scriptsTab.BackColor = SystemColors.Control;
            scriptsTab.Location = new Point(4, 24);
            scriptsTab.Name = "scriptsTab";
            scriptsTab.Size = new Size(192, 72);
            scriptsTab.TabIndex = 5;
            scriptsTab.Text = "Scripts (txt ...)";
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
            dataSelectionTab.ResumeLayout(false);
            elementPathLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)templatePathData).EndInit();
            transformTab.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformToolStrip.ResumeLayout(false);
            transformToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            templateToolStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingPath).EndInit();
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
        private ToolStripTextBox transformFilePath;
        private TabPage elementTab;
        private TabPage dataSelectionTab;
        private TabPage documentsTab;
        private TabPage scriptsTab;
        private ToolStripMenuItem deleteTemplateCommand;
        private FolderBrowserDialog folderBrowserDialog;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Controls.NamedScopeData templatePathSelect;
        private DataGridView templatePathData;
        private BindingSource bindingPath;
        private DataGridViewTextBoxColumn scopeNameData;
        private DataGridViewTextBoxColumn selectionMemberData;
    }
}