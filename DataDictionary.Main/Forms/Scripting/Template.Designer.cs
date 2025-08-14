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
            TabControl documentTabs;
            TabPage documentPreTransformTab;
            TableLayoutPanel documentGroupLayout;
            Label documentPlaceholder;
            TabPage documentPostTransformTab;
            TableLayoutPanel scriptLayout;
            Label scriptPlaceHolder;
            TableLayoutPanel optionsLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            templateOptionsTab = new TabControl();
            modelToolStrip = new ToolStrip();
            toolStripButton2 = new ToolStripButton();
            nodeToolStrip = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            nodeTreeView = new TreeView();
            nodeGroup = new GroupBox();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            nodeRenderAs = new DataDictionary.Main.Controls.ComboBoxData();
            transformScriptException = new DataDictionary.Main.Controls.TextBoxData();
            transformScriptData = new DataDictionary.Main.Controls.TextBoxData();
            transformToolStrip = new ToolStrip();
            toolStripButton4 = new ToolStripButton();
            documentToolStrip = new ToolStrip();
            toolStripButton3 = new ToolStripButton();
            documentDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            documentPrefixData = new DataDictionary.Main.Controls.TextBoxData();
            socumentSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            documentExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            scriptDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            scriptPrefixData = new DataDictionary.Main.Controls.TextBoxData();
            scriptSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            scriptExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            documentRoot = new TabPage();
            bindingTemplate = new BindingSource(components);
            physicalDirectory = new DataDictionary.Main.Controls.TextBoxData();
            rootDirectoryData = new DataDictionary.Main.Controls.ComboBoxData();
            breakOnScopeData = new DataDictionary.Main.Controls.ComboBoxData();
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
            documentTabs = new TabControl();
            documentPreTransformTab = new TabPage();
            documentGroupLayout = new TableLayoutPanel();
            documentPlaceholder = new Label();
            documentPostTransformTab = new TabPage();
            scriptLayout = new TableLayoutPanel();
            scriptPlaceHolder = new Label();
            optionsLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            templateOptionsTab.SuspendLayout();
            dataSourceTab.SuspendLayout();
            modelLayout.SuspendLayout();
            modelToolStrip.SuspendLayout();
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
            documentTabs.SuspendLayout();
            documentPreTransformTab.SuspendLayout();
            documentGroupLayout.SuspendLayout();
            documentPostTransformTab.SuspendLayout();
            scriptLayout.SuspendLayout();
            documentRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            optionsLayout.SuspendLayout();
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
            mainLayout.Size = new Size(545, 459);
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
            templateDescriptionData.Size = new Size(539, 116);
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
            templateOptionsTab.Location = new Point(3, 175);
            templateOptionsTab.Name = "templateOptionsTab";
            templateOptionsTab.SelectedIndex = 0;
            templateOptionsTab.Size = new Size(539, 281);
            templateOptionsTab.TabIndex = 2;
            // 
            // dataSourceTab
            // 
            dataSourceTab.BackColor = SystemColors.Control;
            dataSourceTab.Controls.Add(modelLayout);
            dataSourceTab.Location = new Point(4, 24);
            dataSourceTab.Name = "dataSourceTab";
            dataSourceTab.Size = new Size(531, 253);
            dataSourceTab.TabIndex = 1;
            dataSourceTab.Text = "Model (data source)";
            // 
            // modelLayout
            // 
            modelLayout.ColumnCount = 1;
            modelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelLayout.Controls.Add(modelToolStrip, 0, 0);
            modelLayout.Dock = DockStyle.Fill;
            modelLayout.Location = new Point(0, 0);
            modelLayout.Name = "modelLayout";
            modelLayout.RowCount = 2;
            modelLayout.RowStyles.Add(new RowStyle());
            modelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modelLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            modelLayout.Size = new Size(531, 253);
            modelLayout.TabIndex = 0;
            // 
            // modelToolStrip
            // 
            modelToolStrip.Items.AddRange(new ToolStripItem[] { toolStripButton2 });
            modelToolStrip.Location = new Point(0, 0);
            modelToolStrip.Name = "modelToolStrip";
            modelToolStrip.Size = new Size(531, 25);
            modelToolStrip.TabIndex = 0;
            modelToolStrip.Text = "Data Source";
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = Properties.Resources.NewXPath;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // nodeTab
            // 
            nodeTab.BackColor = SystemColors.Control;
            nodeTab.Controls.Add(nodeLayout);
            nodeTab.Location = new Point(4, 24);
            nodeTab.Name = "nodeTab";
            nodeTab.Size = new Size(531, 253);
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
            nodeLayout.Size = new Size(531, 253);
            nodeLayout.TabIndex = 0;
            // 
            // nodeToolStrip
            // 
            nodeLayout.SetColumnSpan(nodeToolStrip, 2);
            nodeToolStrip.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
            nodeToolStrip.Location = new Point(0, 0);
            nodeToolStrip.Name = "nodeToolStrip";
            nodeToolStrip.Size = new Size(531, 25);
            nodeToolStrip.TabIndex = 0;
            nodeToolStrip.Text = "Node";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.NewXMLSchema;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // nodeTreeView
            // 
            nodeTreeView.Dock = DockStyle.Fill;
            nodeTreeView.Location = new Point(3, 28);
            nodeTreeView.Name = "nodeTreeView";
            nodeTreeView.Size = new Size(153, 222);
            nodeTreeView.TabIndex = 1;
            // 
            // nodeGroup
            // 
            nodeGroup.Controls.Add(nodeDetailLayout);
            nodeGroup.Dock = DockStyle.Fill;
            nodeGroup.Location = new Point(162, 28);
            nodeGroup.Name = "nodeGroup";
            nodeGroup.Size = new Size(366, 222);
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
            nodeDetailLayout.Size = new Size(360, 200);
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
            transformTab.Size = new Size(531, 253);
            transformTab.TabIndex = 0;
            transformTab.Text = "Transform (XSLT)";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformScriptException, 0, 2);
            transformLayout.Controls.Add(transformScriptData, 0, 1);
            transformLayout.Controls.Add(transformToolStrip, 0, 0);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(0, 0);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 3;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            transformLayout.Size = new Size(531, 253);
            transformLayout.TabIndex = 0;
            // 
            // transformScriptException
            // 
            transformScriptException.AutoSize = true;
            transformScriptException.Dock = DockStyle.Fill;
            transformScriptException.HeaderText = "Exception(s)";
            transformScriptException.Location = new Point(3, 187);
            transformScriptException.Multiline = true;
            transformScriptException.Name = "transformScriptException";
            transformScriptException.ReadOnly = true;
            transformScriptException.Size = new Size(525, 63);
            transformScriptException.TabIndex = 1;
            transformScriptException.WordWrap = true;
            // 
            // transformScriptData
            // 
            transformScriptData.AutoSize = true;
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.HeaderText = "Transform Script";
            transformScriptData.Location = new Point(3, 28);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.ReadOnly = false;
            transformScriptData.Size = new Size(525, 153);
            transformScriptData.TabIndex = 0;
            transformScriptData.WordWrap = false;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Items.AddRange(new ToolStripItem[] { toolStripButton4 });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(531, 25);
            transformToolStrip.TabIndex = 2;
            transformToolStrip.Text = "Transform";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = Properties.Resources.XSLTransform;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Text = "toolStripButton4";
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(documentLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Size = new Size(531, 253);
            documentTab.TabIndex = 2;
            documentTab.Text = "Document (result)";
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 1;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.Controls.Add(documentToolStrip, 0, 0);
            documentLayout.Controls.Add(documentTabs, 0, 1);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 0);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 2;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            documentLayout.Size = new Size(531, 253);
            documentLayout.TabIndex = 0;
            // 
            // documentToolStrip
            // 
            documentLayout.SetColumnSpan(documentToolStrip, 2);
            documentToolStrip.Items.AddRange(new ToolStripItem[] { toolStripButton3 });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(531, 25);
            documentToolStrip.TabIndex = 0;
            documentToolStrip.Text = "Document";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = Properties.Resources.XMLFile;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            // 
            // documentTabs
            // 
            documentTabs.Controls.Add(documentRoot);
            documentTabs.Controls.Add(documentPreTransformTab);
            documentTabs.Controls.Add(documentPostTransformTab);
            documentTabs.Dock = DockStyle.Fill;
            documentTabs.Location = new Point(3, 28);
            documentTabs.Name = "documentTabs";
            documentTabs.SelectedIndex = 0;
            documentTabs.Size = new Size(525, 222);
            documentTabs.TabIndex = 7;
            // 
            // documentPreTransformTab
            // 
            documentPreTransformTab.BackColor = SystemColors.Control;
            documentPreTransformTab.Controls.Add(documentGroupLayout);
            documentPreTransformTab.Location = new Point(4, 24);
            documentPreTransformTab.Name = "documentPreTransformTab";
            documentPreTransformTab.Size = new Size(517, 194);
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
            documentGroupLayout.Controls.Add(documentPrefixData, 0, 1);
            documentGroupLayout.Controls.Add(socumentSuffixData, 2, 1);
            documentGroupLayout.Controls.Add(documentExtensionData, 3, 1);
            documentGroupLayout.Controls.Add(documentPlaceholder, 1, 1);
            documentGroupLayout.Dock = DockStyle.Fill;
            documentGroupLayout.Location = new Point(0, 0);
            documentGroupLayout.Name = "documentGroupLayout";
            documentGroupLayout.RowCount = 3;
            documentGroupLayout.RowStyles.Add(new RowStyle());
            documentGroupLayout.RowStyles.Add(new RowStyle());
            documentGroupLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentGroupLayout.Size = new Size(517, 194);
            documentGroupLayout.TabIndex = 1;
            // 
            // documentDirectoryData
            // 
            documentDirectoryData.AutoSize = true;
            documentGroupLayout.SetColumnSpan(documentDirectoryData, 4);
            documentDirectoryData.Dock = DockStyle.Fill;
            documentDirectoryData.HeaderText = "XML Directory";
            documentDirectoryData.Location = new Point(3, 3);
            documentDirectoryData.Multiline = false;
            documentDirectoryData.Name = "documentDirectoryData";
            documentDirectoryData.ReadOnly = false;
            documentDirectoryData.Size = new Size(511, 44);
            documentDirectoryData.TabIndex = 1;
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
            documentPrefixData.Size = new Size(120, 44);
            documentPrefixData.TabIndex = 2;
            documentPrefixData.WordWrap = true;
            // 
            // socumentSuffixData
            // 
            socumentSuffixData.AutoSize = true;
            socumentSuffixData.Dock = DockStyle.Fill;
            socumentSuffixData.HeaderText = "Suffix";
            socumentSuffixData.Location = new Point(268, 53);
            socumentSuffixData.Multiline = false;
            socumentSuffixData.Name = "socumentSuffixData";
            socumentSuffixData.ReadOnly = false;
            socumentSuffixData.Size = new Size(120, 44);
            socumentSuffixData.TabIndex = 3;
            socumentSuffixData.WordWrap = true;
            // 
            // documentExtensionData
            // 
            documentExtensionData.AutoSize = true;
            documentExtensionData.Dock = DockStyle.Fill;
            documentExtensionData.HeaderText = "Extension";
            documentExtensionData.Location = new Point(394, 53);
            documentExtensionData.Multiline = false;
            documentExtensionData.Name = "documentExtensionData";
            documentExtensionData.ReadOnly = false;
            documentExtensionData.Size = new Size(120, 44);
            documentExtensionData.TabIndex = 4;
            documentExtensionData.WordWrap = true;
            // 
            // documentPlaceholder
            // 
            documentPlaceholder.AutoSize = true;
            documentPlaceholder.Dock = DockStyle.Fill;
            documentPlaceholder.Location = new Point(129, 50);
            documentPlaceholder.Name = "documentPlaceholder";
            documentPlaceholder.Size = new Size(133, 50);
            documentPlaceholder.TabIndex = 5;
            documentPlaceholder.Text = "<element name/>";
            documentPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // documentPostTransformTab
            // 
            documentPostTransformTab.BackColor = SystemColors.Control;
            documentPostTransformTab.Controls.Add(scriptLayout);
            documentPostTransformTab.Location = new Point(4, 24);
            documentPostTransformTab.Name = "documentPostTransformTab";
            documentPostTransformTab.Size = new Size(517, 194);
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
            scriptLayout.Controls.Add(scriptPlaceHolder, 1, 1);
            scriptLayout.Controls.Add(scriptDirectoryData, 0, 0);
            scriptLayout.Controls.Add(scriptPrefixData, 0, 1);
            scriptLayout.Controls.Add(scriptSuffixData, 2, 1);
            scriptLayout.Controls.Add(scriptExtensionData, 3, 1);
            scriptLayout.Dock = DockStyle.Fill;
            scriptLayout.Location = new Point(0, 0);
            scriptLayout.Name = "scriptLayout";
            scriptLayout.RowCount = 3;
            scriptLayout.RowStyles.Add(new RowStyle());
            scriptLayout.RowStyles.Add(new RowStyle());
            scriptLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scriptLayout.Size = new Size(517, 194);
            scriptLayout.TabIndex = 8;
            // 
            // scriptPlaceHolder
            // 
            scriptPlaceHolder.AutoSize = true;
            scriptPlaceHolder.Dock = DockStyle.Fill;
            scriptPlaceHolder.Location = new Point(129, 50);
            scriptPlaceHolder.Name = "scriptPlaceHolder";
            scriptPlaceHolder.Size = new Size(133, 50);
            scriptPlaceHolder.TabIndex = 6;
            scriptPlaceHolder.Text = "<element name/>";
            scriptPlaceHolder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scriptDirectoryData
            // 
            scriptDirectoryData.AutoSize = true;
            scriptLayout.SetColumnSpan(scriptDirectoryData, 4);
            scriptDirectoryData.Dock = DockStyle.Fill;
            scriptDirectoryData.HeaderText = "Script Directory";
            scriptDirectoryData.Location = new Point(3, 3);
            scriptDirectoryData.Multiline = false;
            scriptDirectoryData.Name = "scriptDirectoryData";
            scriptDirectoryData.ReadOnly = false;
            scriptDirectoryData.Size = new Size(511, 44);
            scriptDirectoryData.TabIndex = 1;
            scriptDirectoryData.WordWrap = true;
            // 
            // scriptPrefixData
            // 
            scriptPrefixData.AutoSize = true;
            scriptPrefixData.Dock = DockStyle.Fill;
            scriptPrefixData.HeaderText = "Prefix";
            scriptPrefixData.Location = new Point(3, 53);
            scriptPrefixData.Multiline = false;
            scriptPrefixData.Name = "scriptPrefixData";
            scriptPrefixData.ReadOnly = false;
            scriptPrefixData.Size = new Size(120, 44);
            scriptPrefixData.TabIndex = 2;
            scriptPrefixData.WordWrap = true;
            // 
            // scriptSuffixData
            // 
            scriptSuffixData.AutoSize = true;
            scriptSuffixData.Dock = DockStyle.Fill;
            scriptSuffixData.HeaderText = "Suffix";
            scriptSuffixData.Location = new Point(268, 53);
            scriptSuffixData.Multiline = false;
            scriptSuffixData.Name = "scriptSuffixData";
            scriptSuffixData.ReadOnly = false;
            scriptSuffixData.Size = new Size(120, 44);
            scriptSuffixData.TabIndex = 3;
            scriptSuffixData.WordWrap = true;
            // 
            // scriptExtensionData
            // 
            scriptExtensionData.AutoSize = true;
            scriptExtensionData.Dock = DockStyle.Fill;
            scriptExtensionData.HeaderText = "Extension";
            scriptExtensionData.Location = new Point(394, 53);
            scriptExtensionData.Multiline = false;
            scriptExtensionData.Name = "scriptExtensionData";
            scriptExtensionData.ReadOnly = false;
            scriptExtensionData.Size = new Size(120, 44);
            scriptExtensionData.TabIndex = 4;
            scriptExtensionData.WordWrap = true;
            // 
            // documentRoot
            // 
            documentRoot.BackColor = SystemColors.Control;
            documentRoot.Controls.Add(optionsLayout);
            documentRoot.Location = new Point(4, 24);
            documentRoot.Name = "documentRoot";
            documentRoot.Padding = new Padding(3);
            documentRoot.Size = new Size(517, 194);
            documentRoot.TabIndex = 4;
            documentRoot.Text = "Root";
            // 
            // optionsLayout
            // 
            optionsLayout.AutoSize = true;
            optionsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsLayout.ColumnCount = 2;
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            optionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            optionsLayout.Controls.Add(physicalDirectory, 0, 1);
            optionsLayout.Controls.Add(rootDirectoryData, 0, 0);
            optionsLayout.Controls.Add(breakOnScopeData, 1, 0);
            optionsLayout.Dock = DockStyle.Fill;
            optionsLayout.Location = new Point(3, 3);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 3;
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            optionsLayout.Size = new Size(511, 188);
            optionsLayout.TabIndex = 2;
            // 
            // physicalDirectory
            // 
            physicalDirectory.AutoSize = true;
            physicalDirectory.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsLayout.SetColumnSpan(physicalDirectory, 2);
            physicalDirectory.Dock = DockStyle.Fill;
            physicalDirectory.HeaderText = "Phyiscal Directory";
            physicalDirectory.Location = new Point(3, 55);
            physicalDirectory.Multiline = false;
            physicalDirectory.Name = "physicalDirectory";
            physicalDirectory.ReadOnly = true;
            physicalDirectory.Size = new Size(505, 44);
            physicalDirectory.TabIndex = 0;
            physicalDirectory.WordWrap = true;
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
            rootDirectoryData.Size = new Size(351, 46);
            rootDirectoryData.TabIndex = 1;
            // 
            // breakOnScopeData
            // 
            breakOnScopeData.AutoSize = true;
            breakOnScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            breakOnScopeData.Dock = DockStyle.Fill;
            breakOnScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            breakOnScopeData.HeaderText = "New Document On";
            breakOnScopeData.Location = new Point(360, 3);
            breakOnScopeData.Name = "breakOnScopeData";
            breakOnScopeData.ReadOnly = false;
            breakOnScopeData.Size = new Size(148, 46);
            breakOnScopeData.TabIndex = 2;
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 484);
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
            documentTabs.ResumeLayout(false);
            documentPreTransformTab.ResumeLayout(false);
            documentPreTransformTab.PerformLayout();
            documentGroupLayout.ResumeLayout(false);
            documentGroupLayout.PerformLayout();
            documentPostTransformTab.ResumeLayout(false);
            documentPostTransformTab.PerformLayout();
            scriptLayout.ResumeLayout(false);
            scriptLayout.PerformLayout();
            documentRoot.ResumeLayout(false);
            documentRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            optionsLayout.ResumeLayout(false);
            optionsLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private TabControl templateOptionsTab;
        private TabPage transformTab;
        private TableLayoutPanel transformLayout;
        private Controls.TextBoxData transformScriptData;
        private Controls.TextBoxData transformScriptException;
        private ToolStrip transformToolStrip;
        private TabPage dataSourceTab;
        private TableLayoutPanel modelLayout;
        private TableLayoutPanel documentLayout;
        private ToolStrip documentToolStrip;
        private ToolStrip modelToolStrip;
        private TableLayoutPanel nodeLayout;
        private ToolStrip nodeToolStrip;
        private TreeView nodeTreeView;
        private GroupBox nodeGroup;
        private TableLayoutPanel nodeDetailLayout;
        private Controls.TextBoxData nodeNameData;
        private Controls.ComboBoxData nodeRenderAs;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private BindingSource bindingTemplate;
        private TableLayoutPanel documentGroupLayout;
        private TabPage documentPostTransformTab;
        private Controls.TextBoxData documentDirectoryData;
        private Controls.TextBoxData documentPrefixData;
        private Controls.TextBoxData socumentSuffixData;
        private Controls.TextBoxData documentExtensionData;
        private Controls.TextBoxData scriptDirectoryData;
        private Controls.TextBoxData scriptPrefixData;
        private Controls.TextBoxData scriptSuffixData;
        private Controls.TextBoxData scriptExtensionData;
        private TabPage documentRoot;
        private Controls.TextBoxData physicalDirectory;
        private Controls.ComboBoxData rootDirectoryData;
        private Controls.ComboBoxData breakOnScopeData;
    }
}