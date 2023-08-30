namespace DataDictionary.Main.Dialogs
{
    partial class ApplicationDefinition
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
            TableLayoutPanel applicationDefinitionLayout;
            TableLayoutPanel definitionTitleLayout;
            definitionNavigation = new DataGridView();
            definitionTitleColumn = new DataGridViewTextBoxColumn();
            definitionDescriptionColumn = new DataGridViewTextBoxColumn();
            definitionDescriptionData = new Controls.TextBoxData();
            definitionTitleData = new Controls.TextBoxData();
            obsoleteData = new CheckBox();
            applicationDefinitionLayout = new TableLayoutPanel();
            definitionTitleLayout = new TableLayoutPanel();
            applicationDefinitionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)definitionNavigation).BeginInit();
            definitionTitleLayout.SuspendLayout();
            SuspendLayout();
            // 
            // applicationDefinitionLayout
            // 
            applicationDefinitionLayout.ColumnCount = 1;
            applicationDefinitionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            applicationDefinitionLayout.Controls.Add(definitionNavigation, 0, 0);
            applicationDefinitionLayout.Controls.Add(definitionDescriptionData, 0, 2);
            applicationDefinitionLayout.Controls.Add(definitionTitleLayout, 0, 1);
            applicationDefinitionLayout.Dock = DockStyle.Fill;
            applicationDefinitionLayout.Location = new Point(0, 25);
            applicationDefinitionLayout.Name = "applicationDefinitionLayout";
            applicationDefinitionLayout.RowCount = 3;
            applicationDefinitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            applicationDefinitionLayout.RowStyles.Add(new RowStyle());
            applicationDefinitionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            applicationDefinitionLayout.Size = new Size(391, 408);
            applicationDefinitionLayout.TabIndex = 0;
            // 
            // definitionNavigation
            // 
            definitionNavigation.AllowUserToAddRows = false;
            definitionNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            definitionNavigation.Columns.AddRange(new DataGridViewColumn[] { definitionTitleColumn, definitionDescriptionColumn });
            definitionNavigation.Dock = DockStyle.Fill;
            definitionNavigation.Location = new Point(3, 3);
            definitionNavigation.MultiSelect = false;
            definitionNavigation.Name = "definitionNavigation";
            definitionNavigation.RowTemplate.Height = 25;
            definitionNavigation.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            definitionNavigation.Size = new Size(385, 258);
            definitionNavigation.TabIndex = 0;
            definitionNavigation.SelectionChanged += definitionNavigation_SelectionChanged;
            // 
            // definitionTitleColumn
            // 
            definitionTitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            definitionTitleColumn.DataPropertyName = "DefinitionTitle";
            definitionTitleColumn.HeaderText = "Definition Title";
            definitionTitleColumn.Name = "definitionTitleColumn";
            // 
            // definitionDescriptionColumn
            // 
            definitionDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            definitionDescriptionColumn.DataPropertyName = "DefinitionDescription";
            definitionDescriptionColumn.HeaderText = "Definition Description";
            definitionDescriptionColumn.Name = "definitionDescriptionColumn";
            // 
            // definitionDescriptionData
            // 
            definitionDescriptionData.AutoSize = true;
            definitionDescriptionData.Dock = DockStyle.Fill;
            definitionDescriptionData.HeaderText = "Definition Description";
            definitionDescriptionData.Location = new Point(3, 323);
            definitionDescriptionData.Multiline = true;
            definitionDescriptionData.Name = "definitionDescriptionData";
            definitionDescriptionData.ReadOnly = false;
            definitionDescriptionData.Size = new Size(385, 82);
            definitionDescriptionData.TabIndex = 2;
            // 
            // definitionTitleLayout
            // 
            definitionTitleLayout.AutoSize = true;
            definitionTitleLayout.ColumnCount = 2;
            definitionTitleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            definitionTitleLayout.ColumnStyles.Add(new ColumnStyle());
            definitionTitleLayout.Controls.Add(definitionTitleData, 0, 0);
            definitionTitleLayout.Controls.Add(obsoleteData, 1, 0);
            definitionTitleLayout.Dock = DockStyle.Fill;
            definitionTitleLayout.Location = new Point(3, 267);
            definitionTitleLayout.Name = "definitionTitleLayout";
            definitionTitleLayout.RowCount = 1;
            definitionTitleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            definitionTitleLayout.Size = new Size(385, 50);
            definitionTitleLayout.TabIndex = 3;
            // 
            // definitionTitleData
            // 
            definitionTitleData.AutoSize = true;
            definitionTitleData.Dock = DockStyle.Fill;
            definitionTitleData.HeaderText = "Definition Title";
            definitionTitleData.Location = new Point(3, 3);
            definitionTitleData.Multiline = false;
            definitionTitleData.Name = "definitionTitleData";
            definitionTitleData.ReadOnly = false;
            definitionTitleData.Size = new Size(300, 44);
            definitionTitleData.TabIndex = 1;
            // 
            // obsoleteData
            // 
            obsoleteData.AutoSize = true;
            obsoleteData.Location = new Point(309, 3);
            obsoleteData.Name = "obsoleteData";
            obsoleteData.Size = new Size(73, 19);
            obsoleteData.TabIndex = 2;
            obsoleteData.Text = "Obsolete";
            obsoleteData.UseVisualStyleBackColor = true;
            // 
            // ApplicationDefinition
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(391, 433);
            Controls.Add(applicationDefinitionLayout);
            Name = "ApplicationDefinition";
            Text = "Application Definition";
            Load += ApplicationDefinition_Load;
            Controls.SetChildIndex(applicationDefinitionLayout, 0);
            applicationDefinitionLayout.ResumeLayout(false);
            applicationDefinitionLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)definitionNavigation).EndInit();
            definitionTitleLayout.ResumeLayout(false);
            definitionTitleLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel applicationDefinitionLayout;
        private DataGridView definitionNavigation;
        private Controls.TextBoxData definitionTitleData;
        private Controls.TextBoxData definitionDescriptionData;
        private DataGridViewTextBoxColumn definitionTitleColumn;
        private DataGridViewTextBoxColumn definitionDescriptionColumn;
        private CheckBox obsoleteData;
    }
}