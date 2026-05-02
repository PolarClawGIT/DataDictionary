namespace DataDictionary.Main.Forms.Scripting
{
    partial class TransformDocument
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
            TableLayoutPanel transformDocumentLayout;
            TableLayoutPanel sourceDocumentLayout;
            TableLayoutPanel transformScriptLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformDocument));
            TableLayoutPanel transformResultLayout;
            transformTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentTabs = new TabControl();
            sourceTab = new TabPage();
            schemaDocumentIdData = new DataDictionary.Main.Controls.ComboBoxData();
            sourceDocumentData = new DataDictionary.Main.Controls.TextBoxData();
            schemaDocumentPathData = new DataDictionary.Main.Controls.TextBoxData();
            schemaSourceObjectData = new DataDictionary.Main.Controls.ComboBoxData();
            transformTab = new TabPage();
            transformScriptData = new DataDictionary.Main.Controls.TextBoxData();
            transformFileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            resultTab = new TabPage();
            documentFileData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentFileContentData = new DataDictionary.Main.Controls.TextBoxData();
            transformDocumentLayout = new TableLayoutPanel();
            sourceDocumentLayout = new TableLayoutPanel();
            transformScriptLayout = new TableLayoutPanel();
            transformResultLayout = new TableLayoutPanel();
            transformDocumentLayout.SuspendLayout();
            documentTabs.SuspendLayout();
            sourceTab.SuspendLayout();
            sourceDocumentLayout.SuspendLayout();
            transformTab.SuspendLayout();
            transformScriptLayout.SuspendLayout();
            resultTab.SuspendLayout();
            transformResultLayout.SuspendLayout();
            SuspendLayout();
            // 
            // transformDocumentLayout
            // 
            transformDocumentLayout.ColumnCount = 1;
            transformDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformDocumentLayout.Controls.Add(transformTitleData, 0, 1);
            transformDocumentLayout.Controls.Add(templateTitleData, 0, 0);
            transformDocumentLayout.Controls.Add(documentTabs, 0, 2);
            transformDocumentLayout.Dock = DockStyle.Fill;
            transformDocumentLayout.Location = new Point(0, 25);
            transformDocumentLayout.Name = "transformDocumentLayout";
            transformDocumentLayout.RowCount = 3;
            transformDocumentLayout.RowStyles.Add(new RowStyle());
            transformDocumentLayout.RowStyles.Add(new RowStyle());
            transformDocumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            transformDocumentLayout.Size = new Size(612, 561);
            transformDocumentLayout.TabIndex = 4;
            // 
            // transformTitleData
            // 
            transformTitleData.AutoSize = true;
            transformTitleData.Dock = DockStyle.Fill;
            transformTitleData.HeaderText = "Transform";
            transformTitleData.Location = new Point(3, 53);
            transformTitleData.Multiline = false;
            transformTitleData.Name = "transformTitleData";
            transformTitleData.ReadOnly = true;
            transformTitleData.Size = new Size(606, 44);
            transformTitleData.TabIndex = 2;
            transformTitleData.WordWrap = true;
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
            templateTitleData.Size = new Size(606, 44);
            templateTitleData.TabIndex = 1;
            templateTitleData.WordWrap = true;
            // 
            // documentTabs
            // 
            documentTabs.Controls.Add(sourceTab);
            documentTabs.Controls.Add(transformTab);
            documentTabs.Controls.Add(resultTab);
            documentTabs.Dock = DockStyle.Fill;
            documentTabs.Location = new Point(3, 103);
            documentTabs.Name = "documentTabs";
            documentTabs.SelectedIndex = 0;
            documentTabs.Size = new Size(606, 455);
            documentTabs.TabIndex = 3;
            // 
            // sourceTab
            // 
            sourceTab.BackColor = SystemColors.Control;
            sourceTab.Controls.Add(sourceDocumentLayout);
            sourceTab.Location = new Point(4, 24);
            sourceTab.Name = "sourceTab";
            sourceTab.Padding = new Padding(3);
            sourceTab.Size = new Size(598, 427);
            sourceTab.TabIndex = 0;
            sourceTab.Text = "Source";
            // 
            // sourceDocumentLayout
            // 
            sourceDocumentLayout.ColumnCount = 1;
            sourceDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sourceDocumentLayout.Controls.Add(schemaDocumentIdData, 0, 0);
            sourceDocumentLayout.Controls.Add(sourceDocumentData, 0, 3);
            sourceDocumentLayout.Controls.Add(schemaDocumentPathData, 0, 2);
            sourceDocumentLayout.Controls.Add(schemaSourceObjectData, 0, 1);
            sourceDocumentLayout.Dock = DockStyle.Fill;
            sourceDocumentLayout.Location = new Point(3, 3);
            sourceDocumentLayout.Name = "sourceDocumentLayout";
            sourceDocumentLayout.RowCount = 4;
            sourceDocumentLayout.RowStyles.Add(new RowStyle());
            sourceDocumentLayout.RowStyles.Add(new RowStyle());
            sourceDocumentLayout.RowStyles.Add(new RowStyle());
            sourceDocumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            sourceDocumentLayout.Size = new Size(592, 421);
            sourceDocumentLayout.TabIndex = 5;
            // 
            // schemaDocumentIdData
            // 
            schemaDocumentIdData.AutoSize = true;
            schemaDocumentIdData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaDocumentIdData.Dock = DockStyle.Fill;
            schemaDocumentIdData.DropDownStyle = ComboBoxStyle.DropDown;
            schemaDocumentIdData.HeaderText = "Schema Document";
            schemaDocumentIdData.Location = new Point(3, 3);
            schemaDocumentIdData.Name = "schemaDocumentIdData";
            schemaDocumentIdData.ReadOnly = false;
            schemaDocumentIdData.Size = new Size(586, 46);
            schemaDocumentIdData.TabIndex = 0;
            // 
            // sourceDocumentData
            // 
            sourceDocumentData.AutoSize = true;
            sourceDocumentData.Dock = DockStyle.Fill;
            sourceDocumentData.HeaderText = "Source Data";
            sourceDocumentData.Location = new Point(3, 157);
            sourceDocumentData.Multiline = true;
            sourceDocumentData.Name = "sourceDocumentData";
            sourceDocumentData.ReadOnly = false;
            sourceDocumentData.Size = new Size(586, 261);
            sourceDocumentData.TabIndex = 2;
            sourceDocumentData.WordWrap = false;
            // 
            // schemaDocumentPathData
            // 
            schemaDocumentPathData.AutoSize = true;
            schemaDocumentPathData.Dock = DockStyle.Fill;
            schemaDocumentPathData.HeaderText = "Source File (Local path)";
            schemaDocumentPathData.Location = new Point(3, 107);
            schemaDocumentPathData.Multiline = false;
            schemaDocumentPathData.Name = "schemaDocumentPathData";
            schemaDocumentPathData.ReadOnly = true;
            schemaDocumentPathData.Size = new Size(586, 44);
            schemaDocumentPathData.TabIndex = 1;
            schemaDocumentPathData.WordWrap = true;
            // 
            // schemaSourceObjectData
            // 
            schemaSourceObjectData.AutoSize = true;
            schemaSourceObjectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaSourceObjectData.Dock = DockStyle.Fill;
            schemaSourceObjectData.DropDownStyle = ComboBoxStyle.DropDownList;
            schemaSourceObjectData.HeaderText = "Source Object";
            schemaSourceObjectData.Location = new Point(3, 55);
            schemaSourceObjectData.Name = "schemaSourceObjectData";
            schemaSourceObjectData.ReadOnly = true;
            schemaSourceObjectData.Size = new Size(586, 46);
            schemaSourceObjectData.TabIndex = 3;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformScriptLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(598, 427);
            transformTab.TabIndex = 1;
            transformTab.Text = "Transform";
            // 
            // transformScriptLayout
            // 
            transformScriptLayout.ColumnCount = 1;
            transformScriptLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformScriptLayout.Controls.Add(transformScriptData, 0, 1);
            transformScriptLayout.Controls.Add(transformFileNameData, 0, 0);
            transformScriptLayout.Dock = DockStyle.Fill;
            transformScriptLayout.Location = new Point(3, 3);
            transformScriptLayout.Name = "transformScriptLayout";
            transformScriptLayout.RowCount = 2;
            transformScriptLayout.RowStyles.Add(new RowStyle());
            transformScriptLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            transformScriptLayout.Size = new Size(592, 421);
            transformScriptLayout.TabIndex = 6;
            // 
            // transformScriptData
            // 
            transformScriptData.AutoSize = true;
            transformScriptData.Dock = DockStyle.Fill;
            transformScriptData.HeaderText = "Transform Script";
            transformScriptData.Location = new Point(3, 53);
            transformScriptData.Multiline = true;
            transformScriptData.Name = "transformScriptData";
            transformScriptData.ReadOnly = false;
            transformScriptData.Size = new Size(586, 365);
            transformScriptData.TabIndex = 1;
            transformScriptData.WordWrap = false;
            // 
            // transformFileNameData
            // 
            transformFileNameData.AutoSize = true;
            transformFileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformFileNameData.Dock = DockStyle.Fill;
            transformFileNameData.HeaderText = "Script File (Local path)";
            transformFileNameData.Location = new Point(3, 3);
            transformFileNameData.Name = "transformFileNameData";
            transformFileNameData.ReadOnly = false;
            transformFileNameData.SelectIcon = (Image)resources.GetObject("transformFileNameData.SelectIcon");
            transformFileNameData.Size = new Size(586, 44);
            transformFileNameData.TabIndex = 2;
            // 
            // resultTab
            // 
            resultTab.BackColor = SystemColors.Control;
            resultTab.Controls.Add(transformResultLayout);
            resultTab.Location = new Point(4, 24);
            resultTab.Name = "resultTab";
            resultTab.Padding = new Padding(3);
            resultTab.Size = new Size(598, 427);
            resultTab.TabIndex = 2;
            resultTab.Text = "Result";
            // 
            // transformResultLayout
            // 
            transformResultLayout.ColumnCount = 1;
            transformResultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformResultLayout.Controls.Add(documentFileData, 0, 0);
            transformResultLayout.Controls.Add(documentFileContentData, 0, 1);
            transformResultLayout.Dock = DockStyle.Fill;
            transformResultLayout.Location = new Point(3, 3);
            transformResultLayout.Name = "transformResultLayout";
            transformResultLayout.RowCount = 2;
            transformResultLayout.RowStyles.Add(new RowStyle());
            transformResultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            transformResultLayout.Size = new Size(592, 421);
            transformResultLayout.TabIndex = 7;
            // 
            // documentFileData
            // 
            documentFileData.AutoSize = true;
            documentFileData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            documentFileData.Dock = DockStyle.Fill;
            documentFileData.HeaderText = "Transform Result (Local Path)";
            documentFileData.Location = new Point(3, 3);
            documentFileData.Name = "documentFileData";
            documentFileData.ReadOnly = false;
            documentFileData.SelectIcon = (Image)resources.GetObject("transformResultFileData.SelectIcon");
            documentFileData.Size = new Size(586, 44);
            documentFileData.TabIndex = 0;
            // 
            // documentFileContentData
            // 
            documentFileContentData.AutoSize = true;
            documentFileContentData.Dock = DockStyle.Fill;
            documentFileContentData.HeaderText = "Transform Result";
            documentFileContentData.Location = new Point(3, 53);
            documentFileContentData.Multiline = true;
            documentFileContentData.Name = "documentFileContentData";
            documentFileContentData.ReadOnly = true;
            documentFileContentData.Size = new Size(586, 365);
            documentFileContentData.TabIndex = 1;
            documentFileContentData.WordWrap = false;
            // 
            // TransformDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(612, 586);
            Controls.Add(transformDocumentLayout);
            Name = "TransformDocument";
            Text = "TransformDocument";
            Load += TransformDocument_Load;
            Controls.SetChildIndex(transformDocumentLayout, 0);
            transformDocumentLayout.ResumeLayout(false);
            transformDocumentLayout.PerformLayout();
            documentTabs.ResumeLayout(false);
            sourceTab.ResumeLayout(false);
            sourceDocumentLayout.ResumeLayout(false);
            sourceDocumentLayout.PerformLayout();
            transformTab.ResumeLayout(false);
            transformScriptLayout.ResumeLayout(false);
            transformScriptLayout.PerformLayout();
            resultTab.ResumeLayout(false);
            transformResultLayout.ResumeLayout(false);
            transformResultLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel transformDocumentLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData transformTitleData;
        private Controls.ComboBoxData schemaDocumentIdData;
        private Controls.TextBoxData schemaDocumentPathData;
        private Controls.TextBoxData sourceDocumentData;
        private TableLayoutPanel transformScriptLayout;
        private Controls.TextBoxData transformScriptData;
        private Controls.ComboBoxData schemaSourceObjectData;
        private TableLayoutPanel transformResultLayout;
        private Controls.SelectTextBoxData documentFileData;
        private Controls.TextBoxData documentFileContentData;
        private TabControl documentTabs;
        private TabPage sourceTab;
        private TabPage transformTab;
        private TabPage resultTab;
        private Controls.SelectTextBoxData transformFileNameData;
    }
}