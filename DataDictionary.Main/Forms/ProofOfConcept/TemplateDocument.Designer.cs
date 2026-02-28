namespace DataDictionary.Main.Forms.ProofOfConcept
{
    partial class TemplateDocument
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
            TableLayoutPanel templateDocumentLayout;
            GroupBox documentTypeGroup;
            TableLayoutPanel documentTypeLayout;
            GroupBox documentFileGroup;
            TableLayoutPanel documentFileLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateDocument));
            documentTypeData = new DataDictionary.Main.Controls.ComboBoxData();
            documentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentGridView = new DataGridView();
            documentColumn = new DataGridViewTextBoxColumn();
            documentTypeColumn = new DataGridViewComboBoxColumn();
            documentTransformData = new DataDictionary.Main.Controls.ComboBoxData();
            documentSchemaDefinitionData = new DataDictionary.Main.Controls.ComboBoxData();
            comboBoxData3 = new DataDictionary.Main.Controls.ComboBoxData();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            fileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            templateDocumentLayout = new TableLayoutPanel();
            documentTypeGroup = new GroupBox();
            documentTypeLayout = new TableLayoutPanel();
            documentFileGroup = new GroupBox();
            documentFileLayout = new TableLayoutPanel();
            templateDocumentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentGridView).BeginInit();
            documentTypeGroup.SuspendLayout();
            documentTypeLayout.SuspendLayout();
            documentFileGroup.SuspendLayout();
            documentFileLayout.SuspendLayout();
            SuspendLayout();
            // 
            // templateDocumentLayout
            // 
            templateDocumentLayout.ColumnCount = 2;
            templateDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            templateDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            templateDocumentLayout.Controls.Add(documentTypeData, 1, 2);
            templateDocumentLayout.Controls.Add(documentTitleData, 0, 2);
            templateDocumentLayout.Controls.Add(templateTitleData, 0, 0);
            templateDocumentLayout.Controls.Add(documentGridView, 0, 1);
            templateDocumentLayout.Controls.Add(documentTypeGroup, 0, 3);
            templateDocumentLayout.Controls.Add(documentFileGroup, 0, 4);
            templateDocumentLayout.Dock = DockStyle.Fill;
            templateDocumentLayout.Location = new Point(0, 25);
            templateDocumentLayout.Name = "templateDocumentLayout";
            templateDocumentLayout.RowCount = 5;
            templateDocumentLayout.RowStyles.Add(new RowStyle());
            templateDocumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            templateDocumentLayout.RowStyles.Add(new RowStyle());
            templateDocumentLayout.RowStyles.Add(new RowStyle());
            templateDocumentLayout.RowStyles.Add(new RowStyle());
            templateDocumentLayout.Size = new Size(563, 691);
            templateDocumentLayout.TabIndex = 4;
            // 
            // documentTypeData
            // 
            documentTypeData.AutoSize = true;
            documentTypeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentTypeData.Dock = DockStyle.Fill;
            documentTypeData.DropDownStyle = ComboBoxStyle.DropDown;
            documentTypeData.HeaderText = "Type";
            documentTypeData.Location = new Point(284, 228);
            documentTypeData.Name = "documentTypeData";
            documentTypeData.ReadOnly = false;
            documentTypeData.Size = new Size(276, 46);
            documentTypeData.TabIndex = 2;
            // 
            // documentTitleData
            // 
            documentTitleData.AutoSize = true;
            documentTitleData.Dock = DockStyle.Fill;
            documentTitleData.HeaderText = "Title";
            documentTitleData.Location = new Point(3, 228);
            documentTitleData.Multiline = false;
            documentTitleData.Name = "documentTitleData";
            documentTitleData.ReadOnly = false;
            documentTitleData.Size = new Size(275, 46);
            documentTitleData.TabIndex = 1;
            documentTitleData.WordWrap = true;
            documentTitleData.Load += DocumentTitleData_Load;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            templateDocumentLayout.SetColumnSpan(templateTitleData, 2);
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = true;
            templateTitleData.Size = new Size(557, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // documentGridView
            // 
            documentGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentGridView.Columns.AddRange(new DataGridViewColumn[] { documentColumn, documentTypeColumn });
            templateDocumentLayout.SetColumnSpan(documentGridView, 2);
            documentGridView.Dock = DockStyle.Fill;
            documentGridView.Location = new Point(3, 53);
            documentGridView.Name = "documentGridView";
            documentGridView.Size = new Size(557, 169);
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
            // documentTypeGroup
            // 
            documentTypeGroup.AutoSize = true;
            templateDocumentLayout.SetColumnSpan(documentTypeGroup, 2);
            documentTypeGroup.Controls.Add(documentTypeLayout);
            documentTypeGroup.Dock = DockStyle.Fill;
            documentTypeGroup.Location = new Point(3, 280);
            documentTypeGroup.Name = "documentTypeGroup";
            documentTypeGroup.Size = new Size(557, 178);
            documentTypeGroup.TabIndex = 3;
            documentTypeGroup.TabStop = false;
            documentTypeGroup.Text = "Document Type";
            // 
            // documentTypeLayout
            // 
            documentTypeLayout.AutoSize = true;
            documentTypeLayout.ColumnCount = 1;
            documentTypeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentTypeLayout.Controls.Add(documentTransformData, 0, 0);
            documentTypeLayout.Controls.Add(documentSchemaDefinitionData, 0, 1);
            documentTypeLayout.Controls.Add(comboBoxData3, 0, 2);
            documentTypeLayout.Dock = DockStyle.Fill;
            documentTypeLayout.Location = new Point(3, 19);
            documentTypeLayout.Name = "documentTypeLayout";
            documentTypeLayout.RowCount = 3;
            documentTypeLayout.RowStyles.Add(new RowStyle());
            documentTypeLayout.RowStyles.Add(new RowStyle());
            documentTypeLayout.RowStyles.Add(new RowStyle());
            documentTypeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            documentTypeLayout.Size = new Size(551, 156);
            documentTypeLayout.TabIndex = 0;
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
            documentTransformData.Size = new Size(545, 46);
            documentTransformData.TabIndex = 9;
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
            documentSchemaDefinitionData.Size = new Size(545, 46);
            documentSchemaDefinitionData.TabIndex = 8;
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
            comboBoxData3.Size = new Size(545, 46);
            comboBoxData3.TabIndex = 10;
            // 
            // documentFileGroup
            // 
            documentFileGroup.AutoSize = true;
            templateDocumentLayout.SetColumnSpan(documentFileGroup, 2);
            documentFileGroup.Controls.Add(documentFileLayout);
            documentFileGroup.Dock = DockStyle.Fill;
            documentFileGroup.Location = new Point(3, 464);
            documentFileGroup.Name = "documentFileGroup";
            documentFileGroup.Size = new Size(557, 224);
            documentFileGroup.TabIndex = 4;
            documentFileGroup.TabStop = false;
            documentFileGroup.Text = "Document File";
            // 
            // documentFileLayout
            // 
            documentFileLayout.AutoSize = true;
            documentFileLayout.ColumnCount = 1;
            documentFileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentFileLayout.Controls.Add(rootFolderData, 0, 0);
            documentFileLayout.Controls.Add(relativePathData, 0, 1);
            documentFileLayout.Controls.Add(localPathData, 0, 2);
            documentFileLayout.Controls.Add(fileNameData, 0, 3);
            documentFileLayout.Dock = DockStyle.Fill;
            documentFileLayout.Location = new Point(3, 19);
            documentFileLayout.Name = "documentFileLayout";
            documentFileLayout.RowCount = 4;
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle());
            documentFileLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            documentFileLayout.Size = new Size(551, 202);
            documentFileLayout.TabIndex = 0;
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
            rootFolderData.Size = new Size(545, 46);
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
            relativePathData.Size = new Size(545, 44);
            relativePathData.TabIndex = 5;
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
            localPathData.Size = new Size(545, 44);
            localPathData.TabIndex = 6;
            localPathData.WordWrap = true;
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
            fileNameData.Size = new Size(545, 44);
            fileNameData.TabIndex = 7;
            // 
            // TemplateDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(563, 716);
            Controls.Add(templateDocumentLayout);
            Name = "TemplateDocument";
            Text = "TemplateDocument";
            Controls.SetChildIndex(templateDocumentLayout, 0);
            templateDocumentLayout.ResumeLayout(false);
            templateDocumentLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentGridView).EndInit();
            documentTypeGroup.ResumeLayout(false);
            documentTypeGroup.PerformLayout();
            documentTypeLayout.ResumeLayout(false);
            documentTypeLayout.PerformLayout();
            documentFileGroup.ResumeLayout(false);
            documentFileGroup.PerformLayout();
            documentFileLayout.ResumeLayout(false);
            documentFileLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private DataGridView documentGridView;
        private DataGridViewTextBoxColumn documentColumn;
        private DataGridViewComboBoxColumn documentTypeColumn;
        private Controls.TextBoxData documentTitleData;
        private Controls.ComboBoxData documentTypeData;
        private Controls.ComboBoxData documentTransformData;
        private Controls.ComboBoxData documentSchemaDefinitionData;
        private Controls.ComboBoxData comboBoxData3;
        private Controls.ComboBoxData rootFolderData;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.TextBoxData localPathData;
        private Controls.SelectTextBoxData fileNameData;
    }
}