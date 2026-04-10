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
            TableLayoutPanel schemaLayout;
            GroupBox schemaRootNodeGroup;
            TableLayoutPanel rootNodeLayout;
            GroupBox filePatternGroup;
            TableLayoutPanel filePatternLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaDefinition));
            Label fileBaseName;
            rootNodeData = new DataDictionary.Main.Controls.TextBoxData();
            forEachScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            transformTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            filePrefixData = new DataDictionary.Main.Controls.TextBoxData();
            fileSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            fileExtensionData = new DataDictionary.Main.Controls.TextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            schemaLayout = new TableLayoutPanel();
            schemaRootNodeGroup = new GroupBox();
            rootNodeLayout = new TableLayoutPanel();
            filePatternGroup = new GroupBox();
            filePatternLayout = new TableLayoutPanel();
            fileBaseName = new Label();
            schemaLayout.SuspendLayout();
            schemaRootNodeGroup.SuspendLayout();
            rootNodeLayout.SuspendLayout();
            filePatternGroup.SuspendLayout();
            filePatternLayout.SuspendLayout();
            SuspendLayout();
            // 
            // schemaLayout
            // 
            schemaLayout.AutoSize = true;
            schemaLayout.ColumnCount = 1;
            schemaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaLayout.Controls.Add(filePatternGroup, 0, 3);
            schemaLayout.Controls.Add(schemaRootNodeGroup, 0, 2);
            schemaLayout.Controls.Add(transformTitleData, 0, 0);
            schemaLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaLayout.Dock = DockStyle.Fill;
            schemaLayout.Location = new Point(0, 25);
            schemaLayout.Name = "schemaLayout";
            schemaLayout.RowCount = 4;
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaLayout.Size = new Size(500, 422);
            schemaLayout.TabIndex = 4;
            // 
            // schemaRootNodeGroup
            // 
            schemaRootNodeGroup.AutoSize = true;
            schemaRootNodeGroup.Controls.Add(rootNodeLayout);
            schemaRootNodeGroup.Dock = DockStyle.Fill;
            schemaRootNodeGroup.Location = new Point(3, 103);
            schemaRootNodeGroup.Name = "schemaRootNodeGroup";
            schemaRootNodeGroup.Size = new Size(494, 126);
            schemaRootNodeGroup.TabIndex = 5;
            schemaRootNodeGroup.TabStop = false;
            schemaRootNodeGroup.Text = "Root Node";
            // 
            // rootNodeLayout
            // 
            rootNodeLayout.AutoSize = true;
            rootNodeLayout.ColumnCount = 1;
            rootNodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            rootNodeLayout.Controls.Add(rootNodeData, 0, 1);
            rootNodeLayout.Controls.Add(forEachScopeData, 0, 0);
            rootNodeLayout.Dock = DockStyle.Fill;
            rootNodeLayout.Location = new Point(3, 19);
            rootNodeLayout.Name = "rootNodeLayout";
            rootNodeLayout.RowCount = 2;
            rootNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            rootNodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            rootNodeLayout.Size = new Size(488, 104);
            rootNodeLayout.TabIndex = 0;
            // 
            // rootNodeData
            // 
            rootNodeData.AutoSize = true;
            rootNodeData.Dock = DockStyle.Fill;
            rootNodeData.HeaderText = "Root Node Name (override)";
            rootNodeData.Location = new Point(3, 55);
            rootNodeData.Multiline = false;
            rootNodeData.Name = "rootNodeData";
            rootNodeData.ReadOnly = false;
            rootNodeData.Size = new Size(482, 46);
            rootNodeData.TabIndex = 0;
            rootNodeData.WordWrap = true;
            // 
            // forEachScopeData
            // 
            forEachScopeData.AutoSize = true;
            forEachScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            forEachScopeData.Dock = DockStyle.Fill;
            forEachScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            forEachScopeData.HeaderText = "Root Node Scope";
            forEachScopeData.Location = new Point(3, 3);
            forEachScopeData.Name = "forEachScopeData";
            forEachScopeData.ReadOnly = false;
            forEachScopeData.Size = new Size(482, 46);
            forEachScopeData.TabIndex = 1;
            // 
            // transformTitleData
            // 
            transformTitleData.AutoSize = true;
            transformTitleData.Dock = DockStyle.Fill;
            transformTitleData.HeaderText = "Transform";
            transformTitleData.Location = new Point(3, 3);
            transformTitleData.Multiline = false;
            transformTitleData.Name = "transformTitleData";
            transformTitleData.ReadOnly = true;
            transformTitleData.Size = new Size(494, 44);
            transformTitleData.TabIndex = 0;
            transformTitleData.WordWrap = true;
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
            schemaTitleData.Size = new Size(494, 44);
            schemaTitleData.TabIndex = 1;
            schemaTitleData.WordWrap = true;
            // 
            // filePatternGroup
            // 
            filePatternGroup.AutoSize = true;
            filePatternGroup.Controls.Add(filePatternLayout);
            filePatternGroup.Dock = DockStyle.Fill;
            filePatternGroup.Location = new Point(3, 235);
            filePatternGroup.Name = "filePatternGroup";
            filePatternGroup.Size = new Size(494, 184);
            filePatternGroup.TabIndex = 5;
            filePatternGroup.TabStop = false;
            filePatternGroup.Text = "File Pattern";
            // 
            // filePatternLayout
            // 
            filePatternLayout.AutoSize = true;
            filePatternLayout.ColumnCount = 4;
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
            filePatternLayout.ColumnStyles.Add(new ColumnStyle());
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
            filePatternLayout.Size = new Size(488, 162);
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
            rootFolderData.Size = new Size(219, 46);
            rootFolderData.TabIndex = 0;
            // 
            // relativePathData
            // 
            relativePathData.AutoSize = true;
            relativePathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            filePatternLayout.SetColumnSpan(relativePathData, 2);
            relativePathData.Dock = DockStyle.Fill;
            relativePathData.HeaderText = "Relative Path";
            relativePathData.Location = new Point(228, 3);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(257, 46);
            relativePathData.TabIndex = 1;
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
            filePrefixData.Size = new Size(120, 54);
            filePrefixData.TabIndex = 2;
            filePrefixData.WordWrap = true;
            // 
            // fileBaseName
            // 
            fileBaseName.Anchor = AnchorStyles.None;
            fileBaseName.AutoSize = true;
            fileBaseName.Location = new Point(129, 124);
            fileBaseName.Name = "fileBaseName";
            fileBaseName.Size = new Size(93, 15);
            fileBaseName.TabIndex = 5;
            fileBaseName.Text = "<objectName/>";
            // 
            // fileSuffixData
            // 
            fileSuffixData.AutoSize = true;
            fileSuffixData.Dock = DockStyle.Fill;
            fileSuffixData.HeaderText = "File Suffix";
            fileSuffixData.Location = new Point(228, 105);
            fileSuffixData.Multiline = false;
            fileSuffixData.Name = "fileSuffixData";
            fileSuffixData.ReadOnly = false;
            fileSuffixData.Size = new Size(120, 54);
            fileSuffixData.TabIndex = 3;
            fileSuffixData.WordWrap = true;
            // 
            // fileExtensionData
            // 
            fileExtensionData.AutoSize = true;
            fileExtensionData.Dock = DockStyle.Fill;
            fileExtensionData.HeaderText = "File Extension";
            fileExtensionData.Location = new Point(354, 105);
            fileExtensionData.Multiline = false;
            fileExtensionData.Name = "fileExtensionData";
            fileExtensionData.ReadOnly = false;
            fileExtensionData.Size = new Size(131, 54);
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
            localPathData.Size = new Size(482, 44);
            localPathData.TabIndex = 7;
            localPathData.WordWrap = false;
            // 
            // SchemaDefinition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 447);
            Controls.Add(schemaLayout);
            Name = "SchemaDefinition";
            Text = "SchemaDefinition";
            Load += SchemaDefinition_Load;
            Controls.SetChildIndex(schemaLayout, 0);
            schemaLayout.ResumeLayout(false);
            schemaLayout.PerformLayout();
            schemaRootNodeGroup.ResumeLayout(false);
            schemaRootNodeGroup.PerformLayout();
            rootNodeLayout.ResumeLayout(false);
            rootNodeLayout.PerformLayout();
            filePatternGroup.ResumeLayout(false);
            filePatternGroup.PerformLayout();
            filePatternLayout.ResumeLayout(false);
            filePatternLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel schemaLayout;
        private Controls.TextBoxData transformTitleData;
        private Controls.TextBoxData schemaTitleData;
        private Controls.TextBoxData rootNodeData;
        private Controls.ComboBoxData forEachScopeData;
        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData filePrefixData;
        private Controls.TextBoxData fileSuffixData;
        private Label fileBaseName;
        private Controls.TextBoxData fileExtensionData;
        private Controls.TextBoxData localPathData;
    }
}