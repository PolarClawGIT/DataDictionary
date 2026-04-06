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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Document));
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentSplit = new SplitContainer();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            fileNameData = new DataDictionary.Main.Controls.TextBoxData();
            filePathData = new DataDictionary.Main.Controls.TextBoxData();
            objectData = new DataDictionary.Main.Controls.ComboBoxData();
            transformData = new DataDictionary.Main.Controls.ComboBoxData();
            schemaData = new DataDictionary.Main.Controls.ComboBoxData();
            tableLayoutPanel1 = new TableLayoutPanel();
            documentContentTools = new ToolStrip();
            documentContent = new TextBox();
            bindingTemplate = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            documentSaveCommand = new ToolStripButton();
            documentOpenCommand = new ToolStripButton();
            transformSourceData = new DataDictionary.Main.Controls.TextBoxData();
            documentLayout = new TableLayoutPanel();
            documentDetailLayout = new TableLayoutPanel();
            documentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentSplit).BeginInit();
            documentSplit.Panel1.SuspendLayout();
            documentSplit.Panel2.SuspendLayout();
            documentSplit.SuspendLayout();
            documentDetailLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            documentContentTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
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
            documentLayout.Size = new Size(1012, 650);
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
            templateTitleData.Size = new Size(1006, 44);
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
            documentSplit.Size = new Size(1006, 594);
            documentSplit.SplitterDistance = 334;
            documentSplit.TabIndex = 1;
            // 
            // documentDetailLayout
            // 
            documentDetailLayout.ColumnCount = 1;
            documentDetailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentDetailLayout.Controls.Add(documentData, 0, 0);
            documentDetailLayout.Controls.Add(fileNameData, 0, 6);
            documentDetailLayout.Controls.Add(filePathData, 0, 5);
            documentDetailLayout.Controls.Add(transformData, 0, 3);
            documentDetailLayout.Controls.Add(schemaData, 0, 2);
            documentDetailLayout.Controls.Add(objectData, 0, 1);
            documentDetailLayout.Controls.Add(transformSourceData, 0, 4);
            documentDetailLayout.Dock = DockStyle.Fill;
            documentDetailLayout.Location = new Point(0, 0);
            documentDetailLayout.Name = "documentDetailLayout";
            documentDetailLayout.RowCount = 7;
            documentDetailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.RowStyles.Add(new RowStyle());
            documentDetailLayout.Size = new Size(334, 594);
            documentDetailLayout.TabIndex = 5;
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
            documentData.Size = new Size(328, 282);
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
            // fileNameData
            // 
            fileNameData.AutoSize = true;
            fileNameData.Dock = DockStyle.Fill;
            fileNameData.HeaderText = "File Name";
            fileNameData.Location = new Point(3, 547);
            fileNameData.Multiline = false;
            fileNameData.Name = "fileNameData";
            fileNameData.ReadOnly = false;
            fileNameData.Size = new Size(328, 44);
            fileNameData.TabIndex = 13;
            fileNameData.WordWrap = true;
            // 
            // filePathData
            // 
            filePathData.AutoSize = true;
            filePathData.Dock = DockStyle.Fill;
            filePathData.HeaderText = "File Path (local)";
            filePathData.Location = new Point(3, 497);
            filePathData.Multiline = false;
            filePathData.Name = "filePathData";
            filePathData.ReadOnly = false;
            filePathData.Size = new Size(328, 44);
            filePathData.TabIndex = 12;
            filePathData.WordWrap = true;
            // 
            // objectData
            // 
            objectData.AutoSize = true;
            objectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectData.Dock = DockStyle.Fill;
            objectData.DropDownStyle = ComboBoxStyle.DropDown;
            objectData.HeaderText = "Object";
            objectData.Location = new Point(3, 291);
            objectData.Name = "objectData";
            objectData.ReadOnly = false;
            objectData.Size = new Size(328, 46);
            objectData.TabIndex = 11;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformData.Dock = DockStyle.Fill;
            transformData.DropDownStyle = ComboBoxStyle.DropDown;
            transformData.HeaderText = "Transform";
            transformData.Location = new Point(3, 395);
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(328, 46);
            transformData.TabIndex = 10;
            // 
            // schemaData
            // 
            schemaData.AutoSize = true;
            schemaData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaData.Dock = DockStyle.Fill;
            schemaData.DropDownStyle = ComboBoxStyle.DropDown;
            schemaData.HeaderText = "Schema";
            schemaData.Location = new Point(3, 343);
            schemaData.Name = "schemaData";
            schemaData.ReadOnly = false;
            schemaData.Size = new Size(328, 46);
            schemaData.TabIndex = 9;
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
            tableLayoutPanel1.Size = new Size(668, 594);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // documentContentTools
            // 
            documentContentTools.Items.AddRange(new ToolStripItem[] { documentSaveCommand, documentOpenCommand });
            documentContentTools.Location = new Point(0, 0);
            documentContentTools.Name = "documentContentTools";
            documentContentTools.Size = new Size(668, 25);
            documentContentTools.TabIndex = 0;
            documentContentTools.Text = "toolStrip1";
            // 
            // documentContent
            // 
            documentContent.Dock = DockStyle.Fill;
            documentContent.Location = new Point(3, 28);
            documentContent.MaxLength = 0;
            documentContent.Multiline = true;
            documentContent.Name = "documentContent";
            documentContent.ScrollBars = ScrollBars.Both;
            documentContent.Size = new Size(662, 563);
            documentContent.TabIndex = 1;
            documentContent.WordWrap = false;
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
            // transformSourceData
            // 
            transformSourceData.AutoSize = true;
            transformSourceData.Dock = DockStyle.Fill;
            transformSourceData.HeaderText = "Transform Source Document";
            transformSourceData.Location = new Point(3, 447);
            transformSourceData.Multiline = false;
            transformSourceData.Name = "transformSourceData";
            transformSourceData.ReadOnly = true;
            transformSourceData.Size = new Size(328, 44);
            transformSourceData.TabIndex = 14;
            transformSourceData.WordWrap = true;
            // 
            // Document
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1012, 675);
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
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            documentContentTools.ResumeLayout(false);
            documentContentTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
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
        private BindingSource bindingDocument;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStrip documentContentTools;
        private TextBox documentContent;
        private SplitContainer documentSplit;
        private TabPage sourceTab;
        private ToolStripButton documentSaveCommand;
        private ToolStripButton documentOpenCommand;
        private Controls.TextBoxData transformSourceData;
    }
}