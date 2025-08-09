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
            TableLayoutPanel mainLayout;
            TabPage dataSourceTab;
            TableLayoutPanel modelLayout;
            TabPage documentTab;
            TabPage transformTab;
            TableLayoutPanel transformLayout;
            TabPage scriptTab;
            GroupBox optionsGroup;
            TableLayoutPanel optionsLayout;
            TableLayoutPanel documentLayout;
            TableLayoutPanel scriptLayout;
            TabPage nodeTab;
            TableLayoutPanel nodeLayout;
            TableLayoutPanel nodeDetailLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            templateOptionsTab = new TabControl();
            transformScriptException = new DataDictionary.Main.Controls.TextBoxData();
            transformScriptData = new DataDictionary.Main.Controls.TextBoxData();
            transformToolStrip = new ToolStrip();
            physicalDirectory = new DataDictionary.Main.Controls.TextBoxData();
            rootDirectoryData = new DataDictionary.Main.Controls.ComboBoxData();
            breakOnScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            documentToolStrip = new ToolStrip();
            documentDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            documentPrefixData = new DataDictionary.Main.Controls.TextBoxData();
            socumentSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            documentExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            scriptToolStrip = new ToolStrip();
            scriptDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            scriptPrefixData = new DataDictionary.Main.Controls.TextBoxData();
            scriptSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            scriptExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            modelToolStrip = new ToolStrip();
            nodeToolStrip = new ToolStrip();
            nodeTreeView = new TreeView();
            nodeGroup = new GroupBox();
            nodeNameData = new DataDictionary.Main.Controls.TextBoxData();
            nodeRenderAs = new DataDictionary.Main.Controls.ComboBoxData();
            mainLayout = new TableLayoutPanel();
            dataSourceTab = new TabPage();
            modelLayout = new TableLayoutPanel();
            documentTab = new TabPage();
            transformTab = new TabPage();
            transformLayout = new TableLayoutPanel();
            scriptTab = new TabPage();
            optionsGroup = new GroupBox();
            optionsLayout = new TableLayoutPanel();
            documentLayout = new TableLayoutPanel();
            scriptLayout = new TableLayoutPanel();
            nodeTab = new TabPage();
            nodeLayout = new TableLayoutPanel();
            nodeDetailLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            templateOptionsTab.SuspendLayout();
            dataSourceTab.SuspendLayout();
            modelLayout.SuspendLayout();
            documentTab.SuspendLayout();
            transformTab.SuspendLayout();
            transformLayout.SuspendLayout();
            scriptTab.SuspendLayout();
            optionsGroup.SuspendLayout();
            optionsLayout.SuspendLayout();
            documentLayout.SuspendLayout();
            scriptLayout.SuspendLayout();
            nodeTab.SuspendLayout();
            nodeLayout.SuspendLayout();
            nodeGroup.SuspendLayout();
            nodeDetailLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(templateTitleData, 0, 0);
            mainLayout.Controls.Add(templateDescriptionData, 0, 1);
            mainLayout.Controls.Add(templateOptionsTab, 0, 3);
            mainLayout.Controls.Add(optionsGroup, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 25);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 4;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            mainLayout.Size = new Size(545, 550);
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
            templateDescriptionData.Size = new Size(539, 142);
            templateDescriptionData.TabIndex = 1;
            templateDescriptionData.WordWrap = true;
            // 
            // templateOptionsTab
            // 
            templateOptionsTab.Controls.Add(dataSourceTab);
            templateOptionsTab.Controls.Add(nodeTab);
            templateOptionsTab.Controls.Add(documentTab);
            templateOptionsTab.Controls.Add(transformTab);
            templateOptionsTab.Controls.Add(scriptTab);
            templateOptionsTab.Dock = DockStyle.Fill;
            templateOptionsTab.Location = new Point(3, 331);
            templateOptionsTab.Name = "templateOptionsTab";
            templateOptionsTab.SelectedIndex = 0;
            templateOptionsTab.Size = new Size(539, 216);
            templateOptionsTab.TabIndex = 2;
            // 
            // dataSourceTab
            // 
            dataSourceTab.BackColor = SystemColors.Control;
            dataSourceTab.Controls.Add(modelLayout);
            dataSourceTab.Location = new Point(4, 24);
            dataSourceTab.Name = "dataSourceTab";
            dataSourceTab.Size = new Size(531, 188);
            dataSourceTab.TabIndex = 1;
            dataSourceTab.Text = "Model (data source)";
            // 
            // modelLayout
            // 
            modelLayout.ColumnCount = 1;
            modelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            modelLayout.Controls.Add(modelToolStrip, 0, 0);
            modelLayout.Location = new Point(111, 39);
            modelLayout.Name = "modelLayout";
            modelLayout.RowCount = 2;
            modelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            modelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            modelLayout.Size = new Size(354, 119);
            modelLayout.TabIndex = 0;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(documentLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Size = new Size(531, 188);
            documentTab.TabIndex = 2;
            documentTab.Text = "Document (XML)";
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Size = new Size(531, 188);
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
            transformLayout.Size = new Size(531, 188);
            transformLayout.TabIndex = 0;
            // 
            // transformScriptException
            // 
            transformScriptException.AutoSize = true;
            transformScriptException.Dock = DockStyle.Fill;
            transformScriptException.HeaderText = "Exception(s)";
            transformScriptException.Location = new Point(3, 142);
            transformScriptException.Multiline = true;
            transformScriptException.Name = "transformScriptException";
            transformScriptException.ReadOnly = true;
            transformScriptException.Size = new Size(525, 43);
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
            transformScriptData.Size = new Size(525, 108);
            transformScriptData.TabIndex = 0;
            transformScriptData.WordWrap = false;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(531, 25);
            transformToolStrip.TabIndex = 2;
            transformToolStrip.Text = "toolStrip1";
            // 
            // scriptTab
            // 
            scriptTab.BackColor = SystemColors.Control;
            scriptTab.Controls.Add(scriptLayout);
            scriptTab.Location = new Point(4, 24);
            scriptTab.Name = "scriptTab";
            scriptTab.Size = new Size(531, 188);
            scriptTab.TabIndex = 3;
            scriptTab.Text = "Script (result)";
            // 
            // optionsGroup
            // 
            optionsGroup.AutoSize = true;
            optionsGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsGroup.Controls.Add(optionsLayout);
            optionsGroup.Dock = DockStyle.Fill;
            optionsGroup.Location = new Point(3, 201);
            optionsGroup.Name = "optionsGroup";
            optionsGroup.Size = new Size(539, 124);
            optionsGroup.TabIndex = 3;
            optionsGroup.TabStop = false;
            optionsGroup.Text = "Options";
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
            optionsLayout.Location = new Point(3, 19);
            optionsLayout.Name = "optionsLayout";
            optionsLayout.RowCount = 2;
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.RowStyles.Add(new RowStyle());
            optionsLayout.Size = new Size(533, 102);
            optionsLayout.TabIndex = 0;
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
            physicalDirectory.Size = new Size(527, 44);
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
            rootDirectoryData.Size = new Size(367, 46);
            rootDirectoryData.TabIndex = 1;
            // 
            // breakOnScopeData
            // 
            breakOnScopeData.AutoSize = true;
            breakOnScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            breakOnScopeData.Dock = DockStyle.Fill;
            breakOnScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            breakOnScopeData.HeaderText = "New Document On";
            breakOnScopeData.Location = new Point(376, 3);
            breakOnScopeData.Name = "breakOnScopeData";
            breakOnScopeData.ReadOnly = false;
            breakOnScopeData.Size = new Size(154, 46);
            breakOnScopeData.TabIndex = 2;
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 4;
            documentLayout.ColumnStyles.Add(new ColumnStyle());
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.ColumnStyles.Add(new ColumnStyle());
            documentLayout.ColumnStyles.Add(new ColumnStyle());
            documentLayout.Controls.Add(documentToolStrip, 0, 0);
            documentLayout.Controls.Add(documentDirectoryData, 0, 1);
            documentLayout.Controls.Add(documentPrefixData, 0, 2);
            documentLayout.Controls.Add(socumentSuffixData, 2, 2);
            documentLayout.Controls.Add(documentExtensionData, 3, 2);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 0);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 4;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.Size = new Size(531, 188);
            documentLayout.TabIndex = 0;
            // 
            // documentToolStrip
            // 
            documentLayout.SetColumnSpan(documentToolStrip, 5);
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(531, 25);
            documentToolStrip.TabIndex = 0;
            documentToolStrip.Text = "toolStrip1";
            // 
            // documentDirectoryData
            // 
            documentDirectoryData.AutoSize = true;
            documentLayout.SetColumnSpan(documentDirectoryData, 4);
            documentDirectoryData.Dock = DockStyle.Fill;
            documentDirectoryData.HeaderText = "Document Directory";
            documentDirectoryData.Location = new Point(3, 28);
            documentDirectoryData.Multiline = false;
            documentDirectoryData.Name = "documentDirectoryData";
            documentDirectoryData.ReadOnly = false;
            documentDirectoryData.Size = new Size(525, 44);
            documentDirectoryData.TabIndex = 1;
            documentDirectoryData.WordWrap = true;
            // 
            // documentPrefixData
            // 
            documentPrefixData.AutoSize = true;
            documentPrefixData.HeaderText = "Prefix";
            documentPrefixData.Location = new Point(3, 78);
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
            socumentSuffixData.Location = new Point(282, 78);
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
            documentExtensionData.Location = new Point(408, 78);
            documentExtensionData.Multiline = false;
            documentExtensionData.Name = "documentExtensionData";
            documentExtensionData.ReadOnly = false;
            documentExtensionData.Size = new Size(120, 44);
            documentExtensionData.TabIndex = 4;
            documentExtensionData.WordWrap = true;
            // 
            // scriptLayout
            // 
            scriptLayout.ColumnCount = 4;
            scriptLayout.ColumnStyles.Add(new ColumnStyle());
            scriptLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            scriptLayout.ColumnStyles.Add(new ColumnStyle());
            scriptLayout.ColumnStyles.Add(new ColumnStyle());
            scriptLayout.Controls.Add(scriptToolStrip, 0, 0);
            scriptLayout.Controls.Add(scriptDirectoryData, 0, 1);
            scriptLayout.Controls.Add(scriptPrefixData, 0, 2);
            scriptLayout.Controls.Add(scriptSuffixData, 2, 2);
            scriptLayout.Controls.Add(scriptExtensionData, 3, 2);
            scriptLayout.Dock = DockStyle.Fill;
            scriptLayout.Location = new Point(0, 0);
            scriptLayout.Name = "scriptLayout";
            scriptLayout.RowCount = 4;
            scriptLayout.RowStyles.Add(new RowStyle());
            scriptLayout.RowStyles.Add(new RowStyle());
            scriptLayout.RowStyles.Add(new RowStyle());
            scriptLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scriptLayout.Size = new Size(531, 188);
            scriptLayout.TabIndex = 0;
            // 
            // scriptToolStrip
            // 
            scriptLayout.SetColumnSpan(scriptToolStrip, 4);
            scriptToolStrip.Location = new Point(0, 0);
            scriptToolStrip.Name = "scriptToolStrip";
            scriptToolStrip.Size = new Size(531, 25);
            scriptToolStrip.TabIndex = 0;
            scriptToolStrip.Text = "toolStrip1";
            // 
            // scriptDirectoryData
            // 
            scriptDirectoryData.AutoSize = true;
            scriptLayout.SetColumnSpan(scriptDirectoryData, 4);
            scriptDirectoryData.Dock = DockStyle.Fill;
            scriptDirectoryData.HeaderText = "Script Directory";
            scriptDirectoryData.Location = new Point(3, 28);
            scriptDirectoryData.Multiline = false;
            scriptDirectoryData.Name = "scriptDirectoryData";
            scriptDirectoryData.ReadOnly = false;
            scriptDirectoryData.Size = new Size(525, 44);
            scriptDirectoryData.TabIndex = 1;
            scriptDirectoryData.WordWrap = true;
            // 
            // scriptPrefixData
            // 
            scriptPrefixData.AutoSize = true;
            scriptPrefixData.Dock = DockStyle.Fill;
            scriptPrefixData.HeaderText = "Prefix";
            scriptPrefixData.Location = new Point(3, 78);
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
            scriptSuffixData.Location = new Point(282, 78);
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
            scriptExtensionData.Location = new Point(408, 78);
            scriptExtensionData.Multiline = false;
            scriptExtensionData.Name = "scriptExtensionData";
            scriptExtensionData.ReadOnly = false;
            scriptExtensionData.Size = new Size(120, 44);
            scriptExtensionData.TabIndex = 4;
            scriptExtensionData.WordWrap = true;
            // 
            // modelToolStrip
            // 
            modelToolStrip.Location = new Point(0, 0);
            modelToolStrip.Name = "modelToolStrip";
            modelToolStrip.Size = new Size(354, 25);
            modelToolStrip.TabIndex = 0;
            modelToolStrip.Text = "toolStrip1";
            // 
            // nodeTab
            // 
            nodeTab.BackColor = SystemColors.Control;
            nodeTab.Controls.Add(nodeLayout);
            nodeTab.Location = new Point(4, 24);
            nodeTab.Name = "nodeTab";
            nodeTab.Size = new Size(531, 188);
            nodeTab.TabIndex = 4;
            nodeTab.Text = "Elements/Attributes";
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
            nodeLayout.Size = new Size(531, 188);
            nodeLayout.TabIndex = 0;
            // 
            // nodeToolStrip
            // 
            nodeLayout.SetColumnSpan(nodeToolStrip, 2);
            nodeToolStrip.Location = new Point(0, 0);
            nodeToolStrip.Name = "nodeToolStrip";
            nodeToolStrip.Size = new Size(531, 25);
            nodeToolStrip.TabIndex = 0;
            nodeToolStrip.Text = "toolStrip2";
            // 
            // nodeTreeView
            // 
            nodeTreeView.Dock = DockStyle.Fill;
            nodeTreeView.Location = new Point(3, 28);
            nodeTreeView.Name = "nodeTreeView";
            nodeTreeView.Size = new Size(153, 157);
            nodeTreeView.TabIndex = 1;
            // 
            // nodeGroup
            // 
            nodeGroup.Controls.Add(nodeDetailLayout);
            nodeGroup.Dock = DockStyle.Fill;
            nodeGroup.Location = new Point(162, 28);
            nodeGroup.Name = "nodeGroup";
            nodeGroup.Size = new Size(366, 157);
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
            nodeDetailLayout.Size = new Size(360, 135);
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
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 575);
            Controls.Add(mainLayout);
            Name = "Template";
            Text = "Template";
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            templateOptionsTab.ResumeLayout(false);
            dataSourceTab.ResumeLayout(false);
            modelLayout.ResumeLayout(false);
            modelLayout.PerformLayout();
            documentTab.ResumeLayout(false);
            transformTab.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            scriptTab.ResumeLayout(false);
            optionsGroup.ResumeLayout(false);
            optionsGroup.PerformLayout();
            optionsLayout.ResumeLayout(false);
            optionsLayout.PerformLayout();
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            scriptLayout.ResumeLayout(false);
            scriptLayout.PerformLayout();
            nodeTab.ResumeLayout(false);
            nodeLayout.ResumeLayout(false);
            nodeLayout.PerformLayout();
            nodeGroup.ResumeLayout(false);
            nodeDetailLayout.ResumeLayout(false);
            nodeDetailLayout.PerformLayout();
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
        private Controls.TextBoxData physicalDirectory;
        private Controls.ComboBoxData rootDirectoryData;
        private Controls.ComboBoxData breakOnScopeData;
        private TableLayoutPanel documentLayout;
        private ToolStrip documentToolStrip;
        private Controls.TextBoxData documentDirectoryData;
        private Controls.TextBoxData documentPrefixData;
        private Controls.TextBoxData socumentSuffixData;
        private Controls.TextBoxData documentExtensionData;
        private TableLayoutPanel scriptLayout;
        private ToolStrip scriptToolStrip;
        private Controls.TextBoxData scriptDirectoryData;
        private Controls.TextBoxData scriptPrefixData;
        private Controls.TextBoxData scriptSuffixData;
        private Controls.TextBoxData scriptExtensionData;
        private ToolStrip modelToolStrip;
        private TableLayoutPanel nodeLayout;
        private ToolStrip nodeToolStrip;
        private TreeView nodeTreeView;
        private GroupBox nodeGroup;
        private TableLayoutPanel nodeDetailLayout;
        private Controls.TextBoxData nodeNameData;
        private Controls.ComboBoxData nodeRenderAs;
    }
}