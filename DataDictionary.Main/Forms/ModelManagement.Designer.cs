namespace DataDictionary.Main.Forms
{
    partial class ModelManagement
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
            TableLayoutPanel dialogLayout;
            Label modelsLayout;
            TableLayoutPanel buttonLayout;
            serverNameData = new Controls.TextBoxData();
            databaseNameData = new Controls.TextBoxData();
            modelListData = new DataGridView();
            modelTitleColumn = new DataGridViewTextBoxColumn();
            modelDescriptionColumn = new DataGridViewTextBoxColumn();
            modelObsoleteColumn = new DataGridViewCheckBoxColumn();
            modelTitleData = new Controls.TextBoxData();
            modelDescriptionData = new Controls.TextBoxData();
            deleteCommand = new Button();
            loadCommand = new Button();
            saveCommand = new Button();
            dialogLayout = new TableLayoutPanel();
            modelsLayout = new Label();
            buttonLayout = new TableLayoutPanel();
            dialogLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)modelListData).BeginInit();
            buttonLayout.SuspendLayout();
            SuspendLayout();
            // 
            // dialogLayout
            // 
            dialogLayout.ColumnCount = 1;
            dialogLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dialogLayout.Controls.Add(serverNameData, 0, 0);
            dialogLayout.Controls.Add(databaseNameData, 0, 1);
            dialogLayout.Controls.Add(modelsLayout, 0, 2);
            dialogLayout.Controls.Add(modelListData, 0, 3);
            dialogLayout.Controls.Add(modelTitleData, 0, 4);
            dialogLayout.Controls.Add(modelDescriptionData, 0, 5);
            dialogLayout.Controls.Add(buttonLayout, 0, 6);
            dialogLayout.Dock = DockStyle.Fill;
            dialogLayout.Location = new Point(0, 0);
            dialogLayout.Name = "dialogLayout";
            dialogLayout.RowCount = 7;
            dialogLayout.RowStyles.Add(new RowStyle());
            dialogLayout.RowStyles.Add(new RowStyle());
            dialogLayout.RowStyles.Add(new RowStyle());
            dialogLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 66.6666641F));
            dialogLayout.RowStyles.Add(new RowStyle());
            dialogLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            dialogLayout.RowStyles.Add(new RowStyle());
            dialogLayout.Size = new Size(457, 463);
            dialogLayout.TabIndex = 0;
            // 
            // serverNameData
            // 
            serverNameData.AutoSize = true;
            serverNameData.Dock = DockStyle.Fill;
            serverNameData.HeaderText = "Server Name";
            serverNameData.Location = new Point(3, 3);
            serverNameData.Multiline = false;
            serverNameData.Name = "serverNameData";
            serverNameData.ReadOnly = true;
            serverNameData.Size = new Size(451, 44);
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
            databaseNameData.Size = new Size(451, 44);
            databaseNameData.TabIndex = 1;
            // 
            // modelsLayout
            // 
            modelsLayout.AutoSize = true;
            modelsLayout.Location = new Point(3, 100);
            modelsLayout.Name = "modelsLayout";
            modelsLayout.Size = new Size(46, 15);
            modelsLayout.TabIndex = 2;
            modelsLayout.Text = "Models";
            // 
            // modelListData
            // 
            modelListData.AllowUserToAddRows = false;
            modelListData.AllowUserToDeleteRows = false;
            modelListData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            modelListData.Columns.AddRange(new DataGridViewColumn[] { modelTitleColumn, modelDescriptionColumn, modelObsoleteColumn });
            modelListData.Dock = DockStyle.Fill;
            modelListData.Location = new Point(3, 118);
            modelListData.Name = "modelListData";
            modelListData.RowTemplate.Height = 25;
            modelListData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            modelListData.Size = new Size(451, 169);
            modelListData.TabIndex = 3;
            modelListData.RowValidated += modelListData_RowValidated;
            modelListData.SelectionChanged += modelListData_SelectionChanged;
            // 
            // modelTitleColumn
            // 
            modelTitleColumn.DataPropertyName = "ModelTitle";
            modelTitleColumn.HeaderText = "Model Title";
            modelTitleColumn.Name = "modelTitleColumn";
            modelTitleColumn.Width = 200;
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
            modelTitleData.Location = new Point(3, 293);
            modelTitleData.Multiline = false;
            modelTitleData.Name = "modelTitleData";
            modelTitleData.ReadOnly = false;
            modelTitleData.Size = new Size(451, 44);
            modelTitleData.TabIndex = 4;
            // 
            // modelDescriptionData
            // 
            modelDescriptionData.AutoSize = true;
            modelDescriptionData.Dock = DockStyle.Fill;
            modelDescriptionData.HeaderText = "Model Description";
            modelDescriptionData.Location = new Point(3, 343);
            modelDescriptionData.Multiline = true;
            modelDescriptionData.Name = "modelDescriptionData";
            modelDescriptionData.ReadOnly = false;
            modelDescriptionData.Size = new Size(451, 81);
            modelDescriptionData.TabIndex = 5;
            // 
            // buttonLayout
            // 
            buttonLayout.AutoSize = true;
            buttonLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonLayout.ColumnCount = 5;
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.Controls.Add(deleteCommand, 4, 0);
            buttonLayout.Controls.Add(loadCommand, 1, 0);
            buttonLayout.Controls.Add(saveCommand, 0, 0);
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.Location = new Point(3, 430);
            buttonLayout.Name = "buttonLayout";
            buttonLayout.RowCount = 1;
            buttonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonLayout.Size = new Size(451, 30);
            buttonLayout.TabIndex = 6;
            // 
            // deleteCommand
            // 
            deleteCommand.DialogResult = DialogResult.Cancel;
            deleteCommand.Enabled = false;
            deleteCommand.Location = new Point(373, 3);
            deleteCommand.Name = "deleteCommand";
            deleteCommand.Size = new Size(75, 23);
            deleteCommand.TabIndex = 0;
            deleteCommand.Text = "&Delete";
            deleteCommand.UseVisualStyleBackColor = true;
            deleteCommand.Click += deleteCommand_Click;
            // 
            // loadCommand
            // 
            loadCommand.Location = new Point(84, 3);
            loadCommand.Name = "loadCommand";
            loadCommand.Size = new Size(75, 23);
            loadCommand.TabIndex = 2;
            loadCommand.Text = "&Load";
            loadCommand.UseVisualStyleBackColor = true;
            loadCommand.Click += loadCommand_Click;
            // 
            // saveCommand
            // 
            saveCommand.DialogResult = DialogResult.OK;
            saveCommand.Location = new Point(3, 3);
            saveCommand.Name = "saveCommand";
            saveCommand.Size = new Size(75, 23);
            saveCommand.TabIndex = 1;
            saveCommand.Text = "&Save";
            saveCommand.UseVisualStyleBackColor = true;
            saveCommand.Click += saveCommand_Click;
            // 
            // ModelManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 463);
            Controls.Add(dialogLayout);
            Name = "ModelManagement";
            Text = "Open or Save Model";
            Load += ModelManagement_Load;
            dialogLayout.ResumeLayout(false);
            dialogLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)modelListData).EndInit();
            buttonLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls.TextBoxData serverNameData;
        private Controls.TextBoxData databaseNameData;
        private DataGridView modelListData;
        private Controls.TextBoxData modelTitleData;
        private Controls.TextBoxData modelDescriptionData;
        private Button deleteCommand;
        private Button saveCommand;
        private Button loadCommand;
        private DataGridViewTextBoxColumn modelTitleColumn;
        private DataGridViewTextBoxColumn modelDescriptionColumn;
        private DataGridViewCheckBoxColumn modelObsoleteColumn;
    }
}