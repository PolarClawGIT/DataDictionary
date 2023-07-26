namespace DataDictionary.Main.Dialogs
{
    partial class OpenSaveDbDialog
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
            modelNameData = new Controls.TextBoxData();
            modelDescriptionData = new Controls.TextBoxData();
            cancelCommand = new Button();
            okCommand = new Button();
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
            dialogLayout.Controls.Add(modelNameData, 0, 4);
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
            modelListData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            modelListData.Dock = DockStyle.Fill;
            modelListData.Location = new Point(3, 118);
            modelListData.Name = "modelListData";
            modelListData.RowTemplate.Height = 25;
            modelListData.Size = new Size(451, 169);
            modelListData.TabIndex = 3;
            // 
            // modelNameData
            // 
            modelNameData.AutoSize = true;
            modelNameData.Dock = DockStyle.Fill;
            modelNameData.HeaderText = "Model Name";
            modelNameData.Location = new Point(3, 293);
            modelNameData.Multiline = false;
            modelNameData.Name = "modelNameData";
            modelNameData.ReadOnly = false;
            modelNameData.Size = new Size(451, 44);
            modelNameData.TabIndex = 4;
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
            buttonLayout.ColumnCount = 4;
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.ColumnStyles.Add(new ColumnStyle());
            buttonLayout.Controls.Add(cancelCommand, 3, 0);
            buttonLayout.Controls.Add(okCommand, 2, 0);
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.Location = new Point(3, 430);
            buttonLayout.Name = "buttonLayout";
            buttonLayout.RowCount = 1;
            buttonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonLayout.Size = new Size(451, 30);
            buttonLayout.TabIndex = 6;
            // 
            // cancelCommand
            // 
            cancelCommand.DialogResult = DialogResult.Cancel;
            cancelCommand.Location = new Point(372, 3);
            cancelCommand.Name = "cancelCommand";
            cancelCommand.Size = new Size(75, 23);
            cancelCommand.TabIndex = 0;
            cancelCommand.Text = "&Cancel";
            cancelCommand.UseVisualStyleBackColor = true;
            cancelCommand.Click += cancelCommand_Click;
            // 
            // okCommand
            // 
            okCommand.DialogResult = DialogResult.OK;
            okCommand.Location = new Point(291, 3);
            okCommand.Name = "okCommand";
            okCommand.Size = new Size(75, 23);
            okCommand.TabIndex = 1;
            okCommand.Text = "&Ok";
            okCommand.UseVisualStyleBackColor = true;
            okCommand.Click += okCommand_Click;
            // 
            // OpenSaveDbDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 463);
            Controls.Add(dialogLayout);
            Name = "OpenSaveDbDialog";
            Text = "Open or Save Model";
            Load += OpenSaveDbDialog_Load;
            dialogLayout.ResumeLayout(false);
            dialogLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)modelListData).EndInit();
            buttonLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel dialogLayout;
        private Controls.TextBoxData serverNameData;
        private Controls.TextBoxData databaseNameData;
        private Label modelsLayout;
        private DataGridView modelListData;
        private Controls.TextBoxData modelNameData;
        private Controls.TextBoxData modelDescriptionData;
        private TableLayoutPanel buttonLayout;
        private Button cancelCommand;
        private Button okCommand;
    }
}