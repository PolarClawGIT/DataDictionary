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
            components = new System.ComponentModel.Container();
            TableLayoutPanel schemaDocumentLayout;
            GroupBox objectGroupBox;
            TableLayoutPanel objectLayout;
            TableLayoutPanel objectBehaviorLayout;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaDocument));
            localPathData = new DataDictionary.Main.Controls.TextBoxData();
            documentContentData = new DataDictionary.Main.Controls.TextBoxData();
            objectKeepOrphaned = new CheckBox();
            objectIsExcluded = new CheckBox();
            isInModelData = new CheckBox();
            objectScopeData = new DataDictionary.Main.Controls.ComboBoxData();
            objectPathData = new DataDictionary.Main.Controls.SelectTextBoxData();
            schemaTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            documentFileData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            bindingSchema = new BindingSource(components);
            bindingDocument = new BindingSource(components);
            openFileDialog = new OpenFileDialog();
            errorProvider = new ErrorProvider(components);
            documentMenu = new ContextMenuStrip(components);
            saveFileDialog = new SaveFileDialog();
            schemaDocumentLayout = new TableLayoutPanel();
            objectGroupBox = new GroupBox();
            objectLayout = new TableLayoutPanel();
            objectBehaviorLayout = new TableLayoutPanel();
            schemaDocumentLayout.SuspendLayout();
            objectGroupBox.SuspendLayout();
            objectLayout.SuspendLayout();
            objectBehaviorLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // schemaDocumentLayout
            // 
            schemaDocumentLayout.ColumnCount = 1;
            schemaDocumentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            schemaDocumentLayout.Controls.Add(localPathData, 0, 3);
            schemaDocumentLayout.Controls.Add(documentContentData, 0, 5);
            schemaDocumentLayout.Controls.Add(objectGroupBox, 0, 2);
            schemaDocumentLayout.Controls.Add(schemaTitleData, 0, 1);
            schemaDocumentLayout.Controls.Add(templateTitleData, 0, 0);
            schemaDocumentLayout.Controls.Add(documentFileData, 0, 4);
            schemaDocumentLayout.Dock = DockStyle.Fill;
            schemaDocumentLayout.Location = new Point(0, 25);
            schemaDocumentLayout.Name = "schemaDocumentLayout";
            schemaDocumentLayout.RowCount = 6;
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle());
            schemaDocumentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            schemaDocumentLayout.Size = new Size(508, 610);
            schemaDocumentLayout.TabIndex = 4;
            // 
            // localPathData
            // 
            localPathData.AutoSize = true;
            localPathData.Dock = DockStyle.Fill;
            localPathData.HeaderText = "Local Path";
            localPathData.Location = new Point(3, 264);
            localPathData.Multiline = false;
            localPathData.Name = "localPathData";
            localPathData.ReadOnly = true;
            localPathData.Size = new Size(502, 44);
            localPathData.TabIndex = 8;
            localPathData.WordWrap = false;
            localPathData.Validated += LocalPathData_Validated;
            // 
            // documentContentData
            // 
            documentContentData.AutoSize = true;
            documentContentData.Dock = DockStyle.Fill;
            documentContentData.HeaderText = "Document Content";
            documentContentData.Location = new Point(3, 364);
            documentContentData.Multiline = true;
            documentContentData.Name = "documentContentData";
            documentContentData.ReadOnly = false;
            documentContentData.Size = new Size(502, 243);
            documentContentData.TabIndex = 6;
            documentContentData.WordWrap = false;
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
            objectLayout.Controls.Add(objectPathData, 0, 1);
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
            // objectPathData
            // 
            objectPathData.AutoSize = true;
            objectPathData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            objectPathData.Dock = DockStyle.Fill;
            objectPathData.HeaderText = "Object";
            objectPathData.Location = new Point(3, 55);
            objectPathData.Name = "objectPathData";
            objectPathData.ReadOnly = false;
            objectPathData.SelectIcon = (Image)resources.GetObject("objectPathData.SelectIcon");
            objectPathData.Size = new Size(490, 44);
            objectPathData.TabIndex = 3;
            objectPathData.SelectCommand += ObjectNameData_SelectCommand;
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
            // documentFileData
            // 
            documentFileData.AutoSize = true;
            documentFileData.Dock = DockStyle.Fill;
            documentFileData.HeaderText = "Document";
            documentFileData.Location = new Point(3, 314);
            documentFileData.Multiline = false;
            documentFileData.Name = "documentFileData";
            documentFileData.ReadOnly = false;
            documentFileData.Size = new Size(502, 44);
            documentFileData.TabIndex = 9;
            documentFileData.WordWrap = true;
            documentFileData.Validated += DocumentFileData_Validated;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // documentMenu
            // 
            documentMenu.Name = "documentMenu";
            documentMenu.Size = new Size(181, 26);
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
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSchema).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingDocument).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData schemaTitleData;
        private Controls.ComboBoxData sourceObjectData;
        private Controls.TextBoxData documentContentData;
        private TableLayoutPanel objectLayout;
        private Controls.ComboBoxData objectScopeData;
        private Controls.SelectTextBoxData objectPathData;
        private TableLayoutPanel objectBehaviorLayout;
        private CheckBox isInModelData;
        private CheckBox objectIsExcluded;
        private CheckBox objectKeepOrphaned;
        private BindingSource bindingTemplate;
        private BindingSource bindingSchema;
        private BindingSource bindingDocument;
        private OpenFileDialog openFileDialog;
        private FolderBrowserDialog folderBrowserDialog1;
        private Controls.TextBoxData localPathData;
        private ErrorProvider errorProvider;
        private Controls.TextBoxData documentFileData;
        private ContextMenuStrip documentMenu;
        private SaveFileDialog saveFileDialog;
    }
}