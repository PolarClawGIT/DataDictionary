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
            TableLayoutPanel documentDetailLayout;
            TableLayoutPanel schemaDocumentLayout;
            TableLayoutPanel documentTransformLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Document));
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentSplit = new SplitContainer();
            documentTab = new TabControl();
            schemaTab = new TabPage();
            schemaData = new DataDictionary.Main.Controls.ComboBoxData();
            objectData = new DataDictionary.Main.Controls.ComboBoxData();
            transformTab = new TabPage();
            transformData = new DataDictionary.Main.Controls.ComboBoxData();
            schemaDocumentData = new DataDictionary.Main.Controls.ComboBoxData();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            filePathData = new DataDictionary.Main.Controls.TextBoxData();
            fileNameData = new DataDictionary.Main.Controls.TextBoxData();
            tableLayoutPanel1 = new TableLayoutPanel();
            documentContentTools = new ToolStrip();
            documentSaveCommand = new ToolStripButton();
            documentOpenCommand = new ToolStripButton();
            documentContent = new TextBox();
            bindingTemplate = new BindingSource(components);
            documentLayout = new TableLayoutPanel();
            documentDetailLayout = new TableLayoutPanel();
            schemaDocumentLayout = new TableLayoutPanel();
            documentTransformLayout = new TableLayoutPanel();
            documentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentSplit).BeginInit();
            documentSplit.Panel1.SuspendLayout();
            documentSplit.Panel2.SuspendLayout();
            documentSplit.SuspendLayout();
            documentDetailLayout.SuspendLayout();
            documentTab.SuspendLayout();
            schemaTab.SuspendLayout();
            schemaDocumentLayout.SuspendLayout();
            transformTab.SuspendLayout();
            documentTransformLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            documentContentTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            SuspendLayout();
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 1;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.Controls.Add(templateTitleData, 0, 0);
            documentLayout.Controls.Add(documentSplit, 0, 1);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 25);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 2;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 28.5714283F));
            documentLayout.Size = new Size(816, 565);
            documentLayout.TabIndex = 4;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
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
            // documentSplit
            // 
            documentSplit.Dock = DockStyle.Fill;
            documentSplit.Location = new Point(3, 53);
            documentSplit.Name = "documentSplit";
            // 
            // documentSplit.Panel1
            // 
            documentSplit.Panel1.Controls.Add(documentDetailLayout);
            // 
            // documentSplit.Panel2
            // 
            documentSplit.Panel2.Controls.Add(tableLayoutPanel1);
            documentSplit.Size = new Size(810, 509);
            documentSplit.SplitterDistance = 268;
            documentSplit.TabIndex = 1;
            // 
            // documentDetailLayout
            // 
            documentDetailLayout.ColumnCount = 1;
            documentDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentDetailLayout.Controls.Add(documentTab, 0, 3);
            documentDetailLayout.Controls.Add(documentData, 0, 0);
            documentDetailLayout.Controls.Add(filePathData, 0, 1);
            documentDetailLayout.Controls.Add(fileNameData, 0, 2);
            documentDetailLayout.Dock = DockStyle.Fill;
            documentDetailLayout.Location = new Point(0, 0);
            documentDetailLayout.Name = "documentDetailLayout";
            documentDetailLayout.RowCount = 4;
            documentDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.Size = new Size(268, 509);
            documentDetailLayout.TabIndex = 5;
            // 
            // documentTab
            // 
            documentTab.Controls.Add(schemaTab);
            documentTab.Controls.Add(transformTab);
            documentTab.Dock = DockStyle.Fill;
            documentTab.Location = new Point(3, 335);
            documentTab.Name = "documentTab";
            documentTab.SelectedIndex = 0;
            documentTab.Size = new Size(262, 171);
            documentTab.TabIndex = 5;
            // 
            // schemaTab
            // 
            schemaTab.BackColor = SystemColors.Control;
            schemaTab.Controls.Add(schemaDocumentLayout);
            schemaTab.Location = new Point(4, 24);
            schemaTab.Name = "schemaTab";
            schemaTab.Padding = new Padding(3);
            schemaTab.Size = new Size(254, 143);
            schemaTab.TabIndex = 0;
            schemaTab.Text = "Schema";
            // 
            // schemaDocumentLayout
            // 
            schemaDocumentLayout.ColumnCount = 1;
            schemaDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaDocumentLayout.Controls.Add(schemaData, 0, 0);
            schemaDocumentLayout.Controls.Add(objectData, 0, 1);
            schemaDocumentLayout.Dock = DockStyle.Fill;
            schemaDocumentLayout.Location = new Point(3, 3);
            schemaDocumentLayout.Name = "schemaDocumentLayout";
            schemaDocumentLayout.RowCount = 2;
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.Size = new Size(248, 137);
            schemaDocumentLayout.TabIndex = 0;
            // 
            // schemaData
            // 
            schemaData.AutoSize = true;
            schemaData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaData.Dock = DockStyle.Fill;
            schemaData.DropDownStyle = ComboBoxStyle.DropDown;
            schemaData.HeaderText = "Schema";
            schemaData.Location = new Point(3, 3);
            schemaData.Name = "schemaData";
            schemaData.ReadOnly = false;
            schemaData.Size = new Size(242, 46);
            schemaData.TabIndex = 9;
            // 
            // objectData
            // 
            objectData.AutoSize = true;
            objectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectData.Dock = DockStyle.Fill;
            objectData.DropDownStyle = ComboBoxStyle.DropDown;
            objectData.HeaderText = "Object";
            objectData.Location = new Point(3, 55);
            objectData.Name = "objectData";
            objectData.ReadOnly = false;
            objectData.Size = new Size(242, 79);
            objectData.TabIndex = 11;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(documentTransformLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(192, 72);
            transformTab.TabIndex = 1;
            transformTab.Text = "Transform";
            // 
            // documentTransformLayout
            // 
            documentTransformLayout.ColumnCount = 1;
            documentTransformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentTransformLayout.Controls.Add(transformData, 0, 1);
            documentTransformLayout.Controls.Add(schemaDocumentData, 0, 0);
            documentTransformLayout.Dock = DockStyle.Fill;
            documentTransformLayout.Location = new Point(3, 3);
            documentTransformLayout.Name = "documentTransformLayout";
            documentTransformLayout.RowCount = 2;
            documentTransformLayout.RowStyles.Add(new RowStyle());
            documentTransformLayout.RowStyles.Add(new RowStyle());
            documentTransformLayout.Size = new Size(186, 66);
            documentTransformLayout.TabIndex = 0;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformData.Dock = DockStyle.Fill;
            transformData.DropDownStyle = ComboBoxStyle.DropDown;
            transformData.HeaderText = "Transform";
            transformData.Location = new Point(3, 55);
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(180, 46);
            transformData.TabIndex = 10;
            // 
            // schemaDocumentData
            // 
            schemaDocumentData.AutoSize = true;
            schemaDocumentData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaDocumentData.Dock = DockStyle.Fill;
            schemaDocumentData.DropDownStyle = ComboBoxStyle.DropDown;
            schemaDocumentData.HeaderText = "Source  (XML)";
            schemaDocumentData.Location = new Point(3, 3);
            schemaDocumentData.Name = "schemaDocumentData";
            schemaDocumentData.ReadOnly = false;
            schemaDocumentData.Size = new Size(180, 46);
            schemaDocumentData.TabIndex = 11;
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { FileNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 3);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(262, 226);
            documentData.TabIndex = 8;
            // 
            // FileNameColumn
            // 
            FileNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileNameColumn.DataPropertyName = "FileName";
            FileNameColumn.HeaderText = "File Name";
            FileNameColumn.Name = "FileNameColumn";
            FileNameColumn.ReadOnly = true;
            // 
            // filePathData
            // 
            filePathData.AutoSize = true;
            filePathData.Dock = DockStyle.Fill;
            filePathData.HeaderText = "File Path (local)";
            filePathData.Location = new Point(3, 235);
            filePathData.Multiline = false;
            filePathData.Name = "filePathData";
            filePathData.ReadOnly = false;
            filePathData.Size = new Size(262, 44);
            filePathData.TabIndex = 12;
            filePathData.WordWrap = true;
            // 
            // fileNameData
            // 
            fileNameData.AutoSize = true;
            fileNameData.Dock = DockStyle.Fill;
            fileNameData.HeaderText = "File Name";
            fileNameData.Location = new Point(3, 285);
            fileNameData.Multiline = false;
            fileNameData.Name = "fileNameData";
            fileNameData.ReadOnly = false;
            fileNameData.Size = new Size(262, 44);
            fileNameData.TabIndex = 13;
            fileNameData.WordWrap = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(documentContentTools, 0, 0);
            tableLayoutPanel1.Controls.Add(documentContent, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(538, 509);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // documentContentTools
            // 
            documentContentTools.Items.AddRange(new ToolStripItem[] { documentSaveCommand, documentOpenCommand });
            documentContentTools.Location = new Point(0, 0);
            documentContentTools.Name = "documentContentTools";
            documentContentTools.Size = new Size(538, 25);
            documentContentTools.TabIndex = 0;
            documentContentTools.Text = "toolStrip1";
            // 
            // documentSaveCommand
            // 
            documentSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentSaveCommand.Image = (Image)resources.GetObject("documentSaveCommand.Image");
            documentSaveCommand.ImageTransparentColor = Color.Magenta;
            documentSaveCommand.Name = "documentSaveCommand";
            documentSaveCommand.Size = new Size(23, 22);
            documentSaveCommand.Text = "Save";
            // 
            // documentOpenCommand
            // 
            documentOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentOpenCommand.Image = (Image)resources.GetObject("documentOpenCommand.Image");
            documentOpenCommand.ImageTransparentColor = Color.Magenta;
            documentOpenCommand.Name = "documentOpenCommand";
            documentOpenCommand.Size = new Size(23, 22);
            documentOpenCommand.Text = "Open";
            // 
            // documentContent
            // 
            documentContent.Dock = DockStyle.Fill;
            documentContent.Location = new Point(3, 28);
            documentContent.MaxLength = 0;
            documentContent.Multiline = true;
            documentContent.Name = "documentContent";
            documentContent.ScrollBars = ScrollBars.Both;
            documentContent.Size = new Size(532, 478);
            documentContent.TabIndex = 1;
            documentContent.WordWrap = false;
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
            documentSplit.Panel1.ResumeLayout(false);
            documentSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)documentSplit).EndInit();
            documentSplit.ResumeLayout(false);
            documentDetailLayout.ResumeLayout(false);
            documentDetailLayout.PerformLayout();
            documentTab.ResumeLayout(false);
            schemaTab.ResumeLayout(false);
            schemaDocumentLayout.ResumeLayout(false);
            schemaDocumentLayout.PerformLayout();
            transformTab.ResumeLayout(false);
            documentTransformLayout.ResumeLayout(false);
            documentTransformLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            documentContentTools.ResumeLayout(false);
            documentContentTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel documentLayout;
        private Controls.TextBoxData templateTitleData;
        private DataGridView documentData;
        private DataGridViewTextBoxColumn FileNameColumn;
        private Controls.ComboBoxData schemaData;
        private Controls.ComboBoxData transformData;
        private Controls.ComboBoxData objectData;
        private Controls.TextBoxData filePathData;
        private Controls.TextBoxData fileNameData;
        private BindingSource bindingTemplate;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStrip documentContentTools;
        private TextBox documentContent;
        private SplitContainer documentSplit;
        private TabPage sourceTab;
        private ToolStripButton documentSaveCommand;
        private ToolStripButton documentOpenCommand;
        private TabControl documentTab;
        private TabPage schemaTab;
        private TableLayoutPanel tableLayoutPanel2;
        private TabPage transformTab;
        private TableLayoutPanel tableLayoutPanel3;
        private Controls.ComboBoxData schemaDocumentData;
    }
}