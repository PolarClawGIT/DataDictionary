namespace DataDictionary.Main.Forms.Scripting
{
    partial class Document
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
            TableLayoutPanel documentLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Document));
            GroupBox documentTypeGroup;
            TableLayoutPanel documentTypeLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentFileContentData = new DataDictionary.Main.Controls.TextBoxData();
            documentFileNameData = new DataDictionary.Main.Controls.TextBoxData();
            documentLocalPath = new DataDictionary.Main.Controls.TextBoxData();
            schemaTypeData = new CheckBox();
            transformTypeData = new CheckBox();
            bindingTemplate = new BindingSource(components);
            documentCcontextMenu = new ContextMenuStrip(components);
            documentOpenSchemaCommand = new ToolStripMenuItem();
            documentOpenTransformCommand = new ToolStripMenuItem();
            documentLayout = new TableLayoutPanel();
            documentTypeGroup = new GroupBox();
            documentTypeLayout = new TableLayoutPanel();
            documentLayout.SuspendLayout();
            documentTypeGroup.SuspendLayout();
            documentTypeLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            documentCcontextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 2;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentLayout.Controls.Add(templateTitleData, 0, 0);
            documentLayout.Controls.Add(rootFolderData, 0, 1);
            documentLayout.Controls.Add(relativePathData, 1, 1);
            documentLayout.Controls.Add(documentFileContentData, 0, 4);
            documentLayout.Controls.Add(documentFileNameData, 0, 2);
            documentLayout.Controls.Add(documentLocalPath, 0, 3);
            documentLayout.Controls.Add(documentTypeGroup, 1, 2);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 25);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 5;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.Size = new Size(816, 565);
            documentLayout.TabIndex = 4;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            documentLayout.SetColumnSpan(templateTitleData, 2);
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = true;
            templateTitleData.Size = new Size(810, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // rootFolderData
            // 
            rootFolderData.AutoSize = true;
            rootFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootFolderData.Dock = DockStyle.Fill;
            rootFolderData.DropDownStyle = ComboBoxStyle.DropDown;
            rootFolderData.HeaderText = "Root Folder";
            rootFolderData.Location = new Point(3, 53);
            rootFolderData.Name = "rootFolderData";
            rootFolderData.ReadOnly = false;
            rootFolderData.Size = new Size(402, 46);
            rootFolderData.TabIndex = 1;
            // 
            // relativePathData
            // 
            relativePathData.AutoSize = true;
            relativePathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            relativePathData.Dock = DockStyle.Fill;
            relativePathData.HeaderText = "Relative Path";
            relativePathData.Location = new Point(411, 53);
            relativePathData.Name = "relativePathData";
            relativePathData.ReadOnly = false;
            relativePathData.SelectIcon = (Image)resources.GetObject("relativePathData.SelectIcon");
            relativePathData.Size = new Size(402, 46);
            relativePathData.TabIndex = 2;
            // 
            // documentFileContentData
            // 
            documentFileContentData.AutoSize = true;
            documentLayout.SetColumnSpan(documentFileContentData, 2);
            documentFileContentData.Dock = DockStyle.Fill;
            documentFileContentData.HeaderText = "File Content";
            documentFileContentData.Location = new Point(3, 208);
            documentFileContentData.Multiline = true;
            documentFileContentData.Name = "documentFileContentData";
            documentFileContentData.ReadOnly = false;
            documentFileContentData.Size = new Size(810, 354);
            documentFileContentData.TabIndex = 5;
            documentFileContentData.WordWrap = false;
            // 
            // documentFileNameData
            // 
            documentFileNameData.AutoSize = true;
            documentFileNameData.Dock = DockStyle.Fill;
            documentFileNameData.HeaderText = "File Name";
            documentFileNameData.Location = new Point(3, 105);
            documentFileNameData.Multiline = false;
            documentFileNameData.Name = "documentFileNameData";
            documentFileNameData.ReadOnly = false;
            documentFileNameData.Size = new Size(402, 47);
            documentFileNameData.TabIndex = 3;
            documentFileNameData.WordWrap = true;
            // 
            // documentLocalPath
            // 
            documentLocalPath.AutoSize = true;
            documentLayout.SetColumnSpan(documentLocalPath, 2);
            documentLocalPath.Dock = DockStyle.Fill;
            documentLocalPath.HeaderText = "Local Path";
            documentLocalPath.Location = new Point(3, 158);
            documentLocalPath.Multiline = false;
            documentLocalPath.Name = "documentLocalPath";
            documentLocalPath.ReadOnly = true;
            documentLocalPath.Size = new Size(810, 44);
            documentLocalPath.TabIndex = 4;
            documentLocalPath.WordWrap = true;
            // 
            // documentTypeGroup
            // 
            documentTypeGroup.AutoSize = true;
            documentTypeGroup.Controls.Add(documentTypeLayout);
            documentTypeGroup.Dock = DockStyle.Fill;
            documentTypeGroup.Location = new Point(411, 105);
            documentTypeGroup.Name = "documentTypeGroup";
            documentTypeGroup.Size = new Size(402, 47);
            documentTypeGroup.TabIndex = 6;
            documentTypeGroup.TabStop = false;
            documentTypeGroup.Text = "Document Type";
            // 
            // documentTypeLayout
            // 
            documentTypeLayout.AutoSize = true;
            documentTypeLayout.ColumnCount = 2;
            documentTypeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentTypeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            documentTypeLayout.Controls.Add(schemaTypeData, 0, 0);
            documentTypeLayout.Controls.Add(transformTypeData, 1, 0);
            documentTypeLayout.Dock = DockStyle.Fill;
            documentTypeLayout.Location = new Point(3, 19);
            documentTypeLayout.Name = "documentTypeLayout";
            documentTypeLayout.RowCount = 1;
            documentTypeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            documentTypeLayout.Size = new Size(396, 25);
            documentTypeLayout.TabIndex = 0;
            // 
            // schemaTypeData
            // 
            schemaTypeData.AutoSize = true;
            schemaTypeData.Location = new Point(3, 3);
            schemaTypeData.Name = "schemaTypeData";
            schemaTypeData.Size = new Size(100, 19);
            schemaTypeData.TabIndex = 0;
            schemaTypeData.Text = "Schema result";
            schemaTypeData.UseVisualStyleBackColor = true;
            // 
            // transformTypeData
            // 
            transformTypeData.AutoSize = true;
            transformTypeData.Location = new Point(201, 3);
            transformTypeData.Name = "transformTypeData";
            transformTypeData.Size = new Size(112, 19);
            transformTypeData.TabIndex = 1;
            transformTypeData.Text = "Transform result";
            transformTypeData.UseVisualStyleBackColor = true;
            // 
            // documentCcontextMenu
            // 
            documentCcontextMenu.Items.AddRange(new ToolStripItem[] { documentOpenSchemaCommand, documentOpenTransformCommand });
            documentCcontextMenu.Name = "documentCcontextMenu";
            documentCcontextMenu.Size = new Size(220, 48);
            // 
            // documentOpenSchemaCommand
            // 
            documentOpenSchemaCommand.Name = "documentOpenSchemaCommand";
            documentOpenSchemaCommand.Size = new Size(219, 22);
            documentOpenSchemaCommand.Text = "Open Schema Document";
            documentOpenSchemaCommand.Click += DocumentOpenSchemaCommand_Click;
            // 
            // documentOpenTransformCommand
            // 
            documentOpenTransformCommand.Name = "documentOpenTransformCommand";
            documentOpenTransformCommand.Size = new Size(219, 22);
            documentOpenTransformCommand.Text = "Open Transform Document";
            documentOpenTransformCommand.Click += DocumentOpenTransformCommand_Click;
            // 
            // Document
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 590);
            Controls.Add(documentLayout);
            Name = "Document";
            Text = "Document";
            Load += Document_Load;
            Controls.SetChildIndex(documentLayout, 0);
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            documentTypeGroup.ResumeLayout(false);
            documentTypeGroup.PerformLayout();
            documentTypeLayout.ResumeLayout(false);
            documentTypeLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            documentCcontextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel documentLayout;
        private Controls.TextBoxData templateTitleData;
        private BindingSource bindingTemplate;
        private TabPage sourceTab;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData documentFileContentData;
        private Controls.TextBoxData documentFileNameData;
        private Controls.TextBoxData documentLocalPath;
        private CheckBox schemaTypeData;
        private CheckBox transformTypeData;
        private ContextMenuStrip documentCcontextMenu;
        private ToolStripMenuItem documentOpenSchemaCommand;
        private ToolStripMenuItem documentOpenTransformCommand;
    }
}