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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Template));
            TabPage templateTransformTab;
            TableLayoutPanel templateTransformLayout;
            TableLayoutPanel templateLayoutPanel;
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
            documentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            documentTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            documentObjectData = new DataDictionary.Main.Controls.TextBoxData();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            fileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentColumn = new DataGridViewTextBoxColumn();
            documentTypeColumn = new DataGridViewComboBoxColumn();
            templateTabs = new TabControl();
            templateObjectTab = new TabPage();
            templateObjectLayoutPanel = new TableLayoutPanel();
            templateTransformTab = new TabPage();
            templateTransformLayout = new TableLayoutPanel();
            templateLayoutPanel = new TableLayoutPanel();
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
            templateLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // templateTabs
            // 
            templateTabs.Controls.Add(templateObjectTab);
            templateTabs.Controls.Add(templateTransformTab);
            templateTabs.Controls.Add(templateDocuments);
            templateTabs.Dock = DockStyle.Fill;
            templateTabs.Location = new Point(3, 199);
            templateTabs.Name = "templateTabs";
            templateTabs.SelectedIndex = 0;
            templateTabs.Size = new Size(560, 435);
            templateTabs.TabIndex = 2;
            // 
            // templateObjectTab
            // 
            templateObjectTab.BackColor = SystemColors.Control;
            templateObjectTab.Controls.Add(templateObjectLayoutPanel);
            templateObjectTab.Location = new Point(4, 24);
            templateObjectTab.Name = "templateObjectTab";
            templateObjectTab.Padding = new Padding(3);
            templateObjectTab.Size = new Size(552, 407);
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
            templateObjectLayoutPanel.Size = new Size(546, 401);
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
            templateObjectsGrid.Size = new Size(540, 293);
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
            objectPathData.Location = new Point(3, 302);
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
            objectScopeData.Location = new Point(3, 352);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = true;
            objectScopeData.Size = new Size(461, 46);
            objectScopeData.TabIndex = 2;
            // 
            // objectInModelData
            // 
            objectInModelData.AutoSize = true;
            objectInModelData.Location = new Point(470, 352);
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
            templateTransformTab.Size = new Size(552, 407);
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
            templateTransformLayout.Size = new Size(546, 401);
            templateTransformLayout.TabIndex = 0;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformData.Dock = DockStyle.Fill;
            transformData.DropDownStyle = ComboBoxStyle.DropDown;
            transformData.HeaderText = "Transform (XSLT)";
            transformData.Location = new Point(3, 300);
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(540, 46);
            transformData.TabIndex = 0;
            // 
            // definitionData
            // 
            definitionData.AutoSize = true;
            definitionData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            definitionData.Dock = DockStyle.Fill;
            definitionData.DropDownStyle = ComboBoxStyle.DropDown;
            definitionData.HeaderText = "Definition (XSD)";
            definitionData.Location = new Point(3, 352);
            definitionData.Name = "definitionData";
            definitionData.ReadOnly = false;
            definitionData.Size = new Size(540, 46);
            definitionData.TabIndex = 1;
            // 
            // transformGridView
            // 
            transformGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transformGridView.Columns.AddRange(new DataGridViewColumn[] { transformColumn, definitionColumn });
            transformGridView.Dock = DockStyle.Fill;
            transformGridView.Location = new Point(3, 3);
            transformGridView.Name = "transformGridView";
            transformGridView.Size = new Size(540, 291);
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
            templateDocuments.Size = new Size(552, 407);
            templateDocuments.TabIndex = 2;
            templateDocuments.Text = "Documents";
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 2;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.Controls.Add(documentGridView, 0, 0);
            documentLayout.Controls.Add(documentTitleData, 0, 1);
            documentLayout.Controls.Add(documentTypeData, 1, 1);
            documentLayout.Controls.Add(documentObjectData, 0, 2);
            documentLayout.Controls.Add(rootFolderData, 0, 3);
            documentLayout.Controls.Add(relativePathData, 1, 3);
            documentLayout.Controls.Add(localPathData, 0, 4);
            documentLayout.Controls.Add(fileNameData, 0, 5);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 0);
            documentLayout.Name = "documentLayout";
            documentLayout.Padding = new Padding(3);
            documentLayout.RowCount = 6;
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.Size = new Size(552, 407);
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
            documentGridView.Size = new Size(540, 141);
            documentGridView.TabIndex = 0;
            // 
            // documentTitleData
            // 
            documentTitleData.AutoSize = true;
            documentTitleData.Dock = DockStyle.Fill;
            documentTitleData.HeaderText = "Title";
            documentTitleData.Location = new Point(6, 153);
            documentTitleData.Multiline = false;
            documentTitleData.Name = "documentTitleData";
            documentTitleData.ReadOnly = false;
            documentTitleData.Size = new Size(267, 46);
            documentTitleData.TabIndex = 1;
            documentTitleData.WordWrap = true;
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
            templateLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            templateLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            templateLayoutPanel.Size = new Size(566, 637);
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
            templateDescriptionData.Size = new Size(560, 140);
            templateDescriptionData.TabIndex = 1;
            templateDescriptionData.WordWrap = true;
            // 
            // documentTypeData
            // 
            documentTypeData.AutoSize = true;
            documentTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentTypeData.Dock = DockStyle.Fill;
            documentTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            documentTypeData.HeaderText = "Type";
            documentTypeData.Location = new Point(279, 153);
            documentTypeData.Name = "documentTypeData";
            documentTypeData.ReadOnly = false;
            documentTypeData.Size = new Size(267, 46);
            documentTypeData.TabIndex = 2;
            // 
            // documentObjectData
            // 
            documentObjectData.AutoSize = true;
            documentLayout.SetColumnSpan(documentObjectData, 2);
            documentObjectData.Dock = DockStyle.Fill;
            documentObjectData.HeaderText = "Object";
            documentObjectData.Location = new Point(6, 205);
            documentObjectData.Multiline = false;
            documentObjectData.Name = "documentObjectData";
            documentObjectData.ReadOnly = true;
            documentObjectData.Size = new Size(540, 44);
            documentObjectData.TabIndex = 3;
            documentObjectData.WordWrap = true;
            // 
            // rootFolderData
            // 
            rootFolderData.AutoSize = true;
            rootFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootFolderData.Dock = DockStyle.Fill;
            rootFolderData.DropDownStyle = ComboBoxStyle.DropDown;
            rootFolderData.HeaderText = "Root Folder";
            rootFolderData.Location = new Point(6, 255);
            rootFolderData.Name = "rootFolderData";
            rootFolderData.ReadOnly = false;
            rootFolderData.Size = new Size(267, 46);
            rootFolderData.TabIndex = 4;
            // 
            // relativePathData
            // 
            relativePathData.AutoSize = true;
            relativePathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            relativePathData.Dock = DockStyle.Fill;
            relativePathData.HeaderText = "Relative Path";
            relativePathData.Location = new Point(279, 255);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(267, 46);
            relativePathData.TabIndex = 5;
            // 
            // localPathData
            // 
            localPathData.AutoSize = true;
            documentLayout.SetColumnSpan(localPathData, 2);
            localPathData.Dock = DockStyle.Fill;
            localPathData.HeaderText = "Local Path";
            localPathData.Location = new Point(6, 307);
            localPathData.Multiline = false;
            localPathData.Name = "localPathData";
            localPathData.ReadOnly = true;
            localPathData.Size = new Size(540, 44);
            localPathData.TabIndex = 6;
            localPathData.WordWrap = true;
            // 
            // fileNameData
            // 
            fileNameData.AutoSize = true;
            fileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentLayout.SetColumnSpan(fileNameData, 2);
            fileNameData.Dock = DockStyle.Fill;
            fileNameData.HeaderText = "File Name";
            fileNameData.Location = new Point(6, 357);
            fileNameData.Name = "fileNameData";
            fileNameData.ReadOnly = false;
            fileNameData.SelectIcon = (Image)resources.GetObject("fileNameData.SelectIcon");
            fileNameData.Size = new Size(540, 44);
            fileNameData.TabIndex = 7;
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
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(566, 662);
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
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentGridView).EndInit();
            templateLayoutPanel.ResumeLayout(false);
            templateLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel templateTransformLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private TabPage tabPage1;
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
        private Controls.TextBoxData documentObjectData;
        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData localPathData;
        private Controls.SelectTextBoxData fileNameData;
        private DataGridViewTextBoxColumn documentColumn;
        private DataGridViewComboBoxColumn documentTypeColumn;
    }
}