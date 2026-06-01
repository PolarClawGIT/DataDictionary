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
            GroupBox objectGroupBox;
            TableLayoutPanel objectLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaDocument));
            TableLayoutPanel objectBehaviorLayout;
            documentFileContentData = new DataDictionary.Main.Controls.TextBoxData();
            documentFileData = new DataDictionary.Main.Controls.SelectTextBoxData();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            objectNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            isInModelData = new CheckBox();
            objectIsExcluded = new CheckBox();
            objectKeepOrphaned = new CheckBox();
            schemaDocumentLayout = new TableLayoutPanel();
            objectGroupBox = new GroupBox();
            objectLayout = new TableLayoutPanel();
            objectBehaviorLayout = new TableLayoutPanel();
            schemaDocumentLayout.SuspendLayout();
            objectGroupBox.SuspendLayout();
            objectLayout.SuspendLayout();
            objectBehaviorLayout.SuspendLayout();
            SuspendLayout();
            // 
            // schemaDocumentLayout
            // 
            schemaDocumentLayout.ColumnCount = 1;
            schemaDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaDocumentLayout.Controls.Add(documentFileContentData, 0, 4);
            schemaDocumentLayout.Controls.Add(objectGroupBox, 0, 2);
            schemaDocumentLayout.Controls.Add(documentFileData, 0, 3);
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
            schemaDocumentLayout.Size = new Size(508, 610);
            schemaDocumentLayout.TabIndex = 4;
            // 
            // documentFileContentData
            // 
            documentFileContentData.AutoSize = true;
            documentFileContentData.Dock = DockStyle.Fill;
            documentFileContentData.HeaderText = "Document Content";
            documentFileContentData.Location = new Point(3, 314);
            documentFileContentData.Multiline = true;
            documentFileContentData.Name = "documentFileContentData";
            documentFileContentData.ReadOnly = false;
            documentFileContentData.Size = new Size(502, 293);
            documentFileContentData.TabIndex = 6;
            documentFileContentData.WordWrap = false;
            // 
            // documentFileData
            // 
            documentFileData.AutoSize = true;
            documentFileData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentFileData.Dock = DockStyle.Fill;
            documentFileData.HeaderText = "Document (Local Path)";
            documentFileData.Location = new Point(3, 264);
            documentFileData.Name = "documentFileData";
            documentFileData.ReadOnly = false;
            documentFileData.SelectIcon = (Image)resources.GetObject("documentFileData.SelectIcon");
            documentFileData.Size = new Size(502, 44);
            documentFileData.TabIndex = 5;
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
            // objectGroupBox
            // 
            objectGroupBox.AutoSize = true;
            objectGroupBox.Controls.Add(objectLayout);
            objectGroupBox.Dock = DockStyle.Fill;
            objectGroupBox.Location = new Point(3, 103);
            objectGroupBox.Name = "objectGroupBox";
            objectGroupBox.Size = new Size(502, 155);
            objectGroupBox.TabIndex = 5;
            objectGroupBox.TabStop = false;
            objectGroupBox.Text = "Model Object";
            // 
            // objectLayout
            // 
            objectLayout.AutoSize = true;
            objectLayout.ColumnCount = 1;
            objectLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            objectLayout.Controls.Add(objectBehaviorLayout, 0, 2);
            objectLayout.Controls.Add(objectScopeData, 0, 0);
            objectLayout.Controls.Add(objectNameData, 0, 1);
            objectLayout.Dock = DockStyle.Fill;
            objectLayout.Location = new Point(3, 19);
            objectLayout.Name = "objectLayout";
            objectLayout.RowCount = 3;
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.RowStyles.Add(new RowStyle());
            objectLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            objectLayout.Size = new Size(496, 133);
            objectLayout.TabIndex = 0;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDown;
            objectScopeData.HeaderText = "Scope";
            objectScopeData.Location = new Point(3, 3);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = false;
            objectScopeData.Size = new Size(490, 46);
            objectScopeData.TabIndex = 2;
            // 
            // objectNameData
            // 
            objectNameData.AutoSize = true;
            objectNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectNameData.Dock = DockStyle.Fill;
            objectNameData.HeaderText = "Object";
            objectNameData.Location = new Point(3, 55);
            objectNameData.Name = "objectNameData";
            objectNameData.ReadOnly = false;
            objectNameData.SelectIcon = (Image)resources.GetObject("objectNameData.SelectIcon");
            objectNameData.Size = new Size(490, 44);
            objectNameData.TabIndex = 3;
            // 
            // objectBehaviorLayout
            // 
            objectBehaviorLayout.AutoSize = true;
            objectBehaviorLayout.ColumnCount = 3;
            objectBehaviorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            objectBehaviorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            objectBehaviorLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            objectBehaviorLayout.Controls.Add(objectKeepOrphaned, 2, 0);
            objectBehaviorLayout.Controls.Add(objectIsExcluded, 1, 0);
            objectBehaviorLayout.Controls.Add(isInModelData, 0, 0);
            objectBehaviorLayout.Dock = DockStyle.Fill;
            objectBehaviorLayout.Location = new Point(3, 105);
            objectBehaviorLayout.Name = "objectBehaviorLayout";
            objectBehaviorLayout.RowCount = 1;
            objectBehaviorLayout.RowStyles.Add(new RowStyle());
            objectBehaviorLayout.Size = new Size(490, 25);
            objectBehaviorLayout.TabIndex = 5;
            // 
            // isInModelData
            // 
            isInModelData.AutoSize = true;
            isInModelData.Enabled = false;
            isInModelData.Location = new Point(3, 3);
            isInModelData.Name = "isInModelData";
            isInModelData.Size = new Size(73, 19);
            isInModelData.TabIndex = 1;
            isInModelData.Text = "in Model";
            isInModelData.UseVisualStyleBackColor = true;
            // 
            // objectIsExcluded
            // 
            objectIsExcluded.AutoSize = true;
            objectIsExcluded.Location = new Point(166, 3);
            objectIsExcluded.Name = "objectIsExcluded";
            objectIsExcluded.Size = new Size(84, 19);
            objectIsExcluded.TabIndex = 2;
            objectIsExcluded.Text = "Is Excluded";
            objectIsExcluded.UseVisualStyleBackColor = true;
            // 
            // objectKeepOrphaned
            // 
            objectKeepOrphaned.AutoSize = true;
            objectKeepOrphaned.Location = new Point(329, 3);
            objectKeepOrphaned.Name = "objectKeepOrphaned";
            objectKeepOrphaned.Size = new Size(108, 19);
            objectKeepOrphaned.TabIndex = 3;
            objectKeepOrphaned.Text = "Keep Orphaned";
            objectKeepOrphaned.UseVisualStyleBackColor = true;
            // 
            // SchemaDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 635);
            Controls.Add(schemaDocumentLayout);
            Name = "SchemaDocument";
            Text = "SchemaDocument";
            Load += SchemaDocument_Load;
            Controls.SetChildIndex(schemaDocumentLayout, 0);
            schemaDocumentLayout.ResumeLayout(false);
            schemaDocumentLayout.PerformLayout();
            objectGroupBox.ResumeLayout(false);
            objectGroupBox.PerformLayout();
            objectLayout.ResumeLayout(false);
            objectLayout.PerformLayout();
            objectBehaviorLayout.ResumeLayout(false);
            objectBehaviorLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
        private Controls.ComboBoxData sourceObjectData;
        private Controls.SelectTextBoxData documentFileData;
        private Controls.TextBoxData documentFileContentData;
        private TableLayoutPanel objectLayout;
        private Controls.ComboBoxData objectScopeData;
        private Controls.SelectTextBoxData objectNameData;
        private TableLayoutPanel objectBehaviorLayout;
        private CheckBox isInModelData;
        private CheckBox objectIsExcluded;
        private CheckBox objectKeepOrphaned;
    }
}