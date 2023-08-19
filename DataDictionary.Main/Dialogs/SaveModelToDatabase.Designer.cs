namespace DataDictionary.Main.Dialogs
{
    partial class SaveModelToDatabase
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
            TableLayoutPanel modelPropertiesLayout;
            TableLayoutPanel modelSaveLayout;
            modelSysStartData = new Controls.TextBoxData();
            modelObsoleteData = new CheckBox();
            serverNameData = new Controls.TextBoxData();
            databaseNameData = new Controls.TextBoxData();
            modelTitleData = new Controls.TextBoxData();
            modelDescriptionData = new Controls.TextBoxData();
            buttonLayout = new TableLayoutPanel();
            saveCommand = new Button();
            modelPropertiesLayout = new TableLayoutPanel();
            modelSaveLayout = new TableLayoutPanel();
            modelPropertiesLayout.SuspendLayout();
            modelSaveLayout.SuspendLayout();
            buttonLayout.SuspendLayout();
            SuspendLayout();
            // 
            // modelPropertiesLayout
            // 
            modelPropertiesLayout.AutoSize = true;
            modelPropertiesLayout.ColumnCount = 2;
            modelPropertiesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            modelPropertiesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            modelPropertiesLayout.Controls.Add(modelSysStartData, 1, 0);
            modelPropertiesLayout.Controls.Add(modelObsoleteData, 0, 0);
            modelPropertiesLayout.Dock = DockStyle.Fill;
            modelPropertiesLayout.Location = new Point(3, 359);
            modelPropertiesLayout.Name = "modelPropertiesLayout";
            modelPropertiesLayout.RowCount = 1;
            modelPropertiesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            modelPropertiesLayout.Size = new Size(342, 50);
            modelPropertiesLayout.TabIndex = 4;
            // 
            // modelSysStartData
            // 
            modelSysStartData.AutoSize = true;
            modelSysStartData.HeaderText = "Model Last Saved Date";
            modelSysStartData.Location = new Point(174, 3);
            modelSysStartData.Multiline = false;
            modelSysStartData.Name = "modelSysStartData";
            modelSysStartData.ReadOnly = true;
            modelSysStartData.Size = new Size(151, 44);
            modelSysStartData.TabIndex = 0;
            // 
            // modelObsoleteData
            // 
            modelObsoleteData.AutoSize = true;
            modelObsoleteData.Location = new Point(3, 3);
            modelObsoleteData.Name = "modelObsoleteData";
            modelObsoleteData.Size = new Size(73, 19);
            modelObsoleteData.TabIndex = 1;
            modelObsoleteData.Text = "Obsolete";
            modelObsoleteData.UseVisualStyleBackColor = true;
            // 
            // modelSaveLayout
            // 
            modelSaveLayout.ColumnCount = 1;
            modelSaveLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            modelSaveLayout.Controls.Add(serverNameData, 0, 0);
            modelSaveLayout.Controls.Add(databaseNameData, 0, 1);
            modelSaveLayout.Controls.Add(modelTitleData, 0, 2);
            modelSaveLayout.Controls.Add(modelDescriptionData, 0, 3);
            modelSaveLayout.Controls.Add(modelPropertiesLayout, 0, 4);
            modelSaveLayout.Controls.Add(buttonLayout, 0, 5);
            modelSaveLayout.Dock = DockStyle.Fill;
            modelSaveLayout.Location = new Point(0, 0);
            modelSaveLayout.Name = "modelSaveLayout";
            modelSaveLayout.RowCount = 6;
            modelSaveLayout.RowStyles.Add(new RowStyle());
            modelSaveLayout.RowStyles.Add(new RowStyle());
            modelSaveLayout.RowStyles.Add(new RowStyle());
            modelSaveLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            modelSaveLayout.RowStyles.Add(new RowStyle());
            modelSaveLayout.RowStyles.Add(new RowStyle());
            modelSaveLayout.Size = new Size(348, 447);
            modelSaveLayout.TabIndex = 0;
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
            serverNameData.Size = new Size(342, 44);
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
            databaseNameData.Size = new Size(342, 44);
            databaseNameData.TabIndex = 1;
            // 
            // modelTitleData
            // 
            modelTitleData.AutoSize = true;
            modelTitleData.Dock = DockStyle.Fill;
            modelTitleData.HeaderText = "Model Title";
            modelTitleData.Location = new Point(3, 103);
            modelTitleData.Multiline = false;
            modelTitleData.Name = "modelTitleData";
            modelTitleData.ReadOnly = false;
            modelTitleData.Size = new Size(342, 44);
            modelTitleData.TabIndex = 2;
            // 
            // modelDescriptionData
            // 
            modelDescriptionData.AutoSize = true;
            modelDescriptionData.Dock = DockStyle.Fill;
            modelDescriptionData.HeaderText = "Model Description";
            modelDescriptionData.Location = new Point(3, 153);
            modelDescriptionData.Multiline = true;
            modelDescriptionData.Name = "modelDescriptionData";
            modelDescriptionData.ReadOnly = false;
            modelDescriptionData.Size = new Size(342, 200);
            modelDescriptionData.TabIndex = 3;
            // 
            // buttonLayout
            // 
            buttonLayout.AutoSize = true;
            buttonLayout.ColumnCount = 2;
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonLayout.Controls.Add(saveCommand, 0, 0);
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.Location = new Point(3, 415);
            buttonLayout.Name = "buttonLayout";
            buttonLayout.RowCount = 1;
            buttonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonLayout.Size = new Size(342, 29);
            buttonLayout.TabIndex = 5;
            // 
            // saveCommand
            // 
            saveCommand.Location = new Point(3, 3);
            saveCommand.Name = "saveCommand";
            saveCommand.Size = new Size(75, 23);
            saveCommand.TabIndex = 0;
            saveCommand.Text = "Save";
            saveCommand.UseVisualStyleBackColor = true;
            saveCommand.Click += saveCommand_Click;
            // 
            // SaveModelToDatabase
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(348, 447);
            Controls.Add(modelSaveLayout);
            Name = "SaveModelToDatabase";
            Text = "Save Model to Database";
            Load += SaveModelToDatabase_Load;
            modelPropertiesLayout.ResumeLayout(false);
            modelPropertiesLayout.PerformLayout();
            modelSaveLayout.ResumeLayout(false);
            modelSaveLayout.PerformLayout();
            buttonLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel modelSaveLayout;
        private Controls.TextBoxData serverNameData;
        private Controls.TextBoxData databaseNameData;
        private Controls.TextBoxData modelTitleData;
        private Controls.TextBoxData modelDescriptionData;
        private TableLayoutPanel modelPropertiesLayout;
        private Controls.TextBoxData modelSysStartData;
        private CheckBox modelObsoleteData;
        private TableLayoutPanel buttonLayout;
        private Button saveCommand;
    }
}