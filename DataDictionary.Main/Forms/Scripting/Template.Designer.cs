namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template
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
            TableLayoutPanel templateLayout;
            TableLayoutPanel schemaLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Template));
            TableLayoutPanel transformLayout;
            TableLayoutPanel documentLayout;
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            templateTabs = new TabControl();
            schemaTab = new TabPage();
            schemaData = new DataGridView();
            schemaTitleColumn = new DataGridViewTextBoxColumn();
            schemaToolStrip = new ToolStrip();
            addSchemaCommand = new ToolStripButton();
            openSchemaCommand = new ToolStripButton();
            executeSchemaCommand = new ToolStripButton();
            transformTab = new TabPage();
            transformToolStrip = new ToolStrip();
            addTransformCommand = new ToolStripButton();
            openTransformCommand = new ToolStripButton();
            executeTransformCommand = new ToolStripButton();
            transformsData = new DataGridView();
            transformTitleColumn = new DataGridViewTextBoxColumn();
            documentTab = new TabPage();
            documentToolStrip = new ToolStrip();
            openDocumentCommand = new ToolStripButton();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            bindingTemplate = new BindingSource(components);
            bindingSchema = new BindingSource(components);
            bindingTransform = new BindingSource(components);
            bindingObject = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            contextTemplate = new ContextMenuStrip(components);
            templateLayout = new TableLayoutPanel();
            schemaLayout = new TableLayoutPanel();
            transformLayout = new TableLayoutPanel();
            documentLayout = new TableLayoutPanel();
            templateLayout.SuspendLayout();
            templateTabs.SuspendLayout();
            schemaTab.SuspendLayout();
            schemaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)schemaData).BeginInit();
            schemaToolStrip.SuspendLayout();
            transformTab.SuspendLayout();
            transformLayout.SuspendLayout();
            transformToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)transformsData).BeginInit();
            documentTab.SuspendLayout();
            documentLayout.SuspendLayout();
            documentToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
            SuspendLayout();
            // 
            // templateLayout
            // 
            templateLayout.ColumnCount = 1;
            templateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateLayout.Controls.Add(templateTitleData, 0, 0);
            templateLayout.Controls.Add(templateDescriptionData, 0, 1);
            templateLayout.Controls.Add(templateTabs, 0, 2);
            templateLayout.Dock = DockStyle.Fill;
            templateLayout.Location = new Point(0, 25);
            templateLayout.Name = "templateLayout";
            templateLayout.RowCount = 3;
            templateLayout.RowStyles.Add(new RowStyle());
            templateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            templateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            templateLayout.Size = new Size(470, 608);
            templateLayout.TabIndex = 4;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template Title";
            templateTitleData.Location = new Point(3, 3);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = false;
            templateTitleData.Size = new Size(464, 44);
            templateTitleData.TabIndex = 0;
            templateTitleData.WordWrap = true;
            // 
            // templateDescriptionData
            // 
            templateDescriptionData.AutoSize = true;
            templateDescriptionData.Dock = DockStyle.Fill;
            templateDescriptionData.HeaderText = "Description";
            templateDescriptionData.Location = new Point(3, 53);
            templateDescriptionData.Multiline = true;
            templateDescriptionData.Name = "templateDescriptionData";
            templateDescriptionData.ReadOnly = false;
            templateDescriptionData.Size = new Size(464, 217);
            templateDescriptionData.TabIndex = 1;
            templateDescriptionData.WordWrap = true;
            // 
            // templateTabs
            // 
            templateTabs.Controls.Add(schemaTab);
            templateTabs.Controls.Add(transformTab);
            templateTabs.Controls.Add(documentTab);
            templateTabs.Dock = DockStyle.Fill;
            templateTabs.Location = new Point(3, 276);
            templateTabs.Name = "templateTabs";
            templateTabs.SelectedIndex = 0;
            templateTabs.Size = new Size(464, 329);
            templateTabs.TabIndex = 2;
            // 
            // schemaTab
            // 
            schemaTab.BackColor = SystemColors.Control;
            schemaTab.Controls.Add(schemaLayout);
            schemaTab.Location = new Point(4, 24);
            schemaTab.Name = "schemaTab";
            schemaTab.Padding = new Padding(3);
            schemaTab.Size = new Size(456, 301);
            schemaTab.TabIndex = 1;
            schemaTab.Text = "Schema";
            // 
            // schemaLayout
            // 
            schemaLayout.ColumnCount = 1;
            schemaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaLayout.Controls.Add(schemaData, 0, 1);
            schemaLayout.Controls.Add(schemaToolStrip, 0, 0);
            schemaLayout.Dock = DockStyle.Fill;
            schemaLayout.Location = new Point(3, 3);
            schemaLayout.Name = "schemaLayout";
            schemaLayout.RowCount = 2;
            schemaLayout.RowStyles.Add(new RowStyle());
            schemaLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaLayout.Size = new Size(450, 295);
            schemaLayout.TabIndex = 7;
            // 
            // schemaData
            // 
            schemaData.AllowUserToAddRows = false;
            schemaData.AllowUserToDeleteRows = false;
            schemaData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            schemaData.Columns.AddRange(new DataGridViewColumn[] { schemaTitleColumn });
            schemaData.Dock = DockStyle.Fill;
            schemaData.Location = new Point(3, 28);
            schemaData.Name = "schemaData";
            schemaData.ReadOnly = true;
            schemaData.Size = new Size(444, 264);
            schemaData.TabIndex = 6;
            // 
            // schemaTitleColumn
            // 
            schemaTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            schemaTitleColumn.DataPropertyName = "SchemaTitle";
            schemaTitleColumn.HeaderText = "Schema";
            schemaTitleColumn.Name = "schemaTitleColumn";
            schemaTitleColumn.ReadOnly = true;
            // 
            // schemaToolStrip
            // 
            schemaToolStrip.Items.AddRange(new ToolStripItem[] { addSchemaCommand, openSchemaCommand, executeSchemaCommand });
            schemaToolStrip.Location = new Point(0, 0);
            schemaToolStrip.Name = "schemaToolStrip";
            schemaToolStrip.Size = new Size(450, 25);
            schemaToolStrip.TabIndex = 7;
            schemaToolStrip.Text = "toolStrip1";
            // 
            // addSchemaCommand
            // 
            addSchemaCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            addSchemaCommand.Image = (Image)resources.GetObject("addSchemaCommand.Image");
            addSchemaCommand.ImageTransparentColor = Color.Magenta;
            addSchemaCommand.Name = "addSchemaCommand";
            addSchemaCommand.Size = new Size(23, 22);
            addSchemaCommand.Text = "add Schema";
            addSchemaCommand.Click += AddSchemaCommand_Click;
            // 
            // openSchemaCommand
            // 
            openSchemaCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openSchemaCommand.Image = (Image)resources.GetObject("openSchemaCommand.Image");
            openSchemaCommand.ImageTransparentColor = Color.Magenta;
            openSchemaCommand.Name = "openSchemaCommand";
            openSchemaCommand.Size = new Size(23, 22);
            openSchemaCommand.Text = "open Schema";
            openSchemaCommand.Click += OpenSchemaCommand_Click;
            // 
            // executeSchemaCommand
            // 
            executeSchemaCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            executeSchemaCommand.Image = (Image)resources.GetObject("executeSchemaCommand.Image");
            executeSchemaCommand.ImageTransparentColor = Color.Magenta;
            executeSchemaCommand.Name = "executeSchemaCommand";
            executeSchemaCommand.Size = new Size(23, 22);
            executeSchemaCommand.Text = "build Schema Documents";
            executeSchemaCommand.Click += ExecuteSchemaCommand_Click;
            // 
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformLayout);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(456, 301);
            transformTab.TabIndex = 0;
            transformTab.Text = "Transforms";
            // 
            // transformLayout
            // 
            transformLayout.ColumnCount = 1;
            transformLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            transformLayout.Controls.Add(transformToolStrip, 0, 0);
            transformLayout.Controls.Add(transformsData, 0, 1);
            transformLayout.Dock = DockStyle.Fill;
            transformLayout.Location = new Point(3, 3);
            transformLayout.Name = "transformLayout";
            transformLayout.RowCount = 2;
            transformLayout.RowStyles.Add(new RowStyle());
            transformLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            transformLayout.Size = new Size(450, 295);
            transformLayout.TabIndex = 6;
            // 
            // transformToolStrip
            // 
            transformToolStrip.Items.AddRange(new ToolStripItem[] { addTransformCommand, openTransformCommand, executeTransformCommand });
            transformToolStrip.Location = new Point(0, 0);
            transformToolStrip.Name = "transformToolStrip";
            transformToolStrip.Size = new Size(450, 25);
            transformToolStrip.TabIndex = 0;
            transformToolStrip.Text = "toolStrip1";
            // 
            // addTransformCommand
            // 
            addTransformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            addTransformCommand.Image = (Image)resources.GetObject("addTransformCommand.Image");
            addTransformCommand.ImageTransparentColor = Color.Magenta;
            addTransformCommand.Name = "addTransformCommand";
            addTransformCommand.Size = new Size(23, 22);
            addTransformCommand.Text = "add Transform";
            addTransformCommand.Click += AddTransformCommand_Click;
            // 
            // openTransformCommand
            // 
            openTransformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openTransformCommand.Image = (Image)resources.GetObject("openTransformCommand.Image");
            openTransformCommand.ImageTransparentColor = Color.Magenta;
            openTransformCommand.Name = "openTransformCommand";
            openTransformCommand.Size = new Size(23, 22);
            openTransformCommand.Text = "open Transform";
            openTransformCommand.Click += OpenTransformCommand_Click;
            // 
            // executeTransformCommand
            // 
            executeTransformCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            executeTransformCommand.Image = (Image)resources.GetObject("executeTransformCommand.Image");
            executeTransformCommand.ImageTransparentColor = Color.Magenta;
            executeTransformCommand.Name = "executeTransformCommand";
            executeTransformCommand.Size = new Size(23, 22);
            executeTransformCommand.Text = "build Transform Documents";
            executeTransformCommand.Click += ExecuteTransformCommand_Click;
            // 
            // transformsData
            // 
            transformsData.AllowUserToAddRows = false;
            transformsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transformsData.Columns.AddRange(new DataGridViewColumn[] { transformTitleColumn });
            transformsData.Dock = DockStyle.Fill;
            transformsData.Location = new Point(3, 28);
            transformsData.Name = "transformsData";
            transformsData.ReadOnly = true;
            transformsData.Size = new Size(444, 264);
            transformsData.TabIndex = 5;
            // 
            // transformTitleColumn
            // 
            transformTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            transformTitleColumn.DataPropertyName = "TransformTitle";
            transformTitleColumn.HeaderText = "Transform";
            transformTitleColumn.Name = "transformTitleColumn";
            transformTitleColumn.ReadOnly = true;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(documentLayout);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(456, 301);
            documentTab.TabIndex = 3;
            documentTab.Text = "Documents";
            // 
            // documentLayout
            // 
            documentLayout.ColumnCount = 1;
            documentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            documentLayout.Controls.Add(documentToolStrip, 0, 0);
            documentLayout.Controls.Add(documentData, 0, 1);
            documentLayout.Dock = DockStyle.Fill;
            documentLayout.Location = new Point(3, 3);
            documentLayout.Name = "documentLayout";
            documentLayout.RowCount = 2;
            documentLayout.RowStyles.Add(new RowStyle());
            documentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            documentLayout.Size = new Size(450, 295);
            documentLayout.TabIndex = 8;
            // 
            // documentToolStrip
            // 
            documentToolStrip.Items.AddRange(new ToolStripItem[] { openDocumentCommand });
            documentToolStrip.Location = new Point(0, 0);
            documentToolStrip.Name = "documentToolStrip";
            documentToolStrip.Size = new Size(450, 25);
            documentToolStrip.TabIndex = 0;
            documentToolStrip.Text = "toolStrip1";
            // 
            // openDocumentCommand
            // 
            openDocumentCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openDocumentCommand.Image = (Image)resources.GetObject("openDocumentCommand.Image");
            openDocumentCommand.ImageTransparentColor = Color.Magenta;
            openDocumentCommand.Name = "openDocumentCommand";
            openDocumentCommand.Size = new Size(23, 22);
            openDocumentCommand.Text = "open Document";
            openDocumentCommand.Click += OpenDocumentCommand_Click;
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { FileNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 28);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(444, 264);
            documentData.TabIndex = 7;
            // 
            // FileNameColumn
            // 
            FileNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileNameColumn.DataPropertyName = "FileName";
            FileNameColumn.HeaderText = "File Name";
            FileNameColumn.Name = "FileNameColumn";
            FileNameColumn.ReadOnly = true;
            // 
            // contextTemplate
            // 
            contextTemplate.Name = "contextTemplate";
            contextTemplate.Size = new Size(61, 4);
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 633);
            Controls.Add(templateLayout);
            Name = "Template";
            Text = "Template";
            Load += Template_Load;
            Controls.SetChildIndex(templateLayout, 0);
            templateLayout.ResumeLayout(false);
            templateLayout.PerformLayout();
            templateTabs.ResumeLayout(false);
            schemaTab.ResumeLayout(false);
            schemaLayout.ResumeLayout(false);
            schemaLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)schemaData).EndInit();
            schemaToolStrip.ResumeLayout(false);
            schemaToolStrip.PerformLayout();
            transformTab.ResumeLayout(false);
            transformLayout.ResumeLayout(false);
            transformLayout.PerformLayout();
            transformToolStrip.ResumeLayout(false);
            transformToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)transformsData).EndInit();
            documentTab.ResumeLayout(false);
            documentLayout.ResumeLayout(false);
            documentLayout.PerformLayout();
            documentToolStrip.ResumeLayout(false);
            documentToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource bindingTemplate;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private DataGridView transformsData;
        private DataGridView schemaData;
        private DataGridViewTextBoxColumn transformTitleColumn;
        private DataGridViewTextBoxColumn schemaTitleColumn;
        private DataGridView documentData;
        private DataGridViewTextBoxColumn FileNameColumn;
        private TabControl templateTabs;
        private TabPage schemaTab;
        private TabPage transformTab;
        private TabPage documentTab;
        private BindingSource bindingSchema;
        private BindingSource bindingTransform;
        private BindingSource bindingObject;
        private BindingSource bindingDocument;
        private ContextMenuStrip contextTemplate;
        private ToolStripMenuItem templateOpenDocumentCommand;
        private ToolStrip schemaToolStrip;
        private ToolStripButton addSchemaCommand;
        private ToolStripButton openSchemaCommand;
        private ToolStripButton executeSchemaCommand;
        private ToolStrip transformToolStrip;
        private ToolStripButton addTransformCommand;
        private ToolStripButton openTransformCommand;
        private ToolStripButton executeTransformCommand;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStrip documentToolStrip;
        private ToolStripButton openDocumentCommand;
    }
}