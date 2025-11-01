namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateManager
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
            TableLayoutPanel templateManagerLayout;
            managerData = new DataGridView();
            titleColumn = new DataGridViewTextBoxColumn();
            itemTypeColumn = new DataGridViewTextBoxColumn();
            inModelColumn = new DataGridViewCheckBoxColumn();
            inDatabaseColumn = new DataGridViewCheckBoxColumn();
            titleData = new DataDictionary.Main.Controls.TextBoxData();
            descriptionData = new DataDictionary.Main.Controls.TextBoxData();
            templateCommands = new ContextMenuStrip(components);
            newDataSource = new ToolStripMenuItem();
            newTemplate = new ToolStripMenuItem();
            bindingManager = new BindingSource(components);
            templateManagerLayout = new TableLayoutPanel();
            templateManagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)managerData).BeginInit();
            templateCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingManager).BeginInit();
            SuspendLayout();
            // 
            // templateManagerLayout
            // 
            templateManagerLayout.ColumnCount = 1;
            templateManagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            templateManagerLayout.Controls.Add(managerData, 0, 0);
            templateManagerLayout.Controls.Add(titleData, 0, 1);
            templateManagerLayout.Controls.Add(descriptionData, 0, 2);
            templateManagerLayout.Dock = DockStyle.Fill;
            templateManagerLayout.Location = new Point(0, 25);
            templateManagerLayout.Name = "templateManagerLayout";
            templateManagerLayout.RowCount = 3;
            templateManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            templateManagerLayout.RowStyles.Add(new RowStyle());
            templateManagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            templateManagerLayout.Size = new Size(610, 447);
            templateManagerLayout.TabIndex = 4;
            // 
            // managerData
            // 
            managerData.AllowUserToAddRows = false;
            managerData.AllowUserToDeleteRows = false;
            managerData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            managerData.Columns.AddRange(new DataGridViewColumn[] { titleColumn, itemTypeColumn, inModelColumn, inDatabaseColumn });
            managerData.Dock = DockStyle.Fill;
            managerData.Location = new Point(3, 3);
            managerData.Name = "managerData";
            managerData.Size = new Size(604, 232);
            managerData.TabIndex = 0;
            // 
            // titleColumn
            // 
            titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            titleColumn.DataPropertyName = "Title";
            titleColumn.HeaderText = "Title";
            titleColumn.Name = "titleColumn";
            titleColumn.ReadOnly = true;
            // 
            // itemTypeColumn
            // 
            itemTypeColumn.DataPropertyName = "Type";
            itemTypeColumn.HeaderText = "Type";
            itemTypeColumn.Name = "itemTypeColumn";
            itemTypeColumn.ReadOnly = true;
            // 
            // inModelColumn
            // 
            inModelColumn.DataPropertyName = "InModel";
            inModelColumn.HeaderText = "in Model";
            inModelColumn.Name = "inModelColumn";
            // 
            // inDatabaseColumn
            // 
            inDatabaseColumn.DataPropertyName = "InDatabase";
            inDatabaseColumn.HeaderText = "in Database";
            inDatabaseColumn.Name = "inDatabaseColumn";
            inDatabaseColumn.ReadOnly = true;
            // 
            // titleData
            // 
            titleData.AutoSize = true;
            titleData.Dock = DockStyle.Fill;
            titleData.HeaderText = "Title";
            titleData.Location = new Point(3, 241);
            titleData.Multiline = false;
            titleData.Name = "titleData";
            titleData.ReadOnly = true;
            titleData.Size = new Size(604, 44);
            titleData.TabIndex = 1;
            titleData.WordWrap = true;
            // 
            // descriptionData
            // 
            descriptionData.AutoSize = true;
            descriptionData.Dock = DockStyle.Fill;
            descriptionData.HeaderText = "Description";
            descriptionData.Location = new Point(3, 291);
            descriptionData.Multiline = true;
            descriptionData.Name = "descriptionData";
            descriptionData.ReadOnly = true;
            descriptionData.Size = new Size(604, 153);
            descriptionData.TabIndex = 2;
            descriptionData.WordWrap = true;
            // 
            // templateCommands
            // 
            templateCommands.Items.AddRange(new ToolStripItem[] { newDataSource, newTemplate });
            templateCommands.Name = "templateCommands";
            templateCommands.Size = new Size(163, 48);
            // 
            // newDataSource
            // 
            newDataSource.MergeAction = MergeAction.Insert;
            newDataSource.MergeIndex = 0;
            newDataSource.Name = "newDataSource";
            newDataSource.Size = new Size(162, 22);
            newDataSource.Text = "new Data Source";
            newDataSource.ToolTipText = "Create a new Data Source";
            newDataSource.Click += NewDataSource_Click;
            // 
            // newTemplate
            // 
            newTemplate.MergeAction = MergeAction.Insert;
            newTemplate.MergeIndex = 0;
            newTemplate.Name = "newTemplate";
            newTemplate.Size = new Size(162, 22);
            newTemplate.Text = "new Template";
            newTemplate.ToolTipText = "Create a new Template";
            newTemplate.Click += NewTemplate_Click;
            // 
            // bindingManager
            // 
            bindingManager.CurrentChanged += BindingManager_CurrentChanged;
            // 
            // TemplateManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 472);
            Controls.Add(templateManagerLayout);
            Name = "TemplateManager";
            Text = "Template Manager";
            Load += TemplateManager_Load;
            Controls.SetChildIndex(templateManagerLayout, 0);
            templateManagerLayout.ResumeLayout(false);
            templateManagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)managerData).EndInit();
            templateCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingManager).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView managerData;
        private Controls.TextBoxData titleData;
        private Controls.TextBoxData descriptionData;
        private ContextMenuStrip templateCommands;
        private ToolStripMenuItem newTemplate;
        private ToolStripMenuItem newDataSource;
        private DataGridViewTextBoxColumn titleColumn;
        private DataGridViewTextBoxColumn itemTypeColumn;
        private DataGridViewCheckBoxColumn inModelColumn;
        private DataGridViewCheckBoxColumn inDatabaseColumn;
        private BindingSource bindingManager;
    }
}