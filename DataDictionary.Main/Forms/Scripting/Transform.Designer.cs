namespace DataDictionary.Main.Forms.Scripting
{
    partial class Transform
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
            TableLayoutPanel transformLayout;
            TableLayoutPanel detailLayout;
            GroupBox scriptGroupBox;
            TableLayoutPanel scriptFileLayout;
            GroupBox filePatternGroup;
            TableLayoutPanel filePatternLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transform));
            Label fileBaseName;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            transformTabs = new TabControl();
            transformTab = new TabPage();
            transformLocalPath = new DataDictionary.Main.Controls.TextBoxData();
            scriptFileNameData = new DataDictionary.Main.Controls.SelectTextBoxData();
            scriptData = new DataDictionary.Main.Controls.TextBoxData();
            schemaIdData = new DataDictionary.Main.Controls.ComboBoxData();
            documentTab = new TabPage();
            fileLayout = new TableLayoutPanel();
            relativePathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            filePrefixData = new DataDictionary.Main.Controls.TextBoxData();
            fileSuffixData = new DataDictionary.Main.Controls.TextBoxData();
            documentLocalPathData = new DataDictionary.Main.Controls.TextBoxData();
            fileExtensionData = new DataDictionary.Main.Controls.ComboBoxData();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            documentToolStrip = new ToolStrip();
            documentNewCommand = new ToolStripButton();
            documentOpenCommand = new ToolStripButton();
            transformTitleData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            bindingTransform = new BindingSource(components);
            folderBrowserDialog = new FolderBrowserDialog();
            openFileDialog = new OpenFileDialog();
            errorProvider = new ErrorProvider(components);
            saveFileDialog = new SaveFileDialog();
            transformLayout = new TableLayoutPanel();
            detailLayout = new TableLayoutPanel();
            scriptGroupBox = new GroupBox();
            scriptFileLayout = new TableLayoutPanel();
            filePatternGroup = new GroupBox();
            filePatternLayout = new TableLayoutPanel();
            fileBaseName = new Label();
            transformLayout.SuspendLayout();
            transformTabs.SuspendLayout();
            transformTab.SuspendLayout();
            detailLayout.SuspendLayout();
            scriptGroupBox.SuspendLayout();
            scriptFileLayout.SuspendLayout();
            documentTab.SuspendLayout();
            fileLayout.SuspendLayout();
            filePatternGroup.SuspendLayout();
            filePatternLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            documentToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(templateTitleData, 0, 0);
            transformLayout.Controls.Add(transformTabs, 0, 2);
            transformLayout.Controls.Add(transformTitleData, 0, 1);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(0, 25);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 3;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            transformLayout.Size = new Size(565, 640);
            transformLayout.TabIndex = 5;
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
            templateTitleData.Size = new Size(559, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // transformTabs
            // 
            transformTabs.Controls.Add(transformTab);
            transformTabs.Controls.Add(documentTab);
            transformTabs.Dock = DockStyle.Fill;
            transformTabs.Location = new Point(3, 103);
            transformTabs.Name = "transformTabs";
            transformTabs.SelectedIndex = 0;
            transformTabs.Size = new Size(559, 534);
            transformTabs.TabIndex = 6;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(detailLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(551, 506);
            transformTab.TabIndex = 0;
            transformTab.Text = "Transform";
            // 
            // detailLayout
            // 
            detailLayout.ColumnCount = 1;
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailLayout.Controls.Add(scriptGroupBox, 0, 1);
            detailLayout.Controls.Add(schemaIdData, 0, 0);
            detailLayout.Dock = DockStyle.Fill;
            detailLayout.Location = new Point(3, 3);
            detailLayout.Name = "detailLayout";
            detailLayout.RowCount = 2;
            detailLayout.RowStyles.Add(new RowStyle());
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            detailLayout.Size = new Size(545, 500);
            detailLayout.TabIndex = 7;
            // 
            // scriptGroupBox
            // 
            scriptGroupBox.Controls.Add(scriptFileLayout);
            scriptGroupBox.Dock = DockStyle.Fill;
            scriptGroupBox.Location = new Point(3, 55);
            scriptGroupBox.Name = "scriptGroupBox";
            scriptGroupBox.Size = new Size(539, 442);
            scriptGroupBox.TabIndex = 6;
            scriptGroupBox.TabStop = false;
            scriptGroupBox.Text = "Script (Process)";
            // 
            // scriptFileLayout
            // 
            scriptFileLayout.ColumnCount = 2;
            scriptFileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            scriptFileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            scriptFileLayout.Controls.Add(transformLocalPath, 0, 0);
            scriptFileLayout.Controls.Add(scriptFileNameData, 0, 1);
            scriptFileLayout.Controls.Add(scriptData, 0, 2);
            scriptFileLayout.Dock = DockStyle.Fill;
            scriptFileLayout.Location = new Point(3, 19);
            scriptFileLayout.Name = "scriptFileLayout";
            scriptFileLayout.RowCount = 3;
            scriptFileLayout.RowStyles.Add(new RowStyle());
            scriptFileLayout.RowStyles.Add(new RowStyle());
            scriptFileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scriptFileLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            scriptFileLayout.Size = new Size(533, 420);
            scriptFileLayout.TabIndex = 7;
            // 
            // transformLocalPath
            // 
            transformLocalPath.AutoSize = true;
            scriptFileLayout.SetColumnSpan(transformLocalPath, 4);
            transformLocalPath.Dock = DockStyle.Fill;
            transformLocalPath.HeaderText = "Local Path";
            transformLocalPath.Location = new Point(3, 3);
            transformLocalPath.Multiline = false;
            transformLocalPath.Name = "transformLocalPath";
            transformLocalPath.ReadOnly = true;
            transformLocalPath.Size = new Size(527, 44);
            transformLocalPath.TabIndex = 8;
            transformLocalPath.WordWrap = false;
            // 
            // scriptFileNameData
            // 
            scriptFileNameData.AutoSize = true;
            scriptFileNameData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scriptFileLayout.SetColumnSpan(scriptFileNameData, 2);
            scriptFileNameData.Dock = DockStyle.Fill;
            scriptFileNameData.HeaderText = "Script File";
            scriptFileNameData.Location = new Point(3, 53);
            scriptFileNameData.Name = "scriptFileNameData";
            scriptFileNameData.ReadOnly = false;
            scriptFileNameData.SelectIcon = (Image)resources.GetObject("scriptFileNameData.SelectIcon");
            scriptFileNameData.Size = new Size(527, 44);
            scriptFileNameData.TabIndex = 1;
            scriptFileNameData.SelectCommand += ScriptFileNameData_SelectCommand;
            // 
            // scriptData
            // 
            scriptData.AutoSize = true;
            scriptFileLayout.SetColumnSpan(scriptData, 2);
            scriptData.Dock = DockStyle.Fill;
            scriptData.HeaderText = "Script Code";
            scriptData.Location = new Point(3, 103);
            scriptData.Multiline = true;
            scriptData.Name = "scriptData";
            scriptData.ReadOnly = false;
            scriptData.Size = new Size(527, 314);
            scriptData.TabIndex = 2;
            scriptData.WordWrap = false;
            // 
            // schemaIdData
            // 
            schemaIdData.AutoSize = true;
            schemaIdData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaIdData.Dock = DockStyle.Fill;
            schemaIdData.DropDownStyle = ComboBoxStyle.DropDownList;
            schemaIdData.HeaderText = "Schema (Input)";
            schemaIdData.Location = new Point(3, 3);
            schemaIdData.Name = "schemaIdData";
            schemaIdData.ReadOnly = false;
            schemaIdData.Size = new Size(539, 46);
            schemaIdData.TabIndex = 7;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(fileLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(551, 506);
            documentTab.TabIndex = 1;
            documentTab.Text = "Documents";
            // 
            // fileLayout
            // 
            fileLayout.ColumnCount = 1;
            fileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fileLayout.Controls.Add(filePatternGroup, 0, 1);
            fileLayout.Controls.Add(documentData, 0, 2);
            fileLayout.Controls.Add(documentToolStrip, 0, 0);
            fileLayout.Dock = DockStyle.Fill;
            fileLayout.Location = new Point(3, 3);
            fileLayout.Name = "fileLayout";
            fileLayout.RowCount = 3;
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.RowStyles.Add(new RowStyle());
            fileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            fileLayout.Size = new Size(545, 500);
            fileLayout.TabIndex = 5;
            // 
            // filePatternGroup
            // 
            filePatternGroup.AutoSize = true;
            filePatternGroup.Controls.Add(filePatternLayout);
            filePatternGroup.Dock = DockStyle.Fill;
            filePatternGroup.Location = new Point(3, 28);
            filePatternGroup.Name = "filePatternGroup";
            filePatternGroup.Size = new Size(539, 176);
            filePatternGroup.TabIndex = 15;
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
            filePatternLayout.Controls.Add(relativePathData, 2, 0);
            filePatternLayout.Controls.Add(rootFolderData, 0, 0);
            filePatternLayout.Controls.Add(filePrefixData, 0, 2);
            filePatternLayout.Controls.Add(fileBaseName, 1, 2);
            filePatternLayout.Controls.Add(fileSuffixData, 2, 2);
            filePatternLayout.Controls.Add(documentLocalPathData, 0, 1);
            filePatternLayout.Controls.Add(fileExtensionData, 3, 2);
            filePatternLayout.Dock = DockStyle.Fill;
            filePatternLayout.Location = new Point(3, 19);
            filePatternLayout.Name = "filePatternLayout";
            filePatternLayout.RowCount = 3;
            filePatternLayout.RowStyles.Add(new RowStyle());
            filePatternLayout.RowStyles.Add(new RowStyle());
            filePatternLayout.RowStyles.Add(new RowStyle());
            filePatternLayout.Size = new Size(533, 154);
            filePatternLayout.TabIndex = 0;
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
            relativePathData.Size = new Size(302, 46);
            relativePathData.TabIndex = 10;
            relativePathData.SelectCommand += RelativePathData_SelectCommand;
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
            rootFolderData.TabIndex = 9;
            rootFolderData.Validated += RootFolderData_Validated;
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
            filePrefixData.Size = new Size(120, 46);
            filePrefixData.TabIndex = 2;
            filePrefixData.WordWrap = true;
            // 
            // fileBaseName
            // 
            fileBaseName.Anchor = AnchorStyles.None;
            fileBaseName.AutoSize = true;
            fileBaseName.Location = new Point(129, 120);
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
            fileSuffixData.Size = new Size(120, 46);
            fileSuffixData.TabIndex = 3;
            fileSuffixData.WordWrap = true;
            // 
            // documentLocalPathData
            // 
            documentLocalPathData.AutoSize = true;
            filePatternLayout.SetColumnSpan(documentLocalPathData, 4);
            documentLocalPathData.Dock = DockStyle.Fill;
            documentLocalPathData.HeaderText = "Local Path";
            documentLocalPathData.Location = new Point(3, 55);
            documentLocalPathData.Multiline = false;
            documentLocalPathData.Name = "documentLocalPathData";
            documentLocalPathData.ReadOnly = true;
            documentLocalPathData.Size = new Size(527, 44);
            documentLocalPathData.TabIndex = 7;
            documentLocalPathData.WordWrap = false;
            // 
            // fileExtensionData
            // 
            fileExtensionData.AutoSize = true;
            fileExtensionData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fileExtensionData.Dock = DockStyle.Fill;
            fileExtensionData.DropDownStyle = ComboBoxStyle.DropDown;
            fileExtensionData.HeaderText = "File Extension";
            fileExtensionData.Location = new Point(354, 105);
            fileExtensionData.Name = "fileExtensionData";
            fileExtensionData.ReadOnly = false;
            fileExtensionData.Size = new Size(176, 46);
            fileExtensionData.TabIndex = 8;
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { FileNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 210);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(539, 287);
            documentData.TabIndex = 9;
            // 
            // FileNameColumn
            // 
            FileNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileNameColumn.DataPropertyName = "ScriptedFileName";
            FileNameColumn.HeaderText = "File Name";
            FileNameColumn.Name = "FileNameColumn";
            FileNameColumn.ReadOnly = true;
            // 
            // documentToolStrip
            // 
            documentToolStrip.Items.AddRange(new ToolStripItem[] { documentNewCommand, documentOpenCommand });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(545, 25);
            documentToolStrip.TabIndex = 14;
            documentToolStrip.Text = "Document Tools";
            // 
            // documentNewCommand
            // 
            documentNewCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentNewCommand.Image = (Image)resources.GetObject("documentNewCommand.Image");
            documentNewCommand.ImageTransparentColor = Color.Magenta;
            documentNewCommand.Name = "documentNewCommand";
            documentNewCommand.Size = new Size(23, 22);
            documentNewCommand.Text = "New";
            documentNewCommand.Click += DocumentNewCommand_Click;
            // 
            // documentOpenCommand
            // 
            documentOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentOpenCommand.Image = (Image)resources.GetObject("documentOpenCommand.Image");
            documentOpenCommand.ImageTransparentColor = Color.Magenta;
            documentOpenCommand.Name = "documentOpenCommand";
            documentOpenCommand.Size = new Size(23, 22);
            documentOpenCommand.Text = "Open";
            documentOpenCommand.Click += DocumentOpenCommand_Click;
            // 
            // transformTitleData
            // 
            transformTitleData.AutoSize = true;
            transformTitleData.Dock = DockStyle.Fill;
            transformTitleData.HeaderText = "Transform";
            transformTitleData.Location = new Point(3, 53);
            transformTitleData.Multiline = false;
            transformTitleData.Name = "transformTitleData";
            transformTitleData.ReadOnly = false;
            transformTitleData.Size = new Size(559, 44);
            transformTitleData.TabIndex = 1;
            transformTitleData.WordWrap = true;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // Transform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 665);
            Controls.Add(transformLayout);
            Name = "Transform";
            Text = "Transform";
            Load += Transform_Load;
            Controls.SetChildIndex(transformLayout, 0);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformTabs.ResumeLayout(false);
            transformTab.ResumeLayout(false);
            detailLayout.ResumeLayout(false);
            detailLayout.PerformLayout();
            scriptGroupBox.ResumeLayout(false);
            scriptFileLayout.ResumeLayout(false);
            scriptFileLayout.PerformLayout();
            documentTab.ResumeLayout(false);
            fileLayout.ResumeLayout(false);
            fileLayout.PerformLayout();
            filePatternGroup.ResumeLayout(false);
            filePatternGroup.PerformLayout();
            filePatternLayout.ResumeLayout(false);
            filePatternLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            documentToolStrip.ResumeLayout(false);
            documentToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private TabControl transformTabs;
        private TabPage transformTab;
        private TabPage documentTab;
        private TableLayoutPanel fileLayout;
        private DataGridView documentData;
        private ToolStrip documentToolStrip;
        private Controls.TextBoxData transformTitleData;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripButton documentNewCommand;
        private ToolStripButton documentOpenCommand;
        private BindingSource bindingTemplate;
        private BindingSource bindingTransform;
        private DataGridViewTextBoxColumn FileNameColumn;
        private FolderBrowserDialog folderBrowserDialog;
        private OpenFileDialog openFileDialog;
        private ErrorProvider errorProvider;
        private SaveFileDialog saveFileDialog;
        private Controls.SelectTextBoxData scriptFileNameData;
        private Controls.TextBoxData scriptData;
        private Controls.ComboBoxData schemaIdData;
        private Controls.TextBoxData filePrefixData;
        private Controls.TextBoxData fileSuffixData;
        private Controls.TextBoxData documentLocalPathData;
        private Controls.ComboBoxData fileExtensionData;
        private Controls.TextBoxData transformLocalPath;
        private Controls.SelectTextBoxData relativePathData;
        private Controls.ComboBoxData rootFolderData;
    }
}