namespace DataDictionary.Main.Dialogs
{
    partial class OpenSaveModelDatabase
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
            TableLayoutPanel modelTitleLayout;
            TableLayoutPanel modelFlagsLayout;
            serverNameData = new Controls.TextBoxData();
            databaseNameData = new Controls.TextBoxData();
            modelList = new DataGridView();
            modelTitleColumn = new DataGridViewTextBoxColumn();
            modelDescriptionColumn = new DataGridViewTextBoxColumn();
            modelObsoleteColumn = new DataGridViewCheckBoxColumn();
            modelTitleData = new Controls.TextBoxData();
            saveCommand = new Button();
            deleteCommand = new Button();
            loadCommand = new Button();
            modelDescriptionData = new Controls.TextBoxData();
            modelObsoleteData = new CheckBox();
            formLayout = new TableLayoutPanel();
            buttonLayout = new TableLayoutPanel();
            modelTitleLayout = new TableLayoutPanel();
            modelFlagsLayout = new TableLayoutPanel();
            formLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)modelList).BeginInit();
            buttonLayout.SuspendLayout();
            modelTitleLayout.SuspendLayout();
            modelFlagsLayout.SuspendLayout();
            SuspendLayout();
            // 
            // formLayout
            // 
            formLayout.ColumnCount = 1;
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            formLayout.Controls.Add(serverNameData, 0, 0);
            formLayout.Controls.Add(databaseNameData, 0, 1);
            formLayout.Controls.Add(modelList, 0, 2);
            formLayout.Controls.Add(buttonLayout, 0, 5);
            formLayout.Controls.Add(modelDescriptionData, 0, 4);
            formLayout.Controls.Add(modelTitleLayout, 0, 3);
            formLayout.Dock = DockStyle.Fill;
            formLayout.Location = new Point(0, 0);
            formLayout.Name = "formLayout";
            formLayout.RowCount = 6;
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
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
            modelList.ReadOnly = true;
            modelList.RowTemplate.Height = 25;
            modelList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            modelList.Size = new Size(357, 123);
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
            modelTitleColumn.ReadOnly = true;
            // 
            // modelDescriptionColumn
            // 
            modelDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            modelDescriptionColumn.DataPropertyName = "ModelDescription";
            modelDescriptionColumn.HeaderText = "Description";
            modelDescriptionColumn.Name = "modelDescriptionColumn";
            modelDescriptionColumn.ReadOnly = true;
            // 
            // modelObsoleteColumn
            // 
            modelObsoleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            modelObsoleteColumn.DataPropertyName = "Obsolete";
            modelObsoleteColumn.HeaderText = "Obsolete";
            modelObsoleteColumn.Name = "modelObsoleteColumn";
            modelObsoleteColumn.ReadOnly = true;
            modelObsoleteColumn.Width = 60;
            // 
            // modelTitleData
            // 
            modelTitleData.AutoSize = true;
            modelTitleData.Dock = DockStyle.Fill;
            modelTitleData.HeaderText = "Model Title";
            modelTitleData.Location = new Point(3, 3);
            modelTitleData.Multiline = false;
            modelTitleData.Name = "modelTitleData";
            modelTitleData.ReadOnly = true;
            modelTitleData.Size = new Size(266, 44);
            modelTitleData.TabIndex = 3;
            // 
            // buttonLayout
            // 
            buttonLayout.AutoSize = true;
            buttonLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonLayout.ColumnCount = 4;
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonLayout.Controls.Add(saveCommand, 0, 0);
            buttonLayout.Controls.Add(deleteCommand, 2, 0);
            buttonLayout.Controls.Add(loadCommand, 1, 0);
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.Location = new Point(3, 417);
            buttonLayout.Name = "buttonLayout";
            buttonLayout.RowCount = 1;
            buttonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonLayout.Size = new Size(357, 30);
            buttonLayout.TabIndex = 4;
            // 
            // saveCommand
            // 
            saveCommand.Location = new Point(3, 3);
            saveCommand.Name = "saveCommand";
            saveCommand.Size = new Size(75, 23);
            saveCommand.TabIndex = 2;
            saveCommand.Text = "Save";
            saveCommand.UseVisualStyleBackColor = true;
            saveCommand.Click += saveCommand_Click;
            // 
            // deleteCommand
            // 
            deleteCommand.Location = new Point(165, 3);
            deleteCommand.Name = "deleteCommand";
            deleteCommand.Size = new Size(75, 23);
            deleteCommand.TabIndex = 1;
            deleteCommand.Text = "Delete";
            deleteCommand.UseVisualStyleBackColor = true;
            deleteCommand.Click += deleteCommand_Click;
            // 
            // loadCommand
            // 
            loadCommand.DialogResult = DialogResult.OK;
            loadCommand.Location = new Point(84, 3);
            loadCommand.Name = "loadCommand";
            loadCommand.Size = new Size(75, 23);
            loadCommand.TabIndex = 0;
            loadCommand.Text = "Load";
            loadCommand.UseVisualStyleBackColor = true;
            loadCommand.Click += loadCommand_Click;
            // 
            // modelDescriptionData
            // 
            modelDescriptionData.AutoSize = true;
            modelDescriptionData.Dock = DockStyle.Fill;
            modelDescriptionData.HeaderText = "Model Desription";
            modelDescriptionData.Location = new Point(3, 288);
            modelDescriptionData.Multiline = true;
            modelDescriptionData.Name = "modelDescriptionData";
            modelDescriptionData.ReadOnly = true;
            modelDescriptionData.Size = new Size(357, 123);
            modelDescriptionData.TabIndex = 5;
            // 
            // modelTitleLayout
            // 
            modelTitleLayout.AutoSize = true;
            modelTitleLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            modelTitleLayout.ColumnCount = 2;
            modelTitleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelTitleLayout.ColumnStyles.Add(new ColumnStyle());
            modelTitleLayout.Controls.Add(modelTitleData, 0, 0);
            modelTitleLayout.Controls.Add(modelFlagsLayout, 1, 0);
            modelTitleLayout.Dock = DockStyle.Fill;
            modelTitleLayout.Location = new Point(3, 232);
            modelTitleLayout.Name = "modelTitleLayout";
            modelTitleLayout.RowCount = 1;
            modelTitleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modelTitleLayout.Size = new Size(357, 50);
            modelTitleLayout.TabIndex = 6;
            // 
            // modelFlagsLayout
            // 
            modelFlagsLayout.AutoSize = true;
            modelFlagsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            modelFlagsLayout.ColumnCount = 1;
            modelFlagsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelFlagsLayout.Controls.Add(modelObsoleteData, 0, 0);
            modelFlagsLayout.Dock = DockStyle.Fill;
            modelFlagsLayout.Location = new Point(275, 3);
            modelFlagsLayout.Name = "modelFlagsLayout";
            modelFlagsLayout.RowCount = 2;
            modelFlagsLayout.RowStyles.Add(new RowStyle());
            modelFlagsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modelFlagsLayout.Size = new Size(79, 44);
            modelFlagsLayout.TabIndex = 4;
            // 
            // modelObsoleteData
            // 
            modelObsoleteData.AutoSize = true;
            modelObsoleteData.Location = new Point(3, 3);
            modelObsoleteData.Name = "modelObsoleteData";
            modelObsoleteData.Size = new Size(73, 19);
            modelObsoleteData.TabIndex = 2;
            modelObsoleteData.Text = "Obsolete";
            modelObsoleteData.UseVisualStyleBackColor = true;
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
            modelTitleLayout.ResumeLayout(false);
            modelTitleLayout.PerformLayout();
            modelFlagsLayout.ResumeLayout(false);
            modelFlagsLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls.TextBoxData serverNameData;
        private Controls.TextBoxData databaseNameData;
        private DataGridView modelList;
        private Controls.TextBoxData modelTitleData;
        private Button loadCommand;
        private Button deleteCommand;
        private DataGridViewTextBoxColumn modelTitleColumn;
        private DataGridViewTextBoxColumn modelDescriptionColumn;
        private DataGridViewCheckBoxColumn modelObsoleteColumn;
        private Button saveCommand;
        private Controls.TextBoxData modelDescriptionData;
        private CheckBox modelObsoleteData;
    }
}