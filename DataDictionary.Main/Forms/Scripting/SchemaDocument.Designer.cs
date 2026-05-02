namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDocument
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
            TableLayoutPanel schemaDocumentLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaDocument));
            documentFileContentData = new DataDictionary.Main.Controls.TextBoxData();
            documentFileData = new DataDictionary.Main.Controls.SelectTextBoxData();
            sourceObjectData = new DataDictionary.Main.Controls.ComboBoxData();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaDocumentLayout = new TableLayoutPanel();
            schemaDocumentLayout.SuspendLayout();
            SuspendLayout();
            // 
            // schemaDocumentLayout
            // 
            schemaDocumentLayout.ColumnCount = 1;
            schemaDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaDocumentLayout.Controls.Add(documentFileContentData, 0, 4);
            schemaDocumentLayout.Controls.Add(documentFileData, 0, 3);
            schemaDocumentLayout.Controls.Add(sourceObjectData, 0, 2);
            schemaDocumentLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaDocumentLayout.Controls.Add(templateTitleData, 0, 0);
            schemaDocumentLayout.Dock = DockStyle.Fill;
            schemaDocumentLayout.Location = new Point(0, 25);
            schemaDocumentLayout.Name = "schemaDocumentLayout";
            schemaDocumentLayout.RowCount = 5;
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaDocumentLayout.Size = new Size(508, 411);
            schemaDocumentLayout.TabIndex = 4;
            // 
            // documentFileContentData
            // 
            documentFileContentData.AutoSize = true;
            documentFileContentData.Dock = DockStyle.Fill;
            documentFileContentData.HeaderText = "Document Content";
            documentFileContentData.Location = new Point(3, 205);
            documentFileContentData.Multiline = true;
            documentFileContentData.Name = "documentFileContentData";
            documentFileContentData.ReadOnly = false;
            documentFileContentData.Size = new Size(502, 203);
            documentFileContentData.TabIndex = 6;
            documentFileContentData.WordWrap = false;
            // 
            // documentFileData
            // 
            documentFileData.AutoSize = true;
            documentFileData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentFileData.Dock = DockStyle.Fill;
            documentFileData.HeaderText = "Document (Local Path)";
            documentFileData.Location = new Point(3, 155);
            documentFileData.Name = "documentFileData";
            documentFileData.ReadOnly = false;
            documentFileData.SelectIcon = (Image)resources.GetObject("documentFileData.SelectIcon");
            documentFileData.Size = new Size(502, 44);
            documentFileData.TabIndex = 5;
            // 
            // sourceObjectData
            // 
            sourceObjectData.AutoSize = true;
            sourceObjectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            sourceObjectData.Dock = DockStyle.Fill;
            sourceObjectData.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceObjectData.HeaderText = "Source Object";
            sourceObjectData.Location = new Point(3, 103);
            sourceObjectData.Name = "sourceObjectData";
            sourceObjectData.ReadOnly = false;
            sourceObjectData.Size = new Size(502, 46);
            sourceObjectData.TabIndex = 4;
            // 
            // schemaTitleData
            // 
            schemaTitleData.AutoSize = true;
            schemaTitleData.Dock = DockStyle.Fill;
            schemaTitleData.HeaderText = "Schema";
            schemaTitleData.Location = new Point(3, 53);
            schemaTitleData.Multiline = false;
            schemaTitleData.Name = "schemaTitleData";
            schemaTitleData.ReadOnly = true;
            schemaTitleData.Size = new Size(502, 44);
            schemaTitleData.TabIndex = 3;
            schemaTitleData.WordWrap = true;
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
            templateTitleData.Size = new Size(502, 44);
            templateTitleData.TabIndex = 2;
            templateTitleData.WordWrap = true;
            // 
            // SchemaDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 436);
            Controls.Add(schemaDocumentLayout);
            Name = "SchemaDocument";
            Text = "SchemaDocument";
            Load += SchemaDocument_Load;
            Controls.SetChildIndex(schemaDocumentLayout, 0);
            schemaDocumentLayout.ResumeLayout(false);
            schemaDocumentLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
        private Controls.ComboBoxData sourceObjectData;
        private Controls.SelectTextBoxData documentFileData;
        private Controls.TextBoxData documentFileContentData;
    }
}