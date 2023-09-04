namespace DataDictionary.Main.Dialogs
{
    partial class Experiment
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
            bindingSource = new BindingSource(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            definitionDescriptionData = new Controls.TextBoxData();
            definitionNavigation = new DataGridView();
            definitionTitleColumn = new DataGridViewTextBoxColumn();
            definitionDescriptionColumn = new DataGridViewTextBoxColumn();
            tableLayoutPanel2 = new TableLayoutPanel();
            obsoleteData = new CheckBox();
            definitionTitleData = new Controls.TextBoxData();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)definitionNavigation).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // bindingSource
            // 
            bindingSource.AddingNew += bindingSource_AddingNew;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(definitionDescriptionData, 0, 2);
            tableLayoutPanel1.Controls.Add(definitionNavigation, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(520, 425);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // definitionDescriptionData
            // 
            definitionDescriptionData.AutoSize = true;
            definitionDescriptionData.Dock = DockStyle.Fill;
            definitionDescriptionData.HeaderText = "Definition Description";
            definitionDescriptionData.Location = new Point(3, 358);
            definitionDescriptionData.Multiline = true;
            definitionDescriptionData.Name = "definitionDescriptionData";
            definitionDescriptionData.ReadOnly = false;
            definitionDescriptionData.Size = new Size(514, 44);
            definitionDescriptionData.TabIndex = 3;
            // 
            // definitionNavigation
            // 
            definitionNavigation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            definitionNavigation.Columns.AddRange(new DataGridViewColumn[] { definitionTitleColumn, definitionDescriptionColumn });
            definitionNavigation.Dock = DockStyle.Fill;
            definitionNavigation.Location = new Point(3, 3);
            definitionNavigation.MultiSelect = false;
            definitionNavigation.Name = "definitionNavigation";
            definitionNavigation.RowTemplate.Height = 25;
            definitionNavigation.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            definitionNavigation.Size = new Size(514, 293);
            definitionNavigation.TabIndex = 1;
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
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(obsoleteData, 0, 0);
            tableLayoutPanel2.Controls.Add(definitionTitleData, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 302);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(514, 50);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // obsoleteData
            // 
            obsoleteData.AutoSize = true;
            obsoleteData.Location = new Point(438, 3);
            obsoleteData.Name = "obsoleteData";
            obsoleteData.Size = new Size(73, 19);
            obsoleteData.TabIndex = 3;
            obsoleteData.Text = "Obsolete";
            obsoleteData.UseVisualStyleBackColor = true;
            // 
            // definitionTitleData
            // 
            definitionTitleData.AutoSize = true;
            definitionTitleData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            definitionTitleData.Dock = DockStyle.Fill;
            definitionTitleData.HeaderText = "Definition Title";
            definitionTitleData.Location = new Point(3, 3);
            definitionTitleData.Multiline = false;
            definitionTitleData.Name = "definitionTitleData";
            definitionTitleData.ReadOnly = false;
            definitionTitleData.Size = new Size(429, 44);
            definitionTitleData.TabIndex = 2;
            // 
            // Experiment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Experiment";
            Text = "Experiment";
            Load += Experiment_Load;
            Controls.SetChildIndex(tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)definitionNavigation).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource bindingSource;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView definitionNavigation;
        private Controls.TextBoxData definitionTitleData;
        private CheckBox obsoleteData;
        private Controls.TextBoxData definitionDescriptionData;
        private DataGridViewTextBoxColumn definitionTitleColumn;
        private DataGridViewTextBoxColumn definitionDescriptionColumn;
    }
}