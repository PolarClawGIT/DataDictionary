namespace DataDictionary.Main.Forms.Scripting
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
            components = new System.ComponentModel.Container();
            TableLayoutPanel documentLayout;
            TabControl documentTabs;
            TableLayoutPanel schemaTabLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateDocument));
            TableLayoutPanel transformLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentTitleData = new DataDictionary.Main.Controls.TextBoxData();
            schemaTab = new TabPage();
            schemaIdData = new DataDictionary.Main.Controls.ComboBoxData();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            objectPathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            schemaToolStrip = new ToolStrip();
            schemaOpenCommand = new ToolStripButton();
            schemaBuildCommand = new ToolStripButton();
            schemaSaveCommand = new ToolStripButton();
            schemaFileNameData = new DataDictionary.Main.Controls.TextBoxData();
            schemaFileContentData = new DataDictionary.Main.Controls.TextBoxData();
            dataFileLocationData = new DataDictionary.Main.Controls.TextBoxData();
            transformTab = new TabPage();
            transformFileNameData = new DataDictionary.Main.Controls.TextBoxData();
            transformIdData = new DataDictionary.Main.Controls.ComboBoxData();
            transformToolStrip = new ToolStrip();
            transformOpenCommand = new ToolStripButton();
            transformSaveCommand = new ToolStripButton();
            transformFileContentData = new DataDictionary.Main.Controls.TextBoxData();
            transformFileLocationData = new DataDictionary.Main.Controls.TextBoxData();
            scriptTab = new TabPage();
            scriptedLayout = new TableLayoutPanel();
            scriptedToolStrip = new ToolStrip();
            scriptedOpenCommand = new ToolStripButton();
            scriptedBuildCommand = new ToolStripButton();
            scriptedSaveCommand = new ToolStripButton();
            scriptedFileName = new DataDictionary.Main.Controls.TextBoxData();
            textBoxData2 = new DataDictionary.Main.Controls.TextBoxData();
            scriptedFileLocationData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            bindingSchema = new BindingSource(components);
            bindingTransform = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            errorProvider = new ErrorProvider(components);
            documentLayout = new TableLayoutPanel();
            documentTabs = new TabControl();
            schemaTabLayout = new TableLayoutPanel();
            transformLayout = new TableLayoutPanel();
            documentLayout.SuspendLayout();
            documentTabs.SuspendLayout();
            schemaTab.SuspendLayout();
            schemaTabLayout.SuspendLayout();
            schemaToolStrip.SuspendLayout();
            transformTab.SuspendLayout();
            transformLayout.SuspendLayout();
            transformToolStrip.SuspendLayout();
            scriptTab.SuspendLayout();
            scriptedLayout.SuspendLayout();
            scriptedToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 1;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.Controls.Add(templateTitleData, 0, 0);
            documentLayout.Controls.Add(documentTitleData, 0, 1);
            documentLayout.Controls.Add(documentTabs, 0, 2);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(0, 25);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 3;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.Size = new Size(542, 665);
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
            templateTitleData.Size = new Size(536, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // documentTitleData
            // 
            documentTitleData.AutoSize = true;
            documentTitleData.Dock = DockStyle.Fill;
            documentTitleData.HeaderText = "DocumentTitle";
            documentTitleData.Location = new Point(3, 53);
            documentTitleData.Multiline = false;
            documentTitleData.Name = "documentTitleData";
            documentTitleData.ReadOnly = false;
            documentTitleData.Size = new Size(536, 44);
            documentTitleData.TabIndex = 1;
            documentTitleData.WordWrap = true;
            // 
            // documentTabs
            // 
            documentTabs.Controls.Add(schemaTab);
            documentTabs.Controls.Add(transformTab);
            documentTabs.Controls.Add(scriptTab);
            documentTabs.Dock = DockStyle.Fill;
            documentTabs.Location = new Point(3, 103);
            documentTabs.Name = "documentTabs";
            documentTabs.SelectedIndex = 0;
            documentTabs.Size = new Size(536, 559);
            documentTabs.TabIndex = 2;
            // 
            // schemaTab
            // 
            schemaTab.BackColor = SystemColors.Control;
            schemaTab.Controls.Add(schemaTabLayout);
            schemaTab.Location = new Point(4, 24);
            schemaTab.Name = "schemaTab";
            schemaTab.Padding = new Padding(3);
            schemaTab.Size = new Size(528, 531);
            schemaTab.TabIndex = 0;
            schemaTab.Text = "Schema (input)";
            // 
            // schemaTabLayout
            // 
            schemaTabLayout.ColumnCount = 1;
            schemaTabLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaTabLayout.Controls.Add(schemaIdData, 0, 1);
            schemaTabLayout.Controls.Add(objectScopeData, 0, 1);
            schemaTabLayout.Controls.Add(objectPathData, 0, 2);
            schemaTabLayout.Controls.Add(schemaToolStrip, 0, 0);
            schemaTabLayout.Controls.Add(schemaFileNameData, 0, 5);
            schemaTabLayout.Controls.Add(schemaFileContentData, 0, 6);
            schemaTabLayout.Controls.Add(dataFileLocationData, 0, 4);
            schemaTabLayout.Dock = DockStyle.Fill;
            schemaTabLayout.Location = new Point(3, 3);
            schemaTabLayout.Name = "schemaTabLayout";
            schemaTabLayout.RowCount = 7;
            schemaTabLayout.RowStyles.Add(new RowStyle());
            schemaTabLayout.RowStyles.Add(new RowStyle());
            schemaTabLayout.RowStyles.Add(new RowStyle());
            schemaTabLayout.RowStyles.Add(new RowStyle());
            schemaTabLayout.RowStyles.Add(new RowStyle());
            schemaTabLayout.RowStyles.Add(new RowStyle());
            schemaTabLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaTabLayout.Size = new Size(522, 525);
            schemaTabLayout.TabIndex = 0;
            // 
            // schemaIdData
            // 
            schemaIdData.AutoSize = true;
            schemaIdData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            schemaIdData.Dock = DockStyle.Fill;
            schemaIdData.DropDownStyle = ComboBoxStyle.DropDownList;
            schemaIdData.HeaderText = "Schema";
            schemaIdData.Location = new Point(3, 28);
            schemaIdData.Name = "schemaIdData";
            schemaIdData.ReadOnly = false;
            schemaIdData.Size = new Size(516, 46);
            schemaIdData.TabIndex = 0;
            schemaIdData.Validated += SchemaIdData_Validated;
            // 
            // objectScopeData
            // 
            objectScopeData.AutoSize = true;
            objectScopeData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectScopeData.Dock = DockStyle.Fill;
            objectScopeData.DropDownStyle = ComboBoxStyle.DropDownList;
            objectScopeData.HeaderText = "Object Scope";
            objectScopeData.Location = new Point(3, 80);
            objectScopeData.Name = "objectScopeData";
            objectScopeData.ReadOnly = false;
            objectScopeData.Size = new Size(516, 46);
            objectScopeData.TabIndex = 1;
            // 
            // objectPathData
            // 
            objectPathData.AutoSize = true;
            objectPathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectPathData.Dock = DockStyle.Fill;
            objectPathData.HeaderText = "Object";
            objectPathData.Location = new Point(3, 132);
            objectPathData.Name = "objectPathData";
            objectPathData.ReadOnly = false;
            objectPathData.SelectIcon = (Image)resources.GetObject("objectPathData.SelectIcon");
            objectPathData.Size = new Size(516, 44);
            objectPathData.TabIndex = 2;
            // 
            // schemaToolStrip
            // 
            schemaToolStrip.Items.AddRange(new ToolStripItem[] { schemaOpenCommand, schemaBuildCommand, schemaSaveCommand });
            schemaToolStrip.Location = new Point(0, 0);
            schemaToolStrip.Name = "schemaToolStrip";
            schemaToolStrip.Size = new Size(522, 25);
            schemaToolStrip.TabIndex = 3;
            schemaToolStrip.Text = "toolStrip1";
            // 
            // schemaOpenCommand
            // 
            schemaOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            schemaOpenCommand.Image = (Image)resources.GetObject("schemaOpenCommand.Image");
            schemaOpenCommand.ImageTransparentColor = Color.Magenta;
            schemaOpenCommand.Name = "schemaOpenCommand";
            schemaOpenCommand.Size = new Size(23, 22);
            schemaOpenCommand.Text = "Open Schema";
            // 
            // schemaBuildCommand
            // 
            schemaBuildCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            schemaBuildCommand.Image = (Image)resources.GetObject("schemaBuildCommand.Image");
            schemaBuildCommand.ImageTransparentColor = Color.Magenta;
            schemaBuildCommand.Name = "schemaBuildCommand";
            schemaBuildCommand.Size = new Size(23, 22);
            schemaBuildCommand.Text = "Build Schema";
            // 
            // schemaSaveCommand
            // 
            schemaSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            schemaSaveCommand.Image = (Image)resources.GetObject("schemaSaveCommand.Image");
            schemaSaveCommand.ImageTransparentColor = Color.Magenta;
            schemaSaveCommand.Name = "schemaSaveCommand";
            schemaSaveCommand.Size = new Size(23, 22);
            schemaSaveCommand.Text = "Save Schema";
            // 
            // schemaFileNameData
            // 
            schemaFileNameData.AutoSize = true;
            schemaFileNameData.Dock = DockStyle.Fill;
            schemaFileNameData.HeaderText = "Data File Name";
            schemaFileNameData.Location = new Point(3, 232);
            schemaFileNameData.Multiline = false;
            schemaFileNameData.Name = "schemaFileNameData";
            schemaFileNameData.ReadOnly = false;
            schemaFileNameData.Size = new Size(516, 44);
            schemaFileNameData.TabIndex = 4;
            schemaFileNameData.WordWrap = true;
            schemaFileNameData.Validated += SchemaFileNameData_Validated;
            // 
            // schemaFileContentData
            // 
            schemaFileContentData.AutoSize = true;
            schemaFileContentData.Dock = DockStyle.Fill;
            schemaFileContentData.HeaderText = "Data File Content";
            schemaFileContentData.Location = new Point(3, 282);
            schemaFileContentData.Multiline = true;
            schemaFileContentData.Name = "schemaFileContentData";
            schemaFileContentData.ReadOnly = false;
            schemaFileContentData.Size = new Size(516, 240);
            schemaFileContentData.TabIndex = 5;
            schemaFileContentData.WordWrap = true;
            schemaFileContentData.Validated += SchemaFileContentData_Validated;
            // 
            // dataFileLocationData
            // 
            dataFileLocationData.AutoSize = true;
            dataFileLocationData.Dock = DockStyle.Fill;
            dataFileLocationData.HeaderText = "Data File Location";
            dataFileLocationData.Location = new Point(3, 182);
            dataFileLocationData.Multiline = false;
            dataFileLocationData.Name = "dataFileLocationData";
            dataFileLocationData.ReadOnly = true;
            dataFileLocationData.Size = new Size(516, 44);
            dataFileLocationData.TabIndex = 6;
            dataFileLocationData.WordWrap = true;
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
            transformTab.Text = "Transform (process)";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformFileNameData, 0, 3);
            transformLayout.Controls.Add(transformIdData, 0, 1);
            transformLayout.Controls.Add(transformToolStrip, 0, 0);
            transformLayout.Controls.Add(transformFileContentData, 0, 4);
            transformLayout.Controls.Add(transformFileLocationData, 0, 2);
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
            // transformFileNameData
            // 
            transformFileNameData.AutoSize = true;
            transformFileNameData.Dock = DockStyle.Fill;
            transformFileNameData.HeaderText = "Transform File";
            transformFileNameData.Location = new Point(3, 130);
            transformFileNameData.Multiline = false;
            transformFileNameData.Name = "transformFileNameData";
            transformFileNameData.ReadOnly = false;
            transformFileNameData.Size = new Size(180, 44);
            transformFileNameData.TabIndex = 1;
            transformFileNameData.WordWrap = true;
            // 
            // transformIdData
            // 
            transformIdData.AutoSize = true;
            transformIdData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transformIdData.Dock = DockStyle.Fill;
            transformIdData.DropDownStyle = ComboBoxStyle.DropDownList;
            transformIdData.HeaderText = "Transform";
            transformIdData.Location = new Point(3, 28);
            transformIdData.Name = "transformIdData";
            transformIdData.ReadOnly = false;
            transformIdData.Size = new Size(180, 46);
            transformIdData.TabIndex = 0;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Items.AddRange(new ToolStripItem[] { transformOpenCommand, transformSaveCommand });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(186, 25);
            transformToolStrip.TabIndex = 2;
            transformToolStrip.Text = "toolStrip1";
            // 
            // transformOpenCommand
            // 
            transformOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformOpenCommand.Image = (Image)resources.GetObject("transformOpenCommand.Image");
            transformOpenCommand.ImageTransparentColor = Color.Magenta;
            transformOpenCommand.Name = "transformOpenCommand";
            transformOpenCommand.Size = new Size(23, 22);
            transformOpenCommand.Text = "Transform Open";
            // 
            // transformSaveCommand
            // 
            transformSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            transformSaveCommand.Image = (Image)resources.GetObject("transformSaveCommand.Image");
            transformSaveCommand.ImageTransparentColor = Color.Magenta;
            transformSaveCommand.Name = "transformSaveCommand";
            transformSaveCommand.Size = new Size(23, 22);
            transformSaveCommand.Text = "Transform Save";
            // 
            // transformFileContentData
            // 
            transformFileContentData.AutoSize = true;
            transformFileContentData.Dock = DockStyle.Fill;
            transformFileContentData.HeaderText = "Transform Content";
            transformFileContentData.Location = new Point(3, 180);
            transformFileContentData.Multiline = true;
            transformFileContentData.Name = "transformFileContentData";
            transformFileContentData.ReadOnly = false;
            transformFileContentData.Size = new Size(180, 1);
            transformFileContentData.TabIndex = 3;
            transformFileContentData.WordWrap = true;
            // 
            // transformFileLocationData
            // 
            transformFileLocationData.AutoSize = true;
            transformFileLocationData.Dock = DockStyle.Fill;
            transformFileLocationData.HeaderText = "Transform Location";
            transformFileLocationData.Location = new Point(3, 80);
            transformFileLocationData.Multiline = false;
            transformFileLocationData.Name = "transformFileLocationData";
            transformFileLocationData.ReadOnly = true;
            transformFileLocationData.Size = new Size(180, 44);
            transformFileLocationData.TabIndex = 4;
            transformFileLocationData.WordWrap = true;
            // 
            // scriptTab
            // 
            scriptTab.BackColor = SystemColors.Control;
            scriptTab.Controls.Add(scriptedLayout);
            scriptTab.Location = new Point(4, 24);
            scriptTab.Name = "scriptTab";
            scriptTab.Padding = new Padding(3);
            scriptTab.Size = new Size(192, 72);
            scriptTab.TabIndex = 2;
            scriptTab.Text = "Scripted (output)";
            // 
            // scriptedLayout
            // 
            scriptedLayout.ColumnCount = 1;
            scriptedLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            scriptedLayout.Controls.Add(scriptedToolStrip, 0, 0);
            scriptedLayout.Controls.Add(scriptedFileName, 0, 2);
            scriptedLayout.Controls.Add(textBoxData2, 0, 3);
            scriptedLayout.Controls.Add(scriptedFileLocationData, 0, 1);
            scriptedLayout.Dock = DockStyle.Fill;
            scriptedLayout.Location = new Point(3, 3);
            scriptedLayout.Name = "scriptedLayout";
            scriptedLayout.RowCount = 4;
            scriptedLayout.RowStyles.Add(new RowStyle());
            scriptedLayout.RowStyles.Add(new RowStyle());
            scriptedLayout.RowStyles.Add(new RowStyle());
            scriptedLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scriptedLayout.Size = new Size(186, 66);
            scriptedLayout.TabIndex = 0;
            // 
            // scriptedToolStrip
            // 
            scriptedToolStrip.Items.AddRange(new ToolStripItem[] { scriptedOpenCommand, scriptedBuildCommand, scriptedSaveCommand });
            scriptedToolStrip.Location = new Point(0, 0);
            scriptedToolStrip.Name = "scriptedToolStrip";
            scriptedToolStrip.Size = new Size(186, 25);
            scriptedToolStrip.TabIndex = 0;
            scriptedToolStrip.Text = "toolStrip1";
            // 
            // scriptedOpenCommand
            // 
            scriptedOpenCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            scriptedOpenCommand.Image = (Image)resources.GetObject("scriptedOpenCommand.Image");
            scriptedOpenCommand.ImageTransparentColor = Color.Magenta;
            scriptedOpenCommand.Name = "scriptedOpenCommand";
            scriptedOpenCommand.Size = new Size(23, 22);
            scriptedOpenCommand.Text = "Open Scripted";
            // 
            // scriptedBuildCommand
            // 
            scriptedBuildCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            scriptedBuildCommand.Image = (Image)resources.GetObject("scriptedBuildCommand.Image");
            scriptedBuildCommand.ImageTransparentColor = Color.Magenta;
            scriptedBuildCommand.Name = "scriptedBuildCommand";
            scriptedBuildCommand.Size = new Size(23, 22);
            scriptedBuildCommand.Text = "Script File";
            // 
            // scriptedSaveCommand
            // 
            scriptedSaveCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            scriptedSaveCommand.Image = (Image)resources.GetObject("scriptedSaveCommand.Image");
            scriptedSaveCommand.ImageTransparentColor = Color.Magenta;
            scriptedSaveCommand.Name = "scriptedSaveCommand";
            scriptedSaveCommand.Size = new Size(23, 22);
            scriptedSaveCommand.Text = "Save Scripted";
            // 
            // scriptedFileName
            // 
            scriptedFileName.AutoSize = true;
            scriptedFileName.Dock = DockStyle.Fill;
            scriptedFileName.HeaderText = "Scripted File Name";
            scriptedFileName.Location = new Point(3, 78);
            scriptedFileName.Multiline = false;
            scriptedFileName.Name = "scriptedFileName";
            scriptedFileName.ReadOnly = false;
            scriptedFileName.Size = new Size(180, 44);
            scriptedFileName.TabIndex = 1;
            scriptedFileName.WordWrap = true;
            // 
            // textBoxData2
            // 
            textBoxData2.AutoSize = true;
            textBoxData2.Dock = DockStyle.Fill;
            textBoxData2.HeaderText = "Scripted File Content";
            textBoxData2.Location = new Point(3, 128);
            textBoxData2.Multiline = true;
            textBoxData2.Name = "textBoxData2";
            textBoxData2.ReadOnly = false;
            textBoxData2.Size = new Size(180, 1);
            textBoxData2.TabIndex = 2;
            textBoxData2.WordWrap = true;
            // 
            // scriptedFileLocationData
            // 
            scriptedFileLocationData.AutoSize = true;
            scriptedFileLocationData.Dock = DockStyle.Fill;
            scriptedFileLocationData.HeaderText = "Scripted File Location";
            scriptedFileLocationData.Location = new Point(3, 28);
            scriptedFileLocationData.Multiline = false;
            scriptedFileLocationData.Name = "scriptedFileLocationData";
            scriptedFileLocationData.ReadOnly = true;
            scriptedFileLocationData.Size = new Size(180, 44);
            scriptedFileLocationData.TabIndex = 3;
            scriptedFileLocationData.WordWrap = true;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // TemplateDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 690);
            Controls.Add(documentLayout);
            Name = "TemplateDocument";
            Text = "Document";
            Load += Document_Load;
            Controls.SetChildIndex(documentLayout, 0);
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            documentTabs.ResumeLayout(false);
            schemaTab.ResumeLayout(false);
            schemaTabLayout.ResumeLayout(false);
            schemaTabLayout.PerformLayout();
            schemaToolStrip.ResumeLayout(false);
            schemaToolStrip.PerformLayout();
            transformTab.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformToolStrip.ResumeLayout(false);
            transformToolStrip.PerformLayout();
            scriptTab.ResumeLayout(false);
            scriptedLayout.ResumeLayout(false);
            scriptedLayout.PerformLayout();
            scriptedToolStrip.ResumeLayout(false);
            scriptedToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel documentLayout;
        private Controls.TextBoxData templateTitleData;
        private BindingSource bindingTemplate;
        private TabPage sourceTab;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Controls.TextBoxData documentTitleData;
        private TabPage schemaTab;
        private TabPage transformTab;
        private TabPage scriptTab;
        private TableLayoutPanel schemaTabLayout;
        private Controls.ComboBoxData schemaIdData;
        private Controls.ComboBoxData objectScopeData;
        private Controls.SelectTextBoxData objectPathData;
        private ToolStrip schemaToolStrip;
        private Controls.TextBoxData schemaFileNameData;
        private Controls.TextBoxData schemaFileContentData;
        private Controls.TextBoxData transformFileNameData;
        private Controls.ComboBoxData transformIdData;
        private ToolStrip transformToolStrip;
        private Controls.TextBoxData transformFileContentData;
        private TableLayoutPanel scriptedLayout;
        private ToolStrip scriptedToolStrip;
        private Controls.TextBoxData scriptedFileName;
        private Controls.TextBoxData textBoxData2;
        private Controls.TextBoxData dataFileLocationData;
        private Controls.TextBoxData transformFileLocationData;
        private Controls.TextBoxData scriptedFileLocationData;
        private ToolStripButton scriptedOpenCommand;
        private ToolStripButton scriptedBuildCommand;
        private ToolStripButton scriptedSaveCommand;
        private ToolStripButton transformOpenCommand;
        private ToolStripButton transformSaveCommand;
        private ToolStripButton schemaOpenCommand;
        private ToolStripButton schemaBuildCommand;
        private ToolStripButton schemaSaveCommand;
        private BindingSource bindingSchema;
        private BindingSource bindingTransform;
        private BindingSource bindingDocument;
        private ErrorProvider errorProvider;
    }
}