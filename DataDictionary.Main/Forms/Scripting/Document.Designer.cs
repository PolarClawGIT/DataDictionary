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
            TableLayoutPanel mainLayout;
            GroupBox rootGroupBox;
            TableLayoutPanel rootGroupLayout;
            TabPage dataTab;
            TableLayoutPanel documentInputLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Document));
            TableLayoutPanel transformLayout;
            documentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            rootPathData = new DataDictionary.Main.Controls.TextBoxData();
            rootFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            documentDetailTab = new TabControl();
            inputToolstrip = new ToolStrip();
            inputOpenCommand = new ToolStripButton();
            inputSaveCommand = new ToolStripButton();
            inputData = new DataDictionary.Main.Controls.TextBoxData();
            inputDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            inputFileData = new DataDictionary.Main.Controls.TextBoxData();
            transformTab = new TabPage();
            transformToolStrip = new ToolStrip();
            openTransformCommand = new ToolStripButton();
            saveTransformCommand = new ToolStripButton();
            getTransformCommand = new ToolStripButton();
            templateData = new DataDictionary.Main.Controls.ComboBoxData();
            transformData = new DataDictionary.Main.Controls.TextBoxData();
            transformDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            transformFileData = new DataDictionary.Main.Controls.TextBoxData();
            resultTab = new TabPage();
            resultLayout = new TableLayoutPanel();
            resultsToolStrip = new ToolStrip();
            saveResultCommand = new ToolStripButton();
            refreshResultCommand = new ToolStripButton();
            outputDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            outputFileData = new DataDictionary.Main.Controls.TextBoxData();
            outputData = new DataDictionary.Main.Controls.TextBoxData();
            bindingDocument = new BindingSource(components);
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            errorProvider = new ErrorProvider(components);
            mainLayout = new TableLayoutPanel();
            rootGroupBox = new GroupBox();
            rootGroupLayout = new TableLayoutPanel();
            dataTab = new TabPage();
            documentInputLayout = new TableLayoutPanel();
            transformLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            rootGroupBox.SuspendLayout();
            rootGroupLayout.SuspendLayout();
            documentDetailTab.SuspendLayout();
            dataTab.SuspendLayout();
            documentInputLayout.SuspendLayout();
            inputToolstrip.SuspendLayout();
            transformTab.SuspendLayout();
            transformLayout.SuspendLayout();
            transformToolStrip.SuspendLayout();
            resultTab.SuspendLayout();
            resultLayout.SuspendLayout();
            resultsToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(documentTitleData, 0, 0);
            mainLayout.Controls.Add(rootGroupBox, 0, 1);
            mainLayout.Controls.Add(documentDetailTab, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 25);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(626, 523);
            mainLayout.TabIndex = 4;
            // 
            // documentTitleData
            // 
            documentTitleData.AutoSize = true;
            documentTitleData.Dock = DockStyle.Fill;
            documentTitleData.HeaderText = "Document";
            documentTitleData.Location = new Point(3, 3);
            documentTitleData.Multiline = false;
            documentTitleData.Name = "documentTitleData";
            documentTitleData.ReadOnly = false;
            documentTitleData.Size = new Size(620, 44);
            documentTitleData.TabIndex = 0;
            documentTitleData.WordWrap = true;
            // 
            // rootGroupBox
            // 
            rootGroupBox.AutoSize = true;
            rootGroupBox.Controls.Add(rootGroupLayout);
            rootGroupBox.Dock = DockStyle.Fill;
            rootGroupBox.Location = new Point(3, 53);
            rootGroupBox.Name = "rootGroupBox";
            rootGroupBox.Size = new Size(620, 124);
            rootGroupBox.TabIndex = 6;
            rootGroupBox.TabStop = false;
            rootGroupBox.Text = "Root Location";
            // 
            // rootGroupLayout
            // 
            rootGroupLayout.AutoSize = true;
            rootGroupLayout.ColumnCount = 1;
            rootGroupLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootGroupLayout.Controls.Add(rootPathData, 0, 1);
            rootGroupLayout.Controls.Add(rootFolderData, 0, 0);
            rootGroupLayout.Dock = DockStyle.Fill;
            rootGroupLayout.Location = new Point(3, 19);
            rootGroupLayout.Name = "rootGroupLayout";
            rootGroupLayout.RowCount = 2;
            rootGroupLayout.RowStyles.Add(new RowStyle());
            rootGroupLayout.RowStyles.Add(new RowStyle());
            rootGroupLayout.Size = new Size(614, 102);
            rootGroupLayout.TabIndex = 0;
            // 
            // rootPathData
            // 
            rootPathData.AutoSize = true;
            rootPathData.Dock = DockStyle.Fill;
            rootPathData.HeaderText = "Path";
            rootPathData.Location = new Point(3, 55);
            rootPathData.Multiline = false;
            rootPathData.Name = "rootPathData";
            rootPathData.ReadOnly = true;
            rootPathData.Size = new Size(608, 44);
            rootPathData.TabIndex = 4;
            rootPathData.WordWrap = true;
            // 
            // rootFolderData
            // 
            rootFolderData.AutoSize = true;
            rootFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootFolderData.Dock = DockStyle.Fill;
            rootFolderData.DropDownStyle = ComboBoxStyle.DropDown;
            rootFolderData.HeaderText = "Folder";
            rootFolderData.Location = new Point(3, 3);
            rootFolderData.Name = "rootFolderData";
            rootFolderData.ReadOnly = false;
            rootFolderData.Size = new Size(608, 46);
            rootFolderData.TabIndex = 2;
            rootFolderData.Validated += RootFolderData_Validated;
            // 
            // documentDetailTab
            // 
            documentDetailTab.Controls.Add(dataTab);
            documentDetailTab.Controls.Add(transformTab);
            documentDetailTab.Controls.Add(resultTab);
            documentDetailTab.Dock = DockStyle.Fill;
            documentDetailTab.Location = new Point(3, 183);
            documentDetailTab.Name = "documentDetailTab";
            documentDetailTab.SelectedIndex = 0;
            documentDetailTab.Size = new Size(620, 337);
            documentDetailTab.TabIndex = 5;
            // 
            // dataTab
            // 
            dataTab.BackColor = SystemColors.Control;
            dataTab.Controls.Add(documentInputLayout);
            dataTab.Location = new Point(4, 24);
            dataTab.Name = "dataTab";
            dataTab.Padding = new Padding(3);
            dataTab.Size = new Size(612, 309);
            dataTab.TabIndex = 0;
            dataTab.Text = "Data";
            // 
            // documentInputLayout
            // 
            documentInputLayout.ColumnCount = 1;
            documentInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentInputLayout.Controls.Add(inputToolstrip, 0, 0);
            documentInputLayout.Controls.Add(inputData, 0, 3);
            documentInputLayout.Controls.Add(inputDirectoryData, 0, 1);
            documentInputLayout.Controls.Add(inputFileData, 0, 2);
            documentInputLayout.Dock = DockStyle.Fill;
            documentInputLayout.Location = new Point(3, 3);
            documentInputLayout.Name = "documentInputLayout";
            documentInputLayout.RowCount = 4;
            documentInputLayout.RowStyles.Add(new RowStyle());
            documentInputLayout.RowStyles.Add(new RowStyle());
            documentInputLayout.RowStyles.Add(new RowStyle());
            documentInputLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentInputLayout.Size = new Size(606, 303);
            documentInputLayout.TabIndex = 0;
            // 
            // inputToolstrip
            // 
            inputToolstrip.Items.AddRange(new ToolStripItem[] { inputOpenCommand, inputSaveCommand });
            inputToolstrip.Location = new Point(0, 0);
            inputToolstrip.Name = "inputToolstrip";
            inputToolstrip.Size = new Size(606, 25);
            inputToolstrip.TabIndex = 0;
            inputToolstrip.Text = "toolStrip1";
            // 
            // inputOpenCommand
            // 
            inputOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            inputOpenCommand.Image = (Image)resources.GetObject("inputOpenCommand.Image");
            inputOpenCommand.ImageTransparentColor = Color.Magenta;
            inputOpenCommand.Name = "inputOpenCommand";
            inputOpenCommand.Size = new Size(23, 22);
            inputOpenCommand.Text = "Open Input File";
            inputOpenCommand.Click += InputOpenCommand_Click;
            // 
            // inputSaveCommand
            // 
            inputSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            inputSaveCommand.Image = (Image)resources.GetObject("inputSaveCommand.Image");
            inputSaveCommand.ImageTransparentColor = Color.Magenta;
            inputSaveCommand.Name = "inputSaveCommand";
            inputSaveCommand.Size = new Size(23, 22);
            inputSaveCommand.Text = "Save Input File";
            inputSaveCommand.Click += InputSaveCommand_Click;
            // 
            // inputData
            // 
            inputData.AutoSize = true;
            inputData.Dock = DockStyle.Fill;
            inputData.HeaderText = "Content (XML)";
            inputData.Location = new Point(3, 128);
            inputData.Multiline = true;
            inputData.Name = "inputData";
            inputData.ReadOnly = false;
            inputData.Size = new Size(600, 172);
            inputData.TabIndex = 2;
            inputData.WordWrap = true;
            inputData.Validated += InputData_Validated;
            // 
            // inputDirectoryData
            // 
            inputDirectoryData.AutoSize = true;
            inputDirectoryData.Dock = DockStyle.Fill;
            inputDirectoryData.HeaderText = "Directory";
            inputDirectoryData.Location = new Point(3, 28);
            inputDirectoryData.Multiline = false;
            inputDirectoryData.Name = "inputDirectoryData";
            inputDirectoryData.ReadOnly = true;
            inputDirectoryData.Size = new Size(600, 44);
            inputDirectoryData.TabIndex = 3;
            inputDirectoryData.WordWrap = true;
            // 
            // inputFileData
            // 
            inputFileData.AutoSize = true;
            inputFileData.Dock = DockStyle.Fill;
            inputFileData.HeaderText = "File";
            inputFileData.Location = new Point(3, 78);
            inputFileData.Multiline = false;
            inputFileData.Name = "inputFileData";
            inputFileData.ReadOnly = true;
            inputFileData.Size = new Size(600, 44);
            inputFileData.TabIndex = 4;
            inputFileData.WordWrap = true;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(192, 72);
            transformTab.TabIndex = 1;
            transformTab.Text = "Transform";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformToolStrip, 0, 0);
            transformLayout.Controls.Add(templateData, 0, 1);
            transformLayout.Controls.Add(transformData, 0, 4);
            transformLayout.Controls.Add(transformDirectoryData, 0, 2);
            transformLayout.Controls.Add(transformFileData, 0, 3);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(3, 3);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 5;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            transformLayout.Size = new Size(186, 66);
            transformLayout.TabIndex = 0;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Items.AddRange(new ToolStripItem[] { openTransformCommand, saveTransformCommand, getTransformCommand });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(186, 25);
            transformToolStrip.TabIndex = 0;
            transformToolStrip.Text = "toolStrip1";
            // 
            // openTransformCommand
            // 
            openTransformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openTransformCommand.Image = (Image)resources.GetObject("openTransformCommand.Image");
            openTransformCommand.ImageTransparentColor = Color.Magenta;
            openTransformCommand.Name = "openTransformCommand";
            openTransformCommand.Size = new Size(23, 22);
            openTransformCommand.Text = "Open Transform from file";
            openTransformCommand.Click += OpenTransformCommand_Click;
            // 
            // saveTransformCommand
            // 
            saveTransformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveTransformCommand.Image = (Image)resources.GetObject("saveTransformCommand.Image");
            saveTransformCommand.ImageTransparentColor = Color.Magenta;
            saveTransformCommand.Name = "saveTransformCommand";
            saveTransformCommand.Size = new Size(23, 22);
            saveTransformCommand.Text = "Save Transform to File";
            saveTransformCommand.Click += SaveTransformCommand_Click;
            // 
            // getTransformCommand
            // 
            getTransformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            getTransformCommand.Image = (Image)resources.GetObject("getTransformCommand.Image");
            getTransformCommand.ImageTransparentColor = Color.Magenta;
            getTransformCommand.Name = "getTransformCommand";
            getTransformCommand.Size = new Size(23, 22);
            getTransformCommand.Text = "Get Template Transform";
            getTransformCommand.Click += GetTransformCommand_Click;
            // 
            // templateData
            // 
            templateData.AutoSize = true;
            templateData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            templateData.Dock = DockStyle.Fill;
            templateData.DropDownStyle = ComboBoxStyle.DropDown;
            templateData.HeaderText = "Template";
            templateData.Location = new Point(3, 28);
            templateData.Name = "templateData";
            templateData.ReadOnly = true;
            templateData.Size = new Size(180, 46);
            templateData.TabIndex = 1;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.Dock = DockStyle.Fill;
            transformData.HeaderText = "Transform (XSL)";
            transformData.Location = new Point(3, 180);
            transformData.Multiline = true;
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(180, 1);
            transformData.TabIndex = 2;
            transformData.WordWrap = true;
            transformData.Validated += TransformData_Validated;
            // 
            // transformDirectoryData
            // 
            transformDirectoryData.AutoSize = true;
            transformDirectoryData.Dock = DockStyle.Fill;
            transformDirectoryData.HeaderText = "Directory";
            transformDirectoryData.Location = new Point(3, 80);
            transformDirectoryData.Multiline = false;
            transformDirectoryData.Name = "transformDirectoryData";
            transformDirectoryData.ReadOnly = true;
            transformDirectoryData.Size = new Size(180, 44);
            transformDirectoryData.TabIndex = 3;
            transformDirectoryData.WordWrap = true;
            // 
            // transformFileData
            // 
            transformFileData.AutoSize = true;
            transformFileData.Dock = DockStyle.Fill;
            transformFileData.HeaderText = "File";
            transformFileData.Location = new Point(3, 130);
            transformFileData.Multiline = false;
            transformFileData.Name = "transformFileData";
            transformFileData.ReadOnly = true;
            transformFileData.Size = new Size(180, 44);
            transformFileData.TabIndex = 4;
            transformFileData.WordWrap = true;
            // 
            // resultTab
            // 
            resultTab.BackColor = SystemColors.Control;
            resultTab.Controls.Add(resultLayout);
            resultTab.Location = new Point(4, 24);
            resultTab.Name = "resultTab";
            resultTab.Padding = new Padding(3);
            resultTab.Size = new Size(192, 72);
            resultTab.TabIndex = 2;
            resultTab.Text = "Result";
            // 
            // resultLayout
            // 
            resultLayout.ColumnCount = 1;
            resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            resultLayout.Controls.Add(resultsToolStrip, 0, 0);
            resultLayout.Controls.Add(outputDirectoryData, 0, 1);
            resultLayout.Controls.Add(outputFileData, 0, 2);
            resultLayout.Controls.Add(outputData, 0, 3);
            resultLayout.Dock = DockStyle.Fill;
            resultLayout.Location = new Point(3, 3);
            resultLayout.Name = "resultLayout";
            resultLayout.RowCount = 4;
            resultLayout.RowStyles.Add(new RowStyle());
            resultLayout.RowStyles.Add(new RowStyle());
            resultLayout.RowStyles.Add(new RowStyle());
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            resultLayout.Size = new Size(186, 66);
            resultLayout.TabIndex = 0;
            // 
            // resultsToolStrip
            // 
            resultsToolStrip.Items.AddRange(new ToolStripItem[] { saveResultCommand, refreshResultCommand });
            resultsToolStrip.Location = new Point(0, 0);
            resultsToolStrip.Name = "resultsToolStrip";
            resultsToolStrip.Size = new Size(186, 25);
            resultsToolStrip.TabIndex = 0;
            resultsToolStrip.Text = "toolStrip1";
            // 
            // saveResultCommand
            // 
            saveResultCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveResultCommand.Image = (Image)resources.GetObject("saveResultCommand.Image");
            saveResultCommand.ImageTransparentColor = Color.Magenta;
            saveResultCommand.Name = "saveResultCommand";
            saveResultCommand.Size = new Size(23, 22);
            saveResultCommand.Text = "Save Results to File";
            saveResultCommand.Click += SaveResultCommand_Click;
            // 
            // refreshResultCommand
            // 
            refreshResultCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshResultCommand.Image = (Image)resources.GetObject("refreshResultCommand.Image");
            refreshResultCommand.ImageTransparentColor = Color.Magenta;
            refreshResultCommand.Name = "refreshResultCommand";
            refreshResultCommand.Size = new Size(23, 22);
            refreshResultCommand.Text = "Refresh Result";
            refreshResultCommand.Click += RefreshResultCommand_Click;
            // 
            // outputDirectoryData
            // 
            outputDirectoryData.AutoSize = true;
            outputDirectoryData.Dock = DockStyle.Fill;
            outputDirectoryData.HeaderText = "Directory";
            outputDirectoryData.Location = new Point(3, 28);
            outputDirectoryData.Multiline = false;
            outputDirectoryData.Name = "outputDirectoryData";
            outputDirectoryData.ReadOnly = true;
            outputDirectoryData.Size = new Size(180, 44);
            outputDirectoryData.TabIndex = 1;
            outputDirectoryData.WordWrap = true;
            // 
            // outputFileData
            // 
            outputFileData.AutoSize = true;
            outputFileData.Dock = DockStyle.Fill;
            outputFileData.HeaderText = "File";
            outputFileData.Location = new Point(3, 78);
            outputFileData.Multiline = false;
            outputFileData.Name = "outputFileData";
            outputFileData.ReadOnly = true;
            outputFileData.Size = new Size(180, 44);
            outputFileData.TabIndex = 2;
            outputFileData.WordWrap = true;
            // 
            // outputData
            // 
            outputData.AutoSize = true;
            outputData.Dock = DockStyle.Fill;
            outputData.HeaderText = "Content";
            outputData.Location = new Point(3, 128);
            outputData.Multiline = true;
            outputData.Name = "outputData";
            outputData.ReadOnly = true;
            outputData.Size = new Size(180, 1);
            outputData.TabIndex = 3;
            outputData.WordWrap = true;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // Document
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 548);
            Controls.Add(mainLayout);
            Name = "Document";
            Text = "Document";
            Load += Document_Load;
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            rootGroupBox.ResumeLayout(false);
            rootGroupBox.PerformLayout();
            rootGroupLayout.ResumeLayout(false);
            rootGroupLayout.PerformLayout();
            documentDetailTab.ResumeLayout(false);
            dataTab.ResumeLayout(false);
            documentInputLayout.ResumeLayout(false);
            documentInputLayout.PerformLayout();
            inputToolstrip.ResumeLayout(false);
            inputToolstrip.PerformLayout();
            transformTab.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformToolStrip.ResumeLayout(false);
            transformToolStrip.PerformLayout();
            resultTab.ResumeLayout(false);
            resultLayout.ResumeLayout(false);
            resultLayout.PerformLayout();
            resultsToolStrip.ResumeLayout(false);
            resultsToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData documentTitleData;
        private Controls.ComboBoxData rootFolderData;
        private Controls.TextBoxData rootPathData;
        private TabControl documentDetailTab;
        private TabPage transformTab;
        private TabPage resultTab;
        private ToolStrip inputToolstrip;
        private Controls.TextBoxData inputData;
        private ToolStripButton inputOpenCommand;
        private ToolStripButton inputSaveCommand;
        private TableLayoutPanel transformLayout;
        private ToolStrip transformToolStrip;
        private ToolStripButton openTransformCommand;
        private ToolStripButton saveTransformCommand;
        private ToolStripButton getTransformCommand;
        private Controls.ComboBoxData templateData;
        private Controls.TextBoxData transformData;
        private Controls.TextBoxData inputDirectoryData;
        private Controls.TextBoxData inputFileData;
        private TableLayoutPanel resultLayout;
        private ToolStrip resultsToolStrip;
        private ToolStripButton saveResultCommand;
        private ToolStripButton refreshResultCommand;
        private Controls.TextBoxData outputDirectoryData;
        private Controls.TextBoxData outputFileData;
        private Controls.TextBoxData outputData;
        private BindingSource bindingDocument;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Controls.TextBoxData transformDirectoryData;
        private Controls.TextBoxData transformFileData;
        private ErrorProvider errorProvider;
    }
}