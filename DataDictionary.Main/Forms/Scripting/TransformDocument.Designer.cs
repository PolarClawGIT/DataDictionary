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
            components = new System.ComponentModel.Container();
            TableLayoutPanel transformDocumentLayout;
            TableLayoutPanel sourceDocumentLayout;
            TableLayoutPanel transformScriptLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformDocument));
            TableLayoutPanel transformResultLayout;
            transformTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentTabs = new TabControl();
            sourceTab = new TabPage();
            schemaSourceObjectData = new DataDictionary.Main.Controls.ComboBoxData();
            schemaDocumentPathData = new DataDictionary.Main.Controls.TextBoxData();
            dataFileNameLayout = new TableLayoutPanel();
            reloadDataFileCommand = new Button();
            selectDataFileCommand = new Button();
            dataFileNameData = new DataDictionary.Main.Controls.ComboBoxData();
            sourceDocumentData = new DataDictionary.Main.Controls.TextBoxData();
            transformTab = new TabPage();
            transformScriptData = new DataDictionary.Main.Controls.TextBoxData();
            transformFileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            resultTab = new TabPage();
            documentFileData = new DataDictionary.Main.Controls.SelectTextBoxData();
            documentFileContentData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            bindingTransform = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            transformDocumentLayout = new TableLayoutPanel();
            sourceDocumentLayout = new TableLayoutPanel();
            transformScriptLayout = new TableLayoutPanel();
            transformResultLayout = new TableLayoutPanel();
            transformDocumentLayout.SuspendLayout();
            documentTabs.SuspendLayout();
            sourceTab.SuspendLayout();
            sourceDocumentLayout.SuspendLayout();
            dataFileNameLayout.SuspendLayout();
            transformTab.SuspendLayout();
            transformScriptLayout.SuspendLayout();
            resultTab.SuspendLayout();
            transformResultLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
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
            sourceDocumentLayout.Controls.Add(schemaSourceObjectData, 0, 2);
            sourceDocumentLayout.Controls.Add(schemaDocumentPathData, 0, 0);
            sourceDocumentLayout.Controls.Add(dataFileNameLayout, 0, 1);
            sourceDocumentLayout.Controls.Add(sourceDocumentData, 0, 3);
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
            // schemaSourceObjectData
            // 
            schemaSourceObjectData.AutoSize = true;
            schemaSourceObjectData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaSourceObjectData.Dock = DockStyle.Fill;
            schemaSourceObjectData.DropDownStyle = ComboBoxStyle.DropDownList;
            schemaSourceObjectData.HeaderText = "Object";
            schemaSourceObjectData.Location = new Point(3, 111);
            schemaSourceObjectData.Name = "schemaSourceObjectData";
            schemaSourceObjectData.ReadOnly = true;
            schemaSourceObjectData.Size = new Size(586, 46);
            schemaSourceObjectData.TabIndex = 3;
            // 
            // schemaDocumentPathData
            // 
            schemaDocumentPathData.AutoSize = true;
            schemaDocumentPathData.Dock = DockStyle.Fill;
            schemaDocumentPathData.HeaderText = "Location (Local path)";
            schemaDocumentPathData.Location = new Point(3, 3);
            schemaDocumentPathData.Multiline = false;
            schemaDocumentPathData.Name = "schemaDocumentPathData";
            schemaDocumentPathData.ReadOnly = true;
            schemaDocumentPathData.Size = new Size(586, 44);
            schemaDocumentPathData.TabIndex = 1;
            schemaDocumentPathData.WordWrap = true;
            // 
            // dataFileNameLayout
            // 
            dataFileNameLayout.AutoSize = true;
            dataFileNameLayout.ColumnCount = 3;
            dataFileNameLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dataFileNameLayout.ColumnStyles.Add(new ColumnStyle());
            dataFileNameLayout.ColumnStyles.Add(new ColumnStyle());
            dataFileNameLayout.Controls.Add(reloadDataFileCommand, 2, 0);
            dataFileNameLayout.Controls.Add(selectDataFileCommand, 1, 0);
            dataFileNameLayout.Controls.Add(dataFileNameData, 0, 0);
            dataFileNameLayout.Dock = DockStyle.Fill;
            dataFileNameLayout.Location = new Point(3, 53);
            dataFileNameLayout.Name = "dataFileNameLayout";
            dataFileNameLayout.RowCount = 1;
            dataFileNameLayout.RowStyles.Add(new RowStyle());
            dataFileNameLayout.Size = new Size(586, 52);
            dataFileNameLayout.TabIndex = 4;
            // 
            // reloadDataFileCommand
            // 
            reloadDataFileCommand.Dock = DockStyle.Bottom;
            reloadDataFileCommand.Image = Properties.Resources.OpenFolder;
            reloadDataFileCommand.Location = new Point(559, 26);
            reloadDataFileCommand.Name = "reloadDataFileCommand";
            reloadDataFileCommand.Size = new Size(24, 23);
            reloadDataFileCommand.TabIndex = 5;
            reloadDataFileCommand.UseVisualStyleBackColor = true;
            reloadDataFileCommand.Click += ReloadDataFileCommand_Click;
            // 
            // selectDataFileCommand
            // 
            selectDataFileCommand.Dock = DockStyle.Bottom;
            selectDataFileCommand.Image = Properties.Resources.OpenFolder;
            selectDataFileCommand.Location = new Point(529, 26);
            selectDataFileCommand.Name = "selectDataFileCommand";
            selectDataFileCommand.Size = new Size(24, 23);
            selectDataFileCommand.TabIndex = 4;
            selectDataFileCommand.UseVisualStyleBackColor = true;
            selectDataFileCommand.Click += SelectDataFileCommand_Click;
            // 
            // dataFileNameData
            // 
            dataFileNameData.AutoSize = true;
            dataFileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dataFileNameData.Dock = DockStyle.Fill;
            dataFileNameData.DropDownStyle = ComboBoxStyle.DropDown;
            dataFileNameData.HeaderText = "Data File";
            dataFileNameData.Location = new Point(3, 3);
            dataFileNameData.Name = "dataFileNameData";
            dataFileNameData.ReadOnly = false;
            dataFileNameData.Size = new Size(520, 46);
            dataFileNameData.TabIndex = 0;
            // 
            // sourceDocumentData
            // 
            sourceDocumentData.AutoSize = true;
            sourceDocumentData.Dock = DockStyle.Fill;
            sourceDocumentData.HeaderText = "Data File content";
            sourceDocumentData.Location = new Point(3, 163);
            sourceDocumentData.Multiline = true;
            sourceDocumentData.Name = "sourceDocumentData";
            sourceDocumentData.ReadOnly = false;
            sourceDocumentData.Size = new Size(586, 255);
            sourceDocumentData.TabIndex = 2;
            sourceDocumentData.WordWrap = false;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformScriptLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(192, 72);
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
            transformScriptLayout.Size = new Size(186, 66);
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
            transformScriptData.Size = new Size(180, 10);
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
            transformFileNameData.Size = new Size(180, 44);
            transformFileNameData.TabIndex = 2;
            // 
            // resultTab
            // 
            resultTab.BackColor = SystemColors.Control;
            resultTab.Controls.Add(transformResultLayout);
            resultTab.Location = new Point(4, 24);
            resultTab.Name = "resultTab";
            resultTab.Padding = new Padding(3);
            resultTab.Size = new Size(192, 72);
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
            transformResultLayout.Size = new Size(186, 66);
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
            documentFileData.SelectIcon = (Image)resources.GetObject("documentFileData.SelectIcon");
            documentFileData.Size = new Size(180, 44);
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
            documentFileContentData.Size = new Size(180, 10);
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
            dataFileNameLayout.ResumeLayout(false);
            dataFileNameLayout.PerformLayout();
            transformTab.ResumeLayout(false);
            transformScriptLayout.ResumeLayout(false);
            transformScriptLayout.PerformLayout();
            resultTab.ResumeLayout(false);
            transformResultLayout.ResumeLayout(false);
            transformResultLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel transformDocumentLayout;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData transformTitleData;
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
        private BindingSource bindingTemplate;
        private BindingSource bindingTransform;
        private BindingSource bindingDocument;
        private TableLayoutPanel dataFileNameLayout;
        private Controls.ComboBoxData dataFileNameData;
        private Button selectDataFileCommand;
        private Button reloadDataFileCommand;
    }
}