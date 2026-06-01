namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingManager
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
            templateNavigation = new DataGridView();
            templateTitleColumn = new DataGridViewTextBoxColumn();
            templateDescriptionColumn = new DataGridViewTextBoxColumn();
            inModelColumn = new DataGridViewCheckBoxColumn();
            inDatabaseColumn = new DataGridViewCheckBoxColumn();
            templateTitleData = new DataDictionary.Main.Controls.TextBoxData();
            templateDescriptionData = new DataDictionary.Main.Controls.TextBoxData();
            bindingTemplate = new BindingSource(components);
            templateLayout = new TableLayoutPanel();
            templateLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)templateNavigation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).BeginInit();
            SuspendLayout();
            // 
            // templateLayout
            // 
            templateLayout.ColumnCount = 1;
            templateLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateLayout.Controls.Add(templateNavigation, 0, 0);
            templateLayout.Controls.Add(templateTitleData, 0, 1);
            templateLayout.Controls.Add(templateDescriptionData, 0, 2);
            templateLayout.Dock = DockStyle.Fill;
            templateLayout.Location = new Point(0, 25);
            templateLayout.Name = "templateLayout";
            templateLayout.RowCount = 3;
            templateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            templateLayout.RowStyles.Add(new RowStyle());
            templateLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            templateLayout.Size = new Size(458, 425);
            templateLayout.TabIndex = 4;
            // 
            // templateNavigation
            // 
            templateNavigation.AllowUserToAddRows = false;
            templateNavigation.AllowUserToDeleteRows = false;
            templateNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            templateNavigation.Columns.AddRange(new DataGridViewColumn[] { templateTitleColumn, templateDescriptionColumn, inModelColumn, inDatabaseColumn });
            templateNavigation.Dock = DockStyle.Fill;
            templateNavigation.Location = new Point(3, 3);
            templateNavigation.Name = "templateNavigation";
            templateNavigation.ReadOnly = true;
            templateNavigation.Size = new Size(452, 219);
            templateNavigation.TabIndex = 0;
            // 
            // templateTitleColumn
            // 
            templateTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            templateTitleColumn.DataPropertyName = "TemplateTitle";
            templateTitleColumn.FillWeight = 40F;
            templateTitleColumn.HeaderText = "Template";
            templateTitleColumn.Name = "templateTitleColumn";
            templateTitleColumn.ReadOnly = true;
            // 
            // templateDescriptionColumn
            // 
            templateDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            templateDescriptionColumn.DataPropertyName = "TemplateDescription";
            templateDescriptionColumn.FillWeight = 60F;
            templateDescriptionColumn.HeaderText = "Description";
            templateDescriptionColumn.Name = "templateDescriptionColumn";
            templateDescriptionColumn.ReadOnly = true;
            // 
            // inModelColumn
            // 
            inModelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            inModelColumn.DataPropertyName = "InModel";
            inModelColumn.HeaderText = "In Model";
            inModelColumn.Name = "inModelColumn";
            inModelColumn.ReadOnly = true;
            inModelColumn.Width = 60;
            // 
            // inDatabaseColumn
            // 
            inDatabaseColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            inDatabaseColumn.DataPropertyName = "InDatabase";
            inDatabaseColumn.HeaderText = "In Database";
            inDatabaseColumn.Name = "inDatabaseColumn";
            inDatabaseColumn.ReadOnly = true;
            inDatabaseColumn.Width = 74;
            // 
            // templateTitleData
            // 
            templateTitleData.AutoSize = true;
            templateTitleData.Dock = DockStyle.Fill;
            templateTitleData.HeaderText = "Template";
            templateTitleData.Location = new Point(3, 228);
            templateTitleData.Multiline = false;
            templateTitleData.Name = "templateTitleData";
            templateTitleData.ReadOnly = false;
            templateTitleData.Size = new Size(452, 44);
            templateTitleData.TabIndex = 1;
            templateTitleData.WordWrap = true;
            // 
            // templateDescriptionData
            // 
            templateDescriptionData.AutoSize = true;
            templateDescriptionData.Dock = DockStyle.Fill;
            templateDescriptionData.HeaderText = "Description";
            templateDescriptionData.Location = new Point(3, 278);
            templateDescriptionData.Multiline = true;
            templateDescriptionData.Name = "templateDescriptionData";
            templateDescriptionData.ReadOnly = false;
            templateDescriptionData.Size = new Size(452, 144);
            templateDescriptionData.TabIndex = 2;
            templateDescriptionData.WordWrap = true;
            // 
            // bindingTemplate
            // 
            bindingTemplate.CurrentChanged += BindingTemplate_CurrentChanged;
            // 
            // ScriptingManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 450);
            Controls.Add(templateLayout);
            Name = "ScriptingManager";
            Text = "TemplateManager";
            Load += TemplateManager_Load;
            Controls.SetChildIndex(templateLayout, 0);
            templateLayout.ResumeLayout(false);
            templateLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)templateNavigation).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingTemplate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource bindingTemplate;
        private TableLayoutPanel templateLayout;
        private DataGridView templateNavigation;
        private Controls.TextBoxData templateTitleData;
        private Controls.TextBoxData templateDescriptionData;
        private DataGridViewTextBoxColumn templateTitleColumn;
        private DataGridViewTextBoxColumn templateDescriptionColumn;
        private DataGridViewCheckBoxColumn inModelColumn;
        private DataGridViewCheckBoxColumn inDatabaseColumn;
    }
}