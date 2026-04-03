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
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            templateTabs = new TabControl();
            scemaTab = new TabPage();
            schemaData = new DataGridView();
            schemaTitleColumn = new DataGridViewTextBoxColumn();
            transformTab = new TabPage();
            transformsData = new DataGridView();
            transformTitleColumn = new DataGridViewTextBoxColumn();
            objectTab = new TabPage();
            objectData = new DataGridView();
            objectScopeColumn = new DataGridViewTextBoxColumn();
            objectNameColumn = new DataGridViewTextBoxColumn();
            documentTab = new TabPage();
            documentData = new DataGridView();
            FileNameColumn = new DataGridViewTextBoxColumn();
            bindingTemplate = new BindingSource(components);
            bindingSchema = new BindingSource(components);
            bindingTransform = new BindingSource(components);
            bindingObject = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            contextTemplate = new ContextMenuStrip(components);
            manageSchemaCommand = new ToolStripMenuItem();
            manageTransformCommand = new ToolStripMenuItem();
            manageObjectCommand = new ToolStripMenuItem();
            manageDocumentCommand = new ToolStripMenuItem();
            executeSchemaCommand = new ToolStripMenuItem();
            executeTransformCommand = new ToolStripMenuItem();
            templateLayout = new TableLayoutPanel();
            templateLayout.SuspendLayout();
            templateTabs.SuspendLayout();
            scemaTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)schemaData).BeginInit();
            transformTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)transformsData).BeginInit();
            objectTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectData).BeginInit();
            documentTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)documentData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
            contextTemplate.SuspendLayout();
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
            templateLayout.Size = new Size(470, 425);
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
            templateDescriptionData.Size = new Size(464, 144);
            templateDescriptionData.TabIndex = 1;
            templateDescriptionData.WordWrap = true;
            // 
            // templateTabs
            // 
            templateTabs.Controls.Add(scemaTab);
            templateTabs.Controls.Add(transformTab);
            templateTabs.Controls.Add(objectTab);
            templateTabs.Controls.Add(documentTab);
            templateTabs.Dock = DockStyle.Fill;
            templateTabs.Location = new Point(3, 203);
            templateTabs.Name = "templateTabs";
            templateTabs.SelectedIndex = 0;
            templateTabs.Size = new Size(464, 219);
            templateTabs.TabIndex = 2;
            // 
            // scemaTab
            // 
            scemaTab.BackColor = SystemColors.Control;
            scemaTab.Controls.Add(schemaData);
            scemaTab.Location = new Point(4, 24);
            scemaTab.Name = "scemaTab";
            scemaTab.Padding = new Padding(3);
            scemaTab.Size = new Size(456, 191);
            scemaTab.TabIndex = 1;
            scemaTab.Text = "Schema";
            // 
            // schemaData
            // 
            schemaData.AllowUserToAddRows = false;
            schemaData.AllowUserToDeleteRows = false;
            schemaData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            schemaData.Columns.AddRange(new DataGridViewColumn[] { schemaTitleColumn });
            schemaData.Dock = DockStyle.Fill;
            schemaData.Location = new Point(3, 3);
            schemaData.Name = "schemaData";
            schemaData.ReadOnly = true;
            schemaData.Size = new Size(450, 185);
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
            // transformTab
            // 
            transformTab.BackColor = SystemColors.Control;
            transformTab.Controls.Add(transformsData);
            transformTab.Location = new Point(4, 24);
            transformTab.Name = "transformTab";
            transformTab.Padding = new Padding(3);
            transformTab.Size = new Size(456, 191);
            transformTab.TabIndex = 0;
            transformTab.Text = "Transforms";
            // 
            // transformsData
            // 
            transformsData.AllowUserToAddRows = false;
            transformsData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transformsData.Columns.AddRange(new DataGridViewColumn[] { transformTitleColumn });
            transformsData.Dock = DockStyle.Fill;
            transformsData.Location = new Point(3, 3);
            transformsData.Name = "transformsData";
            transformsData.ReadOnly = true;
            transformsData.Size = new Size(450, 185);
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
            // objectTab
            // 
            objectTab.BackColor = SystemColors.Control;
            objectTab.Controls.Add(objectData);
            objectTab.Location = new Point(4, 24);
            objectTab.Name = "objectTab";
            objectTab.Padding = new Padding(3);
            objectTab.Size = new Size(456, 191);
            objectTab.TabIndex = 2;
            objectTab.Text = "Objects";
            // 
            // objectData
            // 
            objectData.AllowUserToAddRows = false;
            objectData.AllowUserToDeleteRows = false;
            objectData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectData.Columns.AddRange(new DataGridViewColumn[] { objectScopeColumn, objectNameColumn });
            objectData.Dock = DockStyle.Fill;
            objectData.Location = new Point(3, 3);
            objectData.Name = "objectData";
            objectData.ReadOnly = true;
            objectData.Size = new Size(450, 185);
            objectData.TabIndex = 8;
            // 
            // objectScopeColumn
            // 
            objectScopeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectScopeColumn.DataPropertyName = "ObjectScope";
            objectScopeColumn.FillWeight = 40F;
            objectScopeColumn.HeaderText = "Object Scope";
            objectScopeColumn.Name = "objectScopeColumn";
            objectScopeColumn.ReadOnly = true;
            // 
            // objectNameColumn
            // 
            objectNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            objectNameColumn.DataPropertyName = "ObjectName";
            objectNameColumn.FillWeight = 60F;
            objectNameColumn.HeaderText = "Object Name";
            objectNameColumn.Name = "objectNameColumn";
            objectNameColumn.ReadOnly = true;
            // 
            // documentTab
            // 
            documentTab.BackColor = SystemColors.Control;
            documentTab.Controls.Add(documentData);
            documentTab.Location = new Point(4, 24);
            documentTab.Name = "documentTab";
            documentTab.Padding = new Padding(3);
            documentTab.Size = new Size(456, 191);
            documentTab.TabIndex = 3;
            documentTab.Text = "Documents";
            // 
            // documentData
            // 
            documentData.AllowUserToAddRows = false;
            documentData.AllowUserToDeleteRows = false;
            documentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            documentData.Columns.AddRange(new DataGridViewColumn[] { FileNameColumn });
            documentData.Dock = DockStyle.Fill;
            documentData.Location = new Point(3, 3);
            documentData.Name = "documentData";
            documentData.ReadOnly = true;
            documentData.Size = new Size(450, 185);
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
            contextTemplate.Items.AddRange(new ToolStripItem[] { manageSchemaCommand, manageTransformCommand, manageObjectCommand, manageDocumentCommand, executeSchemaCommand, executeTransformCommand });
            contextTemplate.Name = "contextTemplate";
            contextTemplate.Size = new Size(223, 158);
            // 
            // manageSchemaCommand
            // 
            manageSchemaCommand.Name = "manageSchemaCommand";
            manageSchemaCommand.Size = new Size(222, 22);
            manageSchemaCommand.Text = "Schema";
            manageSchemaCommand.Click += ManageSchemaCommand_Click;
            // 
            // manageTransformCommand
            // 
            manageTransformCommand.Name = "manageTransformCommand";
            manageTransformCommand.Size = new Size(222, 22);
            manageTransformCommand.Text = "Transform";
            manageTransformCommand.Click += ManageTransformCommand_Click;
            // 
            // manageObjectCommand
            // 
            manageObjectCommand.Name = "manageObjectCommand";
            manageObjectCommand.Size = new Size(222, 22);
            manageObjectCommand.Text = "Object";
            manageObjectCommand.Click += ManageObjectCommand_Click;
            // 
            // manageDocumentCommand
            // 
            manageDocumentCommand.Name = "manageDocumentCommand";
            manageDocumentCommand.Size = new Size(222, 22);
            manageDocumentCommand.Text = "Document";
            manageDocumentCommand.Click += ManageDocumentCommand_Click;
            // 
            // executeSchemaCommand
            // 
            executeSchemaCommand.Name = "executeSchemaCommand";
            executeSchemaCommand.Size = new Size(222, 22);
            executeSchemaCommand.Text = "build Schema Documents";
            executeSchemaCommand.Click += ExecuteSchemaCommand_Click;
            // 
            // executeTransformCommand
            // 
            executeTransformCommand.Name = "executeTransformCommand";
            executeTransformCommand.Size = new Size(222, 22);
            executeTransformCommand.Text = "build Transform Documents";
            executeTransformCommand.Click += ExecuteTransformCommand_Click;
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 450);
            Controls.Add(templateLayout);
            Name = "Template";
            Text = "Template";
            Load += Template_Load;
            Controls.SetChildIndex(templateLayout, 0);
            templateLayout.ResumeLayout(false);
            templateLayout.PerformLayout();
            templateTabs.ResumeLayout(false);
            scemaTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)schemaData).EndInit();
            transformTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)transformsData).EndInit();
            objectTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)objectData).EndInit();
            documentTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)documentData).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTransform).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingObject).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            contextTemplate.ResumeLayout(false);
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
        private DataGridView objectData;
        private DataGridViewTextBoxColumn objectScopeColumn;
        private DataGridViewTextBoxColumn objectNameColumn;
        private TabControl templateTabs;
        private TabPage scemaTab;
        private TabPage transformTab;
        private TabPage objectTab;
        private TabPage documentTab;
        private BindingSource bindingSchema;
        private BindingSource bindingTransform;
        private BindingSource bindingObject;
        private BindingSource bindingDocument;
        private ContextMenuStrip contextTemplate;
        private ToolStripMenuItem manageSchemaCommand;
        private ToolStripMenuItem manageTransformCommand;
        private ToolStripMenuItem manageObjectCommand;
        private ToolStripMenuItem manageDocumentCommand;
        private ToolStripMenuItem executeSchemaCommand;
        private ToolStripMenuItem executeTransformCommand;
    }
}