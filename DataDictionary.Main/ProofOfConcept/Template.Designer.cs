namespace DataDictionary.Main.ProofOfConcept
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
            TabControl templateTabs;
            TabPage templateObjectTab;
            TableLayoutPanel templateObjectLayoutPanel;
            TabPage templateTransformTab;
            TableLayoutPanel templateTransformLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Template));
            TableLayoutPanel templateLayoutPanel;
            TableLayoutPanel documentTypeLayout;
            TableLayoutPanel documentFileLayout;
            templateObjectsGrid = new DataGridView();
            objectPathColumn = new DataGridViewTextBoxColumn();
            objectScopeColumn = new DataGridViewTextBoxColumn();
            objectInModelColumn = new DataGridViewCheckBoxColumn();
            objectPathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            objectInModelData = new CheckBox();
            transformData = new DataDictionary.Main.Controls.ComboBoxData();
            definitionData = new DataDictionary.Main.Controls.ComboBoxData();
            transformGridView = new DataGridView();
            transformColumn = new DataGridViewComboBoxColumn();
            definitionColumn = new DataGridViewComboBoxColumn();
            templateDocuments = new TabPage();
            documentLayout = new TableLayoutPanel();
            documentGridView = new DataGridView();
            documentColumn = new DataGridViewTextBoxColumn();
            documentTypeColumn = new DataGridViewComboBoxColumn();
            documentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            fileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentTransformData = new DataDictionary.Main.Controls.ComboBoxData();
            comboBoxData3 = new DataDictionary.Main.Controls.ComboBoxData();
            documentSchemaDefinitionData = new DataDictionary.Main.Controls.ComboBoxData();
            documentTabs = new TabControl();
            documentTypeTab = new TabPage();
            documentFileTab = new TabPage();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            templateTabs = new TabControl();
            templateObjectTab = new TabPage();
            templateObjectLayoutPanel = new TableLayoutPanel();
            templateTransformTab = new TabPage();
            templateTransformLayout = new TableLayoutPanel();
            templateLayoutPanel = new TableLayoutPanel();
            documentTypeLayout = new TableLayoutPanel();
            documentFileLayout = new TableLayoutPanel();
            templateTabs.SuspendLayout();
            templateObjectTab.SuspendLayout();
            templateObjectLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)templateObjectsGrid).BeginInit();
            templateTransformTab.SuspendLayout();
            templateTransformLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)transformGridView).BeginInit();
            templateDocuments.SuspendLayout();
            documentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentGridView).BeginInit();
            documentTabs.SuspendLayout();
            documentTypeTab.SuspendLayout();
            documentFileTab.SuspendLayout();
            templateLayoutPanel.SuspendLayout();
            documentTypeLayout.SuspendLayout();
            documentFileLayout.SuspendLayout();
            SuspendLayout();
            // 
            // templateTabs
            // 
            templateTabs.Controls.Add(templateObjectTab);
            templateTabs.Controls.Add(templateTransformTab);
            templateTabs.Controls.Add(templateDocuments);
            templateTabs.Dock = DockStyle.Fill;
            templateTabs.Location = new Point(3, 179);
            templateTabs.Name = "templateTabs";
            templateTabs.SelectedIndex = 0;
            templateTabs.Size = new Size(560, 498);
            templateTabs.TabIndex = 2;
            // 
            // templateObjectTab
            // 
            templateObjectTab.BackColor = SystemColors.Control;
            templateObjectTab.Controls.Add(templateObjectLayoutPanel);
            templateObjectTab.Location = new Point(4, 24);
            templateObjectTab.Name = "templateObjectTab";
            templateObjectTab.Padding = new Padding(3);
            templateObjectTab.Size = new Size(552, 498);
            templateObjectTab.TabIndex = 0;
            templateObjectTab.Text = "Objects";
            // 
            // templateObjectLayoutPanel
            // 
            templateObjectLayoutPanel.ColumnCount = 2;
            templateObjectLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateObjectLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            templateObjectLayoutPanel.Controls.Add(templateObjectsGrid, 0, 0);
            templateObjectLayoutPanel.Controls.Add(objectPathData, 0, 1);
            templateObjectLayoutPanel.Controls.Add(objectScopeData, 0, 2);
            templateObjectLayoutPanel.Controls.Add(objectInModelData, 1, 2);
            templateObjectLayoutPanel.Dock = DockStyle.Fill;
            templateObjectLayoutPanel.Location = new Point(3, 3);
            templateObjectLayoutPanel.Name = "templateObjectLayoutPanel";
            templateObjectLayoutPanel.RowCount = 3;
            templateObjectLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            templateObjectLayoutPanel.RowStyles.Add(new RowStyle());
            templateObjectLayoutPanel.RowStyles.Add(new RowStyle());
            templateObjectLayoutPanel.Size = new Size(546, 492);
            templateObjectLayoutPanel.TabIndex = 0;
            // 
            // templateObjectsGrid
            // 
            templateObjectsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            templateObjectsGrid.Columns.AddRange(new DataGridViewColumn[] { objectPathColumn, objectScopeColumn, objectInModelColumn });
            templateObjectLayoutPanel.SetColumnSpan(templateObjectsGrid, 2);
            templateObjectsGrid.Dock = DockStyle.Fill;
            templateObjectsGrid.Location = new Point(3, 3);
            templateObjectsGrid.Name = "templateObjectsGrid";
            templateObjectsGrid.Size = new Size(540, 384);
            templateObjectsGrid.TabIndex = 0;
            // 
            // objectPathColumn
            // 
            objectPathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectPathColumn.HeaderText = "Object";
            objectPathColumn.Name = "objectPathColumn";
            objectPathColumn.ReadOnly = true;
            // 
            // objectScopeColumn
            // 
            objectScopeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            objectScopeColumn.HeaderText = "Scope";
            objectScopeColumn.Name = "objectScopeColumn";
            objectScopeColumn.ReadOnly = true;
            objectScopeColumn.Width = 64;
            // 
            // objectInModelColumn
            // 
            objectInModelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            objectInModelColumn.HeaderText = "In Model";
            objectInModelColumn.Name = "objectInModelColumn";
            objectInModelColumn.ReadOnly = true;
            objectInModelColumn.Width = 60;
            // 
            // objectPathData
            // 
            objectPathData.AutoSize = true;
            objectPathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            templateObjectLayoutPanel.SetColumnSpan(objectPathData, 2);
            objectPathData.Dock = DockStyle.Fill;
            objectPathData.HeaderText = "Object";
            objectPathData.Location = new Point(3, 393);
            objectPathData.Name = "objectPathData";
            objectPathData.ReadOnly = false;
            objectPathData.SelectIcon = (Image)resources.GetObject("objectPathData.SelectIcon");
            objectPathData.Size = new Size(540, 44);
            objectPathData.TabIndex = 1;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectScopeData.HeaderText = "Scope";
            objectScopeData.Location = new Point(3, 443);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = true;
            objectScopeData.Size = new Size(461, 46);
            objectScopeData.TabIndex = 2;
            // 
            // objectInModelData
            // 
            objectInModelData.AutoSize = true;
            objectInModelData.Location = new Point(470, 443);
            objectInModelData.Name = "objectInModelData";
            objectInModelData.Size = new Size(73, 19);
            objectInModelData.TabIndex = 3;
            objectInModelData.Text = "In Model";
            objectInModelData.UseVisualStyleBackColor = true;
            // 
            // templateTransformTab
            // 
            templateTransformTab.BackColor = SystemColors.Control;
            templateTransformTab.Controls.Add(templateTransformLayout);
            templateTransformTab.Location = new Point(4, 24);
            templateTransformTab.Name = "templateTransformTab";
            templateTransformTab.Padding = new Padding(3);
            templateTransformTab.Size = new Size(192, 72);
            templateTransformTab.TabIndex = 1;
            templateTransformTab.Text = "Transforms";
            // 
            // templateTransformLayout
            // 
            templateTransformLayout.ColumnCount = 1;
            templateTransformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateTransformLayout.Controls.Add(transformData, 0, 1);
            templateTransformLayout.Controls.Add(definitionData, 0, 2);
            templateTransformLayout.Controls.Add(transformGridView, 0, 0);
            templateTransformLayout.Dock = DockStyle.Fill;
            templateTransformLayout.Location = new Point(3, 3);
            templateTransformLayout.Name = "templateTransformLayout";
            templateTransformLayout.RowCount = 3;
            templateTransformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            templateTransformLayout.RowStyles.Add(new RowStyle());
            templateTransformLayout.RowStyles.Add(new RowStyle());
            templateTransformLayout.Size = new Size(186, 66);
            templateTransformLayout.TabIndex = 0;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformData.Dock = DockStyle.Fill;
            transformData.DropDownStyle = ComboBoxStyle.DropDown;
            transformData.HeaderText = "Transform (XSLT)";
            transformData.Location = new Point(3, -35);
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(180, 46);
            transformData.TabIndex = 0;
            // 
            // definitionData
            // 
            definitionData.AutoSize = true;
            definitionData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            definitionData.Dock = DockStyle.Fill;
            definitionData.DropDownStyle = ComboBoxStyle.DropDown;
            definitionData.HeaderText = "Definition (XSD)";
            definitionData.Location = new Point(3, 17);
            definitionData.Name = "definitionData";
            definitionData.ReadOnly = false;
            definitionData.Size = new Size(180, 46);
            definitionData.TabIndex = 1;
            // 
            // transformGridView
            // 
            transformGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transformGridView.Columns.AddRange(new DataGridViewColumn[] { transformColumn, definitionColumn });
            transformGridView.Dock = DockStyle.Fill;
            transformGridView.Location = new Point(3, 3);
            transformGridView.Name = "transformGridView";
            transformGridView.Size = new Size(180, 1);
            transformGridView.TabIndex = 2;
            // 
            // transformColumn
            // 
            transformColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            transformColumn.HeaderText = "Transform (XSLT)";
            transformColumn.Name = "transformColumn";
            // 
            // definitionColumn
            // 
            definitionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            definitionColumn.HeaderText = "Definition (XSD)";
            definitionColumn.Name = "definitionColumn";
            // 
            // templateDocuments
            // 
            templateDocuments.BackColor = SystemColors.Control;
            templateDocuments.Controls.Add(documentLayout);
            templateDocuments.Location = new Point(4, 24);
            templateDocuments.Name = "templateDocuments";
            templateDocuments.Size = new Size(552, 470);
            templateDocuments.TabIndex = 2;
            templateDocuments.Text = "Documents";
            // 
            // documentLayout
            // 
            documentLayout.AutoSize = true;
            documentLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentLayout.ColumnCount = 2;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.Controls.Add(documentGridView, 0, 0);
            documentLayout.Controls.Add(documentTitleData, 0, 1);
            documentLayout.Controls.Add(documentTypeData, 1, 1);
            documentLayout.Controls.Add(documentTabs, 0, 2);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 0);
            documentLayout.Name = "documentLayout";
            documentLayout.Padding = new Padding(3);
            documentLayout.RowCount = 3;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.Size = new Size(552, 470);
            documentLayout.TabIndex = 0;
            // 
            // documentGridView
            // 
            documentGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentGridView.Columns.AddRange(new DataGridViewColumn[] { documentColumn, documentTypeColumn });
            documentLayout.SetColumnSpan(documentGridView, 2);
            documentGridView.Dock = DockStyle.Fill;
            documentGridView.Location = new Point(6, 6);
            documentGridView.Name = "documentGridView";
            documentGridView.Size = new Size(540, 150);
            documentGridView.TabIndex = 0;
            // 
            // documentColumn
            // 
            documentColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentColumn.FillWeight = 66F;
            documentColumn.HeaderText = "Document";
            documentColumn.Name = "documentColumn";
            // 
            // documentTypeColumn
            // 
            documentTypeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            documentTypeColumn.FillWeight = 33F;
            documentTypeColumn.HeaderText = "Document Type";
            documentTypeColumn.Name = "documentTypeColumn";
            // 
            // documentTitleData
            // 
            documentTitleData.AutoSize = true;
            documentTitleData.Dock = DockStyle.Fill;
            documentTitleData.HeaderText = "Title";
            documentTitleData.Location = new Point(6, 162);
            documentTitleData.Multiline = false;
            documentTitleData.Name = "documentTitleData";
            documentTitleData.ReadOnly = false;
            documentTitleData.Size = new Size(267, 46);
            documentTitleData.TabIndex = 1;
            documentTitleData.WordWrap = true;
            // 
            // documentTypeData
            // 
            documentTypeData.AutoSize = true;
            documentTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentTypeData.Dock = DockStyle.Fill;
            documentTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            documentTypeData.HeaderText = "Type";
            documentTypeData.Location = new Point(279, 162);
            documentTypeData.Name = "documentTypeData";
            documentTypeData.ReadOnly = false;
            documentTypeData.Size = new Size(267, 46);
            documentTypeData.TabIndex = 2;
            // 
            // fileNameData
            // 
            fileNameData.AutoSize = true;
            fileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fileNameData.Dock = DockStyle.Fill;
            fileNameData.HeaderText = "File Name";
            fileNameData.Location = new Point(3, 155);
            fileNameData.Name = "fileNameData";
            fileNameData.ReadOnly = false;
            fileNameData.SelectIcon = (Image)resources.GetObject("fileNameData.SelectIcon");
            fileNameData.Size = new Size(520, 44);
            fileNameData.TabIndex = 7;
            // 
            // localPathData
            // 
            localPathData.AutoSize = true;
            localPathData.Dock = DockStyle.Fill;
            localPathData.HeaderText = "Local Path";
            localPathData.Location = new Point(3, 105);
            localPathData.Multiline = false;
            localPathData.Name = "localPathData";
            localPathData.ReadOnly = true;
            localPathData.Size = new Size(520, 44);
            localPathData.TabIndex = 6;
            localPathData.WordWrap = true;
            // 
            // rootFolderData
            // 
            rootFolderData.AutoSize = true;
            rootFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootFolderData.Dock = DockStyle.Fill;
            rootFolderData.DropDownStyle = ComboBoxStyle.DropDown;
            rootFolderData.HeaderText = "Root Folder";
            rootFolderData.Location = new Point(3, 3);
            rootFolderData.Name = "rootFolderData";
            rootFolderData.ReadOnly = false;
            rootFolderData.Size = new Size(520, 46);
            rootFolderData.TabIndex = 4;
            // 
            // relativePathData
            // 
            relativePathData.AutoSize = true;
            relativePathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            relativePathData.Dock = DockStyle.Fill;
            relativePathData.HeaderText = "Relative Path";
            relativePathData.Location = new Point(3, 55);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(520, 44);
            relativePathData.TabIndex = 5;
            // 
            // documentTransformData
            // 
            documentTransformData.AutoSize = true;
            documentTransformData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentTransformData.Dock = DockStyle.Fill;
            documentTransformData.DropDownStyle = ComboBoxStyle.DropDown;
            documentTransformData.HeaderText = "Transform";
            documentTransformData.Location = new Point(3, 3);
            documentTransformData.Name = "documentTransformData";
            documentTransformData.ReadOnly = false;
            documentTransformData.Size = new Size(520, 46);
            documentTransformData.TabIndex = 9;
            // 
            // comboBoxData3
            // 
            comboBoxData3.AutoSize = true;
            comboBoxData3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            comboBoxData3.Dock = DockStyle.Fill;
            comboBoxData3.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxData3.HeaderText = "Object";
            comboBoxData3.Location = new Point(3, 107);
            comboBoxData3.Name = "comboBoxData3";
            comboBoxData3.ReadOnly = false;
            comboBoxData3.Size = new Size(520, 46);
            comboBoxData3.TabIndex = 10;
            // 
            // documentSchemaDefinitionData
            // 
            documentSchemaDefinitionData.AutoSize = true;
            documentSchemaDefinitionData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentSchemaDefinitionData.Dock = DockStyle.Fill;
            documentSchemaDefinitionData.DropDownStyle = ComboBoxStyle.DropDown;
            documentSchemaDefinitionData.HeaderText = "Schema Definition";
            documentSchemaDefinitionData.Location = new Point(3, 55);
            documentSchemaDefinitionData.Name = "documentSchemaDefinitionData";
            documentSchemaDefinitionData.ReadOnly = false;
            documentSchemaDefinitionData.Size = new Size(520, 46);
            documentSchemaDefinitionData.TabIndex = 8;
            // 
            // documentTabs
            // 
            documentLayout.SetColumnSpan(documentTabs, 2);
            documentTabs.Controls.Add(documentTypeTab);
            documentTabs.Controls.Add(documentFileTab);
            documentTabs.Dock = DockStyle.Fill;
            documentTabs.Location = new Point(6, 214);
            documentTabs.Name = "documentTabs";
            documentTabs.SelectedIndex = 0;
            documentTabs.Size = new Size(540, 250);
            documentTabs.TabIndex = 11;
            // 
            // documentTypeTab
            // 
            documentTypeTab.BackColor = SystemColors.Control;
            documentTypeTab.Controls.Add(documentTypeLayout);
            documentTypeTab.Location = new Point(4, 24);
            documentTypeTab.Name = "documentTypeTab";
            documentTypeTab.Padding = new Padding(3);
            documentTypeTab.Size = new Size(532, 222);
            documentTypeTab.TabIndex = 0;
            documentTypeTab.Text = "Document Type";
            // 
            // documentFileTab
            // 
            documentFileTab.BackColor = SystemColors.Control;
            documentFileTab.Controls.Add(documentFileLayout);
            documentFileTab.Location = new Point(4, 24);
            documentFileTab.Name = "documentFileTab";
            documentFileTab.Padding = new Padding(3);
            documentFileTab.Size = new Size(532, 222);
            documentFileTab.TabIndex = 1;
            documentFileTab.Text = "Document File";
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
            templateLayoutPanel.Size = new Size(566, 680);
            templateLayoutPanel.TabIndex = 4;
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
            templateTitleData.Size = new Size(560, 44);
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
            templateDescriptionData.Size = new Size(560, 120);
            templateDescriptionData.TabIndex = 1;
            templateDescriptionData.WordWrap = true;
            // 
            // documentTypeLayout
            // 
            documentTypeLayout.AutoSize = true;
            documentTypeLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentTypeLayout.ColumnCount = 1;
            documentTypeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentTypeLayout.Controls.Add(documentTransformData, 0, 0);
            documentTypeLayout.Controls.Add(documentSchemaDefinitionData, 0, 1);
            documentTypeLayout.Controls.Add(comboBoxData3, 0, 2);
            documentTypeLayout.Dock = DockStyle.Fill;
            documentTypeLayout.Location = new Point(3, 3);
            documentTypeLayout.Name = "documentTypeLayout";
            documentTypeLayout.RowCount = 4;
            documentTypeLayout.RowStyles.Add(new RowStyle());
            documentTypeLayout.RowStyles.Add(new RowStyle());
            documentTypeLayout.RowStyles.Add(new RowStyle());
            documentTypeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentTypeLayout.Size = new Size(526, 216);
            documentTypeLayout.TabIndex = 0;
            // 
            // documentFileLayout
            // 
            documentFileLayout.AutoSize = true;
            documentFileLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentFileLayout.ColumnCount = 1;
            documentFileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentFileLayout.Controls.Add(rootFolderData, 0, 0);
            documentFileLayout.Controls.Add(relativePathData, 0, 1);
            documentFileLayout.Controls.Add(localPathData, 0, 2);
            documentFileLayout.Controls.Add(fileNameData, 0, 3);
            documentFileLayout.Dock = DockStyle.Fill;
            documentFileLayout.Location = new Point(3, 3);
            documentFileLayout.Name = "documentFileLayout";
            documentFileLayout.RowCount = 5;
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentFileLayout.Size = new Size(526, 216);
            documentFileLayout.TabIndex = 0;
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(566, 705);
            Controls.Add(templateLayoutPanel);
            Name = "Template";
            Text = "Template";
            Controls.SetChildIndex(templateLayoutPanel, 0);
            templateTabs.ResumeLayout(false);
            templateObjectTab.ResumeLayout(false);
            templateObjectLayoutPanel.ResumeLayout(false);
            templateObjectLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)templateObjectsGrid).EndInit();
            templateTransformTab.ResumeLayout(false);
            templateTransformLayout.ResumeLayout(false);
            templateTransformLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)transformGridView).EndInit();
            templateDocuments.ResumeLayout(false);
            templateDocuments.PerformLayout();
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentGridView).EndInit();
            documentTabs.ResumeLayout(false);
            documentTypeTab.ResumeLayout(false);
            documentTypeTab.PerformLayout();
            documentFileTab.ResumeLayout(false);
            documentFileTab.PerformLayout();
            templateLayoutPanel.ResumeLayout(false);
            templateLayoutPanel.PerformLayout();
            documentTypeLayout.ResumeLayout(false);
            documentTypeLayout.PerformLayout();
            documentFileLayout.ResumeLayout(false);
            documentFileLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel templateTransformLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private TabPage documentTypeTab;
        private TabPage templateTransformTab;
        private TabPage templateDocuments;
        private DataGridView templateObjectsGrid;
        private Controls.SelectTextBoxData objectPathData;
        private Controls.ComboBoxData objectScopeData;
        private CheckBox objectInModelData;
        private DataGridViewTextBoxColumn objectPathColumn;
        private DataGridViewTextBoxColumn objectScopeColumn;
        private DataGridViewCheckBoxColumn objectInModelColumn;
        private Controls.ComboBoxData transformData;
        private Controls.ComboBoxData definitionData;
        private DataGridView transformGridView;
        private DataGridViewComboBoxColumn transformColumn;
        private DataGridViewComboBoxColumn definitionColumn;
        private TableLayoutPanel documentLayout;
        private DataGridView documentGridView;
        private Controls.TextBoxData documentTitleData;
        private Controls.ComboBoxData documentTypeData;
        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData localPathData;
        private Controls.SelectTextBoxData fileNameData;
        private DataGridViewTextBoxColumn documentColumn;
        private DataGridViewComboBoxColumn documentTypeColumn;
        private Controls.ComboBoxData documentSchemaDefinitionData;
        private Controls.ComboBoxData documentTransformData;
        private Controls.ComboBoxData comboBoxData3;
        private TabControl documentTabs;
        private TabPage documentFileTab;
    }
}