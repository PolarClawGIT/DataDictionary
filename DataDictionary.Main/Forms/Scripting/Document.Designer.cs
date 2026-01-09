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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Document));
            TabPage dataTab;
            TableLayoutPanel documentInputLayout;
            TableLayoutPanel transformLayout;
            documentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentDetailTab = new TabControl();
            generalTab = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            specialFolderData = new DataDictionary.Main.Controls.ComboBoxData();
            rootDirectoryData = new DataDictionary.Main.Controls.SelectTextBoxData();
            fullPathData = new DataDictionary.Main.Controls.TextBoxData();
            exceptionData = new DataDictionary.Main.Controls.TextBoxData();
            inputToolstrip = new ToolStrip();
            inputOpenCommand = new ToolStripButton();
            inputSaveCommand = new ToolStripButton();
            inputContentData = new DataDictionary.Main.Controls.TextBoxData();
            inputDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            inputFileData = new DataDictionary.Main.Controls.TextBoxData();
            transformTab = new TabPage();
            transformToolStrip = new ToolStrip();
            openTransformCommand = new ToolStripButton();
            saveTransformCommand = new ToolStripButton();
            getTemplateTransform = new ToolStripButton();
            templateData = new DataDictionary.Main.Controls.ComboBoxData();
            transformData = new DataDictionary.Main.Controls.TextBoxData();
            resultTab = new TabPage();
            resultLayout = new TableLayoutPanel();
            resultsToolStrip = new ToolStrip();
            saveResultCommand = new ToolStripButton();
            refreshResultCommand = new ToolStripButton();
            outputDirectoryData = new DataDictionary.Main.Controls.TextBoxData();
            outputFileData = new DataDictionary.Main.Controls.TextBoxData();
            outputData = new DataDictionary.Main.Controls.TextBoxData();
            bindingDocument = new BindingSource(components);
            mainLayout = new TableLayoutPanel();
            dataTab = new TabPage();
            documentInputLayout = new TableLayoutPanel();
            transformLayout = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            documentDetailTab.SuspendLayout();
            generalTab.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(documentTitleData, 0, 0);
            mainLayout.Controls.Add(documentDetailTab, 0, 1);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 25);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 62.5F));
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
            // documentDetailTab
            // 
            documentDetailTab.Controls.Add(generalTab);
            documentDetailTab.Controls.Add(dataTab);
            documentDetailTab.Controls.Add(transformTab);
            documentDetailTab.Controls.Add(resultTab);
            documentDetailTab.Dock = DockStyle.Fill;
            documentDetailTab.Location = new Point(3, 53);
            documentDetailTab.Name = "documentDetailTab";
            documentDetailTab.SelectedIndex = 0;
            documentDetailTab.Size = new Size(620, 467);
            documentDetailTab.TabIndex = 5;
            // 
            // generalTab
            // 
            generalTab.BackColor = SystemColors.Control;
            generalTab.Controls.Add(tableLayoutPanel1);
            generalTab.Location = new Point(4, 24);
            generalTab.Name = "generalTab";
            generalTab.Size = new Size(612, 439);
            generalTab.TabIndex = 3;
            generalTab.Text = "General";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(specialFolderData, 0, 0);
            tableLayoutPanel1.Controls.Add(rootDirectoryData, 0, 1);
            tableLayoutPanel1.Controls.Add(fullPathData, 0, 2);
            tableLayoutPanel1.Controls.Add(exceptionData, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(612, 439);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // specialFolderData
            // 
            specialFolderData.AutoSize = true;
            specialFolderData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            specialFolderData.Dock = DockStyle.Fill;
            specialFolderData.DropDownStyle = ComboBoxStyle.DropDown;
            specialFolderData.HeaderText = "Root Folder";
            specialFolderData.Location = new Point(3, 3);
            specialFolderData.Name = "specialFolderData";
            specialFolderData.ReadOnly = false;
            specialFolderData.Size = new Size(606, 46);
            specialFolderData.TabIndex = 2;
            // 
            // rootDirectoryData
            // 
            rootDirectoryData.AutoSize = true;
            rootDirectoryData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rootDirectoryData.Dock = DockStyle.Fill;
            rootDirectoryData.HeaderText = "Root Directory";
            rootDirectoryData.Location = new Point(3, 55);
            rootDirectoryData.Name = "rootDirectoryData";
            rootDirectoryData.ReadOnly = false;
            rootDirectoryData.SelectIcon = (Image)resources.GetObject("rootDirectoryData.SelectIcon");
            rootDirectoryData.Size = new Size(606, 44);
            rootDirectoryData.TabIndex = 3;
            // 
            // fullPathData
            // 
            fullPathData.AutoSize = true;
            fullPathData.Dock = DockStyle.Fill;
            fullPathData.HeaderText = "Root Path";
            fullPathData.Location = new Point(3, 105);
            fullPathData.Multiline = false;
            fullPathData.Name = "fullPathData";
            fullPathData.ReadOnly = true;
            fullPathData.Size = new Size(606, 44);
            fullPathData.TabIndex = 4;
            fullPathData.WordWrap = true;
            // 
            // exceptionData
            // 
            exceptionData.AutoSize = true;
            exceptionData.Dock = DockStyle.Fill;
            exceptionData.HeaderText = "Exception";
            exceptionData.Location = new Point(3, 155);
            exceptionData.Multiline = true;
            exceptionData.Name = "exceptionData";
            exceptionData.ReadOnly = true;
            exceptionData.Size = new Size(606, 281);
            exceptionData.TabIndex = 5;
            exceptionData.WordWrap = true;
            // 
            // dataTab
            // 
            dataTab.BackColor = SystemColors.Control;
            dataTab.Controls.Add(documentInputLayout);
            dataTab.Location = new Point(4, 24);
            dataTab.Name = "dataTab";
            dataTab.Padding = new Padding(3);
            dataTab.Size = new Size(612, 439);
            dataTab.TabIndex = 0;
            dataTab.Text = "Data";
            // 
            // documentInputLayout
            // 
            documentInputLayout.ColumnCount = 1;
            documentInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentInputLayout.Controls.Add(inputToolstrip, 0, 0);
            documentInputLayout.Controls.Add(inputContentData, 0, 3);
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
            documentInputLayout.Size = new Size(606, 433);
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
            // 
            // inputSaveCommand
            // 
            inputSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            inputSaveCommand.Image = (Image)resources.GetObject("inputSaveCommand.Image");
            inputSaveCommand.ImageTransparentColor = Color.Magenta;
            inputSaveCommand.Name = "inputSaveCommand";
            inputSaveCommand.Size = new Size(23, 22);
            inputSaveCommand.Text = "Save Input File";
            // 
            // inputContentData
            // 
            inputContentData.AutoSize = true;
            inputContentData.Dock = DockStyle.Fill;
            inputContentData.HeaderText = "Content (XML)";
            inputContentData.Location = new Point(3, 128);
            inputContentData.Multiline = true;
            inputContentData.Name = "inputContentData";
            inputContentData.ReadOnly = false;
            inputContentData.Size = new Size(600, 302);
            inputContentData.TabIndex = 2;
            inputContentData.WordWrap = true;
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
            transformTab.Size = new Size(612, 439);
            transformTab.TabIndex = 1;
            transformTab.Text = "Transform";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformToolStrip, 0, 0);
            transformLayout.Controls.Add(templateData, 0, 1);
            transformLayout.Controls.Add(transformData, 0, 2);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(3, 3);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 3;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            transformLayout.Size = new Size(606, 433);
            transformLayout.TabIndex = 0;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Items.AddRange(new ToolStripItem[] { openTransformCommand, saveTransformCommand, getTemplateTransform });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(606, 25);
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
            // 
            // saveTransformCommand
            // 
            saveTransformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveTransformCommand.Image = (Image)resources.GetObject("saveTransformCommand.Image");
            saveTransformCommand.ImageTransparentColor = Color.Magenta;
            saveTransformCommand.Name = "saveTransformCommand";
            saveTransformCommand.Size = new Size(23, 22);
            saveTransformCommand.Text = "Save Transform to File";
            // 
            // getTemplateTransform
            // 
            getTemplateTransform.DisplayStyle = ToolStripItemDisplayStyle.Image;
            getTemplateTransform.Image = (Image)resources.GetObject("getTemplateTransform.Image");
            getTemplateTransform.ImageTransparentColor = Color.Magenta;
            getTemplateTransform.Name = "getTemplateTransform";
            getTemplateTransform.Size = new Size(23, 22);
            getTemplateTransform.Text = "Get Template Transform";
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
            templateData.Size = new Size(600, 46);
            templateData.TabIndex = 1;
            // 
            // transformData
            // 
            transformData.AutoSize = true;
            transformData.Dock = DockStyle.Fill;
            transformData.HeaderText = "Transform (XSL)";
            transformData.Location = new Point(3, 80);
            transformData.Multiline = true;
            transformData.Name = "transformData";
            transformData.ReadOnly = false;
            transformData.Size = new Size(600, 350);
            transformData.TabIndex = 2;
            transformData.WordWrap = true;
            // 
            // resultTab
            // 
            resultTab.BackColor = SystemColors.Control;
            resultTab.Controls.Add(resultLayout);
            resultTab.Location = new Point(4, 24);
            resultTab.Name = "resultTab";
            resultTab.Padding = new Padding(3);
            resultTab.Size = new Size(612, 439);
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
            resultLayout.Size = new Size(606, 433);
            resultLayout.TabIndex = 0;
            // 
            // resultsToolStrip
            // 
            resultsToolStrip.Items.AddRange(new ToolStripItem[] { saveResultCommand, refreshResultCommand });
            resultsToolStrip.Location = new Point(0, 0);
            resultsToolStrip.Name = "resultsToolStrip";
            resultsToolStrip.Size = new Size(606, 25);
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
            // 
            // refreshResultCommand
            // 
            refreshResultCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshResultCommand.Image = (Image)resources.GetObject("refreshResultCommand.Image");
            refreshResultCommand.ImageTransparentColor = Color.Magenta;
            refreshResultCommand.Name = "refreshResultCommand";
            refreshResultCommand.Size = new Size(23, 22);
            refreshResultCommand.Text = "Refresh Result";
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
            outputDirectoryData.Size = new Size(600, 44);
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
            outputFileData.Size = new Size(600, 44);
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
            outputData.Size = new Size(600, 302);
            outputData.TabIndex = 3;
            outputData.WordWrap = true;
            // 
            // Document
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 548);
            Controls.Add(mainLayout);
            Name = "Document";
            Text = "Document";
            Controls.SetChildIndex(mainLayout, 0);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            documentDetailTab.ResumeLayout(false);
            generalTab.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData documentTitleData;
        private Controls.ComboBoxData specialFolderData;
        private Controls.SelectTextBoxData rootDirectoryData;
        private Controls.TextBoxData fullPathData;
        private TabControl documentDetailTab;
        private TabPage transformTab;
        private TabPage resultTab;
        private ToolStrip inputToolstrip;
        private Controls.TextBoxData exceptionData;
        private Controls.TextBoxData inputContentData;
        private TabPage generalTab;
        private ToolStripButton inputOpenCommand;
        private ToolStripButton inputSaveCommand;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel transformLayout;
        private ToolStrip transformToolStrip;
        private ToolStripButton openTransformCommand;
        private ToolStripButton saveTransformCommand;
        private ToolStripButton getTemplateTransform;
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
    }
}