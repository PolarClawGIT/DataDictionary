namespace DataDictionary.Main.Dialogs
{
    partial class LoadModelFromDatabase
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
            TableLayoutPanel formLayout;
            TableLayoutPanel buttonLayout;
            serverNameData = new Controls.TextBoxData();
            databaseNameData = new Controls.TextBoxData();
            modelList = new DataGridView();
            modelTitleColumn = new DataGridViewTextBoxColumn();
            modelDescriptionColumn = new DataGridViewTextBoxColumn();
            modelObsoleteColumn = new DataGridViewCheckBoxColumn();
            modelTitleData = new Controls.TextBoxData();
            loadCommand = new Button();
            deleteCommand = new Button();
            formLayout = new TableLayoutPanel();
            buttonLayout = new TableLayoutPanel();
            formLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)modelList).BeginInit();
            buttonLayout.SuspendLayout();
            SuspendLayout();
            // 
            // formLayout
            // 
            formLayout.ColumnCount = 1;
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            formLayout.Controls.Add(serverNameData, 0, 0);
            formLayout.Controls.Add(databaseNameData, 0, 1);
            formLayout.Controls.Add(modelList, 0, 2);
            formLayout.Controls.Add(modelTitleData, 0, 3);
            formLayout.Controls.Add(buttonLayout, 0, 4);
            formLayout.Dock = DockStyle.Fill;
            formLayout.Location = new Point(0, 0);
            formLayout.Name = "formLayout";
            formLayout.RowCount = 5;
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.Size = new Size(363, 450);
            formLayout.TabIndex = 0;
            // 
            // serverNameData
            // 
            serverNameData.AutoSize = true;
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.HeaderText = "Sever Name";
            serverNameData.Location = new Point(3, 3);
            serverNameData.Multiline = false;
            serverNameData.Name = "serverNameData";
            serverNameData.ReadOnly = true;
            serverNameData.Size = new Size(357, 44);
            serverNameData.TabIndex = 0;
            // 
            // databaseNameData
            // 
            databaseNameData.AutoSize = true;
            databaseNameData.Dock = DockStyle.Fill;
            databaseNameData.HeaderText = "Database Name";
            databaseNameData.Location = new Point(3, 53);
            databaseNameData.Multiline = false;
            databaseNameData.Name = "databaseNameData";
            databaseNameData.ReadOnly = true;
            databaseNameData.Size = new Size(357, 44);
            databaseNameData.TabIndex = 1;
            // 
            // modelList
            // 
            modelList.AllowUserToAddRows = false;
            modelList.AllowUserToDeleteRows = false;
            modelList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            modelList.Columns.AddRange(new DataGridViewColumn[] { modelTitleColumn, modelDescriptionColumn, modelObsoleteColumn });
            modelList.Dock = DockStyle.Fill;
            modelList.Location = new Point(3, 103);
            modelList.MultiSelect = false;
            modelList.Name = "modelList";
            modelList.RowTemplate.Height = 25;
            modelList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            modelList.Size = new Size(357, 259);
            modelList.TabIndex = 2;
            modelList.SelectionChanged += modelList_SelectionChanged;
            // 
            // modelTitleColumn
            // 
            modelTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            modelTitleColumn.DataPropertyName = "ModelTitle";
            modelTitleColumn.FillWeight = 70F;
            modelTitleColumn.HeaderText = "Model Title";
            modelTitleColumn.Name = "modelTitleColumn";
            // 
            // modelDescriptionColumn
            // 
            modelDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            modelDescriptionColumn.DataPropertyName = "ModelDescription";
            modelDescriptionColumn.HeaderText = "Description";
            modelDescriptionColumn.Name = "modelDescriptionColumn";
            // 
            // modelObsoleteColumn
            // 
            modelObsoleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            modelObsoleteColumn.DataPropertyName = "Obsolete";
            modelObsoleteColumn.HeaderText = "Obsolete";
            modelObsoleteColumn.Name = "modelObsoleteColumn";
            modelObsoleteColumn.Width = 60;
            // 
            // modelTitleData
            // 
            modelTitleData.AutoSize = true;
            modelTitleData.Dock = DockStyle.Fill;
            modelTitleData.HeaderText = "Model Title";
            modelTitleData.Location = new Point(3, 368);
            modelTitleData.Multiline = false;
            modelTitleData.Name = "modelTitleData";
            modelTitleData.ReadOnly = true;
            modelTitleData.Size = new Size(357, 44);
            modelTitleData.TabIndex = 3;
            // 
            // buttonLayout
            // 
            buttonLayout.AutoSize = true;
            buttonLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonLayout.ColumnCount = 3;
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            buttonLayout.Controls.Add(loadCommand, 0, 0);
            buttonLayout.Controls.Add(deleteCommand, 1, 0);
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.Location = new Point(3, 418);
            buttonLayout.Name = "buttonLayout";
            buttonLayout.RowCount = 1;
            buttonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonLayout.Size = new Size(357, 29);
            buttonLayout.TabIndex = 4;
            // 
            // loadCommand
            // 
            loadCommand.DialogResult = DialogResult.OK;
            loadCommand.Location = new Point(3, 3);
            loadCommand.Name = "loadCommand";
            loadCommand.Size = new Size(75, 23);
            loadCommand.TabIndex = 0;
            loadCommand.Text = "Load";
            loadCommand.UseVisualStyleBackColor = true;
            loadCommand.Click += loadCommand_Click;
            // 
            // deleteCommand
            // 
            deleteCommand.Location = new Point(84, 3);
            deleteCommand.Name = "deleteCommand";
            deleteCommand.Size = new Size(75, 23);
            deleteCommand.TabIndex = 1;
            deleteCommand.Text = "Delete";
            deleteCommand.UseVisualStyleBackColor = true;
            deleteCommand.Click += deleteCommand_Click;
            // 
            // LoadModelFromDatabase
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(363, 450);
            Controls.Add(formLayout);
            Name = "LoadModelFromDatabase";
            Text = "Load Model from Database";
            Load += LoadModelFromDatabase_Load;
            formLayout.ResumeLayout(false);
            formLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)modelList).EndInit();
            buttonLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel formLayout;
        private Controls.TextBoxData serverNameData;
        private Controls.TextBoxData databaseNameData;
        private DataGridView modelList;
        private Controls.TextBoxData modelTitleData;
        private TableLayoutPanel buttonLayout;
        private Button loadCommand;
        private Button deleteCommand;
        private DataGridViewTextBoxColumn modelTitleColumn;
        private DataGridViewTextBoxColumn modelDescriptionColumn;
        private DataGridViewCheckBoxColumn modelObsoleteColumn;
    }
}